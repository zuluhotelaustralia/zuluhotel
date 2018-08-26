/******************************************************************************\
* 
* 
*  Copyright (C) 2004 Daniel 'Necr0Potenc3' Cavalcanti
* 
* 
*  This program is free software; you can redistribute it and/or modify
*  it under the terms of the GNU General Public License as published by
*  the Free Software Foundation; either version 2 of the License, or
*  (at your option) any later version.
* 
*  This program is distributed in the hope that it will be useful,
*  but WITHOUT ANY WARRANTY; without even the implied warranty of
*  MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
*  GNU General Public License for more details.
* 
* 	Jun 08th, 2004 -- started it
* 
\******************************************************************************/


#include <winsock.h>
#include "UONetwork.h"
#include "UOProtocol.h"
#include "UOPacketHandler.h"
#include "UOEncryption.h"
#include "UOCrypt.h"
#include "UOWorld.h"
#include "BufferQueue.h"
#include "Logger.h"
#include "INIProfile.h"


/* checks the return of winsock functions */
#define WSCHECKRET(ret, id) \
{ \
	if(ret == SOCKET_ERROR) \
	{ \
		if(WSAGetLastError() == WSAEWOULDBLOCK) \
		{ \
			LogPrint(API_LOG, "%s would block\r\n", id); \
			WSASetLastError(WSAEWOULDBLOCK); \
			return SOCKET_ERROR; \
		} \
		else \
		{ \
			LogPrint(ERROR_LOG, "%s error detected: %d - reseting conn\r\n", id, WSAGetLastError()); \
			WSASetLastError(WSAECONNABORTED); \
			return SOCKET_ERROR; \
		} \
	} \
}\


/*
* rumors say old clients leave open sockets on exit...
* IRW goes once again, on it's white armor to defend the
* player from osi's lame programming. jk guys...
* you know I love reversing your ugly code ;)
*/
#define MAX_SOCKETCOUNT 5
static SOCKET SocketList[MAX_SOCKETCOUNT];

/* buffers to hold data before building the packets */
static BufferQueue RecvPartial;
static BufferQueue SendPartial;

/* buffers for full packets ONLY */
static BufferQueue RecvFull; /* 400c> clients are bugged. this buffer should hold only ONE packet at a time */
static BufferQueue SendFull;

/* for incomplete codeword support */
static HuffmanObj HuffObj;

/* for HandleSocket */
static BOOL ExpectSeed = FALSE; /* expect a seed after the 0x8c packet is received from the server */
static BOOL HasFirstPacket = FALSE; /* if the first packet in the socket came (not the seed) */
static BOOL RecvCompress = FALSE; /* FALSE in the login socket, TRUE in the game socket */
static SOCKET CurSocket = SOCKET_NULL;
static int SocketState = SOCKET_NULL; /* 0 for login socket, 1 for game socket */

unsigned int TmpSeed = 0;
unsigned int LoginSeed = 0;
unsigned int GameSeed = 0;
unsigned int RelaySeed = 0;


/*******************************************************************************
* 
*   Bools
* 
*******************************************************************************/

BOOL GetDropBadPackets(void)
{
	unsigned int Value = 0;
	GetAlias("dropbadpackets", &Value);
	return (Value != 0);
};

void SetDropBadPackets(BOOL drop){ SetAlias("dropbadpackets", drop); return; }


/*******************************************************************************
* 
*   General functions for the api hooks
* 
*******************************************************************************/

