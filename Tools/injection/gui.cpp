////////////////////////////////////////////////////////////////////////////////
//
// gui.cpp
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
//  This file contains code for the GUI wrapper classes
//
////////////////////////////////////////////////////////////////////////////////

#include "gui.h"

#include <assert.h>

#include <commctrl.h>
#include "common.h"


HINSTANCE g_hinstance = 0;

////////////////////////////////////////////////////////////////////////////////

//// Methods of Window:

Window::~Window()
{
    // *****
    // The window *MUST* be destroy()ed before being deleted because if
    // destroy() were called here, event_destroy() would die because the
    // derived class has already been deallocated.
    // *****
    assert(m_hwnd == 0);
}

void Window::destroy()
{
    if(m_hwnd != 0)
    {
        // WM_NCDESTROY handler sets m_hwnd = 0
        DestroyWindow(m_hwnd);
    }
}

HWND Window::get_control(int id)
{
    return GetDlgItem(m_hwnd, id);
}

void Window::enable_control(int id, bool enable)
{
    HWND hwnd = GetDlgItem(m_hwnd, id);
    if(hwnd != 0)
        EnableWindow(hwnd, enable);
}

void Window::error_box(const char * text)
{
    if(m_hwnd == 0)
        OutputDebugString("Warning: m_hwnd == 0 in error_box\n");
    int r = MessageBox(m_hwnd, text, "Error", MB_OK | MB_ICONEXCLAMATION);
    if(r == 0)
        OutputDebugString("MessageBox() failed.\n");
}

LRESULT Window::send_message(UINT msg, WPARAM wparam, LPARAM lparam)
{
    return SendMessage(m_hwnd, msg, wparam, lparam);
}

void Window::set_rect(const RECT & rect)
{
    MoveWindow(m_hwnd, rect.left, rect.top,
        rect.right - rect.left, rect.bottom - rect.top, FALSE);
}

void Window::set_size(int width, int height)
{
    SetWindowPos(m_hwnd, 0, 0, 0, width, height, SWP_NOMOVE | SWP_NOZORDER);
}

void Window::show(bool bshow)
{
    if(bshow)
        ShowWindow(m_hwnd, SW_SHOW);
    else
        ShowWindow(m_hwnd, SW_HIDE);
}

////////////////////////////////////////////////////////////////////////////////

//// Methods of NormalWindow:

// static, private
ATOM NormalWindow::m_window_class = 0;

// static, private
LRESULT CALLBACK NormalWindow::window_proc(HWND hwnd, UINT msg, WPARAM wparam,
    LPARAM lparam)
{
    NormalWindow * obj;
    if(msg == WM_CREATE)
    {
        CREATESTRUCT * cs = reinterpret_cast<CREATESTRUCT *>(lparam);
        obj = reinterpret_cast<NormalWindow *>(cs->lpCreateParams);
        assert(obj != 0);
        // Associate the object with the dialog for future message handling.
        SetWindowLong(hwnd, GWL_USERDATA, reinterpret_cast<LPARAM>(obj));
        obj->m_hwnd = hwnd;
        if(!obj->event_create())
            return -1;
        return 0;
    }
    else
    {
        // Find the object associated with this dialog.
        obj = reinterpret_cast<NormalWindow *>(GetWindowLong(hwnd,
            GWL_USERDATA));
        // Some messages arrive before WM_CREATE.
        if(obj == 0)
            return DefWindowProc(hwnd, msg, wparam, lparam);
        switch(msg)
        {
        case WM_COMMAND:
            return obj->event_command(LOWORD(wparam), HIWORD(wparam));
        case WM_NOTIFY:
            return obj->event_notify(wparam,
                reinterpret_cast<NMHDR *>(lparam));
        case WM_DESTROY:
            obj->event_destroy();
            return 0;
        case WM_NCDESTROY:
            obj->m_hwnd = 0;
            obj->event_delete();    // possibly deletes 'this'
            return 0;
        }
    }
    return DefWindowProc(hwnd, msg, wparam, lparam);
}

// static, private
bool NormalWindow::register_class()
{
    if(m_window_class == 0)
    {
        WNDCLASSEX wcx;
        wcx.cbSize = sizeof(wcx);
        wcx.style = CS_HREDRAW | CS_VREDRAW | CS_NOCLOSE;
        wcx.lpfnWndProc = window_proc;
        wcx.cbClsExtra = 0;
        wcx.cbWndExtra = 0;
        wcx.hInstance = g_hinstance;
        wcx.hIcon = LoadIcon(NULL, IDI_APPLICATION);
        wcx.hCursor = LoadCursor(NULL, IDC_ARROW);
        wcx.hbrBackground = reinterpret_cast<HBRUSH>(COLOR_BTNFACE + 1);
        wcx.lpszMenuName = NULL;
        wcx.lpszClassName = "InjNormalWindow";
        wcx.hIconSm = NULL;
        m_window_class = RegisterClassEx(&wcx);
        return m_window_class != 0;
    }
    return true;
}

