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
* 	Jun 14th, 2004 -- handle client and server encryption and decryption to aliviate
* 	the job of the hooking functions
* 
\******************************************************************************/


#include <windows.h>
#include "Logger.h"
#include "UOProtocol.h"
#include "UOCrypt.h"
#include "UOEncryption.h"
#include "INIProfile.h"


/* for login/game decrypt (client -> irw) */
static LoginCryptObj LCFromClient;
static BlowfishObj BCFromClient;
static TwofishObj TCFromClient;

/* for login/game encrypt (irw -> server) */
static LoginCryptObj LCToServer;
static BlowfishObj BCToServer;
static TwofishObj TCToServer;

/* for game decrypt (server -> irw) */
static MD5Obj MD5FromServer;

/* for game encrypt (irw -> client) */
static MD5Obj MD5ToClient;

/* client->irw - autodetected */
static int ClientLoginCryptType = -1;
static int ClientGameCryptType = -1;

/* irw->server - set by user */
static int IRWLoginCryptType = -1;
static int IRWGameCryptType = -1;

/* to be called on the first client->server packet (login socket) */
void StartLoginCrypt(char *Buf, int Len, int LoginSeed)
{
	unsigned char LoginSample[GETKEYS_MIN_SIZE];

	if(ClientLoginCryptType == -1)
	{
		memset(LoginSample, 0, GETKEYS_MIN_SIZE);

		if(Len == LOGIN1_SIZE)
		{
			LoginSample[0] = 0x80;
			LogPrint(CRYPT_LOG | INTEREST_LOG, ":CRYPT: Regular login packet detected\r\n");
		}
		else if(Len == LOGIN3_SIZE)
		{
			LoginSample[0] = 0xCF;
			LogPrint(CRYPT_LOG | INTEREST_LOG, ":CRYPT: IGR login packet detected\r\n");
		}

		/* the user shouldn't type his login and pwd in the client */
		strncpy((char*)(LoginSample + 1), "", 30);
		strncpy((char*)(LoginSample + 31), "", 30);

		LogPrint(CRYPT_LOG |ERROR_LOG, "Sample:\r\n");
				LogDump(CRYPT_LOG |ERROR_LOG, (unsigned char*)LoginSample, GETKEYS_MIN_SIZE);

		/* if the sample == the buffer, theres no encryption */
		if(!memcmp(LoginSample, Buf, GETKEYS_MIN_SIZE))
		{
			ClientLoginCryptType = CRYPT_NONE;
			LogPrint(CRYPT_LOG | INTEREST_LOG, ":CRYPT: No login crypt detected\r\n");
		}
		else /* calculate the login keys */
		{
            ClientLoginCryptType = CRYPT_SAME;

			LCFromClient.pseed = LoginSeed;
            if(CalculateKeys(LoginSample, (unsigned char*)Buf, &LCFromClient.pseed, &LCFromClient.k1, &LCFromClient.k2) == -1)
			{
				LogPrint(CRYPT_LOG | ERROR_LOG, "Calculated: seed: 0x%08X key1: 0x%08X key2: 0x%08X\r\n", LCFromClient.pseed, LCFromClient.k1, LCFromClient.k2);
				LogPrint(CRYPT_LOG |ERROR_LOG, "The keys are wrong. Check for wrong login/password or a bad uo client\r\n");
				LogPrint(CRYPT_LOG |ERROR_LOG, "Bad packet:\r\n");
				LogDump(CRYPT_LOG |ERROR_LOG, (unsigned char*)Buf, Len);
				LogPrint(CRYPT_LOG |ERROR_LOG, "Sample:\r\n");
				LogDump(CRYPT_LOG |ERROR_LOG, (unsigned char*)LoginSample, GETKEYS_MIN_SIZE);

				MBOut("Couldn't calculate login keys!!", "Make sure you didn't type the account and password in the UO client.");
				ExitProcess(0);
				return;
			}

			LogPrint(CRYPT_LOG | INTEREST_LOG, ":CRYPT: Calculated Seed: 0x%08X Key1: 0x%08X Key2: 0x%08X\r\n", LCFromClient.pseed, LCFromClient.k1, LCFromClient.k2);
		}
	}

	/* get the profile info */
	GetIRWProfileInt(NULL, "IRW", "CryptType", &IRWLoginCryptType);
	GetIRWProfileInt(NULL, "IRW", "CryptKey1", (int*)&LCToServer.k1);
	GetIRWProfileInt(NULL, "IRW", "CryptKey2", (int*)&LCToServer.k2);

	if(ClientLoginCryptType != CRYPT_NONE)
	{
		LCFromClient.pseed = LoginSeed;
		LoginCryptInit(&LCFromClient);
	}

	if(IRWLoginCryptType == CRYPT_SAME)
	{
		LCToServer.k1 = LCFromClient.k1;
		LCToServer.k2 = LCFromClient.k2;
		IRWLoginCryptType = ClientLoginCryptType;
		LogPrint(CRYPT_LOG, "Setting IRW's encryption to same as client: %d\r\n", ClientLoginCryptType);
		LogPrint(CRYPT_LOG, "Key1: 0x%08X Key2: 0x%08X\r\n", LCToServer.k1, LCToServer.k2);
	}

	if(IRWLoginCryptType != CRYPT_NONE)
	{
		LCToServer.pseed = LoginSeed;
		LoginCryptInit(&LCToServer);
		LogPrint(CRYPT_LOG, "Initializing IRW encryption: %d\r\n", IRWLoginCryptType);
		LogPrint(CRYPT_LOG, "Key1: 0x%08X Key2: 0x%08X\r\n", LCToServer.k1, LCToServer.k2);
	}

	LogPrint(CRYPT_LOG | INTEREST_LOG, ":CRYPT: IRW encryption set to: %d. Key1: 0x%08X Key2: 0x%08X\r\n", IRWLoginCryptType, LCToServer.k1, LCToServer.k2);

	return;
}

