////////////////////////////////////////////////////////////////////////////////
//
// Hooks.cpp
//
// Copyright (C) 2001 Luke 'Infidel' Dunstan
//
// Based on Sniffy:
// Copyright (C) 2000 Bruno 'Beosil' Heidelberger
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
//  These are the socket hook functions
//
////////////////////////////////////////////////////////////////////////////////

#define WIN32_LEAN_AND_MEAN
#include <windows.h>

#include "common.h"
#include "hooks.h"

bool g_FixUnicodeCaption=false;

////////////////////////////////////////////////////////////////////////////////

Buffer::Buffer()
: m_alloc(0), m_ptr(0), m_size(0), m_ref_count(0)
{
    warning_printf("Buffer default constructor called\n");
}

Buffer::Buffer(char * data, int size)
: m_alloc(data), m_ptr(data), m_alloc_size(size), m_size(size)
{
    ASSERT(data != 0);
    ASSERT(size != 0);
    m_ref_count = new int;
    *m_ref_count = 1;
}

Buffer::Buffer(const Buffer & other)
: m_alloc(other.m_alloc), m_ptr(other.m_ptr),
  m_alloc_size(other.m_alloc_size), m_size(other.m_size),
  m_ref_count(other.m_ref_count)
{
    if(&other == this)
        warning_printf("Buffer [%p] copy constructed from self\n",
            this);
    else
    {
        ASSERT(m_ref_count != 0);
        ASSERT(*m_ref_count > 0);
        (*m_ref_count)++;
    }
}

Buffer::~Buffer()
{
    if(m_ref_count == 0)
        warning_printf("buffer [%p] deleted with m_ref_count==0\n",
            this);
    else
    {
        ASSERT(*m_ref_count > 0);
        (*m_ref_count)--;
        if(*m_ref_count == 0)
        {
            delete /*[]*/ m_alloc;
            delete m_ref_count;
        }
    }
}

int Buffer::get(char * dest, int dest_size, Copier & copier)
{
    ASSERT(m_size > 0);
    int in_bytes = m_size, out_bytes = dest_size;

    copier(dest, m_ptr, out_bytes, in_bytes);
    if(in_bytes != m_size)
        warning_printf("destination full in Buffer::get\n");
    m_size -= in_bytes;
    m_ptr += in_bytes;
    return out_bytes;
}

////////////////////////////////////////////////////////////////////////////////

int BufferQueue::get(char * dest, int dest_size, Copier & copier)
{
    int dest_space = dest_size;

    while(!q.empty() && dest_space > 0)
    {
        Buffer & buffer = q.front();
        int moved = buffer.get(dest, dest_space, copier);
        dest += moved;
        dest_space -= moved;
        if(buffer.is_empty())
            q.pop();
    }
    trace_printf("Dequeued %d bytes\n", dest_size - dest_space);
    return dest_size - dest_space;
}

void BufferQueue::push_copy(char * buf, int size)
{
    char * newbuf = new char[size];
    memcpy(newbuf, buf, size);
    // The Buffer object will take ownership of the memory.
    q.push(Buffer(newbuf, size));
}

void BufferQueue::push_move(char * buf, int size)
{
    q.push(Buffer(buf, size));
}

////////////////////////////////////////////////////////////////////////////////

class MessageFragment
{
private:
    uint8 * m_buf;
    int m_size, m_msg_size;

public:
    // The constructor allocates memory for a copy of the given buffer.
    // msg_size should be zero if the fragment is not large enough to
    // determine the actual message size.
    MessageFragment(uint8 * buf, int size, int msg_size);
    ~MessageFragment()
    {
        delete /*[]*/ m_buf;
    }

    // Extracts data from the beginning of the given buffer on the fragment
    // buffer. Returns the number of bytes extracted.
    int append(uint8 * buf, int size);
    bool is_complete() const
    { return m_size == m_msg_size && m_msg_size != 0; }
    uint8 * get_buf() { return m_buf; }
    int get_size() { return m_size; }
};

MessageFragment::MessageFragment(uint8 * buf, int size, int msg_size)
: m_size(size), m_msg_size(msg_size)
{
    if(m_msg_size == 0)
        m_buf = new uint8[3];
    else
        m_buf = new uint8[msg_size];
    memcpy(m_buf, buf, m_size);
}

