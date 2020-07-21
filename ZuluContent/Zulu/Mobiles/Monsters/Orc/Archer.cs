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
  public class Archer : BaseCreature
  {
    static Archer() => CreatureProperties.Register<Archer>(new CreatureProperties
    {
      // ammoamount = 30,
      // ammotype = 0xf3f,
      // DataElementId = orcarcher,
      // DataElementType = NpcTemplate,
      // dstart = 10,
      // Equip = arcarcher,
      // hostile = 1,
      // lootgroup = 52,
      // missileweapon = archer,
      // script = archerkillpcs,
      // speech = 6,
      // TrueColor = 0,
      ActiveSpeed = 0.2,
      AiType = AIType.AI_Archer /* archerkillpcs */,
      AlwaysMurderer = true,
      Body = 0x11,
      CorpseNameOverride = "corpse of <random> the archer",
      CreatureType = CreatureType.Orc,
      Dex = 180,
      Female = false,
      FightMode = FightMode.Closest,
      FightRange = 1,
      HitsMax = 170,
      Hue = 0,
      Int = 35,
      ManaMaxSeed = 25,
      Name = "<random> the archer",
      PassiveSpeed = 0.4,
      PerceptionRange = 10,
      ProvokeSkillOverride = 80,
      Skills = new Dictionary<SkillName, CreatureProp>
      {
        {SkillName.MagicResist, 30},
        {SkillName.Tactics, 80},
        {SkillName.Macing, 85},
        {SkillName.Archery, 85},
      },
      StamMaxSeed = 80,
      Str = 170,
    });


    [Constructible]
    public Archer() : base(CreatureProperties.Get<Archer>())
    {
      // Add customization here
    }

    [Constructible]
    public Archer(Serial serial) : base(serial)
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
