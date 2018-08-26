////////////////////////////////////////////////////////////////////////////////
//
// igui.h
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
//  Declarations for GUI component of Injection
//
////////////////////////////////////////////////////////////////////////////////


#ifndef _IGUI_H_
#define _IGUI_H_

#include "gui.h"
#include "world.h"

class HotkeyTabDialog;
class UseTabDialog;
class ObjectTabDialog;
class GameObject;
class LayersDialog;

typedef void (UseTabDialog::*use_target_handler_t)(GameObject * obj);
typedef void (ObjectTabDialog::*object_target_handler_t)(GameObject * obj);

class SkillsTabDialog;

class GUICallbackInterface
{
public:
    GUICallbackInterface() {}
    virtual ~GUICallbackInterface() {}
    virtual void dump_world() = 0;
    virtual void save_config() = 0;
    virtual void shop() = 0;
    virtual string get_version() = 0;
    virtual bool get_use_target(UseTabDialog * dialog,
        use_target_handler_t handler) = 0;
    virtual bool get_object_target(ObjectTabDialog * dialog,
        object_target_handler_t handler) = 0;
    virtual void update_display() = 0;
    World * m_world;
};

////////////////////////////////////////////////////////////////////////////////

//// Vendor GUI:

class ConfigManager;
class VendorHandler;

// This class must be allocated on the heap and will be deleted automatically.
class ChooseListDialog : public ModelessDialog
{
private:
    ConfigManager & m_config;
    VendorHandler & m_vendor_handler;

protected:
    virtual void event_init_dialog();
    virtual BOOL event_command(int control_id, int notify_code);
    virtual void event_destroy();
    // deletes this:
    virtual void event_delete();

public:
    ChooseListDialog(ConfigManager & config, VendorHandler & vendor_handler);
};

class ShoppingList;
class ShoppingItem;
class VendorBuyList;
class VendorSellList;

// This class must be allocated on the heap and will be deleted automatically.
class EditListDialog : public ModelessDialog
{
private:
    VendorHandler & m_vendor_handler;
    ShoppingList & m_list;

    string shopping_list_string(const ShoppingItem & item);
    void add_shopping_list(const string & name, int quantity);

protected:
    virtual void event_init_dialog();
    virtual BOOL event_command(int control_id, int notify_code);
    virtual void event_destroy();
    // deletes this:
    virtual void event_delete();

public:
    EditListDialog(VendorHandler & vendor_handler, ShoppingList & list);

    void clear_vendor_list();
    void add_buy_list(VendorBuyList & vendor_list);
    void add_sell_list(VendorSellList & vendor_list);
};

// This class must be allocated on the heap and will be deleted automatically.
class ShopListDialog : public ModelessDialog
{
private:
    VendorHandler & m_vendor_handler;
    ShoppingList & m_list;
    bool m_error;

protected:
    virtual void event_init_dialog();
    virtual BOOL event_command(int control_id, int notify_code);
    virtual void event_destroy();
    // deletes this:
    virtual void event_delete();

public:
    ShopListDialog(VendorHandler & vendor_handler, ShoppingList & list);

    void finished_buy();
    void buy_error();
    void finished_sell();
};

////////////////////////////////////////////////////////////////////////////////

//// Main GUI:

class ConfigManager;

class MainTabDialog : public ModelessDialog
{
private:
    static const char * m_encryption_strings[];

    GUICallbackInterface & m_callback;
    ConfigManager & m_config;

protected:
    virtual void event_init_dialog();
    virtual BOOL event_command(int control_id, int notify_code);

public:
    void connected(CharacterConfig * character);
    MainTabDialog(GUICallbackInterface & callback, ConfigManager & config);
};

class UseTabDialog : public ModelessDialog
{
private:
    GUICallbackInterface & m_callback;
    ConfigManager & m_config;
    uint16 * m_current_graphic;
    bool m_targeting;


protected:
    virtual void event_init_dialog();
    virtual BOOL event_command(int control_id, int notify_code);

public:
    void selection_changed();

    UseTabDialog(GUICallbackInterface & callback, ConfigManager & config);

    void handle_target(GameObject * obj);

	void updateSerial(int wich_one=1);
};

class ObjectTabDialog : public ModelessDialog
{
private:
    GUICallbackInterface & m_callback;
    ConfigManager & m_config;
    CharacterConfig * m_character;

