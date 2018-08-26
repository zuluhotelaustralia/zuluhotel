////////////////////////////////////////////////////////////////////////////////
//
// igui.cpp
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
//  This file contains code for the GUI part of Injection
//
//  DllMain() is also defined here.
//
////////////////////////////////////////////////////////////////////////////////

#include "common.h"

#include "iconfig.h"
#include "resrc1.h"
#include "vendor.h"
#include "hotkeyhook.h"
#include "igui.h"
#include "extdll.h"
#include "injection.h"
#include "world.h"

#include <commctrl.h>

#include "shellapi.h"
 
   //link in the library
#pragma comment(lib,"shell32.lib")

HWND g_hmain_dialog = 0, g_dialog2 = 0;
HHOOK g_hhook = 0, g_hhook2 = 0;
WNDPROC g_client_window_proc = 0;

DWORD LastMessageTime=GetTickCount();
HWND g_ClientWindow=0;

int ShowReal=1;

static const char* skilln[50]=
{
"Alchemy",
"Anatomy",
"Animal Lore",
"Item ID",
"Arms Lore",
"Parrying",
"Begging",
"Blacksmithing",
"Bowcraft",
"Peacemaking",
"Camping",
"Carpentry",
"Cartography",
"Cooking",
"Detect Hidden",
"Enticement",
"Evaluate Intelligence",
"Healing",
"Fishing",
"Forensic Evaluation",
"Herding",
"Hiding",
"Provocation",
"Inscription",
"Lockpicking",
"Magery",
"Magic Resistance",
"Tactics",
"Snooping",
"Musicianship",
"Poisoning",
"Archery",
"Spirit Speak",
"Stealing",
"Tailoring",
"Animal Taming",
"Taste Identification",
"Tinkering",
"Tracking",
"Veterinary",
"Swordsmanship",
"Mace Fighting",
"Fencing",
"Wrestling",
"Lumberjacking",
"Mining",
"Meditation",
"Stealth",
"Remove Trap",
"Necromancy"
};
////////////////////////////////////////////////////////////////////////////////

//// Methods of ChooseListDialog:

ChooseListDialog::ChooseListDialog(ConfigManager & config,
    VendorHandler & vendor_handler)
: ModelessDialog(ID_DLG_CHOOSESHOP), m_config(config),
  m_vendor_handler(vendor_handler)
{
}

void ChooseListDialog::event_init_dialog()
{
    ASSERT(g_dialog2 == 0);
    g_dialog2 = m_hwnd;

    const ConfigManager::shoplists_t & lists=(m_config.get_lists());
    // If there are no existing shopping lists, the only option is to
    // create one.
    if(lists.empty())
    {
        // Disable the controls in the 'existing' group
        enable_control(ID_LB_SHOPLISTS, false);
        enable_control(ID_BT_EDITLIST, false);
        enable_control(ID_BT_DELETELIST, false);
        enable_control(ID_BT_SHOPLIST, false);
    }
    else    // The list is not empty
    {
        // Populate the list box
        ListBoxWrapper list_box(m_hwnd, ID_LB_SHOPLISTS);
        for(ConfigManager::shoplists_t::const_iterator i = lists.begin();
                i != lists.end(); i++)
        {
            if(list_box.add_string((*i).second.get_name().c_str()) == -1)
                error_printf("Failed adding string to list box\n");
        }
    }
}

BOOL ChooseListDialog::event_command(int control_id, int notify_code)
{
    if(control_id == IDCANCEL)
    {
        m_vendor_handler.cancel_choose();
        return TRUE;
    }
    if(control_id == ID_LB_SHOPLISTS && notify_code == LBN_DBLCLK)
    {
        // If the user double clicks a list box item, behave as if they
        // clicked the 'Shop' button.
        control_id = ID_BT_SHOPLIST;
    }
    else if(notify_code != BN_CLICKED)
        return FALSE;
    ListBoxWrapper list_box(m_hwnd, ID_LB_SHOPLISTS);
    switch(control_id)
    {
    case ID_BT_EDITLIST:    // 'Edit' button
    {
        string list_name;

        if(list_box.get_selected_string(list_name))
            m_vendor_handler.edit_list(list_name);
        else
            error_box("You must select a list to edit.");
        return TRUE;
    }
    case ID_BT_DELETELIST:  // 'Delete' button
    {
        string list_name;

        if(list_box.get_selected_string(list_name))
        {
            m_vendor_handler.delete_list(list_name);
            int index = list_box.get_current_selection();
            if(index != -1)
                list_box.delete_string(index);
        }
        else
            error_box("You must select a list to delete.");
        return TRUE;
    }
    case ID_BT_SHOPLIST:    // 'Shop' button
    {
        string list_name;

        if(list_box.get_selected_string(list_name))
            m_vendor_handler.shop_list(list_name);
        else
            error_box("You must select a list to shop with.");
        return TRUE;
    }
    case ID_BT_CREATE:
    {
        string list_name;
        EditBoxWrapper edit_box(m_hwnd, ID_EB_CREATENAME);

        edit_box.get_text(list_name);
        if(list_name.length() == 0)
            error_box("You must enter a name for the list.");
        else if(!ConfigManager::valid_key(list_name))
            error_box("Names must contain only letters and digits.");
        else
            // The following call should destroy this dialog:
            m_vendor_handler.create_list(list_name);
        return TRUE;
    }
    }
    return FALSE;
}

void ChooseListDialog::event_destroy()
{
    ASSERT(g_dialog2 == m_hwnd);
    g_dialog2 = 0;
}

void ChooseListDialog::event_delete()
{
    delete this;
}

////////////////////////////////////////////////////////////////////////////////
//// Methods of EditListDialog:

EditListDialog::EditListDialog(VendorHandler & vendor_handler,
    ShoppingList & list)
: ModelessDialog(ID_DLG_EDITLIST), m_vendor_handler(vendor_handler),
  m_list(list)
{
}

// private
string EditListDialog::shopping_list_string(const ShoppingItem & item)
{
    char buf[30];
    if(item.m_want == WANT_ALL)
        strcpy(buf, "all");
    else
        sprintf(buf, "%d", item.m_want);
    return string(buf) + " " + item.m_name;
}

void EditListDialog::event_init_dialog()
{
    ASSERT(g_dialog2 == 0);
    g_dialog2 = m_hwnd;

    // Display the name of the shopping list in a static control:
    SetDlgItemText(m_hwnd, ID_ST_LISTNAME, m_list.get_name().c_str());

    // Populate the shopping list box
    ListBoxWrapper list_box(m_hwnd, ID_LB_SHOPLIST);
    for(ShoppingList::iterator i = m_list.begin(); i != m_list.end(); i++)
        if(list_box.add_string(shopping_list_string(*i).c_str()) == -1)
            error_printf("Failed adding string to list box\n");
}

void EditListDialog::add_shopping_list(const string & name, int quantity)
{
    ListBoxWrapper slist_box(m_hwnd, ID_LB_SHOPLIST);
    // Look for the item in the shopping list.
    int index = m_list.index_of(name);
    if(index == -1)
    {
        // Add to the list
        m_list.add(name, quantity);
        if(slist_box.add_string(
                shopping_list_string(m_list[m_list.size() - 1]).c_str()) == -1)
            error_printf("Failed adding string to list box\n");
    }
    else    // If the string is already present, update it.
    {
        if(quantity == WANT_ALL)
            m_list[index].m_want = WANT_ALL;
        else if(m_list[index].m_want != WANT_ALL)
            m_list[index].m_want += quantity;
        if(!slist_box.set_string(index,
                shopping_list_string(m_list[index]).c_str()))
            error_printf("Failed setting string in list box\n");
    }
}

BOOL EditListDialog::event_command(int control_id, int notify_code)
{
    if(control_id == IDCANCEL)  // closed window or clicked 'Done' button
    {
        m_vendor_handler.done_edit(m_list);
        return TRUE;
    }
    if(control_id == ID_LB_VENDORLIST && notify_code == LBN_DBLCLK)
    {
        // If the user double clicks a vendor list box item, behave as if they
        // clicked the 'Add' button.
        control_id = ID_BT_ADDSOME;
    }
    else if(notify_code != BN_CLICKED)
        return FALSE;

    EditBoxWrapper qedit_box(m_hwnd, ID_EB_QUANTITY);
    int quantity = 0;
    if(control_id == ID_BT_ADDSOME || control_id == ID_BT_ADDNAME)
    {
        string qstring;
        qedit_box.get_text(qstring);
        if(qstring == "" || qstring == "all")
            quantity = WANT_ALL;
        else if(!string_to_int(qstring.c_str(), quantity))
        {
            error_box("Invalid quantity to add.");
            return TRUE;
        }
    }

    switch(control_id)
    {
    case ID_BT_ADDSOME:
    {
        ListBoxWrapper vlist_box(m_hwnd, ID_LB_VENDORLIST);
        string name;
        // Get name from vendor list box (current selection)
        if(!vlist_box.get_selected_string(name))
            error_box("You must select something in the right side list.");
        else
            add_shopping_list(name, quantity);
        return TRUE;
    }
    case ID_BT_DELETE:
    {
        ListBoxWrapper slist_box(m_hwnd, ID_LB_SHOPLIST);
        int index = slist_box.get_current_selection();
        if(index == -1)
            error_box("You must select something in the left side list.");
        else
        {
            // Delete from shopping list
            m_list.erase(index);
            // Delete current selection from list box
            slist_box.delete_string(index);
        }
        return TRUE;
    }
    case ID_BT_ADDNAME:
    {
        EditBoxWrapper edit_box(m_hwnd, ID_EB_ADDNAME);
        string name;
        // Get name from edit box:
        edit_box.get_text(name);
        if(name.length() == 0)
            error_box("You must enter the Name field to be added.");
        else
            // Add/update list and list box:
            add_shopping_list(name, quantity);
        return TRUE;
    }
    }   // switch()
    return FALSE;
}

void EditListDialog::event_destroy()
{
    ASSERT(g_dialog2 == m_hwnd);
    g_dialog2 = 0;
}

void EditListDialog::event_delete()
{
    delete this;
}

void EditListDialog::clear_vendor_list()
{
    // clear vendor list box:
    SendDlgItemMessage(m_hwnd, ID_LB_VENDORLIST, LB_RESETCONTENT, 0, 0);
}

void EditListDialog::add_buy_list(VendorBuyList & vendor_list)
{
    ListBoxWrapper list_box(m_hwnd, ID_LB_VENDORLIST);
    VendorBuyList::iterator vi = vendor_list.begin();
    while(vi.is_valid())
    {
        int i = list_box.add_string(vi.get_name().c_str());
        if(i == -1)
            error_printf("Failed adding string to list box\n");
        ++vi;
    }
}

void EditListDialog::add_sell_list(VendorSellList & vendor_list)
{
    ListBoxWrapper list_box(m_hwnd, ID_LB_VENDORLIST);
    VendorSellList::iterator vi = vendor_list.begin();
    while(vi.is_valid())
    {
        int i = list_box.add_string(vi.get_name().c_str());
        if(i == -1)
            error_printf("Failed adding string to list box\n");
        ++vi;
    }
}

////////////////////////////////////////////////////////////////////////////////
//// Methods of ShopListDialog:

ShopListDialog::ShopListDialog(VendorHandler & vendor_handler,
    ShoppingList & list)
: ModelessDialog(ID_DLG_BUYSELL), m_vendor_handler(vendor_handler),
  m_list(list)
{
}

void ShopListDialog::event_init_dialog()
{
    ASSERT(g_dialog2 == 0);
    g_dialog2 = m_hwnd;

    // Display the name of the shopping list in a static control:
    //SetDlgItemText(m_hwnd, ID_ST_LISTNAME, m_list.get_name().c_str());
}