int MessageFragment::append(uint8 * buf, int size)
{
    if(m_msg_size == 0) // Size is not yet known
    {
        // m_size must be 1 or 2
        if(m_size + size < 3)  // size still not known (size == m_size == 1)
        {
            // Concatenate the new data onto the fragment
            memcpy(m_buf + m_size, buf, size);
            m_size += size;
            return size;
        }
        if(m_size == 2)
            m_msg_size = (m_buf[1] << 8) | buf[0];
        else // m_size == 1
            m_msg_size = (buf[0] << 8) | buf[1];
        // Now that the size is known, make the buffer big enough for the
        // whole message.
        uint8 * newbuf = new uint8[m_msg_size];
        // Move the existing data into the new buffer.
        memcpy(newbuf, m_buf, m_size);
        delete /*[]*/ m_buf;
        m_buf = newbuf;
    }
    int append_size = m_msg_size - m_size;
    if(append_size > size)
        append_size = size;
    // Concatenate the new data onto the fragment
    memcpy(m_buf + m_size, buf, append_size);
    m_size += append_size;
    return append_size;
}

////////////////////////////////////////////////////////////////////////////////

SocketHook::SocketHook(HookCallbackInterface & callback, SOCKET s)
: m_callback(callback), m_s(s), m_disconnected(false), m_recv_error(false),
  m_compressed(false), m_first_send(true),
  m_send_fragment(0), m_receive_fragment(0), m_game_crypt(0)
{
    // With a fairly large decompression buffer it is unlikely to require
    // reallocation.
    m_dec_buf_size = sizeof(m_recv_buf) * 2;
    m_dec_buf = new char[m_dec_buf_size];
    m_send_buf_size = sizeof(m_recv_buf);
    m_send_buf = new uint8[m_send_buf_size];
}

SocketHook::~SocketHook()
{
    m_callback.disconnected(this);
    if(m_send_fragment)
        delete /*[]*/ m_send_fragment;
    if(m_receive_fragment)
        delete /*[]*/ m_receive_fragment;

    delete /*[]*/ m_dec_buf;
    delete m_send_buf;
    if(m_game_crypt)
        delete m_game_crypt;
}

// private
void SocketHook::alloc_dec_buf(int size)
{
    if(m_dec_buf_size < size)
    {
        delete /*[]*/ m_dec_buf;
        m_dec_buf = new char[size];
        m_dec_buf_size = size;
    }
}

// private
void SocketHook::alloc_send_buf(int size)
{
    if(m_send_buf_size < size)
    {
        delete /*[]*/ m_send_buf;
        m_send_buf = new uint8[size];
        m_send_buf_size = size;
    }
}

void SocketHook::recv_ready()
{
    int n = ::recv(m_s, m_recv_buf, sizeof(m_recv_buf), 0);

    if(n == SOCKET_ERROR)
    {
        m_recv_error = true;
        m_last_error = WSAGetLastError();
        error_printf("recv failed: %d\n", m_last_error);
    }
    else if(n == 0)
    {
        m_disconnected = true;
        trace_printf(">> Disconnected\n");
    }
    else    // got some data
    {
        trace_printf(">> recv() got %d bytes\n", n);
        if(m_compressed)
        {
            //trace_printf("Compressed:\n");
            //trace_dump(reinterpret_cast<uint8 *>(m_recv_buf), n);
            // NOTE: the shortest bit string in the Huffman tree is 2 bits,
            // so decompression will output a maximum of 4 times the size
            // of the input.
            alloc_dec_buf(n * 4 + 2);
            int in_bytes = n, out_bytes = m_dec_buf_size;

            if(m_game_crypt)
	            m_game_crypt->decrypt((uint8*)m_recv_buf,(uint8*)m_recv_buf,in_bytes);

            m_decompressor(m_dec_buf, m_recv_buf, out_bytes, in_bytes);
            if(in_bytes != n)   // Shouldn't happen
            {
                error_printf("decompression buffer too small\n");
                m_recv_error = true;
                m_last_error = WSAECONNRESET;
                return;
            }
            trace_printf(">> decompressed into %d bytes\n", out_bytes);
            handle_receive_data(m_dec_buf, out_bytes);
        }
        else
            handle_receive_data(m_recv_buf, n);
    }
}

