# RunZH

RunZH is an emulator for the popular Zulu Hotel series of Ultima Online shards.  RunZH is based on RunUO (github.com/runuo/runuo), many thanks to Mark Sturgill.  RunZH targets Microsoft's new .NET Core platform, and Mono- or Windows-specific builds are deprecated.  Assuming your dotnet core runtime is set up properly, building RunZH should be as simple as:

```
$ make release
$ dotnet Server.dll
```

## Setup

Using your text editor of choice, edit Server/Scripts/Misc/DataPath.cs and change the CustomPath variable to be the full, absolute path to the directory containing the .mul files, e.g.:

```
private static string CustomPath = "/home/runzh/server/muls/";
```

### GatherSystem Setup

Once your server is compiled and running, the Gathering system requires some one-time setup to be performed by a character with Developer-level access.  You will notice in the console spam of a first-time-running server some warnings about node controllers not being set.

First, add three GatherSystemController objects:

```
[add gathersystemcontroller
```

Then make them static:

```
[set movable false
```

Next, set the system references by issuing the following command three times, with different numeric arguments:

```
[setgathersystem <1, 2 or 3>
```

At this point your Gathering engine is fully set up and you should issue a save command.

```
[save
```

## IRC

The official channel of RunZH is on irc.freenode.net in the #zuluhotel channel.
