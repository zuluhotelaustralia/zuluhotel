/******************************************************************************\
* 
* 
*  Modified 2005 Daniel 'Cavalcanti' Necr0Potenc3
*  Copyright (C) 2005 Maciek 'ziemniaq' Wiercinski
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
*    March 1st, 2005 -- journal handling for IRW
*
\******************************************************************************/

#include <windows.h>
#include <string.h>
#include <stdio.h>
#include "UOProtocol.h"
#include "UOPacketHandler.h"
#include "UOJournal.h"
#include "UOWorld.h"
#include "Registry.h"

static JournalEntry Journal[JOURNAL_SIZE];
static BOOL JournalReady = FALSE;
static unsigned int JournalCount = 0;

void CleanJournal(void)
{
	int i = 0;
		
	for(i = 0; i < JOURNAL_SIZE; i++)
	{
		Journal[i].Text = NULL;
		Journal[i].Type = JOURNAL_NONE;
	}

	JournalCount = 0;
	JournalReady = TRUE;

	/* if the player is in the world say the log was cleared */
	if(GetPlayerSerial() != INVALID_SERIAL)
		ClientPrint("Journal cleared");

	return;
}

void JournalAdd(char *Speaker, char *Text, int Type)
{ 
	int Len = 0;

	if(JournalReady == FALSE)
		return;

	while(JournalCount >= JOURNAL_SIZE - 1)
		JournalRemove(0); /* delete the oldest entry */

	if(Journal[JournalCount].Text)
	{
		free(Journal[JournalCount].Text);
		Journal[JournalCount].Text = NULL;
	}

	/* format is: "Speaker: Text" */
	Len = (int)(strlen(Text) & 0xFFFFFFFF);
	if(Speaker) Len += (int)(strlen(Speaker) & 0xFFFFFFFF);

	/* allocate text + space for ": " and the null char */
	Journal[JournalCount].Text = (char*)malloc(Len + 3);

	/* copy the text */
	if(Speaker) sprintf(Journal[JournalCount].Text, "%s: %s", Speaker, Text);
	else strncpy(Journal[JournalCount].Text, Text, Len);
	/* NULL on end of allocated area.. just for safety :) */
	Journal[JournalCount].Text[Len] = 0;
	Journal[JournalCount].Type = Type;
	JournalCount++;

	/* good for debugging but it spams players like hell ;) */
	/*if(GetPlayerSerial() != INVALID_SERIAL)
		ClientPrint("Journal lines: %d, added: %s", JournalCount, GetJournalLine(JournalCount-1));*/

	return;
}

void JournalRemove(unsigned int Line)
{ 
	if(JournalReady == FALSE)
		return;

	if(!JournalCount)
		return;

	if(Journal[Line].Text != NULL)
		free(Journal[Line].Text);
    
	memmove(Journal + Line, Journal + Line + 1, sizeof(JournalEntry) * (JournalCount - Line - 1));

	JournalCount--;
	Journal[JournalCount].Text = NULL;
	Journal[JournalCount].Type = JOURNAL_NONE;

	return;
}

char* GetJournalLine(unsigned int Line)
{
    if(JournalReady == FALSE)
		return NULL;

    if(Line >= JournalCount)
		return "Error: No such line in journal"; 
  
    return Journal[Line].Text; 
}

char* JournalGetLast(void)
{
	if(!JournalCount && !strcmp(GetJournalLine(JournalCount), ""))
		return "Error: No lines in journal";
	else
		return GetJournalLine(JournalCount);
} 

void JournalDump(char *Filename)
{ 
	FILE *d = NULL;
	char DumpPath[4096];
	unsigned int i = 0;

	if(Filename == NULL)
		return;

    strcpy(DumpPath, GetIRWPath());
	strcat(DumpPath, "\\");
	strcat(DumpPath, Filename);

	d = fopen(DumpPath, "at");

	if(!d)
		ClientPrint("Error: Couldnt dump to %s", DumpPath);

	for(i = 0; i < JournalCount; i++) 
		fprintf(d, "\n%s", GetJournalLine(i));

	fclose(d);
	return;
}

void SetJournalLine(unsigned int Line, char *Replace)
{ 
	int Len = 0;

	if(JournalReady == FALSE)
		return;

	if(Line >= JOURNAL_SIZE)
		return;

	if(Replace == NULL)
		return;

	/* if its long its probably bad, and 512 is so nice number */ 
	Len = (int)(strlen(Replace) & 0xFFFFFFFF);
	if(Len > 512 ) return; 

	if(Journal[Line].Text)
		free(Journal[Line].Text);

	Journal[Line].Text = (char *)malloc(sizeof(char)*(Len + 1)); 
	strncpy(Journal[Line].Text, Replace, Len + 1);
	/* in case it wasnt NULL terminated string */
	Journal[Line].Text[Len] = 0;

	return;
} 

unsigned int IsInJournal(char *Text, int Type)
{ 
	unsigned int i = 0;

	if(Text == NULL)
		return 0;

	for(i = 0; i < JournalCount; i++)
	{
		/* if we're looking for certain message type */
		if(Type != JOURNAL_ANY && Type != Journal[i].Type)
			continue;
		
		if(strstr(Journal[i].Text, Text) != NULL)
			return i;
	}

	return -1;
} 

void IRWCmd_IsInJournal(char **Arg, int ArgCount)
{
	if(ArgCount < 2)
	{
		ClientPrint("Usage: isinjournal text");
		return;
	}

	if(IsInJournal(Arg[1], JOURNAL_ANY) != -1)
		ClientPrint("Oh.. yes. It is in journal.. so what?");
	else
		ClientPrint("Sorry its not in journal ;("); 

	return;
}

void IRWCmd_JournalDump(char **Arg, int ArgCount)
{ 
	if(ArgCount < 2)
	{ 
		ClientPrint("Usage: journaldump [filename]"); 
		return; 
	} 

	JournalDump(Arg[1]); 
	ClientPrint("Dumped to your IRW directory"); 
	return;
}

void IRWCmd_GetLastJournalLine(char **Arg, int ArgCount)
{
	ClientPrint(JournalGetLast());
	return;
}

void IRWCmd_CleanJournal(char **Arg, int ArgCount)
{
	CleanJournal();
	return;
}