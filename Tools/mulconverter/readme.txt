==========================================================================
                            LegacyMULConverter by Eos
==========================================================================

INFORMATION

    LegacyMULCL supports two operations:

        1. Extracting all known UOP files in a given directory into MUL
        2. Repacking all known MUL files in a given directory into UOP

    LegacyMULCL will skip files that do not exist, allowing you to
    selectively convert files by copying them into a separate directory.

    If an output file already exists, it will also not be overwritten.

    Supported files are:

        artLegacyMUL.uop
        gumpartLegacyMUL.uop
        mapXLegacyMUL.uop (where X is /[0-5]x?/)
        soundLegacyMUL.uop


SYNTAX

    Extracting all UOP files in a directory:

        LegacyMULCL.exe -x <path>

    Repacking all MUL files in a directory:

        LegacyMULCL.exe <path>

    Opening the syntax instructions:

        LegacyMULCL.exe /?


UOFIDDLER

    If you are using this with UOFiddler, you will have to remove the
    UOFiddlerArt.hash file located in %APPDATA%\UoFiddler to regenerate
    the item list.