bool NormalWindow::create(HWND parent)
{
    assert(m_hwnd == 0);

    if(!register_class())
        return false;
    m_hwnd = CreateWindowEx(WS_EX_WINDOWEDGE | WS_EX_CONTROLPARENT,
        reinterpret_cast<LPCTSTR>(m_window_class),
        "Injection", WS_OVERLAPPED | WS_DLGFRAME | WS_SYSMENU |
        WS_MINIMIZEBOX | WS_CLIPCHILDREN, CW_USEDEFAULT, CW_USEDEFAULT,
        CW_USEDEFAULT, CW_USEDEFAULT, parent, NULL, g_hinstance,
        reinterpret_cast<LPVOID>(this));
    if(m_hwnd == 0)
        OutputDebugString("CreateWindowEx failed.\n");
    return m_hwnd != 0;
}

////////////////////////////////////////////////////////////////////////////////

//// Methods of ModelessDialog:

// static, private
BOOL CALLBACK ModelessDialog::dialog_proc(HWND hwnd, UINT msg, WPARAM wparam,
    LPARAM lparam)
{
    ModelessDialog * obj;
    if(msg == WM_INITDIALOG)
    {
        // For this message only, the object pointer is a parameter.
        obj = reinterpret_cast<ModelessDialog *>(lparam);
        assert(obj != 0);
        // Associate the object with the dialog for future message handling.
        SetWindowLong(hwnd, GWL_USERDATA, lparam);
        obj->m_hwnd = hwnd;
        obj->event_init_dialog();
        return TRUE;
    }
    else
    {
        // Find the object associated with this dialog.
        obj = reinterpret_cast<ModelessDialog *>(GetWindowLong(hwnd,
            GWL_USERDATA));
        // Messages might arrive before WM_INITDIALOG :(
        if(obj == 0)
            return FALSE;
        switch(msg)
        {
        case WM_COMMAND:
            return obj->event_command(LOWORD(wparam), HIWORD(wparam));
        case WM_NOTIFY:
            return obj->event_notify(wparam,
                reinterpret_cast<NMHDR *>(lparam));
        case WM_DESTROY:
            obj->event_destroy();   // resets hwnd
            return TRUE;
        case WM_NCDESTROY:
            obj->m_hwnd = 0;
            obj->event_delete();
            return TRUE;
        }
    }
    return FALSE;
}

bool ModelessDialog::create(HWND parent)
{
    assert(m_hwnd == 0);
	//trace_printf("ModelessDialog::create\n");
    m_hwnd = CreateDialogParam(g_hinstance,
        MAKEINTRESOURCE(m_id), parent, dialog_proc,
        reinterpret_cast<LPARAM>(this));
    if(m_hwnd == 0)
    {
        char Buff[128];
        wsprintf(Buff,"CreateDialogParam failed. Error=%08X\n",GetLastError());
        OutputDebugString(Buff);
    }
    return m_hwnd != 0;
}

////////////////////////////////////////////////////////////////////////////////

//// Methods of ModalDialog:

// static, private
BOOL CALLBACK ModalDialog::dialog_proc(HWND hwnd, UINT msg, WPARAM wparam,
    LPARAM lparam)
{
    ModalDialog * obj;
    if(msg == WM_INITDIALOG)
    {
        // For this message only, the object pointer is a parameter.
        obj = reinterpret_cast<ModalDialog *>(lparam);
        assert(obj != 0);
        // Associate the object with the dialog for future message handling.
        SetWindowLong(hwnd, GWL_USERDATA, lparam);
        obj->m_hwnd = hwnd;
        obj->event_init_dialog();
        return TRUE;
    }
    else
    {
        // Find the object associated with this dialog.
        obj = reinterpret_cast<ModalDialog *>(GetWindowLong(hwnd,
            GWL_USERDATA));
        // Messages might arrive before WM_INITDIALOG :(
        if(obj == 0)
            return FALSE;
        switch(msg)
        {
        case WM_COMMAND:
            return obj->event_command(LOWORD(wparam), HIWORD(wparam));
        case WM_NOTIFY:
            return obj->event_notify(wparam,
                reinterpret_cast<NMHDR *>(lparam));
        case WM_TIMER:
            obj->event_timer();
            return TRUE;
        case WM_DESTROY:
            obj->event_destroy();   // resets hwnd
            return TRUE;
        case WM_NCDESTROY:
            obj->m_hwnd = 0;
            obj->event_delete();
            return TRUE;
        default:
            if(msg >= WM_USER)
            {
                obj->event_user(msg, wparam, lparam);
                return TRUE;
            }
        }
    }
    return FALSE;
}

