////////////////////////////////////////////////////////////////////////////////
//
// iconfig.h
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


////////////////////////////////////////////////////////////////////////////////
//
//  Declarations for classes in iconfig.cpp
//
//  Handles loading/saving of configuration information.
//
////////////////////////////////////////////////////////////////////////////////


#ifndef _ICONFIG_H_
#define _ICONFIG_H_
#include <list>

#include "common.h"
#include "hashstr.h"
#include "hotkeys.h"

const int LIGHT_NORMAL = -1;
const int NUM_CLOTHES = 25;
const int NUM_ARM = 3;

class DressSet
{
private:
    static bool dress_layers[NUM_CLOTHES];

    uint32 m_layers[NUM_CLOTHES];

public:
    DressSet();
    DressSet(const DressSet & other);
    const DressSet & operator = (const DressSet & other);

    int get_size() const { return NUM_CLOTHES; }
    uint32 operator [] (int i) const
    {
        ASSERT(valid_layer(i));
        return m_layers[i];
    }
    uint32 & operator [] (int i)
    {
        ASSERT(valid_layer(i));
        return m_layers[i];
    }
    bool has_layer(int i) const
    {
        ASSERT(i >= 0 && i < NUM_CLOTHES);
        return m_layers[i] != INVALID_SERIAL;
    }

    static bool valid_layer(int i)
    {
        return i >= 0 && i < NUM_CLOTHES && dress_layers[i];
    }
};

class ArmSet
{
private:
    static bool arm_layers[NUM_ARM];

    uint32 m_layers[NUM_ARM];

public:
    ArmSet();
    ArmSet(const ArmSet & other);
    const ArmSet & operator = (const ArmSet & other);

    int get_size() const { return NUM_ARM; }
    uint32 operator [] (int i) const
    {
        ASSERT(valid_layer(i));
        return m_layers[i];
    }
    uint32 & operator [] (int i)
    {
        ASSERT(valid_layer(i));
        return m_layers[i];
    }
    bool has_layer(int i) const
    {
        ASSERT(i >= 0 && i < NUM_ARM);
        return m_layers[i] != INVALID_SERIAL;
    }

    static bool valid_layer(int i)
    {
        return i >= 0 && i < NUM_ARM && arm_layers[i];
    }
};

class FilterSpeech
{
public:
	std::list<string> Filter;
 int add(string str);
 void clear();
 int size() {return Filter.size();}
 bool fit(string str);
 string operator[](int n);
 void save(FILE * fp);
};

class CharacterConfig
{
public:
    typedef std::hash_map<string, uint32> objlist_t;

private:
    typedef std::hash_map<string, DressSet> dress_map_t;
    typedef std::hash_map<string, ArmSet> arm_map_t;

    uint32 m_serial;
    int m_light;
    dress_map_t m_dress;
    arm_map_t m_arm;

    bool m_bm_display, m_bp_display, m_ga_display, m_gs_display,
        m_mr_display, m_ns_display, m_sa_display, m_ss_display,
        m_va_display, m_en_display, m_wh_display,   m_fd_display, m_br_display,
        m_h_display, m_c_display, m_m_display, m_l_display,
        m_b_display, m_ar_display, m_bt_display;

    bool m_hp_display, m_mana_display, m_stamina_display, m_armor_display, m_weight_display, m_gold_display;

    objlist_t m_object;

public:
    CharacterConfig(uint32 serial);
    ~CharacterConfig();
    Hotkeys m_hotkeys;
	FilterSpeech fsp;

    void save(FILE * fp) const;

    int get_light() const { return m_light; }
    void set_light(int light) { m_light = light; }

    // Returns true if the dress set with the specified key exists.
    bool dress_set_exists(const string & key) const;
    bool arm_set_exists(const string & key) const;
    // Looks up the dress set with the specified key. If it does not exist,
    // creates a set initialised to INVALID_SERIALs.
    // *** 'key' must be a valid key.
    DressSet & get_dress_set(const string & key);
    ArmSet & get_arm_set(const string & key);
    // Looks up a dress set that must already exist.
    const DressSet & get_dress_set(const string & key) const;
    const ArmSet & get_arm_set(const string & key) const;
    // Removes the dress set with the specified key. If it does not exist,
    // no action is taken.
    void remove_dress_set(const string & key);
    void remove_arm_set(const string & key);