bool SocketHook::is_ready() const
{
    return m_disconnected || m_recv_error || !m_receive_queue.is_empty();
}

int SocketHook::close()
{
    return ::closesocket(m_s);
}

int SocketHook::send(char * buf, int size)
{
    uint8 * ptr = reinterpret_cast<uint8 *>(buf);
    // First send is the special 4 byte key, which is not a message.
    if(m_first_send)
    {
        m_first_send = false;
        m_crypt_mode = CRYPT_NONE;
        trace_printf("First message:\n");
        if(size != 4)
            trace_printf("Warning: key size not 4\n");
        trace_dump(ptr, size);
        memcpy(m_key, buf, 4);  // Remember key
        m_callback.handle_key(this, m_key);
        return ::send(m_s, reinterpret_cast<char *>(m_key), size, 0);
    }
    uint8 * end = ptr + size;
    int total_sent = 0;

    // Last send ended in a partial message
    if(m_send_fragment != 0)
    {
        ptr += m_send_fragment->append(ptr, size);
        // If still not a complete message, nothing else to do.
        if(!m_send_fragment->is_complete())
            return size;
        // Analyse and send message
        if(m_callback.handle_send_message(this, m_send_fragment->get_buf(),
                m_send_fragment->get_size()))
        {
            int ret = send_server(m_send_fragment->get_buf(),
                m_send_fragment->get_size());
            if(ret == SOCKET_ERROR)
            {
                delete /*[]*/ m_send_fragment;
                return SOCKET_ERROR;
            }
            total_sent += ret;
        }
        else
            total_sent += m_send_fragment->get_size();  // required
        delete /*[]*/ m_send_fragment;
        m_send_fragment = 0;
    }

    while(ptr < end)
    {
        int msg_size = m_callback.get_message_size(*ptr);
        // Determine message boundaries.
        if(msg_size == -1)
        {
            error_printf("Length unknown for message 0x%02X\n", *ptr);
            msg_size = 1;
        }
        else
        {
            // The message code might be the last byte of the buffer
            if(msg_size == 0 && ptr + 3 <= end)
                msg_size = (ptr[1] << 8) | ptr[2];
        }
        // If data ends in an incomplete message, buffer it.
        if(msg_size == 0 || ptr + msg_size > end)
        {
            trace_printf("send: message fragment code 0x%02X: got %d of %d bytes\n",
                *ptr, end - ptr, msg_size);
            m_send_fragment = new MessageFragment(ptr, end - ptr, msg_size);
            ptr = end;
        }
        else
        {
            // For each complete message, send() it
            if(m_callback.handle_send_message(this, ptr, msg_size))
            {
                int ret = send_server(ptr, msg_size);
                if(ret == SOCKET_ERROR)
                    return SOCKET_ERROR;
                total_sent += ret;
            }
            else
                total_sent += msg_size;     // otherwise client sends again
            ptr += msg_size;
        }
    }

    return total_sent;
}

int SocketHook::recv(char *buf, int len)
{
    if(m_disconnected)
        return 0;
    if(m_recv_error)
    {
        WSASetLastError(m_last_error);
        return SOCKET_ERROR;
    }
    if(m_compressed)
    {
        int n = m_receive_queue.get(buf, len, m_compressor);
        int out_bytes = len - n;
        m_compressor.flush(buf + n, out_bytes);
        //trace_printf("Recompressed:\n");
        //trace_dump(reinterpret_cast<uint8 *>(buf), n + out_bytes);
        return n + out_bytes;
    }
    return m_receive_queue.get(buf, len, m_copier);
}