BOOL ShopListDialog::event_command(int control_id, int notify_code)
{
    if(control_id == IDCANCEL)
    {
        m_vendor_handler.done_shop();
        return TRUE;
    }
    if(notify_code != BN_CLICKED)
        return FALSE;

    switch(control_id)
    {
    case ID_BT_BUY:
    case ID_BT_SELL:
    {
        EditBoxWrapper edit_box(m_hwnd, ID_EB_NPCNAME);
        string npc_name;

        // Disable buy/sell buttons and enable cancel button
        enable_control(ID_BT_BUY, false);
        enable_control(ID_BT_SELL, false);
        enable_control(ID_BT_CANCEL, true);
        // Get vendor name from edit box
        edit_box.get_text(npc_name);
        // start waiting for buy/sell
        if(control_id == ID_BT_BUY)
        {
            m_error = false;
            m_vendor_handler.shop_buy(m_list, npc_name);
            SetDlgItemText(m_hwnd, ID_ST_STATUS, "Waiting to buy");
        }
        else    // control_id == ID_BT_SELL
        {
            m_vendor_handler.shop_sell(m_list, npc_name);
            SetDlgItemText(m_hwnd, ID_ST_STATUS, "Waiting to sell");
        }
        return TRUE;
    }
    case ID_BT_CANCEL:
        // cancel waiting for buy/sell
        if(m_vendor_handler.shop_cancel())
        {
            SetDlgItemText(m_hwnd, ID_ST_STATUS, "Cancelled");
            // Enable buy/sell buttons and disable cancel button
            enable_control(ID_BT_BUY, true);
            enable_control(ID_BT_SELL, true);
            enable_control(ID_BT_CANCEL, false);
        }
        return TRUE;
    case ID_BT_DONEBUY:
        m_vendor_handler.done_shop();
        return TRUE;
    }
    return FALSE;
}

void ShopListDialog::event_destroy()
{
    ASSERT(g_dialog2 == m_hwnd);
    g_dialog2 = 0;
}

void ShopListDialog::event_delete()
{
    delete this;
}

void ShopListDialog::finished_buy()
{
    SetDlgItemText(m_hwnd, ID_ST_STATUS, "Finished buy");
    // Enable buy/sell buttons and disable cancel button
    enable_control(ID_BT_BUY, true);
    enable_control(ID_BT_SELL, true);
    enable_control(ID_BT_CANCEL, false);

    ListBoxWrapper list_box(m_hwnd, ID_LB_RESULTS);
    char buf[200];
    int total = 0;
    list_box.clear();
    if(m_error)
    {
        list_box.add_string("Error");
        return;
    }
    for(ShoppingList::iterator i = m_list.begin(); i != m_list.end(); i++)
    {
        total += (*i).m_paid;
        if((*i).m_want == WANT_ALL)
        {
            if((*i).m_got == (*i).m_available)
                sprintf(buf, "Bought all %d of '%s'", (*i).m_got,
                    (*i).m_name.c_str());
            else
                sprintf(buf, "Wanted all of '%s', bought %d/%d",
                    (*i).m_name.c_str(), (*i).m_got, (*i).m_available);
        }
        else
        {
            if((*i).m_got == (*i).m_want)
                sprintf(buf, "Bought %d/%d of '%s'", (*i).m_got,
                    (*i).m_available, (*i).m_name.c_str());
            else
                sprintf(buf, "Wanted %d of '%s', bought %d/%d",
                    (*i).m_want, (*i).m_name.c_str(), (*i).m_got, (*i).m_available);
        }
        list_box.add_string(buf);
    }
    sprintf(buf, "Total cost: %d gp", total);
    list_box.add_string(buf);
}

void ShopListDialog::buy_error()
{
    SetDlgItemText(m_hwnd, ID_ST_STATUS, "Buy error");
    m_error = true;
}

void ShopListDialog::finished_sell()
{
    SetDlgItemText(m_hwnd, ID_ST_STATUS, "Finished sell");
    // Enable buy/sell buttons and disable cancel button
    enable_control(ID_BT_BUY, true);
    enable_control(ID_BT_SELL, true);
    enable_control(ID_BT_CANCEL, false);

    ListBoxWrapper list_box(m_hwnd, ID_LB_RESULTS);
    char buf[200];
    int total = 0;
    list_box.clear();
    for(ShoppingList::iterator i = m_list.begin(); i != m_list.end(); i++)
    {
        total += (*i).m_paid;
        if((*i).m_want == WANT_ALL)
        {
            if((*i).m_got == (*i).m_available)
                sprintf(buf, "Sold all %d of '%s'", (*i).m_got,
                    (*i).m_name.c_str());
            else
                sprintf(buf, "Wanted to sell all of '%s', sold %d/%d",
                    (*i).m_name.c_str(), (*i).m_got, (*i).m_available);
        }
        else
        {
            if((*i).m_got == (*i).m_want)
                sprintf(buf, "Sold %d/%d of '%s'", (*i).m_got,
                    (*i).m_available, (*i).m_name.c_str());
            else
                sprintf(buf, "Wanted to sell %d of '%s', sold %d/%d",
                    (*i).m_want, (*i).m_name.c_str(), (*i).m_got, (*i).m_available);
        }
        list_box.add_string(buf);
    }
    sprintf(buf, "Total price: %d gp", total);
    list_box.add_string(buf);
}

////////////////////////////////////////////////////////////////////////////////
//// Methods of MainTabDialog:

const char * MainTabDialog::m_encryption_strings[] =
{
	"3.0.6j (OSI Client > 3.0.6j)", //-7
    "3.0.5 (OSI Client > 3.0.5)", //-6
	"2.0.3 (OSI Client = 2.0.3)", ///zorm203  //-5
    "2.0.0 (OSI Client = 2.0.0)",  //-4
    "1.26.4 (OSI Client < 2.0.0)", //-3
    "None (SphereClient)", //-2
    "None (Ignition)" //-1
};

MainTabDialog::MainTabDialog(GUICallbackInterface & callback,
    ConfigManager & config)
: ModelessDialog(ID_DLG_TAB_MAIN), m_callback(callback), m_config(config)
{
}
#define CheckboxSet(RES_ID,Var) {SendMessage(GetDlgItem(m_hwnd, RES_ID), BM_SETCHECK, (Var) ? BST_CHECKED : BST_UNCHECKED, 0);}

void MainTabDialog::event_init_dialog()
{
    // Initialise combo box.
    ComboBoxWrapper combo(m_hwnd, ID_ENCRYPTION);
    for(int i = 0; i < sizeof(m_encryption_strings)/sizeof(m_encryption_strings[0]); i++)
    {
        if(combo.add_string(m_encryption_strings[i]) == -1)
            error_printf("Failed adding string to combo box\n");
    }
	trace_printf("Custom Encryptions: %d\n",EncryptCnt);
    for(i = 0; i < EncryptCnt; i++)
    {
        if(combo.add_string(EncryptStrs[i].name.c_str()) == -1)
            error_printf("Failed adding string to combo box\n");
    }
    combo.set_current_selection(m_config.get_encryption()-ENCRYPTION_3_0_6j);

    // Initialise the checkboxes
    CheckboxSet(ID_CH_VERBOSE,m_config.get_log_verbose());
    CheckboxSet(ID_CH_FLUSH,  m_config.get_log_flush());
    CheckboxSet(ID_CH_DMENUS, m_config.get_log_dmenus());
    CheckboxSet(ID_CH_FSND,   m_config.get_log_fsnd());
    //CheckboxSet(ID_CH_LIGHT,  g_injection->m_character->get_light()!=0);
    CheckboxSet(ID_CH_VMENU,  MenuTalk);
    CheckboxSet(ID_CH_TRACKER,Tracker);
    CheckboxSet(ID_CH_STLTHCNT,StlthCnt);
	CheckboxSet(ID_CH_SMOOTH,SmoothWalk);
    CheckboxSet(ID_CH_CORPSOP,CorpsesAutoOpen);
	CheckboxSet(ID_CH_UNDEAD,Undead);
	CheckboxSet(ID_CH_SOCKSCAP,SocksCap);
	CheckboxSet(ID_CH_HUNGMSG,NoHungMessage);
	CheckboxSet(ID_CH_USEINJ,VarsLoopback);
	CheckboxSet(ID_TARGXYZ,TargXYZ);
	CheckboxSet(IDC_FONT,FontOverride);
	CheckboxSet(ID_CH_POISON,PoisonRevert);
	CheckboxSet(ID_CH_WORLD,TrackWorld);
	CheckboxSet(IDC_UNSET,UnsetSet);
	
    // Display the version string.
    SetDlgItemText(m_hwnd, ID_ST_VERSION, g_injection->get_version().c_str());
    enable_control(ID_BT_DYE, false);
}

void MainTabDialog::connected(CharacterConfig * character)
{
   CheckboxSet(ID_CH_LIGHT, character->get_light()==0);
   //sprintf(tmps,"Light is : %i",character->get_light());
   //g_injection->client_print(tmps);
   enable_control(ID_BT_DYE, true);
}


#define CheckboxProc(RES_ID,Var) case RES_ID: { \
        ButtonWrapper checkbox(m_hwnd, RES_ID); \
        if(checkbox.get_check()) Var=true; else Var=false; \
        return TRUE; }
#define CheckboxProcV(RES_ID,Var,DoON,DoOFF) case RES_ID: { \
        ButtonWrapper checkbox(m_hwnd, RES_ID); \
        if(checkbox.get_check()) {Var=true;DoON;} else {Var=false;DoOFF;} \
        return TRUE; }

BOOL MainTabDialog::event_command(int control_id, int notify_code)
{

    if(notify_code == EN_CHANGE)
	{
		EditBoxWrapper ed(m_hwnd,control_id);
		string s;ed.get_text(s);
        switch(control_id)
        {
        case ID_ED_FCOLOR:
			{
				FColor=g_injection->str2graphic(s.c_str());
				trace_printf("FColor 0x%04x\n",FColor);
				return TRUE;
			}
		}
		return FALSE;
	}
	if(notify_code != BN_CLICKED && notify_code != CBN_SELCHANGE)
        return FALSE;
    switch(control_id)
    {
    case ID_ENCRYPTION:
    {
        ComboBoxWrapper combo(m_hwnd, ID_ENCRYPTION);
        int i = combo.get_current_selection();
        if(i == -1)
            error_printf("Failed querying combo box\n");
        else
        {
            m_config.set_encryption(i+ENCRYPTION_3_0_6j);
            trace_printf("Encryption set to #%d\n", i+ENCRYPTION_3_0_6j);
        }
        return TRUE;
    }
	CheckboxProc(ID_CH_UNDEAD,Undead)
	CheckboxProcV(ID_CH_TRACKER,Tracker,;,g_injection->track(false))
	CheckboxProc(ID_CH_STLTHCNT,StlthCnt)
	CheckboxProc(ID_CH_CORPSOP,CorpsesAutoOpen)
	CheckboxProc(ID_CH_VMENU,MenuTalk)
	CheckboxProc(ID_CH_SMOOTH,SmoothWalk)
	CheckboxProc(ID_CH_SOCKSCAP,SocksCap)
	CheckboxProc(ID_CH_HUNGMSG,NoHungMessage)
	CheckboxProc(ID_CH_USEINJ,VarsLoopback)
	CheckboxProc(ID_TARGXYZ,TargXYZ)
	CheckboxProc(IDC_FONT,FontOverride)
	CheckboxProc(ID_CH_SBAR,SbarFix)
	CheckboxProc(ID_CH_POISON,PoisonRevert)
	CheckboxProc(ID_CH_WORLD,TrackWorld)
	CheckboxProc(IDC_UNSET,UnsetSet)
	CheckboxProcV(ID_CH_FSPEECH,FilterSpeech,g_injection->client_print("FilterSpeech enabled"),g_injection->client_print("FilterSpeech disabled"))
    case ID_CH_LIGHT:
    {
        ButtonWrapper checkbox(m_hwnd, ID_CH_LIGHT);
        int light=checkbox.get_check();
        int amount;

		if(light) {light=0; amount=0;}
        else {light = LIGHT_NORMAL; amount = g_injection->m_normal_light;}
        uint8 buf[2];
        buf[0] = CODE_GLOBAL_LIGHT_LEVEL;
        buf[1] = amount;
        g_injection->send_client(buf, sizeof(buf));
        trace_printf("Global light level set to %d\n", amount);
		if(g_injection->m_character)g_injection->m_character->set_light(light);
        return TRUE;
    }
    case ID_CH_FLUSH:
    {
        ButtonWrapper checkbox(m_hwnd, ID_CH_FLUSH);
        m_config.set_log_flush(checkbox.get_check());
        return TRUE;
    }
    case ID_BT_FLUSH:
    {
        log_flush();
        return TRUE;
    }
	case ID_BT_DYE:
	{
		unsigned char reqdye[]="\x95\xff\xff\xff\xff\0\0\x0f\xab";
		g_injection->send_client(reqdye,9);
		return TRUE;
	}
    case ID_CH_VERBOSE:
    {
        ButtonWrapper checkbox(m_hwnd, ID_CH_VERBOSE);
        m_config.set_log_verbose(checkbox.get_check());
        return TRUE;
    }
    case ID_CH_DMENUS: 
    {
        ButtonWrapper checkbox(m_hwnd, ID_CH_DMENUS);
        m_config.set_log_dmenus(checkbox.get_check());
        return TRUE;
    }
    case ID_CH_FSND: 
    {
        ButtonWrapper checkbox(m_hwnd, ID_CH_FSND);
        m_config.set_log_fsnd(checkbox.get_check());
        return TRUE;
    }
    case ID_BT_DUMP:
    {
        g_injection->dump_world();
        return TRUE;
    }
    case ID_BT_SHOP:
    {
        g_injection->shop();
        return TRUE;
    }
	case ID_WEBSITE:
	{
	Beep(440,100);
	const char* url="http://www.i.com.ua/~zombie";
    HINSTANCE result = ShellExecute(NULL, "open", url, NULL,NULL, SW_SHOWDEFAULT);
		return TRUE;
	}
	case ID_WEBSITE2:
	{
	Beep(440,100);
	const char* url="http://injection.sourceforge.net";
    HINSTANCE result = ShellExecute(NULL, "open", url, NULL,NULL, SW_SHOWDEFAULT);
		return TRUE;
	}
	case ID_WEBSITE3:
	{
	Beep(440,100);
	const char* url="http://yoko.calpha.com/forum";
    HINSTANCE result = ShellExecute(NULL, "open", url, NULL,NULL, SW_SHOWDEFAULT);
		return TRUE;
	}
    case ID_BT_SAVE:
    {
        g_injection->save_config();
        return TRUE;
    }
    }   // switch
    return FALSE;
}

