/******************************************************************************\
* 
* 
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

#ifndef _UOJOURNAL_H_INCLUDED
#define _UOJOURNAL_H_INCLUDED


#define JOURNAL_SIZE 100

#define JOURNAL_SPEECH 0
#define JOURNAL_SYSMSG 1
#define JOURNAL_EMOTE 2 /* adds *'s as part of text */
#define JOURNAL_YOUSEE 6 /* You see: */
#define JOURNAL_EMPHASIS 7 /* clears previous messages */
#define JOURNAL_WHISPER 8
#define JOURNAL_YELL 9
#define JOURNAL_SPELL 10
#define JOURNAL_NONE -1
#define JOURNAL_ANY -2

typedef struct tagJournalEntry
{
	int Type;
	char *Text;
}JournalEntry;

void CleanJournal(void);
void JournalAdd(char *Speaker, char *Text, int Type);
void JournalRemove(unsigned int Line);
char* GetJournalLine(unsigned int Line);
char * JournalGetLast(void);
char * GetJournalLine(unsigned int Line);
void JournalDump(char * Filename);
void SetJournalLine(unsigned int Line, char *Replace);
unsigned int IsInJournal(char *Text, int Type);

void IRWCmd_IsInJournal(char **Arg, int ArgCount);
void IRWCmd_JournalDump(char **Arg, int ArgCount);
void IRWCmd_GetLastJournalLine(char **Arg, int ArgCount);
void IRWCmd_CleanJournal(char **Arg, int ArgCount);

#endif 