    bool get_m_bm_display(){ return m_bm_display; }
    void set_m_bm_display(bool d) { m_bm_display = d; }
    bool get_m_bp_display(){ return m_bp_display; }
    void set_m_bp_display(bool d) { m_bp_display = d; }
    bool get_m_ga_display(){ return m_ga_display; }
    void set_m_ga_display(bool d) { m_ga_display = d; }
    bool get_m_gs_display(){ return m_gs_display; }
    void set_m_gs_display(bool d) { m_gs_display = d; }
    bool get_m_mr_display(){ return m_mr_display; }
    void set_m_mr_display(bool d) { m_mr_display = d; }
    bool get_m_ns_display(){ return m_ns_display; }
    void set_m_ns_display(bool d) { m_ns_display = d; }
    bool get_m_sa_display(){ return m_sa_display; }
    void set_m_sa_display(bool d) { m_sa_display = d; }
    bool get_m_ss_display(){ return m_ss_display; }
    void set_m_ss_display(bool d) { m_ss_display = d; }
    bool get_m_va_display(){ return m_va_display; }
    void set_m_va_display(bool d) { m_va_display = d; }
    bool get_m_en_display(){ return m_en_display; }
    void set_m_en_display(bool d) { m_en_display = d; }
    bool get_m_wh_display(){ return m_wh_display; }
    void set_m_wh_display(bool d) { m_wh_display = d; }
    bool get_m_fd_display(){ return m_fd_display; }
    void set_m_fd_display(bool d) { m_fd_display = d; }
    bool get_m_br_display(){ return m_br_display; }
    void set_m_br_display(bool d) { m_br_display = d; }
    bool get_m_h_display(){ return m_h_display; }
    void set_m_h_display(bool d) { m_h_display = d; }
    bool get_m_c_display(){ return m_c_display; }
    void set_m_c_display(bool d) { m_c_display = d; }
    bool get_m_m_display(){ return m_m_display; }
    void set_m_m_display(bool d) { m_m_display = d; }
    bool get_m_l_display(){ return m_l_display; }
    void set_m_l_display(bool d) { m_l_display = d; }
    bool get_m_b_display(){ return m_b_display; }
    void set_m_b_display(bool d) { m_b_display = d; }
    bool get_m_ar_display(){ return m_ar_display; }
    void set_m_ar_display(bool d) { m_ar_display = d; }
    bool get_m_bt_display(){ return m_bt_display; }
    void set_m_bt_display(bool d) { m_bt_display = d; }

    bool get_m_hp_display(){ return m_hp_display; }
    void set_m_hp_display(bool d) { m_hp_display = d; }
    bool get_m_mana_display(){ return m_mana_display; }
    void set_m_mana_display(bool d) { m_mana_display = d; }
    bool get_m_stamina_display(){ return m_stamina_display; }
    void set_m_stamina_display(bool d) { m_stamina_display = d; }
    bool get_m_armor_display(){ return m_armor_display; }
    void set_m_armor_display(bool d) { m_armor_display = d; }
    bool get_m_weight_display(){ return m_weight_display; }
    void set_m_weight_display(bool d) { m_weight_display = d; }
    bool get_m_gold_display(){ return m_gold_display; }
    void set_m_gold_display(bool d) { m_gold_display = d; }

    objlist_t & get_objlist() { return m_object; }
    bool obj_exists(const string & name);
    uint32 & find_obj(const string & name);
    // *** 'name' must be a valid key.
    void add_obj(const string & name, uint32 serial);
    void delete_obj(const string & name);
};

class AccountConfig
{
private:
    typedef std::hash_map<uint32, CharacterConfig> map_t;

    map_t m_characters;
    string m_name;

public:
    AccountConfig(const string & name);

    void save(FILE * fp) const;

    CharacterConfig * get(uint32 character_serial);
};

class ServerConfig
{
private:
    typedef std::hash_map<string, AccountConfig> map_t;

    map_t m_accounts;
    string m_name;
    bool m_fixwalk, m_fixtalk;
    string m_buy, m_sell; // text for buy and sell
    bool m_filter_weather;

public:
    ServerConfig(const string & name);

    AccountConfig * get(const string & account_name);
    AccountConfig * get(const char * account_name);
    void save(FILE * fp) const;

    bool get_fixwalk() const { return m_fixwalk; }
    void set_fixwalk(bool fixwalk) { m_fixwalk = fixwalk; }

    bool get_fixtalk() const { return m_fixtalk; }
    void set_fixtalk(bool fixtalk) { m_fixtalk = fixtalk; }