ModalDialog::ModalDialog(int id)
: m_hwnd(0), m_parent_hwnd(0), m_id(id)
{
}

void ModalDialog::end(int result)
{
    EndDialog(m_hwnd, result);
}

int ModalDialog::open(HWND parent)
{
    m_parent_hwnd = parent;
    return DialogBoxParam(g_hinstance, MAKEINTRESOURCE(m_id), m_parent_hwnd,
        dialog_proc, reinterpret_cast<LPARAM>(this));
}

////////////////////////////////////////////////////////////////////////////////

EditBoxWrapper::EditBoxWrapper(HWND hdlg, int id)
{
    m_hwnd = GetDlgItem(hdlg, id);
    assert(m_hwnd != 0);
}

LRESULT EditBoxWrapper::get_line_length(int index)
{
    return SendMessage(m_hwnd, EM_LINELENGTH, index, 0);
}

void EditBoxWrapper::get_text(string & str)
{
    LRESULT len = get_line_length(0);
    if(len < 1) // need room for a WORD (16 bits)
        len = 2;
    char * buf = new char[len + 1];
    // Store the size of the buffer in the buffer itself.
    *reinterpret_cast<LPWORD>(buf) = len;
    LRESULT r = SendMessage(m_hwnd, EM_GETLINE, 0,
        reinterpret_cast<LPARAM>(buf));
    if(r == 0)
        str = "";
    else
    {
        buf[r] = '\0';  // null terminate
        str = buf;
    }
    delete /*[]*/ buf;
}

void EditBoxWrapper::set_text(const char * str)
{
    SetWindowText(m_hwnd, str);
}

////////////////////////////////////////////////////////////////////////////////

ComboBoxWrapper::ComboBoxWrapper(HWND hdlg, int id)
{
    m_hwnd = GetDlgItem(hdlg, id);
    assert(m_hwnd != 0);
}

int ComboBoxWrapper::add_string(const char * str)
{
    LRESULT r = SendMessage(m_hwnd, CB_ADDSTRING, 0,
        reinterpret_cast<LPARAM>(str));
    if(r == CB_ERR || r == CB_ERRSPACE)
        return -1;
    return r;
}

int ComboBoxWrapper::get_current_selection()
{
    LRESULT r = SendMessage(m_hwnd, CB_GETCURSEL, 0, 0);
    if(r == CB_ERR)
        return -1;
    return r;
}

LRESULT ComboBoxWrapper::set_current_selection(int index)
{
    return SendMessage(m_hwnd, CB_SETCURSEL, index, 0);
}

void ComboBoxWrapper::clear()
{
    SendMessage(m_hwnd, CB_RESETCONTENT, 0, 0);
}

////////////////////////////////////////////////////////////////////////////////

ListBoxWrapper::ListBoxWrapper(HWND hdlg, int id)
{
    m_hwnd = GetDlgItem(hdlg, id);
    assert(m_hwnd != 0);
}

int ListBoxWrapper::add_string(const char * str)
{
    LRESULT r = SendMessage(m_hwnd, LB_ADDSTRING, 0,
        reinterpret_cast<LPARAM>(str));
    if(r == CB_ERR || r == CB_ERRSPACE)
        return -1;
    return r;
}

LRESULT ListBoxWrapper::delete_string(int index)
{
    return SendMessage(m_hwnd, LB_DELETESTRING, index, 0);
}

int ListBoxWrapper::get_current_selection()
{
    LRESULT r = SendMessage(m_hwnd, LB_GETCURSEL, 0, 0);
    if(r == LB_ERR)
        return -1;
    return r;
}

LRESULT ListBoxWrapper::set_current_selection(int index)
{
    return SendMessage(m_hwnd, LB_SETCURSEL, index, 0);
}

bool ListBoxWrapper::set_item_data(int index, LPARAM data)
{
    return SendMessage(m_hwnd, LB_SETITEMDATA, index, data) != LB_ERR;
}

LPARAM ListBoxWrapper::get_item_data(int index)
{
    return SendMessage(m_hwnd, LB_GETITEMDATA, index, 0);
}

