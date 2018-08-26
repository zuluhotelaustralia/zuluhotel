# Microsoft Developer Studio Project File - Name="Injection_vc" - Package Owner=<4>
# Microsoft Developer Studio Generated Build File, Format Version 6.00
# ** DO NOT EDIT **

# TARGTYPE "Win32 (x86) Dynamic-Link Library" 0x0102

CFG=Injection_vc - Win32 Debug
!MESSAGE This is not a valid makefile. To build this project using NMAKE,
!MESSAGE use the Export Makefile command and run
!MESSAGE 
!MESSAGE NMAKE /f "Injection_vc.mak".
!MESSAGE 
!MESSAGE You can specify a configuration when running NMAKE
!MESSAGE by defining the macro CFG on the command line. For example:
!MESSAGE 
!MESSAGE NMAKE /f "Injection_vc.mak" CFG="Injection_vc - Win32 Debug"
!MESSAGE 
!MESSAGE Possible choices for configuration are:
!MESSAGE 
!MESSAGE "Injection_vc - Win32 Release" (based on "Win32 (x86) Dynamic-Link Library")
!MESSAGE "Injection_vc - Win32 Debug" (based on "Win32 (x86) Dynamic-Link Library")
!MESSAGE 

# Begin Project
# PROP AllowPerConfigDependencies 0
# PROP Scc_ProjName ""
# PROP Scc_LocalPath ""
CPP=cl.exe
MTL=midl.exe
RSC=rc.exe

!IF  "$(CFG)" == "Injection_vc - Win32 Release"

# PROP BASE Use_MFC 0
# PROP BASE Use_Debug_Libraries 0
# PROP BASE Output_Dir "Release"
# PROP BASE Intermediate_Dir "Release"
# PROP BASE Target_Dir ""
# PROP Use_MFC 0
# PROP Use_Debug_Libraries 0
# PROP Output_Dir "Release"
# PROP Intermediate_Dir "Release"
# PROP Ignore_Export_Lib 0
# PROP Target_Dir ""
# ADD BASE CPP /nologo /MT /W3 /GX /O2 /D "WIN32" /D "NDEBUG" /D "_WINDOWS" /D "_MBCS" /D "_USRDLL" /D "INJECTION_VC_EXPORTS" /YX /FD /c
# ADD CPP /nologo /MT /W3 /GX /Zi /O2 /I "script" /D "NDEBUG" /D VERSION=0.3.30.4 /D "WIN32" /D "_WINDOWS" /D "_MBCS" /D "_USRDLL" /D "INJECTION_VC_EXPORTS" /FR /YX /J /FD /c
# ADD BASE MTL /nologo /D "NDEBUG" /mktyplib203 /win32
# ADD MTL /nologo /D "NDEBUG" /mktyplib203 /win32
# ADD BASE RSC /l 0x419 /d "NDEBUG"
# ADD RSC /l 0x409 /d "NDEBUG"
BSC32=bscmake.exe
# ADD BASE BSC32 /nologo
# ADD BSC32 /nologo
LINK32=link.exe
# ADD BASE LINK32 kernel32.lib user32.lib gdi32.lib winspool.lib comdlg32.lib advapi32.lib shell32.lib ole32.lib oleaut32.lib uuid.lib odbc32.lib odbccp32.lib /nologo /dll /machine:I386
# ADD LINK32 expat.lib comctl32.lib ws2_32.lib kernel32.lib user32.lib gdi32.lib winspool.lib comdlg32.lib advapi32.lib shell32.lib ole32.lib oleaut32.lib uuid.lib odbc32.lib odbccp32.lib /nologo /base:"0x21000000" /dll /debug /machine:I386 /out:"Release/Injection.dll" /libpath:"g:\openssl\openssl-0.9.6b\out32dll\Release"

!ELSEIF  "$(CFG)" == "Injection_vc - Win32 Debug"

# PROP BASE Use_MFC 0
# PROP BASE Use_Debug_Libraries 1
# PROP BASE Output_Dir "Debug"
# PROP BASE Intermediate_Dir "Debug"
# PROP BASE Target_Dir ""
# PROP Use_MFC 0
# PROP Use_Debug_Libraries 1
# PROP Output_Dir "Debug"
# PROP Intermediate_Dir "Debug"
# PROP Ignore_Export_Lib 0
# PROP Target_Dir ""
# ADD BASE CPP /nologo /MTd /W3 /Gm /GX /ZI /Od /D "WIN32" /D "_DEBUG" /D "_WINDOWS" /D "_MBCS" /D "_USRDLL" /D "INJECTION_VC_EXPORTS" /YX /FD /GZ /c
# ADD CPP /nologo /MTd /W3 /Gm /GX /ZI /Od /I ".\script\\" /D "_DEBUG" /D VERSION=0.3.30.3 /D "WIN32" /D "_WINDOWS" /D "_MBCS" /D "_USRDLL" /D "INJECTION_VC_EXPORTS" /FR /YX /J /FD /GZ /c
# ADD BASE MTL /nologo /D "_DEBUG" /mktyplib203 /win32
# ADD MTL /nologo /D "_DEBUG" /mktyplib203 /win32
# ADD BASE RSC /l 0x419 /d "_DEBUG"
# ADD RSC /l 0x419 /d "_DEBUG"
BSC32=bscmake.exe
# ADD BASE BSC32 /nologo
# ADD BSC32 /nologo
LINK32=link.exe
# ADD BASE LINK32 kernel32.lib user32.lib gdi32.lib winspool.lib comdlg32.lib advapi32.lib shell32.lib ole32.lib oleaut32.lib uuid.lib odbc32.lib odbccp32.lib /nologo /dll /debug /machine:I386 /pdbtype:sept
# ADD LINK32 expat.lib comctl32.lib ws2_32.lib kernel32.lib user32.lib gdi32.lib winspool.lib comdlg32.lib advapi32.lib shell32.lib ole32.lib oleaut32.lib uuid.lib odbc32.lib odbccp32.lib /nologo /dll /debug /machine:I386 /out:"Debug/Injection.dll" /pdbtype:sept /libpath:"g:\openssl\openssl-0.9.6b\out32dll\Debug"