void SocketHook::handle_receive_data(char * buf, int size)
{
    // If the received data is nothing but a compressor flush code, it may
    // become nothing when decompressed.
    if(size == 0)
        return;

    uint8 * ptr = reinterpret_cast<uint8 *>(buf);
    uint8 * end = ptr + size;

    // Last receive ended in a partial message
    if(m_receive_fragment != 0)
    {
        ptr += m_receive_fragment->append(ptr, size);
        // If still not a complete message, nothing else to do.
        if(!m_receive_fragment->is_complete())
            return;
        // Analyse and queue message
        if(m_callback.handle_receive_message(this,
                m_receive_fragment->get_buf(), m_receive_fragment->get_size()))
            m_receive_queue.push_copy(m_receive_fragment->get_buf(),
                m_receive_fragment->get_size());
        delete m_receive_fragment;
        m_receive_fragment = 0;
    }

    while(ptr < end)
    {
        int msg_size = m_callback.get_message_size(*ptr);
        // Determine message boundaries.
        if(msg_size == -1)
        {
            error_printf("Length unknown for message 0x%02X\n", *ptr);
            msg_size = 1;
        }
        else
        {
            // The message code might be the last byte of the buffer
            if(msg_size == 0 && ptr + 3 <= end)
                msg_size = (ptr[1] << 8) | ptr[2];
        }

        // If data ends in an incomplete message, buffer it.
        if(msg_size == 0 || ptr + msg_size > end)
        {
            trace_printf("recv: message fragment code 0x%02X: got %d of %d bytes\n",
                *ptr, end - ptr, msg_size);
            m_receive_fragment = new MessageFragment(ptr, end - ptr, msg_size);
            ptr = end;
        }
        else
        {
            if(m_callback.handle_receive_message(this, ptr, msg_size))
                // Enqueue message.
                m_receive_queue.push_copy(ptr, msg_size);
            ptr += msg_size;
        }
    }   // while(ptr < end)
}

int SocketHook::send_server(uint8 * buf, int size)
{
    if(m_crypt_mode == CRYPT_LOGIN)
    {
        alloc_send_buf(size);
        m_login_crypt.encrypt(buf, m_send_buf, size);
        buf = m_send_buf;
    }
    else if(m_crypt_mode == CRYPT_GAME)
    {
        ASSERT(m_game_crypt != 0);

        alloc_send_buf(size);
        m_game_crypt->encrypt(buf, m_send_buf, size);
        buf = m_send_buf;
		///zorm203start
		if (Encrypt203 == 1) {  
			alloc_send_buf(size);
			m_game_cryptnew->encrypt(buf, m_send_buf, size);
			buf = m_send_buf;
		}
		///zorm203end

    }

    int ret = ::send(m_s, reinterpret_cast<char *>(buf), size, 0);
    if(ret == SOCKET_ERROR)
        warning_printf("send() returned SOCKET_ERROR\n");
    else if(ret != size)
    {
        warning_printf("send(,,%d) => %d\n", size, ret);
        ret = size;
    }
    return ret;
}

void SocketHook::send_client(uint8 * buf, int size)
{
    m_receive_queue.push_copy(buf, size);
}

void SocketHook::set_login_encryption(uint32 k1, uint32 k2)
{
    m_crypt_mode = CRYPT_LOGIN;
    m_login_crypt.init(m_key, k1, k2);
}

void SocketHook::set_game_encryption(int version)
{
    m_crypt_mode = CRYPT_GAME;

    if(m_game_crypt)
        warning_printf("set_game_encryption() - encryption already set!\n");

//    if((version==ENCRYPTION_3_0_5) || (version==ENCRYPTION_3_0_6j))
//        m_game_crypt = new NewGameCrypt(m_key);
//    else
//        m_game_crypt = new OldGameCrypt();

		///zorm203start
    if((version==ENCRYPTION_3_0_5) || (version==ENCRYPTION_3_0_6j)
		||(version>=0 && EncryptStrs[version].type==eET_Two)) {
        m_game_crypt = new NewGameCrypt(m_key);
		Encrypt203 = 0;
    } else if(version==ENCRYPTION_2_0_3||(version>=0 && EncryptStrs[version].type==eET_203)) {
		m_game_cryptnew = new NewGameCrypt(m_key);
		m_game_crypt = new OldGameCrypt();
		m_game_cryptnew->init();
		Encrypt203 = 1;
	} else {
        m_game_crypt = new OldGameCrypt(); //eET_Blow
		Encrypt203 = 0;
	}
		///zorm203end

    m_game_crypt->init();
}

////////////////////////////////////////////////////////////////////////////////

SocketHookSet * SocketHookSet::m_instance = 0;

SocketHookSet::SocketHookSet(HookCallbackInterface & callback)
: m_callback(callback)
{
    ASSERT(m_instance == 0);    // only one instance is allowed

    m_instance = this;
    m_sockets[0] = m_sockets[1] = INVALID_SOCKET;
    m_hooks[0] = m_hooks[1] = 0;
}