int HandleSocket(SOCKET Socket, char *Buf, int Len)
{
	/* first use of the socket, clear it */
	if(CurSocket != Socket || ExpectSeed)
	{
		LogPrint(NOFILTER_LOG, "Clearing sockets\r\n");
		LogPrint(NOFILTER_LOG, "New socket: %d\r\n", Socket);

		if(CurSocket == Socket && ExpectSeed)
			LogPrint(NOFILTER_LOG, "Client is re-using this socket\r\n");

		CleanWorld();
		DecompressClean(&HuffObj);
		HasFirstPacket = FALSE;
		SocketState = SOCKET_NULL;
		CurSocket = Socket;
		ExpectSeed = FALSE;

		BQClean(&RecvPartial);
		BQClean(&RecvFull);
		BQClean(&SendPartial);
		BQClean(&SendFull);

		/*
		* if this is a new socket, it's the seed. clients might
		* reuse the socket and don't resend the seed. this
		* happens in game socket when the user presses enter
		* instead of choosing the server.
		* if the client doesnt send a seed, just the login packet
		* use the last seed (TmpSeed)
		*/
		if(Len == LOGIN1_SIZE || Len == LOGIN3_SIZE)
			LogPrint(NOFILTER_LOG, "Reusing loginseed\r\n");
		else if(Len == LOGIN2_SIZE)
			LogPrint(NOFILTER_LOG, "Reusing gameseed\r\n");
		else /* should be a seed */
		{
			if(Len != SEED_SIZE)
				LogPrint(ERROR_LOG, "Bad seed len\r\n");

			if(Len >= SEED_SIZE)
			{
				/* any changes to the seed should be done here. */
				memcpy(&TmpSeed, Buf, 4);

				LogPrint(IRWCLIENT_LOG, "Client->IRW Login seed %d bytes\r\n", Len);
				LogDump(IRWCLIENT_LOG, (unsigned char*)Buf, Len);
			}

			return 1; /* the seed can't go to the packet builder/handler */
		}
	}

	/* determine which socket this is, login or game */
	if(!HasFirstPacket && (Len == LOGIN1_SIZE || Len == LOGIN3_SIZE))
	{
        SocketState = SOCKET_LOGIN;
		HasFirstPacket = TRUE;
		LoginSeed = TmpSeed;
		RecvCompress = FALSE;
		StartLoginCrypt(Buf, Len, LoginSeed);
		LogPrint(NOFILTER_LOG, "Switching to login socket, seed 0x%08X\r\n", LoginSeed);
	}

	if(!HasFirstPacket && Len == LOGIN2_SIZE)
	{
		SocketState = SOCKET_GAME;
		HasFirstPacket = TRUE;
		GameSeed = TmpSeed;
		RecvCompress = TRUE;
		StartGameCrypt(Buf, Len, RelaySeed, GameSeed);
		LogPrint(NOFILTER_LOG, "Switching to game socket, seed 0x%08X\r\n", GameSeed);
	}

	return 0;
}

void SendToServer(unsigned char *Buf, unsigned int Size)
{
	int PacketLen = GetPacketLen(Buf, Size);

	if(Size != PacketLen)
	{
		ClientPrintWarning("Bad packet in send. Len %d should be %d", Size, PacketLen);
		LogPrint(WARNING_LOG | ERROR_LOG, ":SENDTOSV: Packet size (%d) and buffer size (%d) differ. Expect a crash. DropBadPackets is %s\r\n", PacketLen, Size, GetDropBadPackets() ? "on" : "off");

		if(GetDropBadPackets())
			return;
	}

	LogPrint(IRWSERVER_LOG, "IRW->Server %s %d bytes\r\n", GetPacketName(Buf), Size);
	LogDump(IRWSERVER_LOG, Buf, Size);
	BQAddTail(&SendFull, Buf, Size);

	return;
}

void SendToClient(unsigned char *Buf, unsigned int Size)
{
	int PacketLen = GetPacketLen(Buf, Size);

	if(Size != PacketLen)
	{
		ClientPrintWarning("Bad packet in recv. Len %d should be %d", Size, PacketLen);
		LogPrint(WARNING_LOG | ERROR_LOG, ":SENDTOCL: Packet size (%d) and buffer size (%d) differ. Expect a crash. DropBadPackets is %s\r\n", PacketLen, Size, GetDropBadPackets() ? "on" : "off");

		if(GetDropBadPackets())
			return;
	}

	LogPrint(IRWCLIENT_LOG, "IRW->Client %s %d bytes\r\n", GetPacketName(Buf), Size);
	LogDump(IRWCLIENT_LOG, Buf, Size);
	BQAddTail(&RecvFull, Buf, Size);

	return;
}

int InternalSendToServer(void)
{
	int ret = 0;

	EncryptToServer((char*)SendFull.Buffer, SendFull.Used, SocketState);
	ret = send(CurSocket, (const char*)SendFull.Buffer, SendFull.Used, 0);
	WSCHECKRET(ret, ">> send()"); /* it will return if an error occured */

	SendFull.Used = 0; /* zero the buffer if sent */
	return ret;
}

