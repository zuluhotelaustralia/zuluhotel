# RunZH

RunZH is an emulator for the popular Zulu Hotel series of Ultima Online shards.  RunZH is based on RunUO (github.com/runuo/runuo), many thanks to Mark Sturgill.  RunZH targets Microsoft's new .NET Core platform, and Mono- or Windows-specific builds are deprecated.  Assuming your dotnet core runtime is set up properly, building RunZH should be as simple as:

```
$ make
$ ./RunZH
```

## Setup

Using your text editor of choice, edit Server/Scripts/Misc/DataPath.cs and change the CustomPath variable to be the full, absolute path to the directory containing the .mul files, e.g.:

```
private static string CustomPath = "/home/runzh/server/muls/";
```

### GatherSystem Setup

The Gather System controls the rate at which players may acquire raw resources (logs, ore, and fish) based on programmed difficulty settings and dynamic distance of the player from the spawn point or GatherNode of each resource.  Each Node can drift around the map.  The subsystems for each resource category are static C# objects and as such cannot be directly serialized.  To work around this the location of the GatherNodes are stored in list collections on three control stones which are serialized when the server saves.  This allows the locations of GatherNodes to persist across server restarts.

As such, once your server is compiled and running, the Gathering system requires some one-time setup to be performed by a character with Developer-level access.  You will notice in the console spam of a first-time-running server some warnings about node controllers not being set.

First, add three GatherSystemController objects:

```
[add gathersystemcontroller
```

Next, set the system references by issuing the following command three times, with different numeric arguments:

```
[setgathersystem <1, 2 or 3>
```

This command should also dye the controller a corresponding colour for easy visual reference, set the item to be immovable, and finally name the stone according to which system it controls.  At this point your Gathering engine is fully set up and you should issue a save command.

```
[save
```

Should you wish to re-set the location of Gather Nodes simply delete each control stone, save, and restart the server.  The nodes will then respawn at random locations within 10 tiles of Lord British's throne, at which point you should repeat the setup process.

### World Setup

Next it's required to generate doors, signs, and invisible teleporters (for cave entrances, dungeon stairs, etc.) by issuing the following commands:

```
[doorgen
[decorate
[telgen
```

Note that the `doorgen` command uses very rudimentary heuristics to try to find door frames and then add the appropriate doors automagically.  In the developers' experience it is finicky, especially if using a custom map, as the frames may or may not exist.  It would be wise to manually inspect all the doors in the world and add them as necessary with the `adddoor` utility command.

### Next Steps

At this point you have a more-or-less playable world, lacking only appropriate spawns.  You should investigate the XMLSpawner system or else look into how the default spawners work.

## IRC

The official channel of RunZH is on irc.freenode.net in the #zuluhotel channel.