////////////////////////////////////////////////////////////////////////////////
//// Methods of UseTabDialog:

UseTabDialog::UseTabDialog(GUICallbackInterface & callback,
    ConfigManager & config)
: ModelessDialog(ID_DLG_TAB_USE), m_callback(callback), m_config(config),
  m_current_graphic(0), m_targeting(false)
{
}

void UseTabDialog::event_init_dialog()
{
    // Initialise use list
    ListBoxWrapper list_box(m_hwnd, ID_LB_USE);
    ConfigManager::uselist_t & list=(m_config.get_uselist());
    for(ConfigManager::uselist_t::const_iterator i = list.begin();
            i != list.end(); i++)
        if(list_box.add_string((*i).first.c_str()) == -1)
            error_printf("Failed adding string to use list box\n");
    // Disable buttons
    enable_control(ID_BT_SAVEUSE, false);
    enable_control(ID_BT_DELETEUSE, false);
    enable_control(ID_BT_TARGET, false);
}

void UseTabDialog::selection_changed()
{
    string name;
    ListBoxWrapper list_box(m_hwnd, ID_LB_USE);

    // When a list item is single clicked, update the graphic.
    list_box.get_selected_string(name);
    if(m_config.use_exists(name))
    {
        char buf[30];
        m_current_graphic = &m_config.find_use(name);
        sprintf(buf, "0x%04x", *m_current_graphic);
        SetDlgItemText(m_hwnd, ID_EB_GRAPHIC, buf);
        enable_control(ID_BT_SAVEUSE);
        enable_control(ID_BT_DELETEUSE);
//        if(!m_targeting)
        enable_control(ID_BT_TARGET);
    }
    else
        warning_printf("use list box item not in config: %s\n", name.c_str());
}

void UseTabDialog::updateSerial(int wich_one) //0 - inject  1 - easyuo
{
if(wich_one)
{char buf0[21],buf[21]="";
 uint32 serial=0;
 GetDlgItemText(m_hwnd, ID_EB_GRAPHIC, buf, 20);
 GetDlgItemText(m_hwnd, ID_EB_GRAPHIC2, buf0, 20);
 string_to_serial(buf,serial);
 inj2euo(serial,buf);
 if(strcmp(buf,buf0))
   SetDlgItemText(m_hwnd, ID_EB_GRAPHIC2, buf);
}
else
{char buf0[21],buf[21]="";
 uint32 serial=0;
 GetDlgItemText(m_hwnd, ID_EB_GRAPHIC2, buf, 20);
 GetDlgItemText(m_hwnd, ID_EB_GRAPHIC, buf0, 20);
 euo2inj(buf,&serial);
 sprintf(buf,"0x%04lX",serial);
 if(strcmp(buf,buf0))
   SetDlgItemText(m_hwnd, ID_EB_GRAPHIC, buf);
}
}

BOOL UseTabDialog::event_command(int control_id, int notify_code)
{
    static bool changeok=true;
	if(notify_code == LBN_DBLCLK && control_id == ID_LB_USE)
    {
        // If a list item is double clicked, "Use" it.
        notify_code = BN_CLICKED;
        control_id = ID_BT_USE;
    }
    if(notify_code == EN_CHANGE)
	{
		if(changeok)
        switch(control_id)
        {
        case ID_EB_GRAPHIC:  changeok=false; updateSerial(1); changeok=true; break;
        case ID_EB_GRAPHIC2: changeok=false; updateSerial(0); changeok=true; break;
		}
	}
	else
    if(notify_code == LBN_SELCHANGE && control_id == ID_LB_USE)
    {
        selection_changed();
        return TRUE;
    }
    else if(notify_code == BN_CLICKED)
    {
        string name;
        ListBoxWrapper list_box(m_hwnd, ID_LB_USE);
        switch(control_id)
        {
        case ID_BT_SAVEUSE:
        {
            ASSERT(m_current_graphic != 0);
            EditBoxWrapper graphic_box(m_hwnd, ID_EB_GRAPHIC);
            string graphic_str;
            int graphic;

            graphic_box.get_text(graphic_str);
            if(!string_to_int(graphic_str.c_str(), graphic) || graphic < 0 ||
                    graphic > 0xffff)
                error_box("Invalid graphic.");
            else
                *m_current_graphic = graphic;
            return TRUE;
        }
        case ID_BT_DELETEUSE:
        {
            int i = list_box.get_current_selection();
            if(i != -1 && list_box.get_string(i, name))
            {
                m_config.delete_use(name);
                list_box.delete_string(i);
                // Nothing is selected now in the list box.
                enable_control(ID_BT_SAVEUSE, false);
                enable_control(ID_BT_DELETEUSE, false);
                enable_control(ID_BT_TARGET, false);
            }
            return TRUE;
        }
        case ID_BT_USE:
            // This "button" can only be activated by double clicking an item
            // in the list.
            if(list_box.get_selected_string(name))
                g_injection->use(g_injection->str2graphic(name.c_str()));
            return TRUE;
        case ID_BT_TARGET:
            ASSERT(!m_targeting);   // button should be disabled
            if(g_injection->get_use_target(this, &UseTabDialog::handle_target))
            {
                m_targeting = true;
                enable_control(ID_BT_TARGET, false);
            }
            return TRUE;
        case ID_BT_NEWUSE:
        {
            EditBoxWrapper new_box(m_hwnd, ID_EB_USENAME);
            new_box.get_text(name);
            if(m_config.use_exists(name))
                error_box("That name already exists.");
            else if(!ConfigManager::valid_key(name))
                error_box("Names must contain only letters and digits.");
            else
            {
                EditBoxWrapper graphic_box(m_hwnd, ID_EB_GRAPHIC);
                string graphic_str;
                int graphic;

                graphic_box.get_text(graphic_str);
                if(!string_to_int(graphic_str.c_str(), graphic) ||
                        graphic < 0 || graphic > 0xffff)
                    error_box("Invalid graphic.");
                else
                {
                    m_config.add_use(name, graphic);
                    int i = list_box.add_string(name.c_str());
                    if(i == -1)
                        error_printf("Failed adding string to use list box\n");
                    else
                    {
                        list_box.set_current_selection(i);
                        // Update graphic field, etc.
                        selection_changed();
                    }
                }
            }
            return TRUE;
        }
        }   // switch
    }
    return FALSE;
}

void UseTabDialog::handle_target(GameObject * obj)
{
    ASSERT(m_targeting);
    if(obj != 0)
    {
        char buf[30];
        sprintf(buf, "0x%04x", obj->get_graphic());
        SetDlgItemText(m_hwnd, ID_EB_GRAPHIC, buf);
        // Save graphic.
        *m_current_graphic = obj->get_graphic();
    }
    enable_control(ID_BT_TARGET, true);
    m_targeting = false;
}

////////////////////////////////////////////////////////////////////////////////
//// Methods of ObjectTabDialog:

ObjectTabDialog::ObjectTabDialog(GUICallbackInterface & callback,
    ConfigManager & config)
: ModelessDialog(ID_DLG_TAB_OBJECT), m_callback(callback), m_config(config), m_character(0),
  m_current_serial(0), m_targeting(false), m_layers_dialog(0)
{
}

ObjectTabDialog::~ObjectTabDialog()
{
    if(m_layers_dialog != 0)
        m_layers_dialog->destroy();
}
void ObjectTabDialog::event_init_dialog()
{
    // Disable buttons
    enable_control(ID_LB_OBJECT, false);
    enable_control(ID_BT_SAVEOBJ, false);
    enable_control(ID_BT_DELETEOBJ, false);
    enable_control(ID_BT_TARGETOBJ, false);
    enable_control(ID_BT_NEWOBJ, false);
    enable_control(ID_EB_SERIAL, false);
    enable_control(ID_EB_SERIAL, true);
    enable_control(ID_EB_OBJNAME, false);
}

void ObjectTabDialog::updateSerial(int wich_one) //0 - inject  1 - easyuo
{
if(wich_one)
{char buf0[21],buf[21]="";
 uint32 serial=0;
 GetDlgItemText(m_hwnd, ID_EB_SERIAL, buf, 20);
 GetDlgItemText(m_hwnd, ID_EB_SERIAL2, buf0, 20);
 string_to_serial(buf,serial);
 inj2euo(serial,buf);
 if(strcmp(buf,buf0))
   SetDlgItemText(m_hwnd, ID_EB_SERIAL2, buf);
}
else
{char buf0[21],buf[21]="";
 uint32 serial=0;
 GetDlgItemText(m_hwnd, ID_EB_SERIAL2, buf, 20);
 GetDlgItemText(m_hwnd, ID_EB_SERIAL, buf0, 20);
 euo2inj(buf,&serial);
 sprintf(buf,"0x%08lX",serial);
 if(strcmp(buf,buf0))
   SetDlgItemText(m_hwnd, ID_EB_SERIAL, buf);
}
}

void ObjectTabDialog::selection_changed()
{
    string name;
    ListBoxWrapper list_box(m_hwnd, ID_LB_OBJECT);

    // When a list item is single clicked, update the serial.
    list_box.get_selected_string(name);
    if(m_character->obj_exists(name))
    {
        char buf[30];
        m_current_serial = &m_character->find_obj(name);
        sprintf(buf, "0x%08lx", *m_current_serial);
        SetDlgItemText(m_hwnd, ID_EB_SERIAL, buf);
		updateSerial();
        enable_control(ID_BT_SAVEOBJ);
        enable_control(ID_BT_DELETEOBJ);
//        if(!m_targeting)
        enable_control(ID_BT_TARGETOBJ);

		if(!g_injection->m_world) return;
		GameObject* obj=g_injection->m_world->find_object(*m_current_serial);
		if(obj)
		{SetDlgItemText(m_hwnd, ID_EB_OBJDESC, obj->get_name());
		char desc[4096];
        sprintf(desc, "[%s] 0x%08lX:0x%04X*%d/0x%04X L%i %i:%i:%i F%02X",
		obj->get_name(), obj->get_serial(), obj->get_graphic(), obj->m_quantity,obj->m_colour,
		obj->get_layer(),obj->get_x(),obj->get_y(),obj->get_z(),obj->m_flags);
		SetDlgItemText(m_hwnd, ID_EB_OBJINFO, desc);
		}
		else
			SetDlgItemText(m_hwnd, ID_EB_OBJDESC, "<no object>");
    }
    else
        warning_printf("object list box item not in config: %s\n", name.c_str());
}