bool ListBoxWrapper::get_string(int index, string & str)
{
    LRESULT len = SendMessage(m_hwnd, LB_GETTEXTLEN, index, 0);
    if(len == LB_ERR)
        return false;
    char * buf = new char[len + 1];
    LRESULT r = SendMessage(m_hwnd, LB_GETTEXT, index,
        reinterpret_cast<LPARAM>(buf));
    if(r == LB_ERR)
    {
        delete /*[]*/ buf;
        return false;
    }
    str = buf;
    delete /*[]*/ buf;
    return true;
}

bool ListBoxWrapper::get_selected_string(string & str)
{
    int index = get_current_selection();
    if(index == -1)
        return false;
    return get_string(index, str);
}

bool ListBoxWrapper::insert_string(int index, const char * str)
{
    LRESULT r = SendMessage(m_hwnd, LB_INSERTSTRING, index,
        reinterpret_cast<LPARAM>(str));
    return r != LB_ERR && r != LB_ERRSPACE;
}

bool ListBoxWrapper::set_string(int index, const char * str)
{
    if(delete_string(index) == LB_ERR)
        return false;
    return insert_string(index, str);
}

void ListBoxWrapper::clear()
{
    SendMessage(m_hwnd, LB_RESETCONTENT, 0, 0);
}

////////////////////////////////////////////////////////////////////////////////

ButtonWrapper::ButtonWrapper(HWND hdlg, int id)
{
    m_hwnd = GetDlgItem(hdlg, id);
    assert(m_hwnd != 0);
}

bool ButtonWrapper::get_check()
{
    return SendMessage(m_hwnd, BM_GETCHECK, 0, 0) == BST_CHECKED;
}

void ButtonWrapper::set_check(bool check)
{
    SendMessage(m_hwnd, BM_SETCHECK, check ? BST_CHECKED : BST_UNCHECKED, 0);
}

////////////////////////////////////////////////////////////////////////////////

//// Methods of DialogTabControl:

DialogTabControl::DialogTabControl()
: Window(), m_index(0), m_current_page(0)
{
    m_child_rect.left = m_child_rect.top = 0;
    m_child_rect.right = m_child_rect.bottom = 0;
}

bool DialogTabControl::create(HWND parent)
{
    assert(parent != 0);

    m_hwnd = ::CreateWindowEx(0, WC_TABCONTROL,
        "InjTC", WS_CHILD | WS_VISIBLE | WS_CLIPSIBLINGS | TCS_MULTILINE,
        0, 0, 180, 180, parent, NULL, g_hinstance, 0);
    if(m_hwnd == 0)
        OutputDebugString("TabCtrl CreateWindowEx failed.\n");
    return m_hwnd != 0;
}

void DialogTabControl::destroy()
{
    Window::destroy();
    m_hwnd = 0;
}

void DialogTabControl::add(ModelessDialog & dialog, const char * label)
{
    // Recalculate the maximum child dialog size.
    RECT dialog_rect, child_rect;
    GetClientRect(dialog.get_hwnd(), &dialog_rect);
    CopyRect(&child_rect, &m_child_rect);
    UnionRect(&m_child_rect, &child_rect, &dialog_rect);

    // Add a tab.
    TCITEM item;
    item.mask = TCIF_TEXT | TCIF_PARAM;
    item.pszText = const_cast<char *>(label);
    item.lParam = reinterpret_cast<LPARAM>(&dialog);
    TabCtrl_InsertItem(m_hwnd, m_index, &item);
	trace_printf("Tab #%i added: %s\n",m_index,label);
    m_index++;
}

void DialogTabControl::get_desired_rect(RECT & rect)
{
    CopyRect(&rect, &m_child_rect);
    TabCtrl_AdjustRect(m_hwnd, TRUE, &rect);
}

void DialogTabControl::get_display_rect(RECT & tc_rect)
{
    TabCtrl_AdjustRect(m_hwnd, FALSE, &tc_rect);
}

int DialogTabControl::get_current_selection()
{
    return TabCtrl_GetCurSel(m_hwnd);
}

void DialogTabControl::notify_sel_change()
{
    if(m_current_page != 0)
        m_current_page->show(false);
    int index = get_current_selection();
    TCITEM item;
    item.mask = TCIF_PARAM;
    TabCtrl_GetItem(m_hwnd, index, &item);
    m_current_page = reinterpret_cast<ModelessDialog *>(item.lParam);
	trace_printf("nowtab=%i\tpage %p\n",index,m_current_page);
    assert(m_current_page != 0);
    m_current_page->show();
}