/* to be called on the first client->server packet (game socket) */
void StartGameCrypt(char *Buf, int Len, int SeedFromRelay, int GameSeed)
{
	unsigned char LoginSample[LOGIN2_SIZE];
	unsigned char BFLogin[LOGIN2_SIZE];
	unsigned char TFLogin[LOGIN2_SIZE];
	unsigned char BTFLogin[LOGIN2_SIZE];
	BlowfishObj BFTmp;
	TwofishObj TFTmp;

	/* find out which encryption the client is using */
	if(ClientGameCryptType == -1)
	{
		TFTmp.IP = GameSeed;
		TwofishInit(&TFTmp);
        BlowfishInit(&BFTmp);
		memset(LoginSample, 0, LOGIN2_SIZE);

		LoginSample[0] = 0x91;
		memcpy(LoginSample + 1, &SeedFromRelay, SEED_SIZE);
		strncpy((char*)(LoginSample + 5), "", 30);
		strncpy((char*)(LoginSample + 35), "", 30);

        /* do the 3 possible encryptions */
		BlowfishEncrypt(&BFTmp, (unsigned char*)LoginSample, (unsigned char*)BFLogin, LOGIN2_SIZE);
		TwofishEncrypt(&TFTmp, (unsigned char*)LoginSample, (unsigned char*)TFLogin, LOGIN2_SIZE);

		/* reinit and do the blowfish+twofish encrypt */
		TFTmp.IP = GameSeed;
		TwofishInit(&TFTmp);
        BlowfishInit(&BFTmp);

		BlowfishEncrypt(&BFTmp, (unsigned char*)LoginSample, (unsigned char*)BTFLogin, LOGIN2_SIZE);
		TwofishEncrypt(&TFTmp, (unsigned char*)BTFLogin, (unsigned char*)BTFLogin, LOGIN2_SIZE);

		/* check to see which one is a match */
		if(!memcmp(LoginSample, Buf, LOGIN2_SIZE))
		{
			LogPrint(CRYPT_LOG | INTEREST_LOG, ":CRYPT DETECT: No encryption detected (patched client?)\r\n");
			ClientGameCryptType = CRYPT_NONE;
		}

		if(!memcmp(Buf, BFLogin, LOGIN2_SIZE))
		{
			LogPrint(CRYPT_LOG | INTEREST_LOG, ":CRYPT DETECT: Blowfish encryption detected\r\n");
			ClientGameCryptType = CRYPT_BFISH;
		}

		if(!memcmp(Buf, TFLogin, LOGIN2_SIZE))
		{
			LogPrint(CRYPT_LOG | INTEREST_LOG, ":CRYPT DETECT: Twofish encryption detected\r\n");
			ClientGameCryptType = CRYPT_TFISH;
		}

		if(!memcmp(Buf, BTFLogin, LOGIN2_SIZE))
		{
			LogPrint(CRYPT_LOG | INTEREST_LOG, ":CRYPT DETECT: Blowfish+Twofish encryption detected\r\n");
			ClientGameCryptType = CRYPT_BTFISH;
		}

		if(ClientGameCryptType == -1)
		{
			LogPrint(CRYPT_LOG | ERROR_LOG, "Failed to detect game crypt. Game seed: 0x%08X\r\n", GameSeed);
			LogPrint(CRYPT_LOG | ERROR_LOG, "Login sample dump:\r\n");
			LogDump(CRYPT_LOG | ERROR_LOG, LoginSample, LOGIN2_SIZE);
			LogPrint(CRYPT_LOG | ERROR_LOG, "Blowfish sample dump:\r\n");
			LogDump(CRYPT_LOG | ERROR_LOG, BFLogin, LOGIN2_SIZE);
			LogPrint(CRYPT_LOG | ERROR_LOG, "Twofish sample dump:\r\n");
			LogDump(CRYPT_LOG | ERROR_LOG, TFLogin, LOGIN2_SIZE);
			LogPrint(CRYPT_LOG | ERROR_LOG, "Blowfish + twofish sample dump:\r\n");
			LogDump(CRYPT_LOG | ERROR_LOG, BTFLogin, LOGIN2_SIZE);
			LogPrint(CRYPT_LOG | ERROR_LOG, "Client packet dump:\r\n");
			LogDump(CRYPT_LOG | ERROR_LOG, Buf, Len);

			if(SeedFromRelay != GameSeed)
				MBOut("CLIENT-ERROR", "Please choose the server you wish to login instead of pressing enter or clicking the arrow");
			else
				MBOut("IRW-ERROR", "Could not detect the client's game encryption");

			ExitProcess(0);
			return;
		}
	}

	/* get the profile's encryption type */
    GetIRWProfileInt(NULL, "IRW", "CryptType", &IRWGameCryptType);
	if(IRWGameCryptType == CRYPT_SAME)
	{
		IRWGameCryptType = ClientGameCryptType;
		LogPrint(CRYPT_LOG, "IRW: Setting encryption to same as client\r\n");
	}

	LogPrint(CRYPT_LOG | INTEREST_LOG, ":CRYPT: IRW encryption set to: %d\r\n", IRWGameCryptType);

	if(ClientGameCryptType == CRYPT_BFISH || ClientGameCryptType == CRYPT_BTFISH)
	{
		LogPrint(CRYPT_LOG, "CLIENT: Initializing blowfish crypt with seed %X \"the seed is unimportant\" *the voices mutter*\r\n", GameSeed);
		BlowfishInit(&BCFromClient);
	}

	if(ClientGameCryptType == CRYPT_TFISH || ClientGameCryptType == CRYPT_BTFISH)
	{
		LogPrint(CRYPT_LOG, "CLIENT: Initializing twofish crypt with seed %X\r\n", GameSeed);
		TCFromClient.IP = GameSeed;
		TwofishInit(&TCFromClient);

		/* initialize server->client crypt */
		if(ClientGameCryptType == CRYPT_TFISH)
		{
			LogPrint(CRYPT_LOG, "CLIENT: Initializing MD5 table:\r\n");

			MD5Init(&MD5ToClient, TCFromClient.subData3, 256);
			LogDump(CRYPT_LOG, MD5ToClient.Digest, 16);
		}
	}

	if(IRWGameCryptType == CRYPT_BFISH || IRWGameCryptType == CRYPT_BTFISH)
	{
		LogPrint(CRYPT_LOG, "IRW: Initializing blowfish crypt with seed %X\r\n", GameSeed);
		BlowfishInit(&BCToServer);
	}

	if(IRWGameCryptType == CRYPT_TFISH || IRWGameCryptType == CRYPT_BTFISH)
	{
		LogPrint(CRYPT_LOG, "IRW: Initializing twofish crypt with seed %X\r\n", GameSeed);
		TCToServer.IP = GameSeed;
		TwofishInit(&TCToServer);

		/* initialize server->client crypt */
		if(IRWGameCryptType == CRYPT_TFISH)
		{
			LogPrint(CRYPT_LOG, "IRW: Initializing MD5 table:\r\n");

			MD5Init(&MD5FromServer, TCToServer.subData3, 256);
			LogDump(CRYPT_LOG, MD5FromServer.Digest, 16);
		}
	}

	return;
}

