////////////////////////////////////////////////////////////////////////////////
//
// iconfig.cpp
//
// Copyright (C) 2001 Luke 'Infidel' Dunstan
//
// This program is free software; you can redistribute it and/or modify
// it under the terms of the GNU General Public License as published by
// the Free Software Foundation; either version 2 of the License, or
// (at your option) any later version.
//
// This program is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
// GNU General Public License for more details.
//
// You should have received a copy of the GNU General Public License
// along with this program; if not, write to the Free Software
// Foundation, Inc., 59 Temple Place, Suite 330, Boston, MA  02111-1307  USA
//
////////////////////////////////////////////////////////////////////////////////

#include <stdlib.h>
#include <stdio.h>
#include <errno.h>

#include <expat.h>

#include "common.h"
#include "iconfig.h"
#include "hotkeys.h"
#include "world.h"
#include "injection.h"

////////////////////////////////////////////////////////////////////////////////
//
//  This module deals with loading and saving configuration data.
//
////////////////////////////////////////////////////////////////////////////////

const int BUFFER_SIZE = 4096;

class ExpatParser
{
private:
    XML_Parser m_parser;
    enum XML_Error m_error;
    int m_line;

    static void s_start_element(void * user, const XML_Char * name,
        const XML_Char ** attrs);
    static void s_end_element(void * user, const XML_Char * name);

protected:
    virtual void start_element(const XML_Char * name,
        const XML_Char ** attrs) = 0;
    virtual void end_element(const XML_Char * name) = 0;

public:
    ExpatParser();
    virtual ~ExpatParser();

    bool parse_file(FILE * fp);
    const char * get_error_msg() const;
    int get_error_line() const { return m_line; }
};

ExpatParser::ExpatParser()
{
    m_parser = XML_ParserCreate(NULL);
    XML_SetUserData(m_parser, this);
    XML_SetStartElementHandler(m_parser, ExpatParser::s_start_element);
    XML_SetEndElementHandler(m_parser, ExpatParser::s_end_element);
}

ExpatParser::~ExpatParser()
{
    XML_ParserFree(m_parser);
}

// static, private
void ExpatParser::s_start_element(void * user, const XML_Char * name,
    const XML_Char ** attrs)
{
    reinterpret_cast<ExpatParser *>(user)->start_element(name, attrs);
}

// static, private
void ExpatParser::s_end_element(void * user, const XML_Char * name)
{
    reinterpret_cast<ExpatParser *>(user)->end_element(name);
}

bool ExpatParser::parse_file(FILE * fp)
{
    while(!feof(fp))
    {
        void * buf = XML_GetBuffer(m_parser, BUFFER_SIZE);
        if(buf == NULL)
            return false;

        int n = fread(buf, 1, BUFFER_SIZE, fp);

        if(XML_ParseBuffer(m_parser, n, feof(fp)) == 0)
        {
            m_error = XML_GetErrorCode(m_parser);
            m_line = XML_GetCurrentLineNumber(m_parser);
            return false;
        }
    }
    return true;
}

const char * ExpatParser::get_error_msg() const
{
    return XML_ErrorString(m_error);
}

////////////////////////////////////////////////////////////////////////////////

class ConfigParser : public ExpatParser
{
private:
    ConfigManager & m_config;
    enum
    {
        TOP, CONFIG,
        SERVER, SHOPLIST, SHOPITEM, USE,
        ACCOUNT, CHARACTER, DRESS, DRESSITEM, ARM, ARMITEM, OBJECT, FSP, HOTKEY
    } m_level;
    ServerConfig * m_server;
    ShoppingList * m_shoplist;
    AccountConfig * m_account;
    CharacterConfig * m_character;
    DressSet * m_dress;
    ArmSet * m_arm;

    void begin_config(const XML_Char ** attrs);
    void begin_server(const XML_Char ** attrs);
    void begin_shoplist(const XML_Char ** attrs);
    void begin_shopitem(const XML_Char ** attrs);
    void begin_use(const XML_Char ** attrs);
    void begin_account(const XML_Char ** attrs);
    void begin_character(const XML_Char ** attrs);
    void begin_dress(const XML_Char ** attrs);
    void begin_dress_item(const XML_Char ** attrs);
    void begin_arm(const XML_Char ** attrs);
    void begin_arm_item(const XML_Char ** attrs);
    void begin_object(const XML_Char ** attrs);
    void begin_filter(const XML_Char ** attrs);
    void begin_hotkey(const XML_Char ** attrs);

protected:
    virtual void start_element(const XML_Char * name,
        const XML_Char ** attrs);
    virtual void end_element(const XML_Char * name);

public:
    ConfigParser(ConfigManager & config);
};

ConfigParser::ConfigParser(ConfigManager & config)
: m_config(config), m_level(TOP), m_server(0), m_shoplist(0), m_account(0),
  m_character(0), m_dress(0), m_arm(0)
{
}

void ConfigParser::start_element(const XML_Char * name,
    const XML_Char ** attrs)
{
    switch(m_level)
    {
    case TOP:
        if(strcmp(name, "config") == 0)
        {
            m_level = CONFIG;
            begin_config(attrs);
        }
        break;
    case CONFIG:
        if(strcmp(name, "server") == 0)
        {
            m_level = SERVER;
            begin_server(attrs);
        }
        else if(strcmp(name, "shoplist") == 0)
        {
            m_level = SHOPLIST;
            begin_shoplist(attrs);
        }
        else if(strcmp(name, "use") == 0)
        {
            m_level = USE;
            begin_use(attrs);
        }
        break;
    case SERVER:
        if(m_server == 0)
            break;
        if(strcmp(name, "account") == 0)
        {
            m_level = ACCOUNT;
            begin_account(attrs);
        }
        break;
    case SHOPLIST:
        if(m_shoplist == 0)
            break;
        if(strcmp(name, "shopitem") == 0)
        {
            m_level = SHOPITEM;
            begin_shopitem(attrs);
        }
        break;
    case SHOPITEM:
        break;
    case USE:
        break;
    case ACCOUNT:
        if(m_account == 0)
            break;
        if(strcmp(name, "character") == 0)
        {
            m_level = CHARACTER;
            begin_character(attrs);
        }
        break;
    case CHARACTER:
        if(m_character == 0)
            break;
        if(strcmp(name, "dress") == 0)
        {
            m_level = DRESS;
            begin_dress(attrs);
            break;
        }
        if(strcmp(name, "arm") == 0)
        {
            m_level = ARM;
            begin_arm(attrs);
            break;
        }
        if(strcmp(name, "object") == 0)
        {
            m_level = OBJECT;
            begin_object(attrs);
            break;
        }
        if(strcmp(name, "filter") == 0)
        {
            m_level = FSP;
            begin_filter(attrs);
            break;
        }
        if(strcmp(name, "hotkey") == 0)
        {
            m_level = HOTKEY;
            begin_hotkey(attrs);
            break;
        }
        break;
    case DRESS:
        if(m_dress == 0)
            break;
        if(strcmp(name, "item") == 0)
        {
            m_level = DRESSITEM;
            begin_dress_item(attrs);
        }
        break;
    case DRESSITEM:
        break;
    case ARM:
        if(m_arm == 0)
            break;
        if(strcmp(name, "item") == 0)
        {
            m_level = ARMITEM;
            begin_arm_item(attrs);
        }
        break;
    case ARMITEM:
        break;
    case OBJECT:
        break;
    case FSP:
        break;
    case HOTKEY:
        break;
    }
}