SocketHookSet::~SocketHookSet()
{
    m_instance=0;
    delete m_hooks[0];
    delete m_hooks[1];
}

// private
SocketHook * SocketHookSet::find_hook(SOCKET s)
{
    if(s == INVALID_SOCKET)
        warning_printf("operation on INVALID_SOCKET\n");
    else if(s == m_sockets[0])
        return m_hooks[0];
    else if(s == m_sockets[1])
        return m_hooks[1];
    return 0;
}

// private
void SocketHookSet::check_ready(int index, fd_set * readfds, int & count)
{
    if(m_hooks[index] != 0)
    {
        // If data is available, receive and analyse it.
        if(FD_ISSET(m_sockets[index], readfds))
        {
            trace_printf("Data ready on socket %d\n", m_sockets[index]);
            m_hooks[index]->recv_ready();
            FD_CLR(m_sockets[index], readfds);
            count--;
            // DEBUG:
            if(!m_hooks[index]->is_ready())
                trace_printf("Buffer NOT ready on socket\n");
        }

        // If there is any data that the client should see, pretend that
        // the socket is ready.
        if(m_hooks[index]->is_ready())
        {
            trace_printf("Buffer ready on socket %d\n", m_sockets[index]);
            FD_SET(m_sockets[index], readfds);
            count++;
        }
    }
}

struct ImportEntryHook
{
    const char * name;
    int ordinal;
    void * hook_func;
};

static ImportEntryHook wsock32_hooks[] =
{
    { "closesocket", 3, SocketHookSet::hook_closesocket },
    { "connect", 4, SocketHookSet::hook_connect },
    { "recv", 16, SocketHookSet::hook_recv },
    { "select", 18, SocketHookSet::hook_select },
    { "send", 19, SocketHookSet::hook_send },
    { "socket", 23, SocketHookSet::hook_socket },
    { 0, 0, 0 }
};

// this should help with the problem with showing only one letter in UO caption
BOOL __stdcall MyIsWindowUnicode(
  HWND hWnd   // handle to window
)
{
    if(g_FixUnicodeCaption)
        return TRUE;
    else
        return IsWindowUnicode(hWnd);
}

static ImportEntryHook user32_hooks[] =
{
    { "IsWindowUnicode", 401, MyIsWindowUnicode },
    { 0, 0, 0 }
};

struct ImportDescriptorHook
{
    const char * name;
    ImportEntryHook * entries;
};

static ImportDescriptorHook desc_hooks[] =
{
    { "wsock32.dll", wsock32_hooks },
    { "user32.dll", user32_hooks },
    { 0, 0 }
};

void SocketHookSet::install()
{
    DWORD oldProtect;

    DWORD image_base = (DWORD)GetModuleHandle(NULL);
    IMAGE_DOS_HEADER *idh = (IMAGE_DOS_HEADER *)image_base;
    IMAGE_FILE_HEADER *ifh = (IMAGE_FILE_HEADER *)(image_base +
        idh->e_lfanew + sizeof(DWORD));
    IMAGE_OPTIONAL_HEADER *ioh = (IMAGE_OPTIONAL_HEADER *)((DWORD)(ifh) +
        sizeof(IMAGE_FILE_HEADER));
    IMAGE_IMPORT_DESCRIPTOR *iid = (IMAGE_IMPORT_DESCRIPTOR *)(image_base +
        ioh->DataDirectory[IMAGE_DIRECTORY_ENTRY_IMPORT].VirtualAddress);

    VirtualProtect((LPVOID)(image_base +
        ioh->DataDirectory[IMAGE_DIRECTORY_ENTRY_IAT].VirtualAddress),
        ioh->DataDirectory[IMAGE_DIRECTORY_ENTRY_IAT].Size, PAGE_READWRITE,
        &oldProtect);

    while(iid->Name)
    {
        for(ImportDescriptorHook * dhook = desc_hooks; dhook->name != 0; dhook++)
            if(stricmp(dhook->name, (char *)(image_base + iid->Name)) == 0)
            {
                //trace_printf("Found descriptor: %s\n", dhook->name);
                IMAGE_THUNK_DATA * pThunk = (IMAGE_THUNK_DATA *)
                    ((DWORD)iid->OriginalFirstThunk + image_base);
                IMAGE_THUNK_DATA * pThunk2 = (IMAGE_THUNK_DATA *)
                    ((DWORD)iid->FirstThunk + image_base);
                while(pThunk->u1.AddressOfData)
                {
                    char * name = 0;
                    int ordinal;
                    // Imported by ordinal only:
                    if(pThunk->u1.Ordinal & 0x80000000)
                        ordinal = pThunk->u1.Ordinal & 0xffff;
                    else    // Imported by name, with ordinal hint
                    {
                        IMAGE_IMPORT_BY_NAME * pname = (IMAGE_IMPORT_BY_NAME *)
                            ((DWORD)pThunk->u1.AddressOfData + image_base);
                        ordinal = pname->Hint;
                        name = (char *)pname->Name;
                    }
                    for(ImportEntryHook * ehook = dhook->entries; ehook->name != 0; ehook++)
                    {
                        if(name != 0 && strcmp(name, ehook->name) == 0)
                        {
                            //trace_printf("Found entry name: %s\n", ehook->name);
                            pThunk2->u1.Function = (PDWORD)ehook->hook_func;
                        }
                        else if(ordinal == ehook->ordinal)
                        {
                            //trace_printf("Found entry ordinal: %s\n", ehook->name);
                            pThunk2->u1.Function = (PDWORD)ehook->hook_func;
                        }
                    }
                    pThunk++;
                    pThunk2++;
                }
            }
        iid++;
    }
}