int InternalSendToClient(char *Buf, int Len)
{
	/* 400c> client fix */
	char *Packet = NULL;
	int PacketLen = 0;

	int InBytes = 0, OutBytes = 0;

	if(!RecvFull.Used)
		return 0;

	/* 
	* 400c> fix: transfer ONE packet from the full buffer to the client.
	* for some reason osi screwed up their packet handler, the client
	* can only process one packet at a time.
	* so if you throw more than one full packet (built) the client processes
	* only one and kinda hangs till the packet builder is called again (by recv)
	*/
	PacketLen = GetPacketLen(RecvFull.Buffer, RecvFull.Size);
	Packet = (char*)malloc(PacketLen);
	memcpy(Packet, RecvFull.Buffer, PacketLen);
	BQRemoveHead(&RecvFull, PacketLen);

	/* transfer the queued data to the client's buf and return the amount of bytes there */
	if(!RecvCompress)
	{
		/* check if the client's buffer is big enough, but do nothing if it isn't */
		if(Len < (int)PacketLen)
			LogPrint(WARNING_LOG, ":WARNING: small recv buf. len: %d min: %d. Expect a crash\r\n", Len, PacketLen);

		memcpy(Buf, Packet, PacketLen);
		EncryptToClient(Buf, PacketLen, SocketState);
		free(Packet);
		return PacketLen;
	}
	else
	{
		InBytes = PacketLen;
		OutBytes = Len; /* size of the client's buffer */
		Compress(Buf, (const char*)Packet, &OutBytes, &InBytes);
		LogPrint(COMPRESSOR_LOG, "Compressed %d bytes into %d bytes\r\n", PacketLen, OutBytes);

		if(Len < (int)OutBytes)
			LogPrint(WARNING_LOG, ":WARNING: small recv buf. len: %d min: %d. Expect a crash\r\n", OutBytes, PacketLen);

		EncryptToClient(Buf, OutBytes, SocketState);
		free(Packet);
		return OutBytes;
	}
}
/*
* adds data to the partial buffer
* and then checks for full packets in that buffer
* if there is a full packet, call the PacketHandler and InternalSendData
*/
void BuildPacket(unsigned char *Buf, unsigned int BufLen, int From)
{
	BufferQueue *PartialBuf = NULL;
	unsigned int PacketID = 0, PacketLen = 0;

	if(From == CLIENT_MESSAGE)
		PartialBuf = &SendPartial;
	else /* SERVER_MESSAGE */
		PartialBuf = &RecvPartial;

	/* add the data to the end of the buffer */
	BQAddTail(PartialBuf, Buf, BufLen);

	/* while there's data in partialbuf and full packets in it */
	while(PartialBuf->Used && (PacketLen = GetPacketLen(PartialBuf->Buffer, PartialBuf->Used)) <= PartialBuf->Used)
	{
		PacketID = GetPacketID(PartialBuf->Buffer);

		/* check for unknown packets */
		if(!PacketLen)
		{
			MBOut("Ignorance sux", ":BUILDER: Unknown packet %02X from %s\r\n", PacketID, (From == CLIENT_MESSAGE) ? "Client" : "Server");
			LogPrint(BUILDER_LOG | ERROR_LOG, ":BUILDER: Unknown packet %02X from %s\r\n", PacketID, (From == CLIENT_MESSAGE) ? "Client" : "Server");
			LogDump(BUILDER_LOG | ERROR_LOG, PartialBuf->Buffer, PartialBuf->Used);
			ExitProcess(0);
			return;
		}

		if(From == CLIENT_MESSAGE)
		{
			/* log the packets before they are handled/modified */
			LogPrint(CLIENTIRW_LOG, "Client->IRW %s %d bytes\r\n", GetPacketName(PartialBuf->Buffer), PacketLen);
			LogDump(CLIENTIRW_LOG, PartialBuf->Buffer, PacketLen);

			/* send the data if the packet handler approved it */
			if(HandlePacket(PartialBuf->Buffer, PacketLen, From) == SEND_PACKET)
				SendToServer(PartialBuf->Buffer, PacketLen);
		}
		else
		{
			LogPrint(SERVERIRW_LOG, "Server->IRW %s %d bytes\r\n", GetPacketName(PartialBuf->Buffer), PacketLen);
			LogDump(SERVERIRW_LOG, PartialBuf->Buffer, PacketLen);

			/*
			* a primitive packet handler ;)
			* to be in a new socket, we have to check:
			* - the client created/used/closed a socket (HandleSocket() & SocketList)
			* - the client is being redirected to the game server (HandleSocket() & ExpectSeed)
			*/
			if(PartialBuf->Buffer[0] == 0x8c)
			{
				ExpectSeed = TRUE;
				memcpy(&RelaySeed, PartialBuf->Buffer + 7, 4);
				LogPrint(CRYPT_LOG, "Relay seed: 0x%08X\r\n", RelaySeed);
			}

			if(HandlePacket(PartialBuf->Buffer, PacketLen, From) == SEND_PACKET)
				SendToClient(PartialBuf->Buffer, PacketLen);
		}

		/* remove it from the partial buffer */
		BQRemoveHead(PartialBuf, PacketLen);
	}

	LogPrint(BUILDER_LOG, "BUILDER:%d:DONE: %d in queue\r\n", From, PartialBuf->Used);
	return;
}


