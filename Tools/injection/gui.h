////////////////////////////////////////////////////////////////////////////////
//
// gui.h
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
//  Declarations for Windows GUI wrapper classes
//
////////////////////////////////////////////////////////////////////////////////


#ifndef _GUI_H_
#define _GUI_H_

#define WIN32_LEAN_AND_MEAN
#include <windows.h>

#include <string>
using std::string;

extern HINSTANCE g_hinstance;

class Window
{
protected:

public:
    HWND m_hwnd;
    Window() : m_hwnd(0) { }
    virtual ~Window();

    // Returns false for failure
    virtual bool create(HWND parent = 0) = 0;
    virtual void destroy();

    HWND get_hwnd() { return m_hwnd; }
    HWND get_control(int id);
    void enable_control(int id, bool enable = true);

    void error_box(const char * text);

    LRESULT send_message(UINT msg, WPARAM wparam, LPARAM lparam);
    void set_rect(const RECT & rect);
    void set_size(int width, int height);
    void show(bool bshow = true);
};

// This class used for windows other than dialog boxes.
class NormalWindow : public Window
{
private:
    static ATOM m_window_class;

    static LRESULT CALLBACK window_proc(HWND hwnd, UINT msg, WPARAM wparam,
        LPARAM lparam);
    static bool register_class();

protected:
    // This method must return true to allow window creation.
    virtual bool event_create() { return true; }
    virtual LRESULT event_command(int /*control_id*/, int /*notify_code*/)
    { return 1; }
    virtual LRESULT event_notify(int /*control_id*/, NMHDR * /*hdr*/)
    { return 0; }
    virtual void event_destroy() { }
    virtual void event_delete() { }

public:
    NormalWindow() : Window() { }

    // Returns false for failure
    virtual bool create(HWND parent);
};

class ModelessDialog : public Window
{
private:
    static BOOL CALLBACK dialog_proc(HWND hwnd, UINT msg, WPARAM wparam,
        LPARAM lparam);

protected:
    int m_id;

    virtual void event_init_dialog() { }
    virtual BOOL event_command(int /*control_id*/, int /*notify_code*/)
    { return FALSE; }
    virtual BOOL event_notify(int /*control_id*/, NMHDR * /*hdr*/)
    { return FALSE; }
    virtual void event_destroy() { }
    virtual void event_delete() { }

public:
    ModelessDialog(int id) : Window(), m_id(id) { }

    // Returns false for failure
    virtual bool create(HWND parent = 0);
};

class ModalDialog
{
private:
    static BOOL CALLBACK dialog_proc(HWND hwnd, UINT msg, WPARAM wparam,
        LPARAM lparam);

protected:
    HWND m_hwnd, m_parent_hwnd;
    int m_id;

    virtual void event_init_dialog() { }
    virtual BOOL event_command(int /*control_id*/, int /*notify_code*/)
    { return FALSE; }
    virtual BOOL event_notify(int /*control_id*/, NMHDR * /*hdr*/)
    { return FALSE; }
    virtual void event_timer() { }
    virtual void event_user(int /*msg*/, WPARAM /*wparam*/, LPARAM /*lparam*/)
    { }
    virtual void event_destroy() { }
    virtual void event_delete() { }

    void end(int result);

public:
    ModalDialog(int id);

    // Returns -1 for failure
    int open(HWND parent);
};

////////////////////////////////////////////////////////////////////////////////

class EditBoxWrapper
{
private:
    HWND m_hwnd;

public:
    EditBoxWrapper(HWND hdlg, int id);

    LRESULT get_line_length(int index);
    // For single line controls:
    void get_text(string & str);
    void set_text(const char * str);
};

class ComboBoxWrapper
{
private:
    HWND m_hwnd;

public:
    ComboBoxWrapper(HWND hdlg, int id);

    // Returns the index of the string, or -1 for error.
    int add_string(const char * str);
    // Returns -1 for error.
    int get_current_selection();
    LRESULT set_current_selection(int index);
    void clear();
};

class ListBoxWrapper
{
private:
    HWND m_hwnd;

public:
    ListBoxWrapper(HWND hdlg, int id);

    // Returns the index of the string, or -1 for error.
    int add_string(const char * str);
    LRESULT delete_string(int index);
    int get_current_selection();
    LRESULT set_current_selection(int index);
    bool set_item_data(int index, LPARAM data);
    LPARAM get_item_data(int index);
    // Returns false for failure
    bool get_string(int index, string & str);
    bool get_selected_string(string & str);
    bool insert_string(int index, const char * str);
    // WARNING: set_string() resets the item data and selection status
    bool set_string(int index, const char * str);
    // Remove all items.
    void clear();
};

class ButtonWrapper
{
private:
    HWND m_hwnd;

public:
    ButtonWrapper(HWND hdlg, int id);

    bool get_check();
    void set_check(bool check);
};

class DialogTabControl : public Window
{
private:
    RECT m_child_rect;
    int m_index;
    ModelessDialog * m_current_page;

public:
    DialogTabControl();

    virtual bool create(HWND parent);
    virtual void destroy();

    void add(ModelessDialog & dialog, const char * label);
    void get_desired_rect(RECT & rect);
    void get_display_rect(RECT & tc_rect);
    int get_current_selection();

    void notify_sel_change();
};

#endif