BOOL ObjectTabDialog::event_command(int control_id, int notify_code)
{
	static bool changeok=true;
	if(!g_injection) {trace_printf("g_injection is void\n"); return TRUE;}
    if(notify_code == LBN_SELCHANGE && control_id == ID_LB_OBJECT)
    {
        selection_changed();
        return TRUE;
    }
    if(notify_code == LBN_DBLCLK && control_id == ID_LB_OBJECT)
    {
        // If a list item is double clicked, "Use" it. //Yoko: useobject
        notify_code = BN_CLICKED;
        control_id = ID_BT_OBJUSE;
    }

    if(notify_code == EN_CHANGE)
	{
		if(changeok)
        switch(control_id)
        {
        case ID_EB_SERIAL:  changeok=false; updateSerial(1); changeok=true; break;
        case ID_EB_SERIAL2: changeok=false; updateSerial(0); changeok=true; break;
		}
	}
	else
    if(notify_code == BN_CLICKED)
    {
        string name;
        ListBoxWrapper list_box(m_hwnd, ID_LB_OBJECT);
        switch(control_id)
        {
        case ID_BT_SAVEOBJ:
        {
            ASSERT(m_current_serial != 0);
            EditBoxWrapper serial_box(m_hwnd, ID_EB_SERIAL);
            string serial_str;
            uint32 serial;

            serial_box.get_text(serial_str);
            if(!string_to_serial(serial_str.c_str(), serial) || serial > 0xffffffff)
                error_box("Invalid serial.");
            else
                *m_current_serial = serial;
            return TRUE;
        }
        case ID_BT_DELETEOBJ:
        {
            int i = list_box.get_current_selection();
            if(i != -1 && list_box.get_string(i, name))
            {
                m_character->delete_obj(name);
                list_box.delete_string(i);
                // Nothing is selected now in the list box.
                enable_control(ID_BT_SAVEOBJ, false);
                enable_control(ID_BT_DELETEOBJ, false);
                enable_control(ID_BT_TARGETOBJ, false);
            }
            return TRUE;
        }
        case ID_BT_OBJECT:
            // This "button" can only be activated by double clicking an item
            // in the list.
//          if(list_box.get_selected_string(name))
//              g_injection->targetobject(name);
            return TRUE;
        case ID_BT_TARGETOBJ:
            ASSERT(!m_targeting);   // button should be disabled
            if(g_injection->get_object_target(this, &ObjectTabDialog::handle_target))
            {
                m_targeting = true;
                enable_control(ID_BT_TARGETOBJ, false);
            }
            return TRUE;
        case ID_BT_NEWOBJ:
        {
            EditBoxWrapper new_box(m_hwnd, ID_EB_OBJNAME);
            new_box.get_text(name);
            if(m_character->obj_exists(name))
                error_box("That name already exists.");
            else if(!ConfigManager::valid_key(name))
                error_box("Names must contain only letters and digits.");
            else
            {
                EditBoxWrapper serial_box(m_hwnd, ID_EB_SERIAL);
                string serial_str;
                uint32 serial;

                serial_box.get_text(serial_str);
                if(!string_to_serial(serial_str.c_str(), serial) || serial > 0xffffffff)
                    error_box("Invalid serial.");
                else
                {
                    m_character->add_obj(name, serial);
                    int i = list_box.add_string(name.c_str());
                    if(i == -1)
                        error_printf("Failed adding string to object list box\n");
                    else
                    {
                        list_box.set_current_selection(i);
                        // Update serial field, etc.
                        selection_changed();
                    }
                }
            }
            return TRUE;
        }
		case ID_BT_OBJUSE:
			{
				list_box.get_selected_string(name);
				if(m_character->obj_exists(name))
				{
					g_injection->useobject(g_injection->str2serial(name.c_str()));
				}
			}
			return TRUE;
		case ID_BT_OBJWAIT:
			{
				list_box.get_selected_string(name);
				if(m_character->obj_exists(name))
				{
					g_injection->targetobject(g_injection->str2serial(name.c_str()));
				}
			}
			return TRUE;
		case ID_BT_OBJTARG:
			{

			}
			return TRUE;
		case ID_BT_OBJCLICK:
			{
				list_box.get_selected_string(name);
				if(m_character->obj_exists(name))
				{
					g_injection->clickobject(name);
				}
			}
			return TRUE;
		case ID_BT_OBJSRC:
			{
				if(m_current_serial)
					g_injection->set_receivingcontainer(*m_current_serial);
				else
					g_injection->client_print("Use 'unset' button to reset");
			}
			return TRUE;
		case ID_BT_OBJCB:
			{
				if(m_current_serial)
					g_injection->set_catchbag(*m_current_serial);
				else
					g_injection->client_print("Use 'unset' button to reset");
			}
			return TRUE;
		case ID_BT_OBJUNS:
			{
				g_injection->m_receiving_container = 0;
				g_injection->m_catchbag = 0;
				g_injection->m_catchbag_set = false;
				g_injection->client_print("Receiving container and catch bag are unset.");
			}
			return TRUE;
		case ID_BT_LAYERSOBJ:
			{
			if(!g_dialog2)
			{
			m_layers_dialog = new LayersDialog(&m_layers_dialog);
			m_layers_dialog->create();
			}
			}
			return TRUE;
        }   // switch

    }
    return FALSE;
}

void ObjectTabDialog::handle_target(GameObject * obj)
{
    ASSERT(m_targeting);
    if(obj != 0)
    {
        char buf[30];
        sprintf(buf, "0x%08lx", obj->get_serial());
        SetDlgItemText(m_hwnd, ID_EB_SERIAL, buf);
        // Save graphic.
        *m_current_serial = obj->get_serial();
    }
    enable_control(ID_BT_TARGETOBJ, true);
    m_targeting = false;
}

void ObjectTabDialog::connected(CharacterConfig * character)
{
    m_character = character;
    // Initialise use list
    ListBoxWrapper list_box(m_hwnd, ID_LB_OBJECT);
    CharacterConfig::objlist_t & list=(m_character->get_objlist());
    for(CharacterConfig::objlist_t::const_iterator i = list.begin();
            i != list.end(); i++)
        if(list_box.add_string((*i).first.c_str()) == -1)
            error_printf("Failed adding string to use list box\n");
    enable_control(ID_LB_OBJECT, true);
    enable_control(ID_BT_SAVEOBJ, false);
    enable_control(ID_BT_DELETEOBJ, false);
    enable_control(ID_BT_TARGETOBJ, false);
    enable_control(ID_BT_NEWOBJ, true);
    enable_control(ID_EB_SERIAL, true);
    enable_control(ID_EB_OBJNAME, true);
}

void ObjectTabDialog::disconnected()
{
    m_character = 0;
    ListBoxWrapper list_box(m_hwnd, ID_LB_OBJECT);
    list_box.clear();
    event_init_dialog();
}

////////////////////////////////////////////////////////////////////////////////
//// Methods of HotkeyTabDialog:

HotkeyTabDialog::HotkeyTabDialog(GUICallbackInterface & callback,
    ConfigManager & config)
: ModelessDialog(ID_DLG_TAB_HOTKEY), m_callback(callback), m_config(config), m_character(0)
{
}


static WNDPROC lpfnOldWndProc = 0;

inline bool IsKeyDown(int vKey)
{
    return (GetAsyncKeyState(vKey) & 0x80000000) != 0;
}

LONG FAR PASCAL SubClassFunc(   HWND hWnd,
               UINT Message,
               WPARAM wParam,
               LONG lParam)
{
    if ( Message == WM_GETDLGCODE )
    {
        return DLGC_WANTALLKEYS;
    }

    if ( Message == WM_KEYDOWN || Message == WM_SYSKEYDOWN )    
    // msctls_hotkey32 ignores VK_TAB & other keys even if 
    // DLGC_WANTALLKEYS specified
    {
        int Flag=0;

        if((lParam&(1<<24))!=0)
            Flag|=HOTKEYF_EXT;
        if(IsKeyDown(VK_CONTROL))
            Flag|=HOTKEYF_CONTROL;
        if(IsKeyDown(VK_MENU))
            Flag|=HOTKEYF_ALT;
        if(IsKeyDown(VK_SHIFT))
            Flag|=HOTKEYF_SHIFT;

        if(wParam!=VK_CONTROL && wParam!=VK_MENU && wParam!=VK_SHIFT)
        {       
            SendMessage(hWnd,HKM_SETHOTKEY,MAKEWORD(wParam,Flag),0);
            return 0;
        }
    }

    return CallWindowProc(lpfnOldWndProc, hWnd, Message, wParam,
                          lParam);
}


bool HotkeyTabDialog::create(HWND parent)
{
    bool res=ModelessDialog::create(parent);

// Now subclass ID_EB_HOTKEY hotkey control so it receives all keyboard msgs
    if(res)
    {   
        HWND hHk=GetDlgItem(get_hwnd(),ID_EB_HOTKEY);

        lpfnOldWndProc = (WNDPROC)SetWindowLong(hHk,
                 GWL_WNDPROC, (DWORD) SubClassFunc);
    }

    return res;
}

void HotkeyTabDialog::event_init_dialog()
{
    // Disable buttons
    enable_control(ID_LB_HOTKEY, false);
    enable_control(ID_BT_SAVEHOTKEY, false);
    enable_control(ID_BT_DELETEHOTKEY, false);
    enable_control(ID_BT_NEWHOTKEY, false);
    enable_control(ID_EB_COMMAND, false);
    enable_control(ID_EB_HOTKEY, false);
}

void HotkeyTabDialog::selection_changed()
{
    string key_hash_str;
    ListBoxWrapper list_box(m_hwnd, ID_LB_HOTKEY);

    // When a list item is single clicked, update the command.
    list_box.get_selected_string(key_hash_str);
    int key_hash;
    string_to_int(key_hash_str.c_str(), key_hash);
    if(m_character->m_hotkeys.exists(key_hash))
    {
        string command = m_character->m_hotkeys.get_command(key_hash);
        SetDlgItemText(m_hwnd, ID_EB_COMMAND, command.c_str());
        SetDlgItemText(m_hwnd, ID_EB_HOTKEY_TEXT, Hotkeys::get_text(key_hash).c_str());
        enable_control(ID_BT_SAVEHOTKEY);
        enable_control(ID_BT_DELETEHOTKEY);
    }
    else
        warning_printf("hotkey list box item not in config: 0x%04x\n", key_hash);
}


