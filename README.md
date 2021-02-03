<p align="center">
    <img src="https://i.imgur.com/KNDuNQs.png" width="200" height="200"  alt="Ultima Online ZHA Logo">
</p>

This README is just a fast *quick overview* document. You can find more detailed documentation in our [Getting Started](https://zuluhotel.com.au/modernzhdocs/docs/getting-started/) guide.

What is Modern Zuluhotel?
--------------
Modern Zuluhotel is an open-source custom server for Ultima Online based on the old Zuluhotel scripts running on-top of the [ModernUO](https://github.com/modernuo/ModernUO/) emulator.
Zuluhotel is an alternative ruleset for UO with diablo-like classes and magic systems with its original release dating back to on or before 2003. 

What is the point of this project?
--------------
The projects aim is to create a faithful reimplementation of the beloved but spaghetti code mess "Zuluhotel" servers using modern technology.
ModernUO and other modern emulators offer a greater degree of flexibility and stability previously possible and new open-source recreations of the UO client like [ClassicUO](https://github.com/andreakarasho/ClassicUO) show there's still great potential to revive an old classic.

Why don't you just use the POL scripts?
--------------

Undeniably there has been dozens if not hundreds of great Zuluhotel shards across the globe running POL and eScript from the beginning with ZHC to ZHBR and ZHA. 

There still exists servers today attracting hundreds of concurrent players. However the scripts those servers run on and the publicly available ones have been made over almost 2 decades by anonymous authors with varying degrees of quality control (usually none) and wildly different visions. 
It's the definition of technical-debt where improving or adding new features often requires ripping out and replacing old code with your own flavour, continuing the cycle. Even then most scripts available are created for a POL version several years out of date containing many unfixable POL core bugs. 

From a technical stand-point: Untangling the spaghetti, cleaning up all the bad code, ripping it back to a baseline, and then updating the scripts to the latest POL requires a huge amount of effort that nears a total rewrite. 
POL whilst a great battle-tested emulator is now showing it's age, namely with it's eScript language. eScript has fallen drastically behind modern languages on performance, ease of use, and features like package management, type-safety, debuggers, editor support, etc.
Continuing to use POL is a non-starter for a rewrite when there exists equivalent or better emulators written in C# like ModernUO.

From an open-source project standpoint: We want to attract as many contributors as possible and grow a community. RunUO/ServUO/ModernUO and by extension C# has orders of magnitude more developers proficient in it than POL's eScript making it again an obvious choice.

From a community standpoint: With possibility of many more contributors, keeping the code open-source, and a high-quality project we hope to foster a new flame of people playing UO on the Zuluhotel-style servers we love.

What functionality will the first version have?
--------------
Right now [our first milestone is to reach a playable MVP](https://github.com/zuluhotelaustralia/zuluhotel/milestone/1) that looks and feels like the ZH3 era of servers (circa around 2003-2005)

The base code for the ruleset is from the [RunUO.T2A](https://github.com/Grimoric/RunUO.T2A) project by Grimoric which aggressively cuts out anything post-T2A.
That gave us a good base to selectively bring over the Zuluhotel functionality from [Daleron & Sith's](https://zuluhotel.ca/) now abandoned RunUO version.

We're attempting to be as accurate as possible with reproducing the behaviour in the POL-based scripts and to that end we are limiting the initial scope to the functionality in the [ZH3 scripts](https://github.com/zuluhotelaustralia/uoaus).
To keep the project on-track we won't be implementing any features from later ZH releases until the first milestone is finished (e.g. No racial benefits, non-standard GM items, champion spawn systems etc.). 

One exception is when we think it's easier to implement a newer and better system than recreate an old messy one, e.g. we won't be rewriting the entire combat system to behave exactly like POLs, instead we'll tweak it to be very similar.
However we do welcome all ideas, and encourage you to create issues detailing your ideas/improvements/feature requests, or discuss with us on discord.

Initial features implemented from ZH3 will include (but not limited to):

 - Classes (Warrior, Bard, Mage, Thief, Crafter, Ranger)
 - Skills refactored from OSI to behave like their POL version counterparts
 - Loot group system
 - All magical enchantments
 - Standard GM items (Omero's Pickaxe, Xarafax Axe, Katana of Kieri etc.)
 - OSI resources replaced with Zulu ores/ingots/logs
 - Modernized (gumps etc) crafting systems
 - Necro/Earth magic
 - Elemental pentagram system (weapons/armour etc)
 - Creatures (golden dragons, great wyrms and the like)
 - And more!

*Note: if we're missing anything of consequence please create an issue to add it*

What do I do now?
-------------
Head over to our documentation site and read through our [Getting Started](https://zuluhotel.com.au/modernzhdocs/docs/getting-started/) guide, or come [join our discord](https://discord.gg/TNtDtK2sG4) to chat with us!

Credits
--------------
1. [Zulu Hotel Canada](https://zuluhotel.ca/) (Daleron & Sith) for the initial open source release that this shard has used as a starting point and now continues.
2. [RunUO.T2A](https://github.com/Grimoric/RunUO.T2A) for the initial T2A base that was used for layering on the Zuluhotel features.
3. [ModernUO](https://github.com/modernuo/ModernUO/) for being our core emulator
4. [ClassicUO](https://github.com/andreakarasho/ClassicUO) for being our target client