/*******************************************************************************
*
*   All API hooks are bellow
* 
*******************************************************************************/

int WINAPI hook_select(int nfds, fd_set *readfds, fd_set *writefds, fd_set *exceptfds, const struct timeval *timeout)
{
	/* send any data in send's buffer */
	if(SendFull.Used)
	{
		InternalSendToServer(); /* TODO: handle send() errors */
		LogPrint(API_LOG, ">> FakeSelect(send): %d bytes in queue\r\n", SendFull.Used);
	}

	/* if there are any packets in recvfull, send them to client before processing recv() */
	if(RecvFull.Used)
	{
		FD_CLR(CurSocket, writefds);
		FD_CLR(CurSocket, exceptfds);
		FD_SET(CurSocket, readfds);

		LogPrint(API_LOG, ">> FakeSelect(recv): %d bytes in queue\r\n", RecvFull.Used);
		return 1;
	}

	return select(nfds, readfds, writefds, exceptfds, timeout);
}

int WINAPI hook_send(SOCKET s, char *buf, int len, int flags)
{
	int ret = 0;

	LogPrint(API_LOG, ">> send() buf: %X s: %d, len: %d, flags: %d == %d\r\n", &buf, s, len, flags, ret);

	/* if its a seed, dont pass it through the packet handler */
	if(HandleSocket(s, buf, len))
		return send(s, buf, len, flags);

	/* decrypt and pass all data to the packet builder */
	DecryptFromClient(buf, len, SocketState);
	BuildPacket((unsigned char*)buf, len, CLIENT_MESSAGE);

	/* return what the client expects */
	return len;
}

int WINAPI hook_senddbg(SOCKET s, char *buf, int len, int flags)
{
	int ret = 0;

	ret = send(s, buf, len, flags);

	if(ret != 0 && ret != SOCKET_ERROR)
	{
		LogPrint(NOFILTER_LOG, ">> send() buf: %X s: %d, len: %d, flags: %d == %d\r\n", &buf, s, len, flags, ret);
		LogDump(NOFILTER_LOG, (unsigned char*)buf, len);

		if((unsigned char)buf[0] == 0x80 && len == LOGIN1_SIZE)
			RecvCompress = FALSE;

		if((unsigned char)buf[0] == 0x91 && len == LOGIN2_SIZE)
			RecvCompress = TRUE;
	}


	return ret;
}

int WINAPI hook_recvdbg(SOCKET s, char *buf, int len, int flags)
{
	int ret = 0;
	int InBytes = 0, OutBytes = 0;
	unsigned char *DecompressBuf = NULL;

	ret = recv(s, buf, len, flags);

	if(ret != 0 && ret != SOCKET_ERROR)
	{
		LogPrint(NOFILTER_LOG, ">> recv() buf: %X s: %d, len: %d, flags: %d == %d\r\n", &buf, s, len, flags, ret);
		LogDump(NOFILTER_LOG, (unsigned char*)buf, ret);

		if(RecvCompress == TRUE)
		{
			InBytes = ret;
			OutBytes = MIN_DECBUF_SIZE(ret);

			/* decompress the data in a buffer (emulation of recv buffer) */
			DecompressBuf = (unsigned char*)malloc(OutBytes);
			if(DecompressBuf == NULL)
				LogPrint(ERROR_LOG, ":ERROR: Could not allocate %d bytes for DecompressBuf\r\n", OutBytes);

			Decompress((char*)DecompressBuf, buf, &OutBytes, &InBytes, &HuffObj);
			LogPrint(NOFILTER_LOG, "Decompressed %d bytes into %d bytes\r\n", ret, OutBytes);
			LogDump(NOFILTER_LOG, DecompressBuf, OutBytes);
			free(DecompressBuf);
		}
	}


	return ret;
}