void ConfigParser::end_element(const XML_Char * name)
{
    switch(m_level)
    {
    case TOP:
        break;
    case CONFIG:
        if(strcmp(name, "config"))
            m_level = TOP;
        break;
    case SERVER:
        if(strcmp(name, "server") == 0)
        {
            m_level = CONFIG;
            m_server = 0;
        }
        break;
    case SHOPLIST:
        if(strcmp(name, "shoplist") == 0)
        {
            m_level = CONFIG;
            m_shoplist = 0;
        }
        break;
    case SHOPITEM:
        if(strcmp(name, "shopitem") == 0)
            m_level = SHOPLIST;
        break;
    case USE:
        if(strcmp(name, "use") == 0)
            m_level = CONFIG;
        break;
    case ACCOUNT:
        if(strcmp(name, "account") == 0)
        {
            m_level = SERVER;
            m_account = 0;
        }
        break;
    case CHARACTER:
        if(strcmp(name, "character") == 0)
        {
            m_level = ACCOUNT;
            m_character = 0;
        }
        break;
    case DRESS:
        if(strcmp(name, "dress") == 0)
        {
            m_level = CHARACTER;
            m_dress = 0;
        }
        break;
    case DRESSITEM:
        if(strcmp(name, "item") == 0)
            m_level = DRESS;
        break;
    case ARM:
        if(strcmp(name, "arm") == 0)
        {
            m_level = CHARACTER;
            m_arm = 0;
        }
        break;
    case ARMITEM:
        if(strcmp(name, "item") == 0)
            m_level = ARM;
        break;
    case OBJECT:
        if(strcmp(name, "object") == 0)
            m_level = CHARACTER;
        break;
    case FSP:
        if(strcmp(name, "filter") == 0)
            m_level = CHARACTER;
        break;
    case HOTKEY:
        if(strcmp(name, "hotkey") == 0)
            m_level = CHARACTER;
        break;
    }
}

// private
#define GlobalIntLoad(var) else if(strcmp(key, #var) == 0){int b;if(!string_to_int(value, b)) warning_printf("Invalid int: charstat\n");else var = b;}
void ConfigParser::begin_config(const XML_Char ** attrs)
{
    while(*attrs != NULL)
    {
        const XML_Char * key = *attrs;
        const XML_Char * value = *(attrs + 1);

        if(strcmp(key, "encryption") == 0)
        {
			bool ok=false;
            for(int k=0;k<EncryptCnt;k++)
				if(strcmp(value,EncryptStrs[k].name.c_str())==0)
				{ok=true;m_config.set_encryption(k); break;}
			if(!ok)
			{
			if(strcmp(value, "ignition") == 0)
                m_config.set_encryption(ENCRYPTION_IGNITION);
            else if(strcmp(value, "sphereclient") == 0)
                m_config.set_encryption(ENCRYPTION_SPHERECLIENT);
            else if(strcmp(value, "1.26.4") == 0)
                m_config.set_encryption(ENCRYPTION_1_26_4);
            else if(strcmp(value, "2.0.0") == 0)
                m_config.set_encryption(ENCRYPTION_2_0_0);
			///zorm203start
			else if(strcmp(value, "2.0.3") == 0)
                m_config.set_encryption(ENCRYPTION_2_0_3);
			///zorm203end
            else if(strcmp(value, "3.0.5") == 0)
                m_config.set_encryption(ENCRYPTION_3_0_5);
            else if(strcmp(value, "3.0.6j") == 0)
                m_config.set_encryption(ENCRYPTION_3_0_6j);
            else
                warning_printf("Invalid encryption config ignored: %s\n",
                    value);
			}
        }
        else if(strcmp(key, "log_flush") == 0)
        {
            int b;
            if(!string_to_int(value, b))
                warning_printf("Invalid int: log_flush\n");
            else
                m_config.set_log_flush(b!=0);
        }
        else if(strcmp(key, "log_verbose") == 0)
        {
            int b;
            if(!string_to_int(value, b))
                warning_printf("Invalid int: log_verbose\n");
            else
                m_config.set_log_verbose(b!=0);
        }
        else if(strcmp(key, "fix_caption") == 0)
        {
            int b;
            if(!string_to_int(value, b))
                warning_printf("Invalid int: fix_caption\n");
            else
                g_FixUnicodeCaption = (b!=0);
        }
		GlobalIntLoad(CharStat)
		GlobalIntLoad(FilterSpeech)
		GlobalIntLoad(Undead)
		GlobalIntLoad(MenuTalk)
		GlobalIntLoad(Tracker)
		GlobalIntLoad(StlthCnt)
		GlobalIntLoad(CorpsesAutoOpen)
		GlobalIntLoad(SmoothWalk)
		GlobalIntLoad(SocksCap)
		GlobalIntLoad(VarsLoopback)
		GlobalIntLoad(NoHungMessage)
		GlobalIntLoad(TargXYZ)
		GlobalIntLoad(PoisonRevert)
		GlobalIntLoad(TrackWorld)
		GlobalIntLoad(ConColor)
		GlobalIntLoad(UnsetSet)

        else if(strcmp(key, "dmenus") == 0)
        {
            int b;
            if(!string_to_int(value, b))
                warning_printf("Invalid int: dmenus\n");
            else
		m_config.set_log_dmenus(b);
        }
        else if(strcmp(key, "fsnd") == 0)
        {
            int b;
            if(!string_to_int(value, b))
                warning_printf("Invalid int: fsnd\n");
            else
		m_config.set_log_fsnd(b);
        }
        else
            warning_printf("config attribute ignored: %s\n", key);
        attrs += 2;
    }
}

