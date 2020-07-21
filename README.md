<p align="center">
    <img src="https://i.imgur.com/KNDuNQs.png" width="200" height="200" >
</p>

An open source custom shard for Ultima Online, running ontop of the [ModernUO](https://github.com/modernuo/ModernUO/) emulator.

# Introduction

Zuluhotel for ModernUO is a custom set of functionality based on the old Penultima Online scripts that date back to 2003.

Custom functionality includes:

 - Classes (Warrior, Bard, Mage, Thief, Crafter, Ranger) with their own unique play styles and bonuses.
 - New resources with their own custom attributes
 - TODO
 
## Setup

You'll need to init the ModernUO git submodule when you first pull the project:
```bash
git submodule update --init --recursive
```

### GatherSystem Setup

TODO

### World Setup

TODO

### Next Steps

At this point you have a more-or-less playable world, lacking only appropriate spawns.  You should investigate the XMLSpawner system or else look into how the default spawners work.

### Credits

1. [Zulu Hotel Canada](https://zuluhotel.ca/) (Daleron & Sith) for the initial Open-source release that this shard has been inspired and continued.
2. [RunUO.T2A](https://github.com/Grimoric/RunUO.T2A) for the initial T2A base that was used for layering on the Zuluhotel features.
3. [ModernUO](https://github.com/modernuo/ModernUO/) for the core emulator
4. [ClassicUO](https://github.com/andreakarasho/ClassicUO) for being our target client