BOOL HotkeyTabDialog::event_command(int control_id, int notify_code)
{
    if((notify_code == LBN_DBLCLK && control_id == ID_LB_HOTKEY)
        ||(notify_code == LBN_SELCHANGE && control_id == ID_LB_HOTKEY))
    {
        selection_changed();
        return TRUE;
    }
    else if(control_id == ID_EB_HOTKEY)
    {
        return FALSE;
    }
    else if(notify_code == BN_CLICKED)
    {
        string key_hash_str;
        int key_hash;
        ListBoxWrapper list_box(m_hwnd, ID_LB_HOTKEY);
        switch(control_id)
        {
        case ID_BT_SAVEHOTKEY:
        {
            EditBoxWrapper serial_box(m_hwnd, ID_EB_COMMAND);
            string command_str;

            serial_box.get_text(command_str);
            list_box.get_string(list_box.get_current_selection(), key_hash_str);
            string_to_int(key_hash_str.c_str(), key_hash);
            if(m_character->m_hotkeys.exists(key_hash))
            {
                m_character->m_hotkeys.remove(key_hash);
                m_character->m_hotkeys.add(key_hash,command_str);
            }
            return TRUE;
        }
        case ID_BT_DELETEHOTKEY:
        {
            int i = list_box.get_current_selection();
            if(i != -1 && list_box.get_string(i, key_hash_str))
            {
                string_to_int(key_hash_str.c_str(), key_hash);
                m_character->m_hotkeys.remove(key_hash);
                list_box.delete_string(i);
                EditBoxWrapper command_box(m_hwnd, ID_EB_COMMAND);
                command_box.set_text("");
                // Nothing is selected now in the list box.
                enable_control(ID_BT_SAVEHOTKEY, false);
                enable_control(ID_BT_DELETEHOTKEY, false);
            }
            return TRUE;
        }
        case ID_BT_NEWHOTKEY:
        {
            int wParam=SendDlgItemMessage(get_hwnd(),ID_EB_HOTKEY,
                    HKM_GETHOTKEY,0,0);
            int Flags=wParam>>8;
            uint16 key_hash = Hotkeys::get_key_hash((uint8)wParam, 
                (Flags&HOTKEYF_EXT)!=0,(Flags&HOTKEYF_CONTROL)!=0, (Flags&HOTKEYF_ALT)!=0, (Flags&HOTKEYF_SHIFT)!=0);

            if(m_character->m_hotkeys.exists(key_hash))
                error_box("That hotkey already exists.");
            else
            {
                EditBoxWrapper command_box(m_hwnd, ID_EB_COMMAND);
                string command_str;
                command_box.get_text(command_str);
                m_character->m_hotkeys.add(key_hash, command_str);
                char buf[30];
                sprintf(buf,"0x%04x", key_hash);
                int i = list_box.add_string(buf);
                if(i == -1)
                    error_printf("Failed adding string to hotkey list box\n");
                else
                {
                    list_box.set_current_selection(i);
                    // Update command field, etc.
                    selection_changed();
                }
            }
            return TRUE;
        }
        }   // switch
    }
    return FALSE;
}

void HotkeyTabDialog::connected(CharacterConfig * character)
{
    m_character = character;
    // Initialise use list
    ListBoxWrapper list_box(m_hwnd, ID_LB_HOTKEY);
    Hotkeys::hotkey_map_t & list=(m_character->m_hotkeys.get_hotkey_list());
    for(Hotkeys::hotkey_map_t::const_iterator i = list.begin(); i != list.end(); i++)
    {
        char buf[30];
        sprintf(buf, "0x%04x", (*i).first);
        if(list_box.add_string(buf) == -1)
            error_printf("Failed adding string to hotkey list box\n");
    }
    enable_control(ID_LB_HOTKEY, true);
    enable_control(ID_BT_SAVEHOTKEY, false);
    enable_control(ID_BT_DELETEHOTKEY, false);
    enable_control(ID_BT_NEWHOTKEY, true);
    enable_control(ID_EB_COMMAND, true);
    enable_control(ID_EB_HOTKEY, true);
}

void HotkeyTabDialog::disconnected()
{
    m_character = 0;
    ListBoxWrapper list_box(m_hwnd, ID_LB_HOTKEY);
    list_box.clear();
    event_init_dialog();
}

////////////////////////////////////////////////////////////////////////////////
//// Methods of DisplayTabDialog:

DisplayTabDialog::DisplayTabDialog(GUICallbackInterface & callback,
    ConfigManager & config)
: ModelessDialog(ID_DLG_TAB_DISPLAY), m_callback(callback), m_config(config),
    m_character(0)
{
}

void DisplayTabDialog::event_init_dialog()
{
    ButtonWrapper checkbox_fix(m_hwnd, ID_CH_DISPLAY_FIX);
    checkbox_fix.set_check(g_FixUnicodeCaption);
     SendMessage(GetDlgItem(m_hwnd,IDR_S1+CharStat-1), BM_SETCHECK, BST_CHECKED, 0);
    enable_control(ID_CH_DISPLAY_HP, false);
    enable_control(ID_CH_DISPLAY_MANA, false);
    enable_control(ID_CH_DISPLAY_STAMINA, false);
    enable_control(ID_CH_DISPLAY_ARMOR, false);
    enable_control(ID_CH_DISPLAY_WEIGHT, false);
    enable_control(ID_CH_DISPLAY_GOLD, false);
    enable_control(ID_CH_DISPLAY_B, false);
    enable_control(ID_CH_DISPLAY_BM, false);
    enable_control(ID_CH_DISPLAY_BP, false);
    enable_control(ID_CH_DISPLAY_GA, false);
    enable_control(ID_CH_DISPLAY_GS, false);
    enable_control(ID_CH_DISPLAY_MR, false);
    enable_control(ID_CH_DISPLAY_NS, false);
    enable_control(ID_CH_DISPLAY_SA, false);
    enable_control(ID_CH_DISPLAY_SS, false);
    enable_control(ID_CH_DISPLAY_VA, false);
    enable_control(ID_CH_DISPLAY_EN, false);
    enable_control(ID_CH_DISPLAY_WH, false);
    enable_control(ID_CH_DISPLAY_FD, false);
    enable_control(ID_CH_DISPLAY_BR, false);
    enable_control(ID_CH_DISPLAY_AR, false);
    enable_control(ID_CH_DISPLAY_H, false);
    enable_control(ID_CH_DISPLAY_C, false);
    enable_control(ID_CH_DISPLAY_M, false);
    enable_control(ID_CH_DISPLAY_L, false);
    enable_control(ID_CH_DISPLAY_BT, false);
}

BOOL DisplayTabDialog::event_command(int control_id, int notify_code)
{
    if(notify_code != BN_CLICKED && notify_code != CBN_SELCHANGE)
        return FALSE;
    switch(control_id)
    {
        case ID_CH_DISPLAY_FIX:
        {
            ButtonWrapper checkbox(m_hwnd, ID_CH_DISPLAY_FIX);
            g_FixUnicodeCaption=(checkbox.get_check());
            g_injection->update_display();
            return TRUE;
        }
        case ID_CH_DISPLAY_HP:
        {
            ButtonWrapper checkbox(m_hwnd, ID_CH_DISPLAY_HP);
            m_character->set_m_hp_display(checkbox.get_check());
            g_injection->update_display();
            return TRUE;
        }
        case ID_CH_DISPLAY_MANA:
        {
            ButtonWrapper checkbox(m_hwnd, ID_CH_DISPLAY_MANA);
            m_character->set_m_mana_display(checkbox.get_check());
            g_injection->update_display();
            return TRUE;
        }
        case ID_CH_DISPLAY_STAMINA:
        {
            ButtonWrapper checkbox(m_hwnd, ID_CH_DISPLAY_STAMINA);
            m_character->set_m_stamina_display(checkbox.get_check());
            g_injection->update_display();
            return TRUE;
        }
        case ID_CH_DISPLAY_ARMOR:
        {
            ButtonWrapper checkbox(m_hwnd, ID_CH_DISPLAY_ARMOR);
            m_character->set_m_armor_display(checkbox.get_check());
            g_injection->update_display();
            return TRUE;
        }
        case ID_CH_DISPLAY_WEIGHT:
        {
            ButtonWrapper checkbox(m_hwnd, ID_CH_DISPLAY_WEIGHT);
            m_character->set_m_weight_display(checkbox.get_check());
            g_injection->update_display();
            return TRUE;
        }
        case ID_CH_DISPLAY_GOLD:
        {
            ButtonWrapper checkbox(m_hwnd, ID_CH_DISPLAY_GOLD);
            m_character->set_m_gold_display(checkbox.get_check());
            g_injection->update_display();
            return TRUE;
        }
        case ID_CH_DISPLAY_B:
        {
            ButtonWrapper checkbox(m_hwnd, ID_CH_DISPLAY_B);
            m_character->set_m_b_display(checkbox.get_check());
            g_injection->update_display();
            return TRUE;
        }
        case ID_CH_DISPLAY_BM:
        {
            ButtonWrapper checkbox(m_hwnd, ID_CH_DISPLAY_BM);
            m_character->set_m_bm_display(checkbox.get_check());
            g_injection->update_display();
            return TRUE;
        }
        case ID_CH_DISPLAY_BP:
        {
            ButtonWrapper checkbox(m_hwnd, ID_CH_DISPLAY_BP);
            m_character->set_m_bp_display(checkbox.get_check());
            g_injection->update_display();
            return TRUE;
        }
        case ID_CH_DISPLAY_GA:
        {
            ButtonWrapper checkbox(m_hwnd, ID_CH_DISPLAY_GA);
            m_character->set_m_ga_display(checkbox.get_check());
            g_injection->update_display();
            return TRUE;
        }
        case ID_CH_DISPLAY_GS:
        {
            ButtonWrapper checkbox(m_hwnd, ID_CH_DISPLAY_GS);
            m_character->set_m_gs_display(checkbox.get_check());
            g_injection->update_display();
            return TRUE;
        }
        case ID_CH_DISPLAY_MR:
        {
            ButtonWrapper checkbox(m_hwnd, ID_CH_DISPLAY_MR);
            m_character->set_m_mr_display(checkbox.get_check());
            g_injection->update_display();
            return TRUE;
        }
        case ID_CH_DISPLAY_NS:
        {
            ButtonWrapper checkbox(m_hwnd, ID_CH_DISPLAY_NS);
            m_character->set_m_ns_display(checkbox.get_check());
            g_injection->update_display();
            return TRUE;
        }
        case ID_CH_DISPLAY_SA:
        {
            ButtonWrapper checkbox(m_hwnd, ID_CH_DISPLAY_SA);
            m_character->set_m_sa_display(checkbox.get_check());
            g_injection->update_display();
            return TRUE;
        }
        case ID_CH_DISPLAY_SS:
        {
            ButtonWrapper checkbox(m_hwnd, ID_CH_DISPLAY_SS);
            m_character->set_m_ss_display(checkbox.get_check());
            g_injection->update_display();
            return TRUE;
        }
        case ID_CH_DISPLAY_VA:
        {
            ButtonWrapper checkbox(m_hwnd, ID_CH_DISPLAY_VA);
            m_character->set_m_va_display(checkbox.get_check());
            g_injection->update_display();
            return TRUE;
        }
        case ID_CH_DISPLAY_EN:
        {
            ButtonWrapper checkbox(m_hwnd, ID_CH_DISPLAY_EN);
            m_character->set_m_en_display(checkbox.get_check());
            g_injection->update_display();
            return TRUE;
        }
        case ID_CH_DISPLAY_WH:
        {
            ButtonWrapper checkbox(m_hwnd, ID_CH_DISPLAY_WH);
            m_character->set_m_wh_display(checkbox.get_check());
            g_injection->update_display();
            return TRUE;
        }
        case ID_CH_DISPLAY_FD:
        {
            ButtonWrapper checkbox(m_hwnd, ID_CH_DISPLAY_FD);
            m_character->set_m_fd_display(checkbox.get_check());
            g_injection->update_display();
            return TRUE;
        }
        case ID_CH_DISPLAY_BR:
        {
            ButtonWrapper checkbox(m_hwnd, ID_CH_DISPLAY_BR);
            m_character->set_m_br_display(checkbox.get_check());
            g_injection->update_display();
            return TRUE;
        }
        case ID_CH_DISPLAY_AR:
        {
            ButtonWrapper checkbox(m_hwnd, ID_CH_DISPLAY_AR);
            m_character->set_m_ar_display(checkbox.get_check());
            g_injection->update_display();
            return TRUE;
        }
        case ID_CH_DISPLAY_H:
        {
            ButtonWrapper checkbox(m_hwnd, ID_CH_DISPLAY_H);
            m_character->set_m_h_display(checkbox.get_check());
            g_injection->update_display();
            return TRUE;
        }
        case ID_CH_DISPLAY_C:
        {
            ButtonWrapper checkbox(m_hwnd, ID_CH_DISPLAY_C);
            m_character->set_m_c_display(checkbox.get_check());
            g_injection->update_display();
            return TRUE;
        }
        case ID_CH_DISPLAY_M:
        {
            ButtonWrapper checkbox(m_hwnd, ID_CH_DISPLAY_M);
            m_character->set_m_m_display(checkbox.get_check());
            g_injection->update_display();
            return TRUE;
        }
        case ID_CH_DISPLAY_L:
        {
            ButtonWrapper checkbox(m_hwnd, ID_CH_DISPLAY_L);
            m_character->set_m_l_display(checkbox.get_check());
            g_injection->update_display();
            return TRUE;
        }
        case ID_CH_DISPLAY_BT:
        {
            ButtonWrapper checkbox(m_hwnd, ID_CH_DISPLAY_BT);
            m_character->set_m_bt_display(checkbox.get_check());
            g_injection->update_display();
            return TRUE;
        }
		case IDR_S1: CharStat=1; break;
		case IDR_S2: CharStat=2; break;
		case IDR_S3: CharStat=3; break;
		case IDR_S4: CharStat=4; break;
    }   // switch
    return FALSE;
}