// private
void ConfigParser::begin_server(const XML_Char ** attrs)
{
    const XML_Char * server_name = 0, * fixwalk = 0, * fixtalk = 0,
            * buy = 0, * sell = 0, * filter_weather = 0;

    while(*attrs != NULL)
    {
        const XML_Char * key = *attrs;
        const XML_Char * value = *(attrs + 1);

        if(strcmp(key, "name") == 0)
            server_name = value;
        else if(strcmp(key, "fixwalk") == 0)
            fixwalk = value;
        else if(strcmp(key, "fixtalk") == 0)
            fixtalk = value;
        else if(strcmp(key, "buy") == 0)
            buy = value;
        else if(strcmp(key, "sell") == 0)
            sell = value;
        else if(strcmp(key, "filter_weather") == 0)
            filter_weather = value;
        else
            warning_printf("server attribute ignored: %s\n", key);
        attrs += 2;
    }

    if(server_name == 0)
        error_printf("config file: server without name\n");
    else
    {
        bool b;

        m_server = m_config.get(server_name);
        if(fixwalk != 0 && string_to_bool(fixwalk, b))
            m_server->set_fixwalk(b);
        if(fixtalk != 0 && string_to_bool(fixtalk, b))
            m_server->set_fixtalk(b);
        if(buy != 0)
            m_server->set_buy_text(buy);
        if(sell != 0)
            m_server->set_sell_text(sell);
        if(filter_weather != 0 && string_to_bool(filter_weather, b))
            m_server->set_filter_weather(b);
    }
}

// private
void ConfigParser::begin_shoplist(const XML_Char ** attrs)
{
    const XML_Char * name = 0;

    while(*attrs != NULL)
    {
        const XML_Char * key = *attrs;
        const XML_Char * value = *(attrs + 1);

        if(strcmp(key, "name") == 0)
            name = value;
        else
            warning_printf("shoplist attribute ignored: %s\n", key);
        attrs += 2;
    }

    if(name == 0)
        error_printf("config file: shoplist without name\n");
    else
        m_shoplist = m_config.get_list(name);
}

// private
void ConfigParser::begin_shopitem(const XML_Char ** attrs)
{
    const XML_Char * name = 0, * want_str = 0;

    while(*attrs != NULL)
    {
        const XML_Char * key = *attrs;
        const XML_Char * value = *(attrs + 1);

        if(strcmp(key, "name") == 0)
            name = value;
        else if(strcmp(key, "want") == 0)
            want_str = value;
        else
            warning_printf("shopitem attribute ignored: %s\n", key);
        attrs += 2;
    }

    int want;
    if(name == 0)
        error_printf("config file: shopitem without name\n");
    else if(want_str == 0)
        error_printf("config file: shopitem without 'want'\n");
    else if(!string_to_int(want_str, want) ||
            (want < 0 && want != WANT_ALL))
        error_printf("config file: invalid shopitem 'want' attribute\n");
    else
    {
        string name2(name);
        int index = m_shoplist->index_of(name2);
        if(index == -1)
            m_shoplist->add(name2, want);
        else
            (*m_shoplist)[index].m_want = want;
    }
}

// private
void ConfigParser::begin_use(const XML_Char ** attrs)
{
    const XML_Char * name = 0, * graphic_str = 0;

    while(*attrs != NULL)
    {
        const XML_Char * key = *attrs;
        const XML_Char * value = *(attrs + 1);

        if(strcmp(key, "name") == 0)
            name = value;
        else if(strcmp(key, "graphic") == 0)
            graphic_str = value;
        else
            warning_printf("use attribute ignored: %s\n", key);
        attrs += 2;
    }

    int graphic;
    if(name == 0)
        error_printf("config file: use without name\n");
    else if(graphic_str == 0)
        error_printf("config file: use without 'graphic'\n");
    else if(!string_to_int(graphic_str, graphic) ||
            graphic < 0 || graphic > 0xffff)
        error_printf("config file: invalid use 'graphic' attribute\n");
    else
    {
        string name2(name);
        if(m_config.use_exists(name2))
            m_config.find_use(name2) = graphic;
        else
            m_config.add_use(name2, graphic);
    }
}

// private
void ConfigParser::begin_object(const XML_Char ** attrs)
{
    const XML_Char * name = 0, * serial_str = 0;

    while(*attrs != NULL)
    {
        const XML_Char * key = *attrs;
        const XML_Char * value = *(attrs + 1);

        if(strcmp(key, "name") == 0)
            name = value;
        else if(strcmp(key, "serial") == 0)
            serial_str = value;
        else
            warning_printf("object attribute ignored: %s\n", key);
        attrs += 2;
    }

    uint32 serial;
    if(name == 0)
        error_printf("config file: object without name\n");
    else if(serial_str == 0)
        error_printf("config file: object without 'serial'\n");
    else if(!string_to_serial(serial_str, serial) ||
            serial > 0xffffffff)
        error_printf("config file: invalid object 'serial' attribute\n");
    else
    {
        string name2(name);
        if(m_character->obj_exists(name2))
            m_character->find_obj(name2) = serial;
        else
            m_character->add_obj(name2, serial);
    }
}

//expat is sux
string expat_suxxR="ÀÁÂÃÄÅÆÇÈÉÊËÌÍÎÏÐÑÒÓÔÕÖ×ØÙÚÛÜÝÞßàáâãäåæçèéêëìíîïðñòóôõö÷øùúûüýþÿ";
string expat_suxxE="ABVGDEXZIQKLMNOPRSTUFHC#$%ÚÛÜ@JYabvgdexziqklmnoprstufhc~|{}^'`jy";

void ConfigParser::begin_filter(const XML_Char ** attrs)
{
    const XML_Char * text = 0;
	trace_printf("filter:");

    while(*attrs != NULL)
    {
        const XML_Char * key = *attrs;
        const XML_Char * value = *(attrs + 1);

        if(strcmp(key, "text") == 0)
            text = value;
        else
            warning_printf("filter attribute ignored: %s\n", key);
		trace_printf("%s",text);
		attrs += 2;
    }
   	trace_printf("\n");

    if(text != 0)
	{
		if(*text=='`')
		{
		 string s(text);
		 s=s.substr(1);
		 for(int i=0;i<s.length();i++)
		 {
			if(expat_suxxE.find_first_of(s[i]))
				s[i]=expat_suxxR[expat_suxxE.find_first_of(s[i])];
		 }
		 m_character->fsp.Filter.push_back(s);
		}
		else
        m_character->fsp.Filter.push_back(text);
	}
}

// private
void ConfigParser::begin_hotkey(const XML_Char ** attrs)
{
    const XML_Char * key_hash_str = 0, * command_str = 0;

    while(*attrs != NULL)
    {
        const XML_Char * key = *attrs;
        const XML_Char * value = *(attrs + 1);

        if(strcmp(key, "key_hash") == 0)
            key_hash_str = value;
        else if(strcmp(key, "command") == 0)
            command_str = value;
        else
            warning_printf("hotkey attribute ignored: %s\n", key);
        attrs += 2;
    }

    int key_hash;
    if(key_hash_str == 0)
        error_printf("config file: hotkey without key_hash\n");
    else if(command_str == 0)
        error_printf("config file: hotkey without 'command'\n");
    else if(!string_to_int(key_hash_str, key_hash) ||
            key_hash < 0 )
        error_printf("config file: invalid hotkey 'key_hash' attribute\n");
    else
    {
        string command(command_str);
//      if(!(m_character->m_hotkeys.exists(key_hash)))
            m_character->m_hotkeys.add(key_hash, command);
    }
}

