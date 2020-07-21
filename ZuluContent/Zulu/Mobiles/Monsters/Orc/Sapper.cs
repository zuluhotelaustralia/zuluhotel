using System;
using System.Collections.Generic;
using Server;
using Server.Misc;
using Server.Items;
using static Server.Mobiles.CreatureProp;
using Server.Engines.Magic;
using Server.Engines.Harvest;

namespace Server.Mobiles
{
  public class Sapper : BaseCreature
  {
    static Sapper() => CreatureProperties.Register<Sapper>(new CreatureProperties
    {
      // CProp_keywordbattlecry = sMiib mek da oomie lubbers smokkee!,
      // DataElementId = orcbomber,
      // DataElementType = NpcTemplate,
      // dstart = 10,
      // Equip = orcbomber,
      // Graphic = 0x1438 /* Weapon */,
      // Hitscript = :combat:bomberhitscript /* Weapon */,
      // HitSound = 0x16D /* Weapon */,
      // hostile = 1,
      // lootgroup = 42,
      // MagicItemChance = 6,
      // Magicitemlevel = 3,
      // MissSound = 0x239 /* Weapon */,
      // script = killpcs,
      // speech = 6,
      // Speed = 50 /* Weapon */,
      // TrueColor = 1295,
      ActiveSpeed = 0.2,
      AiType = AIType.AI_Melee /* killpcs */,
      AlwaysMurderer = true,
      BardImmune = true,
      Body = 0x11,
      CorpseNameOverride = "corpse of <random> the Sapper",
      CreatureType = CreatureType.Orc,
      DamageMax = 8,
      DamageMin = 1,
      Dex = 90,
      Female = false,
      FightMode = FightMode.Aggressor,
      FightRange = 1,
      HitsMax = 205,
      Hue = 1295,
      Int = 30,
      ManaMaxSeed = 0,
      Name = "<random> the Sapper",
      PassiveSpeed = 0.4,
      PerceptionRange = 10,
      Skills = new Dictionary<SkillName, CreatureProp>
      {
        {SkillName.Tactics, 300},
        {SkillName.Macing, 300},
      },
      StamMaxSeed = 80,
      Str = 205,
      Tamable = false,
      VirtualArmor = 45,
    });


    [Constructible]
    public Sapper() : base(CreatureProperties.Get<Sapper>())
    {
      // Add customization here

      AddItem(new WarHammer
      {
        Movable = false,
        Name = "Bomber weapon",
        Speed = 50,
        MaxHitPoints = 250,
        HitPoints = 250,
        HitSound = 0x16D,
        MissSound = 0x239,
      });
    }

    [Constructible]
    public Sapper(Serial serial) : base(serial)
    {
    }


    public override void Serialize(IGenericWriter writer)
    {
      base.Serialize(writer);
      writer.Write((int) 0);
    }

    public override void Deserialize(IGenericReader reader)
    {
      base.Deserialize(reader);
      int version = reader.ReadInt();
    }
  }
}