!ENDIF 

# Begin Target

# Name "Injection_vc - Win32 Release"
# Name "Injection_vc - Win32 Debug"
# Begin Group "Source Files"

# PROP Default_Filter "cpp;c;cxx;rc;def;r;odl;idl;hpj;bat"
# Begin Source File

SOURCE=.\common.cpp
# End Source File
# Begin Source File

SOURCE=.\crypt.cpp
# SUBTRACT CPP /YX
# End Source File
# Begin Source File

SOURCE=.\debug.cpp
# End Source File
# Begin Source File

SOURCE=.\equipment.cpp
# End Source File
# Begin Source File

SOURCE=.\extdll.cpp
# End Source File
# Begin Source File

SOURCE=.\generic_gump.cpp
# End Source File
# Begin Source File

SOURCE=.\gui.cpp
# End Source File
# Begin Source File

SOURCE=.\hooks.cpp
# End Source File
# Begin Source File

SOURCE=.\hotkeyhook.cpp
# End Source File
# Begin Source File

SOURCE=.\hotkeys.cpp
# End Source File
# Begin Source File

SOURCE=.\iconfig.cpp
# End Source File
# Begin Source File

SOURCE=.\ignition.cpp
# End Source File
# Begin Source File

SOURCE=.\igui.cpp
# End Source File
# Begin Source File

SOURCE=.\injection.cpp
# End Source File
# Begin Source File

SOURCE=.\injection.def
# End Source File
# Begin Source File

SOURCE=.\ld_main.cpp
# End Source File
# Begin Source File

SOURCE=.\menus.cpp
# End Source File
# Begin Source File

SOURCE=.\patch.cpp
# End Source File
# Begin Source File

SOURCE=.\runebook.cpp
# End Source File
# Begin Source File

SOURCE=.\skills.cpp
# End Source File
# Begin Source File

SOURCE=.\spells.cpp
# End Source File
# Begin Source File

SOURCE=.\target.cpp
# End Source File
# Begin Source File

SOURCE=.\TWOFISH2.C
# SUBTRACT CPP /YX
# End Source File
# Begin Source File

SOURCE=.\uo_huffman.cpp
# End Source File
# Begin Source File

SOURCE=.\vendor.cpp
# End Source File
# Begin Source File

SOURCE=.\world.cpp
# End Source File
# End Group
# Begin Group "Header Files"

# PROP Default_Filter "h;hpp;hxx;hm;inl"
# Begin Source File

SOURCE=.\client.h
# End Source File
# Begin Source File

SOURCE=.\common.h
# End Source File
# Begin Source File

SOURCE=.\crypt.h
# End Source File
# Begin Source File

SOURCE=.\equipment.h
# End Source File
# Begin Source File

SOURCE=.\extdll.h
# End Source File
# Begin Source File

SOURCE=.\generic_gump.h
# End Source File
# Begin Source File

SOURCE=.\gui.h
# End Source File
# Begin Source File

SOURCE=.\hashstr.h
# End Source File
# Begin Source File

SOURCE=.\hooks.h
# End Source File
# Begin Source File

SOURCE=.\hotkeyhook.h
# End Source File
# Begin Source File

SOURCE=.\hotkeys.h
# End Source File
# Begin Source File

SOURCE=.\iconfig.h
# End Source File
# Begin Source File

SOURCE=.\igui.h
# End Source File
# Begin Source File

SOURCE=.\injection.h
# End Source File
# Begin Source File

SOURCE=.\menus.h
# End Source File
# Begin Source File

SOURCE=.\patch.h
# End Source File
# Begin Source File

SOURCE=.\resrc1.h
# End Source File
# Begin Source File

SOURCE=.\runebook.h
# End Source File
# Begin Source File

SOURCE=.\skills.h
# End Source File
# Begin Source File

SOURCE=.\spells.h
# End Source File
# Begin Source File

SOURCE=.\target.h
# End Source File
# Begin Source File

SOURCE=.\uo_huffman.h
# End Source File
# Begin Source File

SOURCE=.\vendor.h
# End Source File
# Begin Source File

SOURCE=.\world.h
# End Source File
# End Group
# Begin Group "Resource Files"

# PROP Default_Filter "ico;cur;bmp;dlg;rc2;rct;bin;rgs;gif;jpg;jpeg;jpe"
# Begin Source File

SOURCE=.\injection.rc
# End Source File
# End Group
# Begin Group "txt"

# PROP Default_Filter "txt"
# Begin Source File

SOURCE=E:\Uo\inject\1.txt
# End Source File
# Begin Source File

SOURCE=E:\Uo\inject\injection_log.txt
# End Source File
# Begin Source File

SOURCE="..\wpguide-src\protocol.txt"
# End Source File
# End Group
# End Target
# End Project
