////////////////////////////////////////////////////////////////////////////////
//
// injection.h
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
//  Declarations for classes in injection.cpp
//
////////////////////////////////////////////////////////////////////////////////


#ifndef _INJECTION_H_
#define _INJECTION_H_

#include "common.h"
#include "iconfig.h"
#include "hooks.h"
#include "client.h"
#include "igui.h"
#include "runebook.h"
#include "spells.h"
#include "skills.h"

#include <deque>

const int SIZE_VARIABLE = 0;
const int USE_DISTANCE  = 3;

enum msg_dir_t
{
    DIR_SEND,   // client to server
    DIR_RECV,   // server to client
    DIR_BOTH
};

class Injection;
typedef bool (Injection::*msg_handler_t)(uint8 * buf, int size);

class MessageType
{
public:
    const char * name;
    int size;
    msg_dir_t direction;
    msg_handler_t shandler, rhandler;
};

typedef std::deque<string> arglist_t;

typedef void (Injection::*cmd_handler_t)(const arglist_t & args);
typedef void (Injection::*target_handler_t)(GameObject * obj);
typedef void (Injection::*target_handlertile_t)(unsigned char* pkt);

class Command
{
public:
    const char * name;
    cmd_handler_t handler;
};

const int NUM_MESSAGE_TYPES = 0xcd;
const int NUM_COMMANDS = 20;

class ServerList;
class CharacterList;
class World;
class DressHandler;
class MenuHandler;
class VendorHandler;

class SocketHook;
class HotkeyHook;

