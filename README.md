<p align="center">
    <img src="https://i.imgur.com/KNDuNQs.png" width="200" height="200"  alt="Ultima Online ZHA Logo">
</p>

An open source custom shard for Ultima Online, running on-top of the [ModernUO](https://github.com/modernuo/ModernUO/) emulator.

# Introduction

Zuluhotel for ModernUO is a custom set of functionality based on the old Penultima Online scripts that date back to 2003.

Custom functionality includes:

 - Classes (Warrior, Bard, Mage, Thief, Crafter, Ranger) with their own unique play styles and bonuses.
 - New resources with their own custom attributes
 - TODO
 
## Setup

**Tip:** To speed up load times you should clone the project to an SSD if you have one available.

You'll need to init the ModernUO git submodule when you first pull the project:
```bash
git submodule update --init --recursive
```

If you get a `(publickey)` error updating the submodules you can either setup [ssh-key git authentication](https://docs.github.com/en/github/authenticating-to-github/connecting-to-github-with-ssh) or rewrite the git url to https:
```bash
git config --global url."https://github.com/modernuo/ModernUO.git".insteadOf "git@github.com:modernuo/ModernUO.git"
```


### Building and running

#### Running locally

Change the `dataDirectories` entry in the `Distribution/Configuration/modernuo.json` to the root of your UO client installation directory (default is `/app/client/` for docker) e.g.
```json
  "dataDirectories": [
    "C:\\Program Files (x86)\\Ultima Online Classic\\"
  ],
```

Open a terminal in the root of the repository and build/publish the solution
```bash
dotnet publish
dotnet ModernUO/Distribution/ModernUO.dll
```

#### Running with docker

```bash
docker build . -t zuluhotelaustralia/zuluhotel
docker run --rm -it \
    --volume $HOME/zuluhotel/client/:/app/client/ \
    --volume $HOME/zuluhotel/Saves/:/app/Saves/ \
    -p 2593:2593 \
    zuluhotelaustralia/zuluhotel
```

### World Setup

Run these commands as an administrator after first login:

```bash
[decorate
[doorgen
[signgen
[generatespawners felucca.json
```

### Next Steps

At this point you will have a fully spawned world with NPCs. You may want to run the `[save` command to persist the world to disk. 

### Credits

1. [Zulu Hotel Canada](https://zuluhotel.ca/) (Daleron & Sith) for the initial open source release that this shard has used as a starting point and now continues.
2. [RunUO.T2A](https://github.com/Grimoric/RunUO.T2A) for the initial T2A base that was used for layering on the Zuluhotel features.
3. [ModernUO](https://github.com/modernuo/ModernUO/) for being our core emulator
4. [ClassicUO](https://github.com/andreakarasho/ClassicUO) for being our target client