    bool get_filter_weather() const { return m_filter_weather; }
    void set_filter_weather(bool filter_weather) { m_filter_weather = filter_weather; }

    const char * get_buy_text() { return m_buy.c_str(); }
    const char * get_sell_text() { return m_sell.c_str(); }
    void set_buy_text(const char * buy) { m_buy = buy; }
    void set_sell_text(const char * sell) { m_sell = sell; }

};

////////////////////////////////////////////////////////////////////////////////

const int ENCRYPTION_IGNITION = -1;
const int ENCRYPTION_SPHERECLIENT = -2;
const int ENCRYPTION_1_26_4 = -3;
const int ENCRYPTION_2_0_0 = -4; 
const int ENCRYPTION_2_0_3 = -5; ///zorm203
const int ENCRYPTION_3_0_5 = -6;//4; ///zorm203
const int ENCRYPTION_3_0_6j = -7;//5; ///zorm203

enum EncryptStrType
{
eET_None=0,	// no encryption
eET_Same,	//the same encryption that client uses
eET_Blow,	//<=2.0.0 (BlowFish)
eET_203,	//2.0.3 (BlowFish+TwoFish)
eET_Two 	//>2.0.3 (TwoFish)
};

struct EncryptStr //"2.0.3x"  2dbbb7cd a3c95e7f 3
{
 string name;
 uint32 key1;
 uint32 key2;
 EncryptStrType type;
};
extern EncryptStr* EncryptStrs;
extern int EncryptCnt;

class ShoppingItem
{
public:
    string m_name;
    int m_want, m_available, m_got, m_paid;

    ShoppingItem(const string & name, int want)
    : m_name(name), m_want(want)
    {
    }
};

const int WANT_ALL = -1;

class ShoppingList
{
private:
    typedef std::vector<ShoppingItem> list_t;

    string m_name;
    list_t m_items;

public:
    ShoppingList(const string & name);

    // Members for access to m_list
    typedef list_t::iterator iterator;
    size_t size() const { return m_items.size(); }
    iterator begin() { return m_items.begin(); }
    iterator end() { return m_items.end(); }
    ShoppingItem & operator[](size_t index);
    // Returns -1 if not found:
    int index_of(const string & name) const;

    // Sets all of the available/got/paid fields to zero:
    void reset();
    // Adds to the end of the list:
    void add(const string & name, int want);
    void erase(int index);

    const string & get_name() const { return m_name; }
    void save(FILE * fp) const;
};

class ConfigManager
{
public:
    typedef std::hash_map<string, ShoppingList> shoplists_t;
    typedef std::hash_map<string, uint16> uselist_t;

private:
    typedef std::hash_map<string, ServerConfig> map_t;

    string m_filename;
    bool m_loaded;
    map_t m_servers;

    int m_encryption;
    shoplists_t m_lists;
    uselist_t m_use;

public:
    ConfigManager();
    ~ConfigManager();

	bool DestroyMenus;
    static bool valid_key(const string & key);
    static string escape_attribute(const string & key);

    // Returns true if successful.
    bool load(const char * filename);

    // Return a pointer to a Config object initialised by loading
    // the specified configuration settings. If the settings cannot be loaded,
    // default options will be used.
    ServerConfig * get(const string & server_name);
    ServerConfig * get(const char * server_name);
    // Save all configuration.
    void save() const;

    int get_encryption() const { return m_encryption; }
    void set_encryption(int encryption);
    bool get_log_flush() const;
    void set_log_flush(bool log_flush);
    bool get_log_verbose() const;
    void set_log_verbose(bool log_verbose);
    bool get_log_dmenus() const;
    void set_log_dmenus(bool log_dmenus);
    bool get_log_fsnd() const;
    void set_log_fsnd(bool log_fsnd);

    shoplists_t & get_lists() { return m_lists; }
    bool list_exists(const string & name);
    // Return a reference to an existing list.
    ShoppingList & find_list(const string & name);
    // Returns the address of the shopping list or creates a new one.
    // *** 'name' must be a valid key.
    ShoppingList * get_list(const char * name);
    void delete_list(const string & name);
    // *** 'name' must be a valid key.
    void create_list(const string & name);

    uselist_t & get_uselist() { return m_use; }
    bool use_exists(const string & name);
    uint16 & find_use(const string & name);
    // *** 'name' must be a valid key.
    void add_use(const string & name, uint16 graphic);
    void delete_use(const string & name);
};

#endif