// private
void ConfigParser::begin_account(const XML_Char ** attrs)
{
    const XML_Char * account_name = 0;

    while(*attrs != NULL)
    {
        const XML_Char * key = *attrs;
        const XML_Char * value = *(attrs + 1);

        if(strcmp(key, "name") == 0)
            account_name = value;
        else
            warning_printf("account attribute ignored: %s\n", key);
        attrs += 2;
    }

    if(account_name == 0)
        error_printf("config file: account without name\n");
    else
    {
        m_account = m_server->get(account_name);
    }
}

// private
void ConfigParser::begin_character(const XML_Char ** attrs)
{
    const XML_Char * serial_str = 0, * light = 0,
        * bm = 0, * bp = 0, * ga = 0, * gs = 0, * mr = 0, * ns = 0,
        * sa = 0, * ss = 0, * va = 0, * en = 0, * wh = 0, * fd = 0,
        * br = 0, * h = 0, * c = 0, * m = 0, * l = 0, * b = 0,
        * ar = 0, * bt = 0, * hp = 0, * mana = 0, * stamina = 0,
        * armor = 0, * weight = 0, * gold = 0;

    while(*attrs != NULL)
    {
        const XML_Char * key = *attrs;
        const XML_Char * value = *(attrs + 1);

        if(strcmp(key, "serial") == 0) serial_str = value;
        else if(strcmp(key, "light") == 0) light = value;
        else if(strcmp(key, "display_bm_count") == 0) bm = value;
        else if(strcmp(key, "display_bp_count") == 0) bp = value;
        else if(strcmp(key, "display_ga_count") == 0) ga = value;
        else if(strcmp(key, "display_gs_count") == 0) gs = value;
        else if(strcmp(key, "display_mr_count") == 0) mr = value;
        else if(strcmp(key, "display_ns_count") == 0) ns = value;
        else if(strcmp(key, "display_sa_count") == 0) sa = value;
        else if(strcmp(key, "display_ss_count") == 0) ss = value;
        else if(strcmp(key, "display_va_count") == 0) va = value;
        else if(strcmp(key, "display_en_count") == 0) en = value;
        else if(strcmp(key, "display_wh_count") == 0) wh = value;
        else if(strcmp(key, "display_fd_count") == 0) fd = value;
        else if(strcmp(key, "display_br_count") == 0) br = value;
        else if(strcmp(key, "display_h_count") == 0) h = value;
        else if(strcmp(key, "display_c_count") == 0) c = value;
        else if(strcmp(key, "display_m_count") == 0) m = value;
        else if(strcmp(key, "display_l_count") == 0) l = value;
        else if(strcmp(key, "display_b_count") == 0) b = value;
        else if(strcmp(key, "display_ar_count") == 0) ar = value;
        else if(strcmp(key, "display_bt_count") == 0) bt = value;

        else if(strcmp(key, "display_hitpoints") == 0) hp = value;
        else if(strcmp(key, "display_mana") == 0) mana = value;
        else if(strcmp(key, "display_stamina") == 0) stamina = value;
        else if(strcmp(key, "display_armor") == 0) armor = value;
        else if(strcmp(key, "display_weight") == 0) weight = value;
        else if(strcmp(key, "display_gold") == 0) gold = value;

        else
            warning_printf("character attribute ignored: %s\n", key);
        attrs += 2;
    }

    uint32 serial;
    if(serial_str == 0)
        error_printf("config file: character without serial\n");
    else if(!string_to_serial(serial_str, serial))
        error_printf("config file: invalid character serial\n");
    else
    {
        int n;
        bool bb;
        m_character = m_account->get(serial);
        if(light != 0 && string_to_int(light, n))   m_character->set_light(n);
        if(bm != 0 && string_to_bool(bm, bb)) m_character->set_m_bm_display(bb);
        if(bp != 0 && string_to_bool(bp, bb)) m_character->set_m_bp_display(bb);
        if(ga != 0 && string_to_bool(ga, bb)) m_character->set_m_ga_display(bb);
        if(gs != 0 && string_to_bool(gs, bb)) m_character->set_m_gs_display(bb);
        if(mr != 0 && string_to_bool(mr, bb)) m_character->set_m_mr_display(bb);
        if(ns != 0 && string_to_bool(ns, bb)) m_character->set_m_ns_display(bb);
        if(sa != 0 && string_to_bool(sa, bb)) m_character->set_m_sa_display(bb);
        if(ss != 0 && string_to_bool(ss, bb)) m_character->set_m_ss_display(bb);
        if(va != 0 && string_to_bool(va, bb)) m_character->set_m_va_display(bb);
        if(en != 0 && string_to_bool(en, bb)) m_character->set_m_en_display(bb);
        if(wh != 0 && string_to_bool(wh, bb)) m_character->set_m_wh_display(bb);
        if(fd != 0 && string_to_bool(fd, bb)) m_character->set_m_fd_display(bb);
        if(br != 0 && string_to_bool(br, bb)) m_character->set_m_br_display(bb);
        if(h != 0 && string_to_bool(h, bb)) m_character->set_m_h_display(bb);
        if(c != 0 && string_to_bool(c, bb)) m_character->set_m_c_display(bb);
        if(m != 0 && string_to_bool(m, bb)) m_character->set_m_m_display(bb);
        if(l != 0 && string_to_bool(l, bb)) m_character->set_m_l_display(bb);
        if(b != 0 && string_to_bool(b, bb)) m_character->set_m_b_display(bb);
        if(ar != 0 && string_to_bool(ar, bb)) m_character->set_m_ar_display(bb);
        if(bt != 0 && string_to_bool(bt, bb)) m_character->set_m_bt_display(bb);

        if(hp != 0 && string_to_bool(hp, bb)) m_character->set_m_hp_display(bb);
        if(mana != 0 && string_to_bool(mana, bb)) m_character->set_m_mana_display(bb);
        if(stamina != 0 && string_to_bool(stamina, bb)) m_character->set_m_stamina_display(bb);
        if(armor != 0 && string_to_bool(armor, bb)) m_character->set_m_armor_display(bb);
        if(weight != 0 && string_to_bool(weight, bb)) m_character->set_m_weight_display(bb);
        if(gold != 0 && string_to_bool(gold, bb)) m_character->set_m_gold_display(bb);


    }
}

void ConfigParser::begin_dress(const XML_Char ** attrs)
{
    const XML_Char * dress_key = 0;

    while(*attrs != NULL)
    {
        const XML_Char * key = *attrs;
        const XML_Char * value = *(attrs + 1);

        if(strcmp(key, "key") == 0)
            dress_key = value;
        else
            warning_printf("dress attribute ignored: %s\n", key);
        attrs += 2;
    }

    if(dress_key == 0)
        error_printf("config file: dress without key\n");
    else
        m_dress = &m_character->get_dress_set(string(dress_key));
}

