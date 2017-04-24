# RunZH

RunZH is an emulator for the popular Zulu Hotel series of Ultima Online shards.  RunZH is based on RunUO (github.com/runuo/runuo), many thanks to Mark Sturgill.  RunZH targets Microsoft's new .NET Core platform, and Mono- or Windows-specific builds are deprecated.  Assuming your dotnet core runtime is set up properly, building RunZH should be as simple as:

```
$ dotnet restore
$ dotnet run
```

## Setup

Using your text editor of choice, edit Server/Scripts/Misc/DataPath.cs and change the CustomPath variable to be the full, absolute path to the directory containing the .mul files, e.g.:

```
private static string CustomPath = "/home/runzh/server/muls/";
```

IRC:  irc.freenode.net #zuluhotel