/*
    Summary of possible states:

    if m_hook == 0: initial
    elif m_servers == 0: got a message but no server list
    elif m_server_id == -1: got a server list but no server selected
    elif m_server == 0: selected a server but not yet found in list
    elif m_account == 0: server name known but not logged in
    elif m_world == 0: logged in but not yet in world
    else: playing
*/
	struct DROPPARAM
	{
		bool drophere;
		 unsigned short int quantity;
		   signed short int x,y;
		char z;
	};
	
	class Injection : public HookCallbackInterface, public ClientInterface,
    public GUICallbackInterface
{
private:
	int m_drop_count;
    static MessageType m_message_types[];
    static Command m_commands[];

    Logger m_logger;
    SocketHookSet *m_hook_set;
    SocketHook * m_hook;
    ServerList * m_servers;
    ServerConfig * m_server;
    AccountConfig * m_account;
    CharacterList * m_characters;
    HotkeyHook * m_hotkeyhook;

public:
    ConfigManager m_config;
    InjectionGUI m_gui;
    CounterManager m_counter_manager;

    int m_server_id;
	
    CharacterConfig * m_character;

//    World * m_world;


    // Command related variables:
    DressHandler * m_dress_handler; // for dress/undress commands
    MenuHandler * m_menu_handler;   // for waitmenu/menu commands
    VendorHandler * m_vendor_handler;   // for buy/sell commands
    TargetHandler * m_targeting_handler; // for targeting commands
    RunebookHandler * m_runebook_handler; // for adding recalls and gates to runebooks
    Spells * m_spells; // for cast command
    Skills * m_skills; // for useskill command
    int m_normal_light;
    int m_dye_colour;    // -1 when not set
    bool m_targeting, m_client_targeting,m_targetingtile;
	unsigned char lasttargetpkt[19];
    uint8 m_cancel_target[19];
    uint32 m_last_target;
    uint16 m_last_graphic;
    uint32 m_last_object;
    uint32 m_last_objectgraphic;
    uint32 m_last_status;
    uint32 m_last_attacked;
    bool m_last_target_set;
    target_handler_t m_target_handler;
    target_handlertile_t m_targettile_handler;
    use_target_handler_t m_use_target_handler;
	UseTabDialog * m_use_tab_dialog;
    ObjectTabDialog * m_object_tab_dialog;
    object_target_handler_t m_object_target_handler;
    void request_target(target_handler_t handler);
    void request_targettile(target_handlertile_t handler);
    void send_target_request();
    void got_target(GameObject * obj);
    void got_targettile(unsigned char* pkt);
	DROPPARAM dropparam;
	void update_palyer_coord();
	void track(bool enable=true, int x=-1, int y=-1);

protected:
    // Message handlers:
    bool handle_walk_request(uint8 * buf, int size);
    bool handle_client_talk(uint8 * buf, int size);
    bool handle_character_status(uint8 * buf, int size);
    bool handle_update_hitpoints(uint8 * buf, int size);
    bool handle_update_mana(uint8 * buf, int size);
    bool handle_update_stamina(uint8 * buf, int size);
    bool handle_enter_world(uint8 * buf, int size);
    bool handle_update_item(uint8 * buf, int size);
    bool handle_delete_object(uint8 * buf, int size);
    bool handle_update_player(uint8 * buf, int size);
    bool handle_open_container(uint8 * buf, int size);
    bool handle_update_contained_item(uint8 * buf, int size);
    bool handle_server_equip_item(uint8 * buf, int size);
    bool handle_pause_control(uint8 * buf, int size);
    bool handle_status_request(uint8 * buf, int size);
    bool handle_global_light_level(uint8 * buf, int size);
    bool handle_error_code(uint8 * buf, int size);
    bool handle_select_character(uint8 * buf, int size);
    bool handle_weather_change(uint8 * buf, int size);
    bool handle_target_s(uint8 * buf, int size);
    bool handle_target_r(uint8 * buf, int size);
    bool handle_vendor_buy_list(uint8 * buf, int size);
    bool handle_vendor_buy_reply_s(uint8 * buf, int size);
    bool handle_vendor_buy_reply_r(uint8 * buf, int size);
    bool handle_update_contained_items(uint8 * buf, int size);
    bool handle_update_object(uint8 * buf, int size);
    bool handle_open_menu_gump(uint8 * buf, int size);
    bool handle_first_login(uint8 * buf, int size);
    bool handle_character_list2(uint8 * buf, int size);
    bool handle_relay_server(uint8 * buf, int size);
    bool handle_second_login(uint8 * buf, int size);
    bool handle_dye_s(uint8 * buf, int size);
    bool handle_dye_r(uint8 * buf, int size);
	bool handle_updskills_s(uint8 * buf, int size);
	bool handle_updskills_r(uint8 * buf, int size);
    bool handle_vendor_sell_list(uint8 * buf, int size);
    bool handle_vendor_sell_reply(uint8 * buf, int size);
    bool handle_select_server(uint8 * buf, int size);
    bool handle_server_list(uint8 * buf, int size);
    bool handle_character_list(uint8 * buf, int size);
    bool handle_unicode_client_talk(uint8 * buf, int size);
	bool handle_server_talk(uint8 * buf, int size);
    bool handle_open_gump(uint8 * buf, int size);
    bool handle_new_command_filter_s(uint8 * buf, int size);
    bool handle_new_command_filter_r(uint8 * buf, int size);
    bool handle_sound(uint8 * buf, int size);
    bool handle_showcorpse(uint8 * buf, int size);
	bool handle_confirm_walk(uint8 * buf, int size);
	bool handle_perform_action(uint8 * buf, int size);
	bool handle_death(uint8 * buf, int size);
	bool handle_double_click(uint8 * buf, int size);
	bool handle_pickup(uint8 * buf, int size);
	bool handle_drop(uint8 * buf, int size);
	bool handle_update_character(uint8 * buf, int size);
	bool handle_attack_relpy(uint8 * buf, int size);
    // Perform a command supplied as a big endian Unicode string
    void command_fixwalk(const arglist_t & args);
    void command_fixtalk(const arglist_t & args);
    void command_filterweather(const arglist_t & args);
    void command_dump(const arglist_t & args);
    void command_flush(const arglist_t & args);
    void command_usetype(const arglist_t & args);
    void command_usefromground(const arglist_t & args);
    void command_useobject(const arglist_t & args);
    void command_waittargettype(const arglist_t & args);
    void command_waittargetground(const arglist_t & args);
    void command_waittargetobject(const arglist_t & args);
    void command_waittargetobjecttype(const arglist_t & args);
    void command_waittargetlast(const arglist_t & args);
    void command_waittargetself(const arglist_t & args);
    void command_canceltarget(const arglist_t & args);
    void command_setarm(const arglist_t & args);
    void command_unsetarm(const arglist_t & args);
    void command_arm(const arglist_t & args);
    void command_disarm(const arglist_t & args);
    void command_setdress(const arglist_t & args);
    void command_unsetdress(const arglist_t & args);
    void command_dress(const arglist_t & args);
    void command_undress(const arglist_t & args);
    void command_removehat(const arglist_t & args);
    void command_removeearrings(const arglist_t & args);
    void command_removeneckless(const arglist_t & args);
    void command_removering(const arglist_t & args);
    void command_dismount(const arglist_t & args);
    void command_mount(const arglist_t & args);
    void target_mount(GameObject * obj);
    void command_waitmenu(const arglist_t & args);
    void command_automenu(const arglist_t & args);
    void command_cancelmenu(const arglist_t & args);
    void command_buy(const arglist_t & args);
    void command_sell(const arglist_t & args);
    void command_shop(const arglist_t & args);
    void command_light(const arglist_t & args);
    void command_saveconfig(const arglist_t & args);
    void command_version(const arglist_t & args);
    void command_dye(const arglist_t & args);
    void command_snoop(const arglist_t & args);
    void target_snoop(GameObject * obj);
    void command_info(const arglist_t & args);
    void target_info(GameObject * obj);
    void command_hide(const arglist_t & args);
    void target_hide(GameObject * obj);
    void command_setreceivingcontainer(const arglist_t & args);
    void target_setreceivingcontainer(GameObject * obj);
    void command_emptycontainer(const arglist_t & args);
    void target_emptycontainer(GameObject * obj);
    void command_unsetreceivingcontainer(const arglist_t & args);
    void command_grab(const arglist_t & args);
    void target_grab(GameObject * obj);
    void command_drophere(const arglist_t & args);
    void command_drop(const arglist_t & args);
    void target_drop(GameObject * obj);
    void command_makefakeitem(const arglist_t & args);
    void command_cast(const arglist_t & args);
    void command_setcatchbag(const arglist_t & args);
    void command_unsetcatchbag(const arglist_t & args);
    void target_setcatchbag(GameObject * obj);
    void set_backpack();
    void command_bandageself(const arglist_t & args);
    void command_addrecall(const arglist_t & args);
    void command_addgate(const arglist_t & args);
    void command_setdefault(const arglist_t & args);
    void command_recall(const arglist_t & args);
    void command_gate(const arglist_t & args);
    void command_useskill(const arglist_t & args);
    void command_poison(const arglist_t & args);
    void command_fixhotkeys(const arglist_t & args);
    void command_setdressspeed(const arglist_t & args);
    void command_shard(const arglist_t & args);
	void command_filterspeech(const arglist_t & args);
	void command_track(const arglist_t & args);
	void command_repbuy(const arglist_t & args);
	void command_exec(const arglist_t & args);
	void command_addtype(const arglist_t & args);
    void target_addtype(GameObject * obj);
	void command_addobject(const arglist_t & args);
    void target_addobject(GameObject * obj);
	void command_boxhack(const arglist_t & args);
	void command_fontcolor(const arglist_t & args);
	void command_massmove(const arglist_t & args);
    void command_infocolor(const arglist_t & args);
    void target_infocolor(GameObject * obj);
    void command_concolor(const arglist_t & args);
    void command_getstatus(const arglist_t & args);
    void command_getname(const arglist_t & args);
    void command_attack(const arglist_t & args);
    void command_waittargettile(const arglist_t & args);
    void command_infotile(const arglist_t & args);
    void target_infotile(unsigned char* pkt);
	void command_easyobject(const arglist_t & args);
public:
    bool get_use_target(UseTabDialog * dialog, use_target_handler_t handler);
    bool get_object_target(ObjectTabDialog * dialog, object_target_handler_t handler);
    void use(uint16 graphic, uint16 color=0xffff);
    void useground(uint16 graphic, uint16 color=0xffff);
    void useobject(uint32 serial);
    void targettype(uint16 graphic, uint16 color);
    void targetgroundtype(uint16 graphic, uint16 color);
    void targetobject(uint32 serial,uint16 graphic=0xffff,uint32 serial2=0xffffffff,uint16 graphic2=0xffff);
    void targetobjecttype(uint32 serial, uint16 graphic2, uint16 color=0xffff);
//    SkillsTabDialog * m_skills_tab_dialog;
    // target container for transfer command 0 when unset and means use player's backpack
    uint32 m_receiving_container;
    long empty_speed;
    uint32 m_backpack;
    bool m_backpack_set;
    uint32 m_catchbag;
    bool m_catchbag_set;
    uint32 m_lastcaught;
	string lastname;
    uint32 lastcontainer;
    void set_catchbag(uint32 serial);
    void set_receivingcontainer(uint32 serial);
	uint32 str2serial(const char* str);
    uint16 str2graphic(const char* name,uint32** list=NULL);
    Injection();
    virtual ~Injection();

    // Called (indirectly) by Ignition:
    int init(unsigned int checksum, unsigned int length);

    int count_object_type(const string & name);
    int count_object_type(const string & name, const string & color);

    int count_on_ground(const string & name);
    int count_on_ground(const string & name, const string & color);

    // Methods of HookCallbackInterface:
    virtual int get_message_size(int code);
    virtual void disconnected(SocketHook * hook);
    virtual void handle_key(SocketHook * hook, uint8 key[4]);
    virtual bool handle_send_message(SocketHook * hook, uint8 * buf,
        int size);
    virtual bool handle_receive_message(SocketHook * hook, uint8 * buf,
        int size);

    // Methods of ClientInterface:
    virtual void send_server(uint8 * buf, int size);
    virtual void send_client(uint8 * buf, int size);
    // Print a string to the client in the form of a server talk message
    virtual void client_print(const char * text);
    virtual void client_print(const string & text);
    virtual void move_container(uint32 serial, uint32 cserial);
    virtual void move_container(uint32 serial, uint16 quantity, uint32 cserial);
    virtual void move_backpack(uint32 serial);
    virtual void move_backpack(uint32 serial, uint16 quantity);
    virtual void move_ground(uint32 serial, uint16 quantity=1);
    virtual void move_ground(uint32 serial, uint16 quantity, uint16 X, uint16 Y, char Z);
    virtual void move_equip(uint32 serial, int layer);
    virtual void do_command(const char * cmd);

	// Request for description from server
	virtual void request_name(uint32 serial);

    // Methods of GUICallbackInterface:
    virtual void dump_world();
    virtual void save_config();
    virtual void shop();
    virtual string get_version();
    virtual void update_display();

    void clickobject(const string & name);
    void clickobject(uint32 serial);
};