void ConfigParser::begin_dress_item(const XML_Char ** attrs)
{
    const XML_Char * serial_str = 0, * layer_str = 0, * description_str = 0;

    while(*attrs != NULL)
    {
        const XML_Char * key = *attrs;
        const XML_Char * value = *(attrs + 1);

        if(strcmp(key, "serial") == 0)
            serial_str = value;
        else if(strcmp(key, "layer") == 0)
            layer_str = value;
        else if(strcmp(key, "description") == 0)
            description_str = value;
        else
            warning_printf("dress item attribute ignored: %s\n", key);
        attrs += 2;
    }

    uint32 serial;
    int layer;
    if(serial_str == 0)
        error_printf("config file: dress item without serial\n");
    else if(!string_to_serial(serial_str, serial))
        error_printf("config file: invalid dress item serial\n");
    else if(layer_str == 0)
        error_printf("config file: dress item without layer\n");
    else if(!string_to_int(layer_str, layer) || !DressSet::valid_layer(layer))
        error_printf("config file: invalid dress item layer\n");
    else
		{(*m_dress)[layer] = serial;
		 if(description_str) //yoko
		 {
//			 GameObject * obj = world.find_object(serial);
//			 if(obj) obj->set_name(description_str);
		 }
		}
}

void ConfigParser::begin_arm(const XML_Char ** attrs)
{
    const XML_Char * arm_key = 0;

    while(*attrs != NULL)
    {
        const XML_Char * key = *attrs;
        const XML_Char * value = *(attrs + 1);

        if(strcmp(key, "key") == 0)
            arm_key = value;
        else
            warning_printf("arm attribute ignored: %s\n", key);
        attrs += 2;
    }

    if(arm_key == 0)
        error_printf("config file: arm without key\n");
    else
        m_arm = &m_character->get_arm_set(string(arm_key));
}

void ConfigParser::begin_arm_item(const XML_Char ** attrs)
{
    const XML_Char * serial_str = 0, * layer_str = 0;

    while(*attrs != NULL)
    {
        const XML_Char * key = *attrs;
        const XML_Char * value = *(attrs + 1);

        if(strcmp(key, "serial") == 0)
            serial_str = value;
        else if(strcmp(key, "layer") == 0)
            layer_str = value;
        else
            warning_printf("arm item attribute ignored: %s\n", key);
        attrs += 2;
    }

    uint32 serial;
    int layer;
    if(serial_str == 0)
        error_printf("config file: arm item without serial\n");
    else if(!string_to_serial(serial_str, serial))
        error_printf("config file: invalid arm item serial\n");
    else if(layer_str == 0)
        error_printf("config file: arm item without layer\n");
    else if(!string_to_int(layer_str, layer) || !ArmSet::valid_layer(layer))
        error_printf("config file: invalid arm item layer\n");
    else
        (*m_arm)[layer] = serial;
}

////////////////////////////////////////////////////////////////////////////////
// This array contains booleans describing which layers are considered
// clothing. Layers that have a 'false' value are never dressed or undressed.
bool DressSet::dress_layers[NUM_CLOTHES] =
{
    false, // invalid
    false, false, // weapons
    true, true, true, true, true, true, // clothing
    false, // 9: unused
    true, // neck
    false, // hair
    true, true, true, // clothing
    false, // 15: unused
    false, // facial hair
    true, true, true, true, // clothing
    false, // backpack
    true, true, true // clothing
    // 25+ not clothing
};

DressSet::DressSet()
{
    for(int i = 0; i < NUM_CLOTHES; i++)
        m_layers[i] = INVALID_SERIAL;
}

DressSet::DressSet(const DressSet & other)
{
    for(int i = 0; i < NUM_CLOTHES; i++)
        m_layers[i] = other.m_layers[i];
}

const DressSet & DressSet::operator = (const DressSet & other)
{
    for(int i = 0; i < NUM_CLOTHES; i++)
        m_layers[i] = other.m_layers[i];
    return *this;
}

// This array contains booleans describing which layers are considered
// weapons. Layers that have a 'false' value are never armed or disarmed.
bool ArmSet::arm_layers[NUM_ARM] =
{
    false, // invalid
    true, true // weapons
    // 4+ not weapons
};

ArmSet::ArmSet()
{
    for(int i = 0; i < NUM_ARM; i++)
        m_layers[i] = INVALID_SERIAL;
}

ArmSet::ArmSet(const ArmSet & other)
{
    for(int i = 0; i < NUM_ARM; i++)
        m_layers[i] = other.m_layers[i];
}

const ArmSet & ArmSet::operator = (const ArmSet & other)
{
    for(int i = 0; i < NUM_ARM; i++)
        m_layers[i] = other.m_layers[i];
    return *this;
}

CharacterConfig::CharacterConfig(uint32 serial)
: m_serial(serial), m_light(LIGHT_NORMAL),
  m_bm_display(true),
  m_bp_display(true),
  m_ga_display(true),
  m_gs_display(true),
  m_mr_display(true),
  m_ns_display(true),
  m_sa_display(true),
  m_ss_display(true),
  m_va_display(false),
  m_en_display(false),
  m_wh_display(false),
  m_fd_display(false),
  m_br_display(false),
  m_h_display(false),
  m_c_display(false),
  m_m_display(false),
  m_l_display(false),
  m_b_display(false),
  m_ar_display(false),
  m_bt_display(false),
  m_hp_display(true),
  m_mana_display(true),
  m_stamina_display(true),
  m_armor_display(true),
  m_weight_display(true),
  m_gold_display(true)
{
}

CharacterConfig::~CharacterConfig()
{
}