void SocketHookSet::add(SOCKET s, int af, int type, int protocol)
{
    if(af != AF_INET)
        error_printf("socket address family != AF_INET: %d\n", af);
    else if(type != SOCK_STREAM)
        error_printf("socket type != SOCK_STREAM: %d\n", type);
    // I thought it was meant to be IPPROTO_TCP but this is what the client
    // asks for...
    else if(protocol != IPPROTO_IP)
        error_printf("socket protocol != IPPROTO_IP: %d\n", protocol);
    else if(m_hooks[0] == 0)
    {
        m_sockets[0] = s;
        m_hooks[0] = new SocketHook(m_callback, s);
        trace_printf("First socket created\n");
    }
    else if(m_hooks[1] == 0)
    {
        m_sockets[1] = s;
        m_hooks[1] = new SocketHook(m_callback, s);
        trace_printf("Second socket created\n");
    }
    else
        warning_printf("third socket created\n");
}

void SocketHookSet::close(SOCKET s, int error)
{
    if(s == INVALID_SOCKET)
        warning_printf("closed INVALID_SOCKET\n");
    else if(s == m_sockets[0])
    {
        trace_printf("Closed first socket (%d) => %d\n", s, error);
        m_sockets[0] = INVALID_SOCKET;
        delete m_hooks[0];
        m_hooks[0] = 0;
    }
    else if(s == m_sockets[1])
    {
        trace_printf("Closed second socket (%d) => %d\n", s, error);
        m_sockets[1] = INVALID_SOCKET;
        delete m_hooks[1];
        m_hooks[1] = 0;
    }
    else
        warning_printf("Closed unknown socket (%d) => %d\n", s, error);
}

int SocketHookSet::send(SOCKET s, char * buf, int len, int flags)
{
    // Check for invalid/unexpected parameters.
    if(len <= 0)
    {
        error_printf("nonpositive 'len' in send()\n");
        return ::send(s, buf, len, flags);
    }
    if(flags != 0)
    {
        error_printf("nonzero 'flags' in send()\n");
        return ::send(s, buf, len, flags);
    }

    SocketHook * hook = find_hook(s);
    if(hook == 0)
    {
        warning_printf("send() to unexpected socket\n");
        return ::send(s, buf, len, flags);
    }
    return hook->send(buf, len);
}

int SocketHookSet::recv(SOCKET s, char * buf, int len, int flags)
{
    // Check for invalid/unexpected parameters.
    if(len <= 0)
    {
        error_printf("nonpositive 'len' in recv()\n");
        return ::recv(s, buf, len, flags);
    }
    if(flags != 0)
    {
        error_printf("nonzero 'flags' in recv()\n");
        return ::recv(s, buf, len, flags);
    }

    SocketHook * hook = find_hook(s);
    if(hook == 0)
    {
        warning_printf("recv() from unexpected socket\n");
        return ::recv(s, buf, len, flags);
    }
    int r = hook->recv(buf, len);
    if(r == 0 || r == SOCKET_ERROR)
    {
        log_printf("Automatically closing socket\n");
        int err = hook->close();
        close(hook->get_socket(), err);
    }
    return r;
}

