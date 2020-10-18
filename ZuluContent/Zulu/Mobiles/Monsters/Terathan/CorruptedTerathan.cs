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
    public class CorruptedTerathan : BaseCreature
    {
        static CorruptedTerathan()
        {
            CreatureProperties.Register<CorruptedTerathan>(new CreatureProperties
            {
                // DataElementId = corruptedterathan,
                // DataElementType = NpcTemplate,
                // dstart = 10,
                // Equip = corruptedterathan,
                // Graphic = 0x0ec4 /* Weapon */,
                // HitSound = 0x24D /* Weapon */,
                // hostile = 1,
                // lootgroup = 127,
                // MagicItemChance = 90,
                // MagicItemChance_0 = 10,
                // Magicitemlevel = 3,
                // MagicItemLevel_0 = 2,
                // MissSound = 0x24E /* Weapon */,
                // script = killpcs,
                // speech = 6,
                // Speed = 50 /* Weapon */,
                // TrueColor = 1304,
                ActiveSpeed = 0.2,
                AiType = AIType.AI_Melee /* killpcs */,
                AlwaysMurderer = true,
                Body = 0x46,
                CorpseNameOverride = "corpse of a Corrupted Terathan",
                CreatureType = CreatureType.Terathan,
                DamageMax = 43,
                DamageMin = 8,
                Dex = 250,
                Female = false,
                FightMode = FightMode.Aggressor,
                FightRange = 1,
                HitsMax = 400,
                Hue = 1304,
                Int = 110,
                ManaMaxSeed = 0,
                Name = "a Corrupted Terathan",
                PassiveSpeed = 0.4,
                PerceptionRange = 10,
                Skills = new Dictionary<SkillName, CreatureProp>
                {
                    {SkillName.Macing, 120},
                    {SkillName.Tactics, 100},
                    {SkillName.MagicResist, 100},
                    {SkillName.Parry, 100}
                },
                StamMaxSeed = 70,
                Str = 400,
                VirtualArmor = 30
            });
        }


        [Constructible]
        public CorruptedTerathan() : base(CreatureProperties.Get<CorruptedTerathan>())
        {
            // Add customization here

            AddItem(new SkinningKnife
            {
                Movable = false,
                Name = "Corrupted Terathan Weapon",
                Speed = 50,
                MaxHitPoints = 250,
                HitPoints = 250,
                HitSound = 0x24D,
                MissSound = 0x24E
            });

            AddItem(new HeaterShield
            {
                Movable = false,
                Name = "Shield AR20",
                BaseArmorRating = 20,
                MaxHitPoints = 400,
                HitPoints = 400
            });
        }

        [Constructible]
        public CorruptedTerathan(Serial serial) : base(serial)
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