void DisplayTabDialog::connected(CharacterConfig * character)
{
    m_character = character;
    ButtonWrapper checkbox_hp(m_hwnd, ID_CH_DISPLAY_HP);
    checkbox_hp.set_check(m_character->get_m_hp_display());
    ButtonWrapper checkbox_mana(m_hwnd, ID_CH_DISPLAY_MANA);
    checkbox_mana.set_check(m_character->get_m_mana_display());
    ButtonWrapper checkbox_stamina(m_hwnd, ID_CH_DISPLAY_STAMINA);
    checkbox_stamina.set_check(m_character->get_m_stamina_display());
    ButtonWrapper checkbox_armor(m_hwnd, ID_CH_DISPLAY_ARMOR);
    checkbox_armor.set_check(m_character->get_m_armor_display());
    ButtonWrapper checkbox_weight(m_hwnd, ID_CH_DISPLAY_WEIGHT);
    checkbox_weight.set_check(m_character->get_m_weight_display());
    ButtonWrapper checkbox_gold(m_hwnd, ID_CH_DISPLAY_GOLD);
    checkbox_gold.set_check(m_character->get_m_gold_display());
    ButtonWrapper checkbox_bm(m_hwnd, ID_CH_DISPLAY_BM);
    checkbox_bm.set_check(m_character->get_m_bm_display());
    ButtonWrapper checkbox_bp(m_hwnd, ID_CH_DISPLAY_BP);
    checkbox_bp.set_check(m_character->get_m_bp_display());
    ButtonWrapper checkbox_ga(m_hwnd, ID_CH_DISPLAY_GA);
    checkbox_ga.set_check(m_character->get_m_ga_display());
    ButtonWrapper checkbox_gs(m_hwnd, ID_CH_DISPLAY_GS);
    checkbox_gs.set_check(m_character->get_m_gs_display());
    ButtonWrapper checkbox_mr(m_hwnd, ID_CH_DISPLAY_MR);
    checkbox_mr.set_check(m_character->get_m_mr_display());
    ButtonWrapper checkbox_ns(m_hwnd, ID_CH_DISPLAY_NS);
    checkbox_ns.set_check(m_character->get_m_ns_display());
    ButtonWrapper checkbox_sa(m_hwnd, ID_CH_DISPLAY_SA);
    checkbox_sa.set_check(m_character->get_m_sa_display());
    ButtonWrapper checkbox_ss(m_hwnd, ID_CH_DISPLAY_SS);
    checkbox_ss.set_check(m_character->get_m_ss_display());
    ButtonWrapper checkbox_h(m_hwnd, ID_CH_DISPLAY_H);
    checkbox_h.set_check(m_character->get_m_h_display());
    ButtonWrapper checkbox_c(m_hwnd, ID_CH_DISPLAY_C);
    checkbox_c.set_check(m_character->get_m_c_display());
    ButtonWrapper checkbox_m(m_hwnd, ID_CH_DISPLAY_M);
    checkbox_m.set_check(m_character->get_m_m_display());
    ButtonWrapper checkbox_l(m_hwnd, ID_CH_DISPLAY_L);
    checkbox_l.set_check(m_character->get_m_l_display());
    ButtonWrapper checkbox_va(m_hwnd, ID_CH_DISPLAY_VA);
    checkbox_va.set_check(m_character->get_m_va_display());
    ButtonWrapper checkbox_en(m_hwnd, ID_CH_DISPLAY_EN);
    checkbox_en.set_check(m_character->get_m_en_display());
    ButtonWrapper checkbox_wh(m_hwnd, ID_CH_DISPLAY_WH);
    checkbox_wh.set_check(m_character->get_m_wh_display());
    ButtonWrapper checkbox_fd(m_hwnd, ID_CH_DISPLAY_FD);
    checkbox_fd.set_check(m_character->get_m_fd_display());
    ButtonWrapper checkbox_br(m_hwnd, ID_CH_DISPLAY_BR);
    checkbox_br.set_check(m_character->get_m_br_display());
    ButtonWrapper checkbox_b(m_hwnd, ID_CH_DISPLAY_B);
    checkbox_b.set_check(m_character->get_m_b_display());
    ButtonWrapper checkbox_ar(m_hwnd, ID_CH_DISPLAY_AR);
    checkbox_ar.set_check(m_character->get_m_ar_display());
    ButtonWrapper checkbox_bt(m_hwnd, ID_CH_DISPLAY_BT);
    checkbox_bt.set_check(m_character->get_m_bt_display());
    enable_control(ID_CH_DISPLAY_HP, true);
    enable_control(ID_CH_DISPLAY_MANA, true);
    enable_control(ID_CH_DISPLAY_STAMINA, true);
    enable_control(ID_CH_DISPLAY_ARMOR, true);
    enable_control(ID_CH_DISPLAY_WEIGHT, true);
    enable_control(ID_CH_DISPLAY_GOLD, true);
    enable_control(ID_CH_DISPLAY_B, true);
    enable_control(ID_CH_DISPLAY_BM, true);
    enable_control(ID_CH_DISPLAY_BP, true);
    enable_control(ID_CH_DISPLAY_GA, true);
    enable_control(ID_CH_DISPLAY_GS, true);
    enable_control(ID_CH_DISPLAY_MR, true);
    enable_control(ID_CH_DISPLAY_NS, true);
    enable_control(ID_CH_DISPLAY_SA, true);
    enable_control(ID_CH_DISPLAY_SS, true);
    enable_control(ID_CH_DISPLAY_VA, true);
    enable_control(ID_CH_DISPLAY_EN, true);
    enable_control(ID_CH_DISPLAY_WH, true);
    enable_control(ID_CH_DISPLAY_FD, true);
    enable_control(ID_CH_DISPLAY_BR, true);
    enable_control(ID_CH_DISPLAY_AR, true);
    enable_control(ID_CH_DISPLAY_H, true);
    enable_control(ID_CH_DISPLAY_C, true);
    enable_control(ID_CH_DISPLAY_M, true);
    enable_control(ID_CH_DISPLAY_L, true);
    enable_control(ID_CH_DISPLAY_BT, true);

}

void DisplayTabDialog::disconnected()
{
    m_character = 0;
    ButtonWrapper checkbox_hp(m_hwnd, ID_CH_DISPLAY_HP);
    checkbox_hp.set_check(FALSE);
    ButtonWrapper checkbox_mana(m_hwnd, ID_CH_DISPLAY_MANA);
    checkbox_mana.set_check(FALSE);
    ButtonWrapper checkbox_stamina(m_hwnd, ID_CH_DISPLAY_STAMINA);
    checkbox_stamina.set_check(FALSE);
    ButtonWrapper checkbox_armor(m_hwnd, ID_CH_DISPLAY_ARMOR);
    checkbox_armor.set_check(FALSE);
    ButtonWrapper checkbox_weight(m_hwnd, ID_CH_DISPLAY_WEIGHT);
    checkbox_weight.set_check(FALSE);
    ButtonWrapper checkbox_gold(m_hwnd, ID_CH_DISPLAY_GOLD);
    checkbox_gold.set_check(FALSE);
    ButtonWrapper checkbox_bm(m_hwnd, ID_CH_DISPLAY_BM);
    checkbox_bm.set_check(FALSE);
    ButtonWrapper checkbox_bp(m_hwnd, ID_CH_DISPLAY_BP);
    checkbox_bp.set_check(FALSE);
    ButtonWrapper checkbox_ga(m_hwnd, ID_CH_DISPLAY_GA);
    checkbox_ga.set_check(FALSE);
    ButtonWrapper checkbox_gs(m_hwnd, ID_CH_DISPLAY_GS);
    checkbox_gs.set_check(FALSE);
    ButtonWrapper checkbox_mr(m_hwnd, ID_CH_DISPLAY_MR);
    checkbox_mr.set_check(FALSE);
    ButtonWrapper checkbox_ns(m_hwnd, ID_CH_DISPLAY_NS);
    checkbox_ns.set_check(FALSE);
    ButtonWrapper checkbox_sa(m_hwnd, ID_CH_DISPLAY_SA);
    checkbox_sa.set_check(FALSE);
    ButtonWrapper checkbox_ss(m_hwnd, ID_CH_DISPLAY_SS);
    checkbox_ss.set_check(FALSE);
    ButtonWrapper checkbox_h(m_hwnd, ID_CH_DISPLAY_H);
    checkbox_h.set_check(FALSE);
    ButtonWrapper checkbox_c(m_hwnd, ID_CH_DISPLAY_C);
    checkbox_c.set_check(FALSE);
    ButtonWrapper checkbox_m(m_hwnd, ID_CH_DISPLAY_M);
    checkbox_m.set_check(FALSE);
    ButtonWrapper checkbox_l(m_hwnd, ID_CH_DISPLAY_L);
    checkbox_l.set_check(FALSE);
    ButtonWrapper checkbox_va(m_hwnd, ID_CH_DISPLAY_VA);
    checkbox_va.set_check(FALSE);
    ButtonWrapper checkbox_en(m_hwnd, ID_CH_DISPLAY_EN);
    checkbox_en.set_check(FALSE);
    ButtonWrapper checkbox_wh(m_hwnd, ID_CH_DISPLAY_WH);
    checkbox_wh.set_check(FALSE);
    ButtonWrapper checkbox_fd(m_hwnd, ID_CH_DISPLAY_FD);
    checkbox_fd.set_check(FALSE);
    ButtonWrapper checkbox_br(m_hwnd, ID_CH_DISPLAY_BR);
    checkbox_br.set_check(FALSE);
    ButtonWrapper checkbox_b(m_hwnd, ID_CH_DISPLAY_B);
    checkbox_b.set_check(FALSE);
    ButtonWrapper checkbox_ar(m_hwnd, ID_CH_DISPLAY_AR);
    checkbox_ar.set_check(FALSE);
    ButtonWrapper checkbox_bt(m_hwnd, ID_CH_DISPLAY_BT);
    checkbox_bt.set_check(FALSE);
    event_init_dialog();
}

////////////////////////////////////////////////////////////////////////////////

////////////////////////////////////////////////////////////////////////////////
//// Methods of SkillsTabDialog:

SkillsTabDialog::SkillsTabDialog(GUICallbackInterface & callback,
    ConfigManager & config)
: ModelessDialog(ID_DLG_TAB_SKILLS), m_callback(callback), m_config(config),
    m_character(0)
{
}