/* ENCRYPTION EMULATION WITH CLIENT */
void DecryptFromClient(char *Buf, int Len, int SocketState)
{
	/* nothing to do on login socket */
	if(SocketState == SOCKET_LOGIN && ClientLoginCryptType == CRYPT_NONE)
		return;

	/* nothing to do on game socket */
	if(SocketState == SOCKET_GAME && ClientGameCryptType == CRYPT_NONE)
		return;

	/* login decrypt if necessary */
	if(SocketState == SOCKET_LOGIN && ClientLoginCryptType != CRYPT_NONE)
		LoginCryptEncrypt(&LCFromClient, (unsigned char*)Buf, (unsigned char*)Buf, Len);

	/* game decrypt if necessary */
	/* it is MANDATORY that Twofish comes first or else the stream will be decrypted partially right */
	if(SocketState == SOCKET_GAME && (ClientGameCryptType == CRYPT_TFISH || ClientGameCryptType == CRYPT_BTFISH))
		TwofishEncrypt(&TCFromClient, (unsigned char*)Buf, (unsigned char*)Buf, Len);

	if(SocketState == SOCKET_GAME && (ClientGameCryptType == CRYPT_BFISH || ClientGameCryptType == CRYPT_BTFISH))
		BlowfishDecrypt(&BCFromClient, (unsigned char*)Buf, (unsigned char*)Buf, Len);

	return;
}

