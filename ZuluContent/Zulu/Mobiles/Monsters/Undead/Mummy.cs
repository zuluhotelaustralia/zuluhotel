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
    public class Mummy : BaseCreature
    {
        static Mummy()
        {
            CreatureProperties.Register<Mummy>(new CreatureProperties
            {
                // DataElementId = mummy,
                // DataElementType = NpcTemplate,
                // dstart = 10,
                // Equip = mummy,
                // Graphic = 0x0ec4 /* Weapon */,
                // HitSound = 0x1D8 /* Weapon */,
                // hostile = 1,
                // lootgroup = 59,
                // MagicItemChance = 1,
                // MagicItemLevel = 5,
                // MissSound = 0x239 /* Weapon */,
                // script = killpcs,
                // Speed = 40 /* Weapon */,
                // TrueColor = 0x0455,
                ActiveSpeed = 0.2,
                AiType = AIType.AI_Melee /* killpcs */,
                AlwaysMurderer = true,
                Body = 0x03,
                CorpseNameOverride = "corpse of a mummy",
                CreatureType = CreatureType.Undead,
                DamageMax = 64,
                DamageMin = 8,
                Dex = 50,
                Female = false,
                FightMode = FightMode.Aggressor,
                FightRange = 1,
                HitsMax = 250,
                Hue = 0x0455,
                Int = 25,
                ManaMaxSeed = 15,
                Name = "a mummy",
                PassiveSpeed = 0.4,
                PerceptionRange = 10,
                Resistances = new Dictionary<ElementalType, CreatureProp>
                {
                    {ElementalType.Poison, 100},
                    {ElementalType.Fire, 50}
                },
                Skills = new Dictionary<SkillName, CreatureProp>
                {
                    {SkillName.MagicResist, 80},
                    {SkillName.Tactics, 100},
                    {SkillName.Macing, 150}
                },
                StamMaxSeed = 50,
                Str = 250,
                VirtualArmor = 25
            });
        }


        [Constructible]
        public Mummy() : base(CreatureProperties.Get<Mummy>())
        {
            // Add customization here

            AddItem(new SkinningKnife
            {
                Movable = false,
                Name = "Mummy Weapon",
                Speed = 40,
                MaxHitPoints = 250,
                HitPoints = 250,
                HitSound = 0x1D8,
                MissSound = 0x239
            });
        }

        [Constructible]
        public Mummy(Serial serial) : base(serial)
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
            var version = reader.ReadInt();
        }
    }
}