void SkillsTabDialog::event_init_dialog()
{
static once = true;
if (!once) return;
once=false;

ButtonWrapper checkbox_real(m_hwnd, ID_SKILLREAL);
checkbox_real.set_check(true);

LV_COLUMN lvc;
HWND hwndList=get_control(ID_SKILLS);

memset(&lvc, 0, sizeof(lvc));
lvc.mask = LVCF_FMT | LVCF_WIDTH | LVCF_TEXT | LVCF_SUBITEM;
lvc.fmt = LVCFMT_LEFT;


lvc.iSubItem = 0;
lvc.cx=100;
lvc.pszText = "Skill title";
ListView_InsertColumn(hwndList, 0, &lvc);

lvc.iSubItem = 1;
lvc.cx=40;
lvc.pszText = "Val";
ListView_InsertColumn(hwndList, 1, &lvc);

lvc.iSubItem = 2;
lvc.cx=40;
lvc.pszText = "Chg";
ListView_InsertColumn(hwndList, 2, &lvc);

lvc.iSubItem = 3;
lvc.cx=30;
lvc.pszText = "L";
ListView_InsertColumn(hwndList, 3, &lvc);

sortby=0;

LV_ITEM lvitem;
memset(&lvitem, 0, sizeof(LV_ITEM));
//int iActualItem;
lvitem.mask = LVIF_TEXT | LVIF_PARAM;
lvitem.pszText = LPSTR_TEXTCALLBACK;
for(int i=0;i<50;i++)
{
	ZeroMemory(&skill[i],sizeof(YSkill));
	strncpy(skill[i].name,skilln[i],24);
	skill[i].lock=-1;
	lvitem.iItem = i;
	lvitem.iSubItem = 0;
	lvitem.lParam=(long)(&(skill[i]));
//	iActualItem = SendMessage(hwndList, LVM_INSERTITEM,0,long(&lvitem));
//	lvitem.iItem = iActualItem;
	ListView_InsertItem(hwndList, &lvitem);

	lvitem.iItem = i;
	lvitem.iSubItem = 1;
	SendMessage(hwndList, LVM_SETITEM,0,long(&lvitem));

	lvitem.iItem = i;
	lvitem.iSubItem = 2;
	SendMessage(hwndList, LVM_SETITEM,0,long(&lvitem));

	lvitem.iItem = i;
	lvitem.iSubItem = 3;
	SendMessage(hwndList, LVM_SETITEM,0,long(&lvitem));
}
refresh();
}

int CALLBACK
LVCompareProc(LPARAM lParam1, LPARAM lParam2,
  LPARAM lParamSort)
{
  YSkill *skill1 = (YSkill *)lParam1;
  YSkill *skill2 = (YSkill *)lParam2;
  LPSTR lpStr1, lpStr2;
  int iResult=0;
  if(skill1 && skill2)
  {
        lpStr1 = skill1->name;
        lpStr2 = skill2->name;
    switch(lParamSort)
    {
      case 0:
        iResult = strcmpi(lpStr1, lpStr2);
         break;
	  case 1: 
        iResult = skill2->value[ShowReal]-skill1->value[ShowReal];
		if(!iResult) iResult = strcmpi(lpStr1, lpStr2);
        break;
	  case 2: 
        iResult = skill2->value[ShowReal]-skill2->oldvalue[ShowReal]-skill1->value[ShowReal]+skill1->oldvalue[ShowReal];
		if(!iResult) iResult = strcmpi(lpStr1, lpStr2);
        break;
	  case 3: 
        iResult = skill1->lock-skill2->lock;
		if(!iResult) iResult = strcmpi(lpStr1, lpStr2);
        break;
    }
  }
  return(iResult);
}

BOOL SkillsTabDialog::event_notify(int control_id, NMHDR *hdr)
{
int result=0;
switch(control_id)
{
case ID_SKILLS:
	LV_DISPINFO * lpLvdi = (LV_DISPINFO *)hdr;
	float v=-1;
	switch(hdr->code)
	{
	case LVN_GETDISPINFO:
		if(lpLvdi->item.mask & LVIF_TEXT)
		{
		    YSkill *skill = (YSkill *)lpLvdi->item.lParam;
			static char szBuf[20];
			switch(lpLvdi->item.iSubItem)
			{case 0: lpLvdi->item.pszText=skill->name; break;
			 case 1: v=float(skill->value[ShowReal])/10;              sprintf(szBuf,"%3.1f",v);lpLvdi->item.pszText=szBuf; break;
			 case 2: v=float(skill->value[ShowReal]-skill->oldvalue[ShowReal])/10;sprintf(szBuf,"%3.1f",v);lpLvdi->item.pszText=szBuf; break;
			 case 3: 
				 switch(skill->lock)
				 {
					case 0: lpLvdi->item.pszText="^"; break;
					case 1: lpLvdi->item.pszText="D"; break;
					case 2: lpLvdi->item.pszText="L"; break;
					default:lpLvdi->item.pszText="."; break;
				 }
				 break;
			}
		}
		break;
	case LVN_COLUMNCLICK:
		{
		NMLISTVIEW *lpNm = (NM_LISTVIEW *)hdr; sortby=lpNm->iSubItem;
		//ListView_SortItems(lpNm->hdr.hwndFrom,LVCompareProc,(LPARAM)(lpNm->iSubItem));
		refresh();
		}
		break;
	}//hdr->code
}//control_id

    return result;
}
BOOL SkillsTabDialog::event_command(int control_id, int notify_code)
{
   if(notify_code != BN_CLICKED)
        return FALSE;
	switch(control_id)
	{
	case ID_SKILLREAL:
		{
		ButtonWrapper checkbox(m_hwnd, ID_SKILLREAL);
        ShowReal=checkbox.get_check();
		}
		refresh(); break;
	case ID_BT_SKILLSRESET:
		{
			for(int i=0;i<50;i++)
			{
				skill[i].value[1]=skill[i].value[0];
				skill[i].oldvalue[1]=skill[i].oldvalue[0];
			}

		} refresh(); break;
	case ID_BT_SAVESKILLS:
		{
		if(OpenClipboard(m_hwnd))
		{
		Beep(440,100);
		char buff[4096]="";
		char buf[80];
		float skillcap=0;int statcap=g_STR+g_INT+g_DEX;

		SYSTEMTIME st; GetSystemTime(&st);
		
		sprintf(buf,"===Skill (%s) status report, %i.%i.%i %i:%i===\r\n", ShowReal?"real":"not real",
			st.wDay,st.wMonth,st.wYear,st.wMinute,st.wHour);
		strcat(buff,buf);
		for(int i=0;i<50;i++)
		{
			skillcap+=skill[i].value[ShowReal];
			if(skill[i].value[ShowReal]==skill[i].oldvalue[ShowReal]) sprintf(buf,"%22s\t%.1f\r\n",skill[i].name,float(skill[i].value[ShowReal])/10);
				else sprintf(buf,"%22s\t%.1f\t%.1f\r\n",skill[i].name,float(skill[i].value[ShowReal])/10,float(skill[i].value[ShowReal]-skill[i].oldvalue[ShowReal])/10);
			strcat(buff,buf);
		}
		sprintf(buf,"___Skillcap (%s): %.1f Statcap: %i___\r\n",ShowReal?"real":"not real",skillcap/10,statcap);
		strcat(buff,buf);

		HGLOBAL clipbuffer;
		char * buffer;
		EmptyClipboard();
		clipbuffer = GlobalAlloc(GMEM_DDESHARE, strlen(buff)+1);
		buffer = (char*)GlobalLock(clipbuffer);
		strcpy(buffer, buff);
		GlobalUnlock(clipbuffer);
		SetClipboardData(CF_TEXT,clipbuffer);
		CloseClipboard();
		}
		}
		break;
	}
    return FALSE;
}

void SkillsTabDialog::connected(CharacterConfig * character)
{
}

void SkillsTabDialog::disconnected()
{
//    event_init_dialog();
}
void SkillsTabDialog::upd_xy()
{
if(!g_injection||!g_injection->m_world) return;
	GameObject*player=g_injection->m_world->get_player();
	if(!player) return;
	EditBoxWrapper xbox(m_hwnd, ID_ED_XY);
		char s[80];
		if(player->m_direction>=0x80)
        sprintf(s,"%i.%i d%iR ",player->get_x(),player->get_y(),int(player->m_direction-0x80));
		else
        sprintf(s,"%i.%i d%i ",player->get_x(),player->get_y(),int(player->m_direction));
		if(player->isHidden()) sprintf(s,"%s H",s);
		if(player->isPoisoned()) sprintf(s,"%s P",s);
		//if(player->m_flags&0x7b) sprintf(s,"%s f:0x%02x",s,player->m_flags);
		xbox.set_text(s);
}

////////////////////////////////////////////////////////////////////////////////
DllTabDialog::DllTabDialog()
: ModelessDialog(ID_DLG_TAB_EXTERNAL)
{
}

void DllTabDialog::event_init_dialog()
{
}

BOOL DllTabDialog::event_command(int , int )
{
    return FALSE;
}

////////////////////////////////////////////////////////////////////////////////

const int MARGIN_DLG = 8;

//// Methods of InjectionWindow:

InjectionWindow::InjectionWindow(GUICallbackInterface & callback, ConfigManager & config)
: NormalWindow(), m_tab_ctrl(), m_main_tab(callback, config),
  m_use_tab(callback, config), m_display_tab(callback, config), m_object_tab(callback, config),
  m_hotkey_tab(callback, config), m_skills_tab(callback, config)
{
}

InjectionWindow::~InjectionWindow()
{
    m_hotkey_tab.destroy();
    m_skills_tab.destroy();
    m_display_tab.destroy();
    m_object_tab.destroy();
    m_use_tab.destroy();
    m_main_tab.destroy();
    m_dll_tab.destroy();
    m_tab_ctrl.destroy();
}

bool InjectionWindow::event_create()
{
    trace_printf("InjectionWindow::event_create()\n");
	// Create the tab control.
    RECT tc_rect;
    if(!m_tab_ctrl.create(m_hwnd))
        return false;
	//SendMessage(m_tab_ctrl.get_hwnd(),TCM_SETEXTENDEDSTYLE,0,TCS_MULTILINE);
    // Create the child dialogs, and add them as tabs.
    if(!m_main_tab.create(m_hwnd)) return false;
	
    HFONT dlgfont = reinterpret_cast<HFONT>(m_main_tab.send_message(WM_GETFONT, 0, 0));
    m_tab_ctrl.send_message(WM_SETFONT, reinterpret_cast<WPARAM>(dlgfont), 0);
    m_tab_ctrl.add(m_main_tab, "Main");

    if(!m_skills_tab.create(m_hwnd)) return false;
    m_tab_ctrl.add(m_skills_tab, "Skills");

    if(!m_use_tab.create(m_hwnd)) return false;
    m_tab_ctrl.add(m_use_tab, "Object Types");

    if(!m_object_tab.create(m_hwnd)) return false;
    m_tab_ctrl.add(m_object_tab, "Objects");
    
	if(!m_display_tab.create(m_hwnd)) return false;
    m_tab_ctrl.add(m_display_tab, "Display");
    
	if(!m_hotkey_tab.create(m_hwnd)) return false;
    m_tab_ctrl.add(m_hotkey_tab, "Hotkeys");

    m_tab_ctrl.add(m_dll_tab, "Script");
    if(!m_dll_tab.create(m_hwnd)) return false;
    

    // Reposition the tab control.
    m_tab_ctrl.get_desired_rect(tc_rect);
    OffsetRect(&tc_rect, MARGIN_DLG - tc_rect.left, MARGIN_DLG - tc_rect.top);
    m_tab_ctrl.set_rect(tc_rect);
    // Now find the rectangle of the display area.
    RECT child_rect;
    CopyRect(&child_rect, &tc_rect);
    m_tab_ctrl.get_display_rect(child_rect);
    m_main_tab.set_rect(child_rect);
    m_use_tab.set_rect(child_rect);
    m_object_tab.set_rect(child_rect);
    m_display_tab.set_rect(child_rect);
    m_hotkey_tab.set_rect(child_rect);
    m_skills_tab.set_rect(child_rect);
    m_dll_tab.set_rect(child_rect);
    // Make the tab control bottom-most.
    SetWindowPos(m_tab_ctrl.get_hwnd(), HWND_BOTTOM, 0, 0, 0, 0,
        SWP_NOSIZE | SWP_NOMOVE);
    // Simulate selection of the current page.
    m_tab_ctrl.notify_sel_change();

    // Resize and show the dialog.
    RECT win_rect, client_rect;
    // Get total window size.
    GetWindowRect(m_hwnd, &win_rect);
    // Get client rect, and subtract to find border sizes.
    GetClientRect(m_hwnd, &client_rect);
    int dlgw = win_rect.right - win_rect.left - client_rect.right +
        tc_rect.right - tc_rect.left + 2 * MARGIN_DLG;
    int dlgh = win_rect.bottom - win_rect.top - client_rect.bottom +
        tc_rect.bottom - tc_rect.top + 2 * MARGIN_DLG;
    set_size(dlgw, dlgh);
    show();

    InitExternalDll(m_dll_tab.get_hwnd());

    return true;
}