int WINAPI hook_recv(SOCKET s, char *buf, int len, int flags)
{
	unsigned char *DecompressBuf = NULL;
	int InBytes=0, OutBytes=0;
	int ret = 0;

	/* this problem is quite common in clients such as 3.0.0c... maybe its the translation server? */
	if(CurSocket != s)
	{
		ret = recv(s, buf, len, flags);

		if(ret > 0)
		{
			LogPrint(WARNING_LOG, "Bad usage of recv on socket: %d\r\n", s);
			LogDump(WARNING_LOG, (unsigned char*)buf, ret);
		}

		return ret;
	}

	/*
	* if recv was called after select has been faked dont call recv.
	* just pass to the client the data in the RecvFull queue
	*/
	if(RecvFull.Used)
		return InternalSendToClient(buf, len);

	ret = recv(s, buf, len, flags);
	LogPrint(API_LOG, ">> recv() buf: %X s: %d, len: %d, flags: %d == %d\r\n", &buf, s, len, flags, ret);
	WSCHECKRET(ret, ">> recv()");

	/* decrypt server data and pass it to the packet builder */
	DecryptFromServer(buf, ret, SocketState);

	if(!RecvCompress)
		BuildPacket((unsigned char*)buf, ret, SERVER_MESSAGE);
	else /* compressed data */
	{
		InBytes = ret;
		OutBytes = MIN_DECBUF_SIZE(ret);

		/* decompress the data in a buffer (emulation of recv buffer) */
		DecompressBuf = (unsigned char*)malloc(OutBytes);
		if(DecompressBuf == NULL)
			LogPrint(ERROR_LOG, ":ERROR: Could not allocate %d bytes for DecompressBuf\r\n", OutBytes);

		Decompress((char*)DecompressBuf, buf, &OutBytes, &InBytes, &HuffObj);
		LogPrint(COMPRESSOR_LOG, "Decompressed %d bytes into %d bytes\r\n", ret, OutBytes);

		/* pass the decompressed data to the packet builder */
		BuildPacket(DecompressBuf, OutBytes, SERVER_MESSAGE);
		free(DecompressBuf);
	}

	/* encrypt if necessary then dump any data in RecvFull to the client's buffer */
	return InternalSendToClient(buf, len);
}

SOCKET WINAPI hook_socket(int af, int type, int protocol)
{
	int i = 0;
	SOCKET s = socket(af, type, protocol);

	/* if the socket was created, add it to the socket list */
	if(s != INVALID_SOCKET)
	{
		AddSocket(s);
		LogPrint(NOFILTER_LOG, ">> Socket %d created. Type: %d\r\n", s, type);
	}

	return s;
}

int WINAPI hook_closesocket(SOCKET s)
{
	int i = 0, ret = 0;

	ret = closesocket(s);

	/* if the socket was succesfully closed, remove it from the list */
	if(ret != SOCKET_ERROR)
	{
		if(CurSocket == s)
		{
			LogPrint(NOFILTER_LOG, ">> Closing current socket: %d\r\n", s);
			CurSocket = SOCKET_NULL;
		}

		RemoveSocket(s);
		LogPrint(NOFILTER_LOG, ">> Closed socket: %d\r\n", s);
	}

	return ret;
}