    uint32 * m_current_serial;
    bool m_targeting;

	LayersDialog* m_layers_dialog;

protected:
    virtual void event_init_dialog();
    virtual BOOL event_command(int control_id, int notify_code);

public:
    void selection_changed();

    ObjectTabDialog(GUICallbackInterface & callback, ConfigManager & config);

    void handle_target(GameObject * obj);

    void connected(CharacterConfig * character);
    void disconnected();
    ~ObjectTabDialog();

	void updateSerial(int wich_one=1);
};

class HotkeyTabDialog : public ModelessDialog
{
private:
    GUICallbackInterface & m_callback;
    ConfigManager & m_config;
    CharacterConfig * m_character;

    void selection_changed();

protected:
    virtual void event_init_dialog();
    virtual BOOL event_command(int control_id, int notify_code);

public:
    virtual bool create(HWND parent = 0);

    HotkeyTabDialog(GUICallbackInterface & callback, ConfigManager & config);

    void connected(CharacterConfig * character);
    void disconnected();
};

class DisplayTabDialog : public ModelessDialog
{
private:
    GUICallbackInterface & m_callback;
    ConfigManager & m_config;
    CharacterConfig * m_character;

protected:
    virtual void event_init_dialog();
    virtual BOOL event_command(int control_id, int notify_code);

public:
    DisplayTabDialog(GUICallbackInterface & callback, ConfigManager & config);

    void connected(CharacterConfig * character);
    void disconnected();
};

struct YSkill
{
 char name[25];
 int value[2];
 int oldvalue[2];
 char lock;
};

class SkillsTabDialog : public ModelessDialog
{
private:
    GUICallbackInterface & m_callback;
    ConfigManager & m_config;
    CharacterConfig * m_character;

protected:
    virtual void event_init_dialog();
    virtual BOOL event_command(int control_id, int notify_code);
    virtual BOOL event_notify(int control_id, NMHDR * hdr);

public:
	void refresh();
	int sortby;
    SkillsTabDialog(GUICallbackInterface & callback, ConfigManager & config);
	YSkill skill[50];
    void connected(CharacterConfig * character);
    void disconnected();
	void upd_xy();
};

class DllTabDialog : public ModelessDialog
{
private:

protected:
    virtual void event_init_dialog();
    virtual BOOL event_command(int control_id, int notify_code);

public:
    DllTabDialog();
};

class InjectionWindow : public NormalWindow
{
//private:
public:
    DialogTabControl m_tab_ctrl;
    MainTabDialog m_main_tab;
    UseTabDialog m_use_tab;
    DisplayTabDialog m_display_tab;
    ObjectTabDialog m_object_tab;
    HotkeyTabDialog m_hotkey_tab;
    SkillsTabDialog m_skills_tab;
    DllTabDialog m_dll_tab;

protected:
    virtual bool event_create();
    virtual LRESULT event_command(int control_id, int notify_code);
    virtual LRESULT event_notify(int control_id, NMHDR * hdr);

public:
    InjectionWindow(GUICallbackInterface & callback, ConfigManager & config);
    ~InjectionWindow();
    void connected(CharacterConfig * character);
    void disconnected();
};

class World;

class InjectionGUI : public CounterCallbackInterface
{
//private:
public:
    static InjectionGUI * m_instance;

    static LRESULT CALLBACK hook_window_proc(HWND hwnd, UINT msg,
        WPARAM wparam, LPARAM lparam);

    HWND m_client_hwnd;
    WNDPROC m_client_window_proc;
    InjectionWindow m_main_window;
    string m_counter_string;

    GUICallbackInterface & m_callback;

    void event_ncpaint();

public:
    InjectionGUI(GUICallbackInterface & callback, ConfigManager & config);
    virtual ~InjectionGUI();

    bool init();

    // Methods of CounterCallbackInterface:
    virtual void update_counter(const char * str);
    void connected(CharacterConfig * character);
    void disconnected();
};


class LayersDialog : public ModelessDialog
{
private:
 LayersDialog** myptr;
protected:
    virtual void event_init_dialog();
    virtual BOOL event_command(int control_id, int notify_code);
    virtual BOOL event_notify(int control_id, NMHDR * hdr);
    virtual void event_destroy();
    // deletes this:
    virtual void event_delete();

public:
    LayersDialog(LayersDialog** ptr);
	void refresh();
};

#endif