void EncryptToClient(char *Buf, int Len, int SocketState)
{
	if(SocketState == SOCKET_GAME && ClientGameCryptType == CRYPT_TFISH)
		MD5Encrypt(&MD5ToClient, (unsigned char *)Buf, (unsigned char *)Buf, Len);

	return;
}

/* ENCRYPTION EMULATION WITH SERVER */
void DecryptFromServer(char *Buf, int Len, int SocketState)
{
	/* the infamous md5 decrypt */
	if(SocketState == SOCKET_GAME && IRWGameCryptType == CRYPT_TFISH)
		MD5Encrypt(&MD5FromServer, (unsigned char *)Buf, (unsigned char *)Buf, Len);

	return;
}

void EncryptToServer(char *Buf, int Len, int SocketState)
{
	/* nothing to do on login socket */
	if(SocketState == SOCKET_LOGIN && IRWLoginCryptType == CRYPT_NONE)
		return;

	/* nothing to do on game socket */
	if(SocketState == SOCKET_GAME && IRWGameCryptType == CRYPT_NONE)
		return;

	/* login encrypt if necessary */
	if(SocketState == SOCKET_LOGIN && IRWLoginCryptType != CRYPT_NONE)
		LoginCryptEncrypt(&LCToServer, (unsigned char*)Buf, (unsigned char*)Buf, Len);

	/* game encrypt if necessary */
	/* blowfish first cause of blowfish+twofish encryption */
	if(SocketState == SOCKET_GAME && (IRWGameCryptType == CRYPT_BFISH || IRWGameCryptType == CRYPT_BTFISH))
		BlowfishEncrypt(&BCToServer, (unsigned char*)Buf, (unsigned char*)Buf, Len);

	if(SocketState == SOCKET_GAME && (IRWGameCryptType == CRYPT_TFISH || IRWGameCryptType == CRYPT_BTFISH))
		TwofishEncrypt(&TCToServer, (unsigned char*)Buf, (unsigned char*)Buf, Len);

	return;
}