int WINAPI hook_connect(SOCKET s, const struct sockaddr *name, int namelen)
{
	struct hostent *RedirectTarget = NULL;
	struct sockaddr_in *RedirectSocket = NULL;
	char RedirectIP[4096];
	int RedirectPort = 0, ret = 0;

	/* yes this is how you read the IP out of a sockaddr struct... ugly huh? :) */
	RedirectSocket = (struct sockaddr_in*)name;
	LogPrint(NOFILTER_LOG, ">> Socket %d connect() to IP: %d.%d.%d.%d:%d\r\n", s, (unsigned char)RedirectSocket->sin_addr.S_un.S_un_b.s_b1, (unsigned char)RedirectSocket->sin_addr.S_un.S_un_b.s_b2, (unsigned char)RedirectSocket->sin_addr.S_un.S_un_b.s_b3, (unsigned char)RedirectSocket->sin_addr.S_un.S_un_b.s_b4, htons(RedirectSocket->sin_port));

	/* redirect transerv and it will go nuts waiting for a conn and jamming the system... damned OSI bastards :P */
	if(htons(RedirectSocket->sin_port) == 28888)
		return connect(s, name, namelen);

	/* redirection is only for loginserver (RTD: maybe ;)) */
	if(ExpectSeed)
		return connect(s, name, namelen);

	/* get the IP and port */
	GetIRWProfileString(NULL, "IRW", "ShardAddress", RedirectIP, sizeof(RedirectIP));
	GetIRWProfileInt(NULL, "IRW", "ShardPort", &RedirectPort);

	/* apply IP redirection */
	if(!strcmp(RedirectIP, ""))
		LogPrint(NOFILTER_LOG, ">> connect() no ip redirection\r\n");
	else
	{
		RedirectTarget = gethostbyname(RedirectIP);

		if(RedirectTarget == NULL)
			LogPrint(NOFILTER_LOG, ">> connect() could not resolve host %s for redirection\r\n", RedirectIP);
		else
		{
			/* reading hostent IP is so much cleaner than sockaddr... lol */
			memcpy(&RedirectSocket->sin_addr.s_addr, RedirectTarget->h_addr, RedirectTarget->h_length);
			LogPrint(NOFILTER_LOG, ">> connect() redirecting to IP: %d.%d.%d.%d\r\n", (unsigned char)RedirectTarget->h_addr[0], (unsigned char)RedirectTarget->h_addr[1], (unsigned char)RedirectTarget->h_addr[2], (unsigned char)RedirectTarget->h_addr[3]);
		}
	}

	/* apply port redirection */
	if(!RedirectPort)
		LogPrint(NOFILTER_LOG, ">> connect() no port redirection\r\n");
	else
	{
		RedirectSocket->sin_port = htons((USHORT)RedirectPort);
		LogPrint(NOFILTER_LOG, ">> connect() redirecting to port: %d\r\n", htons(RedirectSocket->sin_port));
	}

	if((ret = connect(s, name, namelen)) == SOCKET_ERROR)
		LogPrint(ERROR_LOG, ">> connect() failed. Error: %d\r\n", WSAGetLastError());

	return ret;
}

/*******************************************************************************
*
*   Handles all open and created sockets
*   kept it down here cause its ugly as hell!!!
*
*******************************************************************************/

void CleanSocketList(void)
{
	int i = 0;

	for(i = 0; i < MAX_SOCKETCOUNT; i++)
		SocketList[i] = SOCKET_NULL;

	return;
}

void AddSocket(SOCKET s)
{
	int i = 0;

	for(i = 0; i < MAX_SOCKETCOUNT; i++)
	{
		if(SocketList[i] == SOCKET_NULL)
		{
			SocketList[i] = s;
			return;
		}
	}

	LogPrint(ERROR_LOG, "Could not add socket %d to list\r\n", s);
	return;
}

void RemoveSocket(SOCKET s)
{
	int i = 0;

	for(i = 0; i < MAX_SOCKETCOUNT; i++)
	{
		if(SocketList[i] == s)
		{
			SocketList[i] = SOCKET_NULL;
			return;
		}
	}

	LogPrint(ERROR_LOG, "Could not find socket %d in list\r\n", s);
	return;
}

int CloseOpenSockets(void)
{
	int i = 0, ret = 0, SocketCount = 0;

	for(i = 0; i < MAX_SOCKETCOUNT; i++)
	{
		if(SocketList[i] != SOCKET_NULL)
		{
			ret = closesocket(SocketList[i]);

			if(ret == SOCKET_ERROR)
			{
				/*
				* according to msdn:
				* application may be accessing a socket that the current
				* active task does not own (that is, trying to share a
				* socket between tasks)
				*/
				if(WSAGetLastError() == WSANOTINITIALISED)
					LogPrint(NOFILTER_LOG, ">> Failed to close socket: %d. It should be the translation socket\r\n", SocketList[i]);
				else
					LogPrint(NOFILTER_LOG, ">> Failed to close socket: %d. Error: %d\r\n", SocketList[i], WSAGetLastError());
			}
			else
			{
				SocketList[i] = SOCKET_NULL;
				SocketCount++;
				LogPrint(NOFILTER_LOG, ">> Closed socket: %d\r\n", SocketList[i]);
			}
		}
	}
    
	return SocketCount;
}