void CharacterConfig::save(FILE * fp) const
{
    fprintf(fp, "\t\t\t<character serial=\"%08lX\"\n", m_serial);
    fprintf(fp, "\t\t\t\t\tlight=\"%d\"\n", m_light);
    fprintf(fp, "\t\t\t\t\tdisplay_bm_count=\"%s\"\n", m_bm_display ? "true" : "false");
    fprintf(fp, "\t\t\t\t\tdisplay_bp_count=\"%s\"\n", m_bp_display ? "true" : "false");
    fprintf(fp, "\t\t\t\t\tdisplay_ga_count=\"%s\"\n", m_ga_display ? "true" : "false");
    fprintf(fp, "\t\t\t\t\tdisplay_gs_count=\"%s\"\n", m_gs_display ? "true" : "false");
    fprintf(fp, "\t\t\t\t\tdisplay_mr_count=\"%s\"\n", m_mr_display ? "true" : "false");
    fprintf(fp, "\t\t\t\t\tdisplay_ns_count=\"%s\"\n", m_ns_display ? "true" : "false");
    fprintf(fp, "\t\t\t\t\tdisplay_sa_count=\"%s\"\n", m_sa_display ? "true" : "false");
    fprintf(fp, "\t\t\t\t\tdisplay_ss_count=\"%s\"\n", m_ss_display ? "true" : "false");
    fprintf(fp, "\t\t\t\t\tdisplay_va_count=\"%s\"\n", m_va_display ? "true" : "false");
    fprintf(fp, "\t\t\t\t\tdisplay_en_count=\"%s\"\n", m_en_display ? "true" : "false");
    fprintf(fp, "\t\t\t\t\tdisplay_wh_count=\"%s\"\n", m_wh_display ? "true" : "false");
    fprintf(fp, "\t\t\t\t\tdisplay_fd_count=\"%s\"\n", m_fd_display ? "true" : "false");
    fprintf(fp, "\t\t\t\t\tdisplay_br_count=\"%s\"\n", m_br_display ? "true" : "false");
    fprintf(fp, "\t\t\t\t\tdisplay_h_count=\"%s\"\n",  m_h_display ? "true" : "false");
    fprintf(fp, "\t\t\t\t\tdisplay_c_count=\"%s\"\n",  m_c_display ? "true" : "false");
    fprintf(fp, "\t\t\t\t\tdisplay_m_count=\"%s\"\n",  m_m_display ? "true" : "false");
    fprintf(fp, "\t\t\t\t\tdisplay_l_count=\"%s\"\n",  m_l_display ? "true" : "false");
    fprintf(fp, "\t\t\t\t\tdisplay_b_count=\"%s\"\n",  m_b_display ? "true" : "false");
    fprintf(fp, "\t\t\t\t\tdisplay_ar_count=\"%s\"\n", m_ar_display ? "true" : "false");
    fprintf(fp, "\t\t\t\t\tdisplay_bt_count=\"%s\"\n", m_bt_display ? "true" : "false");

    fprintf(fp, "\t\t\t\t\tdisplay_hitpoints=\"%s\"\n", m_hp_display ? "true" : "false");
    fprintf(fp, "\t\t\t\t\tdisplay_mana=\"%s\"\n", m_mana_display ? "true" : "false");
    fprintf(fp, "\t\t\t\t\tdisplay_stamina=\"%s\"\n", m_stamina_display ? "true" : "false");
    fprintf(fp, "\t\t\t\t\tdisplay_armor=\"%s\"\n", m_armor_display ? "true" : "false");
    fprintf(fp, "\t\t\t\t\tdisplay_weight=\"%s\"\n", m_weight_display ? "true" : "false");
    fprintf(fp, "\t\t\t\t\tdisplay_gold=\"%s\"\n", m_gold_display ? "true" : "false");

    fprintf(fp, "\t\t\t\t\t>\n");

    {for(dress_map_t::const_iterator i = m_dress.begin(); i != m_dress.end(); i++)
    {
        fprintf(fp, "\t\t\t\t<dress key=\"%s\">\n",
            ConfigManager::escape_attribute((*i).first).c_str());
        for(int j = 0; j < NUM_CLOTHES; j++)
            if((*i).second.has_layer(j))
                fprintf(fp, "\t\t\t\t\t<item serial=\"%08lX\" layer=\"%d\"/>\n",
                    (*i).second[j], j);
        fprintf(fp, "\t\t\t\t</dress>\n");
    }}
    {for(arm_map_t::const_iterator i = m_arm.begin(); i != m_arm.end(); i++)
    {
        fprintf(fp, "\t\t\t\t<arm key=\"%s\">\n",
            ConfigManager::escape_attribute((*i).first).c_str());
        for(int j = 0; j < NUM_ARM; j++)
            if((*i).second.has_layer(j))
			{
             fprintf(fp, "\t\t\t\t\t<item serial=\"%08lX\" layer=\"%d\"",(*i).second[j], j);
//		     GameObject * obj = find_object(serial); if(obj) fprintf(fp, " description=\"%s\"",obj->get_name());
			 fprintf(fp, "/>\n");
			}
        fprintf(fp, "\t\t\t\t</arm>\n");
    }}
    for(objlist_t::const_iterator i = m_object.begin(); i != m_object.end(); i++)
        fprintf(fp, "\t\t\t\t<object name=\"%s\" serial=\"0x%08lx\"/>\n",
            ConfigManager::escape_attribute((*i).first).c_str(), (*i).second);
	using namespace std;
    for(list<string>::const_iterator j = fsp.Filter.begin(); j != fsp.Filter.end(); j++)
	{
		bool pure=true; string s(*j);
		for(int i=0; i<s.length(); i++) {if (s[i]>127) pure=false; break;}
		if(pure)
			fprintf(fp, "\t\t\t\t<filter text=\"%s\"/>\n",s.c_str());
		else
		{
		 for(int i=0;i<s.length();i++)
		 {
			if(expat_suxxR.find_first_of(s[i]))
				s[i]=expat_suxxE[expat_suxxR.find_first_of(s[i])];
		 }
			fprintf(fp, "\t\t\t\t<filter text=\"`%s\"/>\n",(*j).c_str());
		}

	}
    m_hotkeys.write_config(fp);
    fprintf(fp, "\t\t\t</character>\n");
}

bool CharacterConfig::dress_set_exists(const string & key) const
{
    return m_dress.find(key) != m_dress.end();
}

DressSet & CharacterConfig::get_dress_set(const string & key)
{
    ASSERT(ConfigManager::valid_key(key));
    return m_dress[key];
}

const DressSet & CharacterConfig::get_dress_set(const string & key) const
{
    dress_map_t::const_iterator i = m_dress.find(key);
    ASSERT(i != m_dress.end());
    return (*i).second;
}

void CharacterConfig::remove_dress_set(const string & key)
{
    m_dress.erase(key);
}

bool CharacterConfig::arm_set_exists(const string & key) const
{
    return m_arm.find(key) != m_arm.end();
}

ArmSet & CharacterConfig::get_arm_set(const string & key)
{
    ASSERT(ConfigManager::valid_key(key));
    return m_arm[key];
}

const ArmSet & CharacterConfig::get_arm_set(const string & key) const
{
    arm_map_t::const_iterator i = m_arm.find(key);
    ASSERT(i != m_arm.end());
    return (*i).second;
}

void CharacterConfig::remove_arm_set(const string & key)
{
    m_arm.erase(key);
}

bool CharacterConfig::obj_exists(const string & name)
{
    return m_object.find(name) != m_object.end();
}

uint32 & CharacterConfig::find_obj(const string & name)
{
    objlist_t::iterator i = m_object.find(name);
    ASSERT(i != m_object.end());
    return (*i).second;
}

void CharacterConfig::add_obj(const string & name, uint32 serial)
{
    ASSERT(!obj_exists(name));
    m_object.insert(objlist_t::value_type(name, serial));
}

void CharacterConfig::delete_obj(const string & name)
{
    m_object.erase(name);
}

////////////////////////////////////////////////////////////////////////////////

