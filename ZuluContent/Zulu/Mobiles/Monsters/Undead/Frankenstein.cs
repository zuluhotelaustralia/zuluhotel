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
    public class Frankenstein : BaseCreature
    {
        static Frankenstein()
        {
            CreatureProperties.Register<Frankenstein>(new CreatureProperties
            {
                // DataElementId = Frankenstein,
                // DataElementType = NpcTemplate,
                // dstart = 10,
                // Equip = Frankenstein,
                // Graphic = 0x0ec4 /* Weapon */,
                // guardignore = 1,
                // HitSound = 0x25A /* Weapon */,
                // hostile = 1,
                LootTable = "37",
                LootItemChance = 15,
                LootItemLevel = 4,
                // MissSound = 0x239 /* Weapon */,
                // script = killpcs,
                // Speed = 35 /* Weapon */,
                // TrueColor = 0x599,
                ActiveSpeed = 0.2,
                AiType = AIType.AI_Melee /* killpcs */,
                AlwaysMurderer = true,
                Body = 0x190,
                CorpseNameOverride = "corpse of Frankenstein",
                CreatureType = CreatureType.Undead,
                DamageMax = 65,
                DamageMin = 25,
                Dex = 450,
                Female = false,
                FightMode = FightMode.Aggressor,
                FightRange = 1,
                HitsMax = 450,
                Hue = 0x599,
                Int = 450,
                ManaMaxSeed = 450,
                Name = "Frankenstein",
                PassiveSpeed = 0.4,
                PerceptionRange = 10,
                Resistances = new Dictionary<ElementalType, CreatureProp>
                {
                    {ElementalType.Poison, 6}
                },
                Skills = new Dictionary<SkillName, CreatureProp>
                {
                    {SkillName.Tactics, 130},
                    {SkillName.Macing, 130},
                    {SkillName.MagicResist, 90}
                },
                StamMaxSeed = 450,
                Str = 450,
                VirtualArmor = 40
            });
        }


        [Constructible]
        public Frankenstein() : base(CreatureProperties.Get<Frankenstein>())
        {
            // Add customization here

            AddItem(new ShortHair(Race.RandomHairHue())
            {
                Movable = false,
                Hue = 0x1
            });

            AddItem(new SkinningKnife
            {
                Movable = false,
                Name = "Frankenstein Weapon",
                Speed = 35,
                MaxHitPoints = 250,
                HitPoints = 250,
                HitSound = 0x25A,
                MissSound = 0x239
            });
        }

        [Constructible]
        public Frankenstein(Serial serial) : base(serial)
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