// Error codes
const int INJECTION_ERROR_NONE = 0;
const int INJECTION_ERROR_CONFIG = 1;
const int INJECTION_ERROR_GUI = 2;
const int INJECTION_ERROR_MEMORY = 3;

extern "C" {

// Get error text ( exported function )
const char * GetErrorText(int error);

// Get info text ( exported function )
const char * GetInfoText();

// Start target process and hook it ( exported function )
int Install(unsigned int checksum, unsigned int length);

// Deallocate memory ( NOT currently exported )
void Uninstall();

}

void inj2euo(uint32 injid, char *euoid);
void euo2inj(const char *euoid, uint32 *injid);
uint32* easyuolist(const char *euoid, uint32 *injid);
extern DWORD LastMessageTime;
extern Injection * g_injection;
extern bool FilterSpeech;
extern bool MenuTalk;
extern bool Undead;
extern bool Tracker;
extern bool StlthCnt;
extern bool CorpsesAutoOpen;
extern bool SmoothWalk;
extern bool SocksCap;
extern bool VarsLoopback;
extern bool TargXYZ;
extern bool FontOverride;
extern uint16 FColor;
extern bool SbarFix;
extern bool PoisonRevert;
extern uint16 ConColor;
extern bool TrackWorld;
extern bool UnsetSet;
extern int ActiveEncryption;

//extern int CharStat;
#endif