AccountConfig::AccountConfig(const string & name)
: m_name(name)
{
}

CharacterConfig * AccountConfig::get(uint32 character_serial)
{
    map_t::iterator i = m_characters.find(character_serial);
    if(i == m_characters.end())
        i = m_characters.insert(map_t::value_type(character_serial,
                CharacterConfig(character_serial))).first;
    return &(*i).second;
}

void AccountConfig::save(FILE * fp) const
{
    fprintf(fp, "\t\t<account name=\"%s\"\n",
        ConfigManager::escape_attribute(m_name).c_str());
    fprintf(fp, "\t\t\t\t>\n");
    for(map_t::const_iterator i = m_characters.begin(); i != m_characters.end(); i++)
        (*i).second.save(fp);
    fprintf(fp, "\t\t</account>\n");
}

ServerConfig::ServerConfig(const string & name)
: m_name(name), m_fixwalk(false), m_fixtalk(false), m_buy("buy"), m_sell("sell"),
  m_filter_weather(false)
{
}

AccountConfig * ServerConfig::get(const char * account_name)
{
    return get(string(account_name));
}

AccountConfig * ServerConfig::get(const string & account_name)
{
    map_t::iterator i = m_accounts.find(account_name);
    if(i == m_accounts.end())
        i= m_accounts.insert(map_t::value_type(account_name,
            AccountConfig(account_name))).first;
    return &(*i).second;
}

void ServerConfig::save(FILE * fp) const
{
    fprintf(fp, "\t<server name=\"%s\"\n",
        ConfigManager::escape_attribute(m_name).c_str());
    fprintf(fp, "\t\t\tfixwalk=\"%s\"\n", m_fixwalk ? "true" : "false");
    fprintf(fp, "\t\t\tfixtalk=\"%s\"\n", m_fixtalk ? "true" : "false");
    fprintf(fp, "\t\t\tfilter_weather=\"%s\"\n", m_filter_weather ? "true" : "false");
    fprintf(fp, "\t\t\tbuy=\"%s\"\n", ConfigManager::escape_attribute(m_buy).c_str());
    fprintf(fp, "\t\t\tsell=\"%s\"\n", ConfigManager::escape_attribute(m_sell).c_str());
    fprintf(fp, "\t\t\t>\n");
    for(map_t::const_iterator i = m_accounts.begin(); i != m_accounts.end(); i++)
        (*i).second.save(fp);
    fprintf(fp, "\t</server>\n\n");
}

////////////////////////////////////////////////////////////////////////////////

//// Members of ShoppingList:

ShoppingList::ShoppingList(const string & name)
: m_name(name)
{
}

ShoppingItem & ShoppingList::operator[](size_t index)
{
    ASSERT(index < m_items.size());
    return m_items[index];
}

int ShoppingList::index_of(const string & name) const
{
    for(list_t::const_iterator i = m_items.begin(); i != m_items.end(); i++)
        if((*i).m_name == name)
            return i - m_items.begin();
    return -1;
}

void ShoppingList::reset()
{
    for(list_t::iterator i = m_items.begin(); i != m_items.end(); i++)
        (*i).m_available = (*i).m_got = (*i).m_paid = 0;
}

void ShoppingList::add(const string & name, int want)
{
    m_items.push_back(ShoppingItem(name, want));
}

void ShoppingList::erase(int index)
{
    m_items.erase(m_items.begin() + index);
}

void ShoppingList::save(FILE * fp) const
{
    fprintf(fp, "\t<shoplist name=\"%s\">\n",
        ConfigManager::escape_attribute(m_name).c_str());
    for(list_t::const_iterator i = m_items.begin(); i != m_items.end(); i++)
        fprintf(fp, "\t\t<shopitem name=\"%s\" want=\"%d\"/>\n",
            ConfigManager::escape_attribute((*i).m_name).c_str(), (*i).m_want);
    fprintf(fp, "\t</shoplist>\n\n");
}

////////////////////////////////////////////////////////////////////////////////
//// Members of ConfigManager:

ConfigManager::ConfigManager()
: m_loaded(false), m_encryption(ENCRYPTION_IGNITION)
{
    m_use["cure"] = 0x0f07;
    m_use["stamina"] = 0x0f0b;
    m_use["heal"] = 0x0f0c;
    m_use["bandage"] = 0x0e21;
    set_log_verbose(false);
    // Should be okay to flush because only error messages are being printed.
    set_log_flush(true);
    set_log_dmenus(false);
    set_log_fsnd(false);
}

ConfigManager::~ConfigManager()
{
}

// static
bool ConfigManager::valid_key(const string & key)
{
    static const char * valid_chars = " _()";
    if(key.length() == 0)
        return false;
    for(string::const_iterator i = key.begin(); i != key.end(); i++)
        if(!isalnum(*i) && (*i == '\0' || strchr(valid_chars, *i) == 0))
            return false;
    return true;
}

string ConfigManager::escape_attribute(const string & key)
{
    string esc;

    for(string::const_iterator i = key.begin(); i != key.end(); i++)
        switch(*i)
        {
        case '<':
            esc += "&lt;";
            break;
        case '&':
            esc += "&amp;";
            break;
        case '"':
            esc += "&quot;";
            break;
        case '\'':
            esc += "&apos;";
            break;
        default:
            if(isprint(*i))     // Strip unprintable characters
                esc += *i;
            break;
        }
    return esc;
}

bool ConfigManager::load(const char * filename)
{
    m_filename = filename;
    FILE * fp = fopen(filename, "rt");
    if(fp == NULL)
    {
        // If the file just doesn't exist, its not an error because
        // defaults will be used and the file will be created later.
        if(errno == ENOENT)
        {
            trace_printf("Configuration file not found.\n");
            m_loaded = true;
            return true;
        }
        // Any other errno is fatal
        error_printf("Cannot open config file: %s: %s\n", filename,
            strerror(errno));
        return false;
    }
    ConfigParser parser(*this);
    bool success = parser.parse_file(fp);
    fclose(fp);
    if(!success)
        error_printf("parsing config file: %d: %s\n",
            parser.get_error_line(), parser.get_error_msg());
    else
    {
        trace_printf("Loaded config file: %s\n", filename);
        m_loaded = true;
    }

    return success;
}

ServerConfig * ConfigManager::get(const char * server_name)
{
    return get(string(server_name));
}

ServerConfig * ConfigManager::get(const string & server_name)
{
    map_t::iterator i = m_servers.find(server_name);
    if(i == m_servers.end())
        i = m_servers.insert(map_t::value_type(server_name,
            ServerConfig(server_name))).first;
    return &(*i).second;
}

#define GlobalStr2Save(name,str) fprintf(fp, "\t\t" #name "=\"%s\"\n",str);
#define GlobalIntSave(var) fprintf(fp, "\t\t" #var "=\"%i\"\n",int(var));
#define GlobalInt2Save(name,var) fprintf(fp, "\t\t" #name "=\"%i\"\n",int(var));