int SocketHookSet::select(int nfds, fd_set * readfds, fd_set * writefds,
    fd_set * exceptfds, const struct timeval * timeout)
{
    int ret = ::select(nfds, readfds, writefds, exceptfds, timeout);
    if(ret != SOCKET_ERROR)
    {
        check_ready(0, readfds, ret);
        check_ready(1, readfds, ret);
    }
    return ret;
}

////////////////////////////////////////////////////////////////////////////////
//
//  The socket hook functions
//  These are all private static members of SocketHookSet
//
////////////////////////////////////////////////////////////////////////////////

int WINAPI SocketHookSet::hook_closesocket(SOCKET s)
{
    int err = closesocket(s);

    //trace_printf(">> closesocket(%d) => %d\n", s, err);
    if(m_instance)
        m_instance->close(s, err);
    return err;
}

int WINAPI SocketHookSet::hook_connect(SOCKET s, const struct sockaddr *name,
    int namelen)
{
    const sockaddr_in * inaddr =
        reinterpret_cast<const sockaddr_in *>(name);

#ifdef USE_GOD_CLIENT
    sockaddr_in newaddr;
    if(inaddr->sin_family == AF_INET)
    {
        if(ntohs(inaddr->sin_port) == 3593)
        {
            // When the server relays the godclient to port 2593, it seems to
            // connect using port 3593.
            log_printf("Connecting to port 2593 instead of 3593.\n");
            memcpy(&newaddr, inaddr, sizeof(sockaddr_in));
            newaddr.sin_port = htons(2593);
            name = reinterpret_cast<sockaddr *>(&newaddr);
        }
    }
#endif

    int err = connect(s, name, namelen);

    trace_printf(">> connect(%d, n:%p, nlen:%d) => %d\n",
        s, name, namelen, err);
    if(err == SOCKET_ERROR)
    {
        trace_printf("Failed connecting to: family:%d  %d.%d.%d.%d:%d\n",
            inaddr->sin_family, inaddr->sin_addr.S_un.S_un_b.s_b1,
            inaddr->sin_addr.S_un.S_un_b.s_b2,
            inaddr->sin_addr.S_un.S_un_b.s_b3,
            inaddr->sin_addr.S_un.S_un_b.s_b4, ntohs(inaddr->sin_port));
    }

    return err;
}

int WINAPI SocketHookSet::hook_recv(SOCKET s, char *buf, int len, int flags)
{
    trace_printf(">> recv(%d, buf:%p, len:%d, f:0x%08X)\n",
        s, buf, len, flags);
    if(m_instance)
        return m_instance->recv(s, buf, len, flags);
    else 
        return ::recv(s, buf, len, flags);
}

int WINAPI SocketHookSet::hook_send(SOCKET s, /*const*/ char *buf, int len,
    int flags)
{
    trace_printf(">> send(%d, buf:%p, len:%d, f:0x%08X)\n",
        s, buf, len, flags);
    if(m_instance)
        return m_instance->send(s, buf, len, flags);
    else
        return ::send(s, buf, len, flags);
}

int WINAPI SocketHookSet::hook_select(int nfds, fd_set * readfds,
    fd_set * writefds, fd_set * exceptfds, const struct timeval * timeout)
{
    //trace_printf(">> select(%d, r:%p, w:%p, e:%p, timeout:%p)\n",
    //  nfds, readfds, writefds, exceptfds, timeout);
    if(m_instance)
        return m_instance->select(nfds, readfds, writefds, exceptfds,
            timeout);
    else
        return ::select(nfds, readfds, writefds, exceptfds,
            timeout); 
}

SOCKET WINAPI SocketHookSet::hook_socket(int af, int type, int protocol)
{
    SOCKET s = ::socket(af, type, protocol);

    trace_printf(">> socket(%d, %d, %d) => %d\n", af, type, protocol, s);
    if(m_instance && s != INVALID_SOCKET)
        m_instance->add(s, af, type, protocol);
    return s;
}

