////////////////////////////////////////////////////////////////////////////////
//
// hooks.h
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
//  Declaration of the socket hook classes and functions
//
////////////////////////////////////////////////////////////////////////////////

#ifndef _HOOKS_H_
#define _HOOKS_H_

#include <winsock.h>

#include <queue>

#include "common.h"
#include "uo_huffman.h"
#include "crypt.h"
#include "iconfig.h"

class SocketHook;

// Abstract class
class HookCallbackInterface
{
public:
    HookCallbackInterface() {}
    virtual ~HookCallbackInterface() {}
    // Should return 0 for a variable sized message, or -1 if the message
    // code is invalid.
    virtual int get_message_size(int code) = 0;
    virtual void disconnected(SocketHook * hook) = 0;
    // This method may only modify the key.
    virtual void handle_key(SocketHook * hook, uint8 key[4]) = 0;
    // If this method returns false, the message will not be sent to the
    // server.
    virtual bool handle_send_message(SocketHook * hook, uint8 * buf,
        int size) = 0;
    // If this method returns false, the message will not be sent to the
    // client.
    virtual bool handle_receive_message(SocketHook * hook, uint8 * buf,
        int size) = 0;
};

////////////////////////////////////////////////////////////////////////////////

const int RECV_BUF_SIZE = 65536;

class Buffer
{
private:
    char * m_alloc, * m_ptr;
    int m_alloc_size, m_size;
    int * m_ref_count;

    // The assignment operator is never defined.
    void operator = (const Buffer & other);

public:
    Buffer();
    // Takes ownership of the memory.
    Buffer(char * data, int size);
    Buffer(const Buffer & other);
    ~Buffer();

    int get_size() const { return m_size; }
    bool is_empty() const { return m_size == 0; }
    int get(char * dest, int dest_size, Copier & copier);
};

class BufferQueue
{
private:
    typedef std::queue<Buffer> queue_t;
    queue_t q;

public:
    //BufferQueue();

    // Moves bytes from the queue into the destination buffer.
    // Returns the number of bytes moved.
    int get(char * dest, int dest_size, Copier & copier);
    bool is_empty() const { return q.empty(); }

    // Put a copy of the data into the queue.
    void push_copy(char * buf, int size);
    void push_copy(uint8 * buf, int size)
        { push_copy(reinterpret_cast<char *>(buf), size); }
    // Put the dynamically allocated buffer into the queue, assuming
    // ownership.
    void push_move(char * buf, int size);
    void push_move(uint8 * buf, int size)
        { push_move(reinterpret_cast<char *>(buf), size); }
};

class MessageFragment;

class SocketHook
{
private:
    HookCallbackInterface & m_callback;
    SOCKET m_s;
    BufferQueue m_receive_queue;
    bool m_disconnected, m_recv_error;
    int m_last_error;

    char m_recv_buf[RECV_BUF_SIZE];
    bool m_compressed, m_first_send;
    char * m_dec_buf;   // buffer for decompressed data
    int m_dec_buf_size;
    uint8 * m_send_buf;
    int m_send_buf_size;
    MessageFragment * m_send_fragment, * m_receive_fragment;
    NormalCopier m_copier;
    CompressingCopier m_compressor;
    DecompressingCopier m_decompressor;

    enum { CRYPT_NONE, CRYPT_LOGIN, CRYPT_GAME } m_crypt_mode;
    uint8 m_key[4];
	int Encrypt203; ///zorm203
    LoginCrypt m_login_crypt;
    GameCrypt *m_game_crypt;
    GameCrypt *m_game_cryptnew; ///zorm203

    void alloc_dec_buf(int size);
    void alloc_send_buf(int size);
    void handle_receive_data(char * buf, int size);

public:
    SocketHook(HookCallbackInterface & callback, SOCKET s);
    ~SocketHook();

    // Called when data is available to be received from the server.
    void recv_ready();
    // Returns true if data is waiting to be sent to the client.
    bool is_ready() const;
    int close();
    SOCKET get_socket() const { return m_s; }

    int send(char *buf, int size);
    int recv(char *buf, int len);

    int send_server(uint8 * buf, int size);
    void send_client(uint8 * buf, int size);
    void set_compressed(bool compressed) { m_compressed = compressed; }

    void set_login_encryption(uint32 k1, uint32 k2);
    void set_game_encryption(int version);
};

class SocketHookSet
{
public:
    static int WINAPI hook_connect(SOCKET s, const struct sockaddr *name,
        int namelen);
    static int WINAPI hook_closesocket(SOCKET s);
    static int WINAPI hook_recv(SOCKET s, char *buf, int len, int flags);
    static int WINAPI hook_send(SOCKET s, /*const*/ char *buf, int len,
        int flags);
    static int WINAPI hook_select(int nfds, fd_set * readfds,
        fd_set * writefds, fd_set * exceptfds, const struct timeval * timeout);
    static SOCKET WINAPI hook_socket(int af, int type, int protocol);

private:
    static SocketHookSet * m_instance;

    HookCallbackInterface & m_callback;
    SOCKET m_sockets[2];
    SocketHook * m_hooks[2];

    SocketHook * find_hook(SOCKET s);
    void check_ready(int index, fd_set * readfds, int & count);

public:
    SocketHookSet(HookCallbackInterface & callback);
    ~SocketHookSet();

    // Install the hook functions.
    void install();

    // called for socket() API
    void add(SOCKET s, int af, int type, int protocol);
    // called for closesocket() API
    void close(SOCKET s, int error);
    int send(SOCKET s, char * buf, int len, int flags);
    int recv(SOCKET s, char * buf, int len, int flags);
    int select(int nfds, fd_set * readfds, fd_set * writefds,
        fd_set * exceptfds, const struct timeval * timeout);
};

// Hook functions:

#endif