void ConfigManager::save() const
{
    if(!m_loaded)
    {
        trace_printf("Not saving configuration.\n");
        return;
    }
    FILE * fp = fopen(m_filename.c_str(), "wt");
    if(fp == NULL)
    {
        error_printf("Cannot create config file: %s: %s\n",
            m_filename.c_str(), strerror(errno));
        return;
    }
	bool test=true;
    fprintf(fp, "<?xml version='1.0'?>\n\n");
    fprintf(fp, "<config\n");
    const char * encryption_str;
    switch(m_encryption)
    {
    case ENCRYPTION_IGNITION:
        encryption_str = "ignition";
        break;
    case ENCRYPTION_SPHERECLIENT:
        encryption_str = "sphereclient";
        break;
    case ENCRYPTION_1_26_4:
        encryption_str = "1.26.4";
        break;
    case ENCRYPTION_2_0_0:
        encryption_str = "2.0.0";
        break;
	///zorm203start
	case ENCRYPTION_2_0_3:
        encryption_str = "2.0.3";
        break;
	///zorm203end
    case ENCRYPTION_3_0_5:
        encryption_str = "3.0.5";
        break;
    case ENCRYPTION_3_0_6j:
        encryption_str = "3.0.6j";
        break;
    default:
        if(m_encryption>=0&&m_encryption<EncryptCnt)
			encryption_str=EncryptStrs[m_encryption].name.c_str();
		else
			FATAL("m_encryption has invalid value");
    }
	GlobalStr2Save(encryption,encryption_str);
	GlobalInt2Save(log_flush,get_log_flush());
	GlobalInt2Save(fix_caption,g_FixUnicodeCaption);
    GlobalInt2Save(log_verbose,get_log_verbose());
    GlobalInt2Save(dmenus,get_log_dmenus());
    GlobalInt2Save(fsnd,get_log_fsnd());
	GlobalIntSave(FilterSpeech);
	GlobalIntSave(Undead);
	GlobalIntSave(MenuTalk);
	GlobalIntSave(Tracker);
	GlobalIntSave(StlthCnt);
	GlobalIntSave(CorpsesAutoOpen);
	GlobalIntSave(SmoothWalk);
	GlobalIntSave(SocksCap);
	GlobalIntSave(VarsLoopback);
	GlobalIntSave(CharStat);
	GlobalIntSave(NoHungMessage);
	GlobalIntSave(TargXYZ);
	GlobalIntSave(PoisonRevert);
	GlobalIntSave(TrackWorld);
	GlobalIntSave(ConColor);
	GlobalIntSave(UnsetSet);
    fprintf(fp, "\t\t>\n\n");
    {for(map_t::const_iterator i = m_servers.begin(); i != m_servers.end(); i++)
        (*i).second.save(fp);}
    {for(shoplists_t::const_iterator i = m_lists.begin(); i != m_lists.end(); i++)
        (*i).second.save(fp);}
    {for(uselist_t::const_iterator i = m_use.begin(); i != m_use.end(); i++)
        fprintf(fp, "\t<use name=\"%s\" graphic=\"0x%04x\"/>\n",
        ConfigManager::escape_attribute((*i).first).c_str(), (*i).second);}
    fprintf(fp, "</config>\n\n");
    fclose(fp);
}

void ConfigManager::set_encryption(int encryption)
{
    ASSERT(encryption >= ENCRYPTION_3_0_6j && encryption <= EncryptCnt);
    m_encryption = encryption;
}

bool ConfigManager::get_log_flush() const
{
    return g_logger->get_flush();
}

void ConfigManager::set_log_flush(bool log_flush)
{
    g_logger->set_flush(log_flush);
}

bool ConfigManager::get_log_verbose() const
{
    return g_logger->get_verbose();
}

void ConfigManager::set_log_verbose(bool log_verbose)
{
    g_logger->set_verbose(log_verbose);
}

bool ConfigManager::get_log_dmenus() const
{
    return g_logger->dmenus;
}

void ConfigManager::set_log_dmenus(bool log_dmenus)
{
    g_logger->dmenus=log_dmenus;
}

bool ConfigManager::get_log_fsnd() const
{
    return g_logger->fsnd;
}

void ConfigManager::set_log_fsnd(bool log_fsnd)
{
    g_logger->fsnd=log_fsnd;
}

bool ConfigManager::list_exists(const string & name)
{
    return m_lists.find(name) != m_lists.end();
}

ShoppingList & ConfigManager::find_list(const string & name)
{
    shoplists_t::iterator i = m_lists.find(name);
    ASSERT(i != m_lists.end());
    return (*i).second;
}

ShoppingList * ConfigManager::get_list(const char * name)
{
    string str(name);
    ASSERT(ConfigManager::valid_key(str));
    shoplists_t::iterator i = m_lists.find(str);
    if(i == m_lists.end())
        i = m_lists.insert(shoplists_t::value_type(str,
            ShoppingList(str))).first;
    return &(*i).second;
}

void ConfigManager::delete_list(const string & name)
{
    m_lists.erase(name);
}

void ConfigManager::create_list(const string & name)
{
    ASSERT(ConfigManager::valid_key(name));
    ASSERT(!list_exists(name));
    m_lists.insert(shoplists_t::value_type(name, ShoppingList(name)));
}

bool ConfigManager::use_exists(const string & name)
{
    return m_use.find(name) != m_use.end();
}

uint16 & ConfigManager::find_use(const string & name)
{
    uselist_t::iterator i = m_use.find(name);
    ASSERT(i != m_use.end());
    return (*i).second;
}

void ConfigManager::add_use(const string & name, uint16 graphic)
{
    //ASSERT(ConfigManager::valid_key(name));
    ASSERT(!use_exists(name));
    m_use.insert(uselist_t::value_type(name, graphic));
}

void ConfigManager::delete_use(const string & name)
{
    m_use.erase(name);
}


int FilterSpeech::add(string str)
{
Filter.push_back(str);
return Filter.size();
}

void FilterSpeech::clear(){Filter.clear();}
string FilterSpeech::operator[](int n)
{
if(n<0||n>=Filter.size()) return string();
	using namespace std;
	list<string>::iterator i;int k=0;
	for(i=Filter.begin();k<n;i++,k++);
	return *i;	
}

bool FilterSpeech::fit(string str)
{
	using namespace std;
	list<string>::iterator i;
	for(i=Filter.begin();i!=Filter.end();i++)
	{if(str.find(*i)!= string::npos)
	 {trace_printf("filtered by: %s\n",(*i).c_str());return true;}
	}
	return false;
}


void FilterSpeech::save(FILE * fp)
{
	using namespace std;
    for(list<string>::const_iterator i = Filter.begin(); i != Filter.end(); i++)
        fprintf(fp, "\t\t\t\t<filter text=\"%s\"/>\n",(*i).c_str());
}