LRESULT InjectionWindow::event_command(int /*control_id*/, int /*notify_code*/)
{
    return 1;
}

LRESULT InjectionWindow::event_notify(int /*control_id*/, NMHDR * hdr)
{
    if(hdr->hwndFrom == m_tab_ctrl.get_hwnd())
    {
        switch(hdr->code)
        {
        case TCN_SELCHANGING:
            return FALSE;   // allow selection to change.
        case TCN_SELCHANGE:
            m_tab_ctrl.notify_sel_change();
            break;
        }
    }
    return 0;
}

void InjectionWindow::connected(CharacterConfig * character)
{
    m_main_tab.connected(character);
    m_object_tab.connected(character);
    m_display_tab.connected(character);
    m_hotkey_tab.connected(character);
    m_skills_tab.connected(character);
}

void InjectionWindow::disconnected()
{
    m_object_tab.disconnected();
    m_display_tab.disconnected();
    m_hotkey_tab.disconnected();
    m_skills_tab.disconnected();
trace_printf("void InjectionWindow::disconnected()\n");
}

////////////////////////////////////////////////////////////////////////////////

//// Methods of InjectionGUI:

// private
InjectionGUI * InjectionGUI::m_instance = 0;

InjectionGUI::InjectionGUI(GUICallbackInterface & callback,
    ConfigManager & config)
: m_client_hwnd(0), m_client_window_proc(0), m_main_window(callback, config), 
    m_callback(callback)
{
    ASSERT(m_instance == 0);
    m_instance = this;

    // Find the client window, which uses the "Ultima Online" window class.
    HWND hwnd = FindWindowEx(0, 0, "Ultima Online", 0);
    while(hwnd != 0)
    {
        // Make sure the window is owned by the current thread
        DWORD threadid = GetWindowThreadProcessId(hwnd, NULL);
        if(threadid == GetCurrentThreadId())
        {
            if(m_client_hwnd == 0)
                m_client_hwnd = hwnd;
            else    // found more than one match
                warning_printf("another client window found: %p\n", hwnd);
        }
        // Find the next sibling.
        hwnd = FindWindowEx(0, hwnd, "Ultima Online", 0);
    }

    if(m_client_hwnd==0)
    {
    	hwnd = FindWindowEx(0, 0, "Ultima Online Third Dawn", 0);
	    while(hwnd != 0)
    	{
	        // Make sure the window is owned by the current thread
    	    DWORD threadid = GetWindowThreadProcessId(hwnd, NULL);
        	if(threadid == GetCurrentThreadId())
	        {
    	        if(m_client_hwnd == 0)
        	        m_client_hwnd = hwnd;
            	else    // found more than one match
	                warning_printf("another client window found: %p\n", hwnd);
    	    }
        	// Find the next sibling.
	        hwnd = FindWindowEx(0, hwnd, "Ultima Online Third Dawn", 0);
	    }    
	}

    if(m_client_hwnd == 0)
        error_printf("unable to find UO client window!\n");
    else
    {
        // Hook the client's window procedure
        LONG old = SetWindowLong(m_client_hwnd, GWL_WNDPROC,
            reinterpret_cast<LONG>(hook_window_proc));
        m_client_window_proc = reinterpret_cast<WNDPROC>(old);
        g_client_window_proc = m_client_window_proc;
//        SetWindowLong(m_client_hwnd, GWL_WNDPROC,(old));
    }
}

InjectionGUI::~InjectionGUI()
{
    m_main_window.destroy();
    if(m_client_hwnd != 0)
    {
        // Unhook the client's window procedure
        SetWindowLong(m_client_hwnd, GWL_WNDPROC,
            reinterpret_cast<LONG>(m_client_window_proc));
        m_client_window_proc = 0;
        m_client_hwnd = 0;
    }
    ASSERT(m_instance == this);
    m_instance = 0;
}

// static, private
LRESULT CALLBACK InjectionGUI::hook_window_proc(HWND hwnd, UINT msg,
    WPARAM wparam, LPARAM lparam)
{
	LastMessageTime=GetTickCount();
	g_ClientWindow=hwnd;

    static LPARAM KeyToEat=0;
    switch(msg)
    {
    case WM_NCPAINT:
        m_instance->event_ncpaint();
        break;

    case WM_CLOSE:
        g_injection->save_config();
        UnloadExternalDll();
        Uninstall();
        break;

    case WM_CHAR:
        if(KeyToEat && KeyToEat==lparam)
        {
            KeyToEat=0;
            return 0;
        }
        break;

    case WM_KEYDOWN:
    case WM_SYSKEYDOWN:
        if(HotkeyHook::KeyboardHook(wparam, lparam))
        {
            KeyToEat=lparam;
            return 0;
        }
        break;
    }

	return CallWindowProc(g_client_window_proc, hwnd, msg,
			wparam, lparam);
}

void InjectionGUI::event_ncpaint()
{
}


bool InjectionGUI::init()
{
    //trace_printf("InjectionGUI::init()");
	if(!m_main_window.create(0))
        return false;
    g_hmain_dialog = m_main_window.get_hwnd();
    ASSERT(g_hmain_dialog != 0);
    return true;
}

void InjectionGUI::update_counter(const char * str)
{
    m_counter_string = str;

    static wchar_t Buff[1024];
    MultiByteToWideChar(CP_ACP,0,str,-1,Buff,1024);
    if(IsWindowUnicode(m_client_hwnd))
        SetWindowTextW(m_client_hwnd,Buff);
    else
        SetWindowTextA(m_client_hwnd,str);
}

void InjectionGUI::connected(CharacterConfig * character)
{
    m_main_window.connected(character);
}

void InjectionGUI::disconnected()
{
    m_main_window.disconnected();
}


////////////////////////////////////////////////////////////////////////////////

extern "C" BOOL WINAPI DllMain(HINSTANCE hinst, DWORD reason, LPVOID reserved);

BOOL WINAPI DllMain(HINSTANCE hinst, DWORD reason, LPVOID /*reserved*/)
{
    if(reason == DLL_PROCESS_ATTACH)
    {
        g_hinstance = hinst;
        InitCommonControls();
    }
    else if(reason == DLL_PROCESS_DETACH)
        g_hinstance = 0;
    
    return TRUE;
}


void SkillsTabDialog::refresh()
{
trace_printf("Sort skills by: %i\n",sortby);
ListView_SortItems(get_control(ID_SKILLS),LVCompareProc,sortby);
ListView_Update(get_control(ID_SKILLS), 0);
int skillcap=0;
for(int i=0;i<50;i++) skillcap+=skill[i].value[ShowReal];
        EditBoxWrapper edit_box(m_hwnd, ID_ED_SKILLCAP);
		char buf[10]; sprintf(buf,"%4.1f",float(skillcap)/10);
        edit_box.set_text(buf);
        EditBoxWrapper edit_box2(m_hwnd, ID_ED_STATCAP);
		sprintf(buf,"%i",g_STR+g_INT+g_DEX);
        edit_box2.set_text(buf);
}

////////////////////////////////////////////////////////////////////////////////

//// Methods of LayersDialog:

LayersDialog::LayersDialog(LayersDialog** ptr)
: ModelessDialog(ID_DLG_LAYERS), myptr(ptr)
{
}

void LayersDialog::event_init_dialog()
{
    ASSERT(g_dialog2 == 0);
    g_dialog2 = m_hwnd;

HWND hwndList=get_control(ID_LOL);
LV_COLUMN lvc;
memset(&lvc, 0, sizeof(lvc));
lvc.mask = LVCF_FMT | LVCF_WIDTH | LVCF_TEXT | LVCF_SUBITEM;
lvc.fmt = LVCFMT_LEFT;

lvc.iSubItem = 0;	lvc.cx=70;	lvc.pszText = "ObjectID";	ListView_InsertColumn(hwndList, 0, &lvc);
lvc.iSubItem = 1;	lvc.cx=50;	lvc.pszText = "Type";		ListView_InsertColumn(hwndList, 1, &lvc);
lvc.iSubItem = 2;	lvc.cx=50;	lvc.pszText = "Layer";		ListView_InsertColumn(hwndList, 2, &lvc);
lvc.iSubItem = 3;	lvc.cx=80;	lvc.pszText = "Name";		ListView_InsertColumn(hwndList, 3, &lvc);

SendMessage(hwndList,LVM_SETEXTENDEDLISTVIEWSTYLE,0,LVS_EX_FULLROWSELECT);

	refresh();
}

BOOL LayersDialog::event_command(int control_id, int notify_code)
{
    if(control_id == IDCANCEL)
    {
		destroy();
        return TRUE;
    }
 if(notify_code!=BN_CLICKED) return FALSE;
    switch(control_id)
    {
    case ID_BLOL:    // 'Refresh' button
    {
        refresh();
		return TRUE;
    }
    }
    return FALSE;
}

void LayersDialog::event_destroy()
{
    ASSERT(g_dialog2 == m_hwnd);
    g_dialog2 = 0;
}

void LayersDialog::event_delete()
{
	*myptr=NULL;
    delete this;
}

const char* layername[30]=
{"*",   //0
"R hand",
"L hand",
"Shoes",
"Pants",
"Shirt",
"Hat",
"Gloves",
"Ring",
"0x09",
"Neck",
"Hair",
"Waist",
"Torso",
"Brace",
"0x0F",
"Beard",
"TorsoH",
"Ear",
"Arms",
"Cloak",
"Bpack", //0x15
"Robe",
"Eggs",
"Legs",
"Horse",
"Rstk",
"NRstk",
"Sell",
"Bank"};

void LayersDialog::refresh()
{
if(!g_injection||!g_injection->m_world||!g_injection->m_world->get_player()) return;

	int i=0,layer;char sbuf[255];
	HWND hList=get_control(ID_LOL);
ListView_DeleteAllItems(hList);
	LV_ITEM LvItem; memset(&LvItem,0,sizeof(LvItem));
	LvItem.mask=LVIF_TEXT;
	LvItem.cchTextMax = 256; // Max size of test
	
	int n=0;
		for(GameObject::iterator o = g_injection->m_world->get_player()->begin();
		o != g_injection->m_world->get_player()->end(); ++o)
        {
            layer=o->get_layer();
			if(layer)
            {
	LvItem.iItem=n; LvItem.iSubItem=0;
	sprintf(sbuf,"0x%08lx",o->get_serial());
	LvItem.pszText=sbuf;
    SendMessage(hList,LVM_INSERTITEM,0,(LPARAM)&LvItem);
				
			LvItem.iSubItem=1;
			sprintf(sbuf,"0x%04x",o->get_graphic());
			LvItem.pszText=sbuf;
			SendMessage(hList,LVM_SETITEM,0,(LPARAM)&LvItem);

			LvItem.iSubItem=2;
			if(layer<30) LvItem.pszText=(char*)layername[layer];
			else {sprintf(sbuf,"0x%02x",layer);LvItem.pszText=sbuf;}
			SendMessage(hList,LVM_SETITEM,0,(LPARAM)&LvItem);

			LvItem.iSubItem=3;
			strncpy(sbuf,o->get_name(),79);
			LvItem.pszText=sbuf;
			SendMessage(hList,LVM_SETITEM,0,(LPARAM)&LvItem);
	n++;
            }
        }
}

BOOL LayersDialog::event_notify(int control_id, NMHDR * hdr)
{
  if(control_id==ID_LOL && hdr->code == NM_DBLCLK)
  {// If a list item is double clicked, "Click" it
	char Text[255]={0};  
	int iSlected=0;
	iSlected=SendMessage(get_control(ID_LOL),LVM_GETNEXTITEM,-1,LVNI_FOCUSED);
	if(iSlected==-1) return FALSE;
	LVITEM LvItem;  // ListView Item struct
	memset(&LvItem,0,sizeof(LvItem));
    LvItem.mask=LVIF_TEXT;	LvItem.iSubItem=0;	LvItem.pszText=Text;
	LvItem.cchTextMax=256;	LvItem.iItem=iSlected;
	SendMessage(get_control(ID_LOL),LVM_GETITEMTEXT, iSlected, (LPARAM)&LvItem);
	g_injection->clickobject(Text); 
	Beep(440,10);
	return TRUE;
  }
 return FALSE;
}