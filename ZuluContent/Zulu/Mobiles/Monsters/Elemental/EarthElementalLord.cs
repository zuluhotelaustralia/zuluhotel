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
    public class EarthElementalLord : BaseCreature
    {
        static EarthElementalLord()
        {
            CreatureProperties.Register<EarthElementalLord>(new CreatureProperties
            {
                // CProp_EarthProtection = i8,
                // DataElementId = earthelementallord,
                // DataElementType = NpcTemplate,
                // Equip = earthelementallord,
                // Graphic = 0x0ec4 /* Weapon */,
                // HitSound = 0x10F /* Weapon */,
                // hostile = 1,
                LootTable = "74",
                LootItemChance = 60,
                LootItemLevel = 4,
                // MissSound = 0x239 /* Weapon */,
                // script = killpcs,
                // Speed = 35 /* Weapon */,
                // TrueColor = 1538,
                ActiveSpeed = 0.2,
                AiType = AIType.AI_Melee /* killpcs */,
                AlwaysMurderer = true,
                Body = 0x0e,
                CorpseNameOverride = "corpse of an earth elemental lord",
                CreatureType = CreatureType.Elemental,
                DamageMax = 45,
                DamageMin = 21,
                Dex = 150,
                Female = false,
                FightMode = FightMode.Aggressor,
                FightRange = 1,
                HitsMax = 450,
                Hue = 1538,
                Int = 55,
                ManaMaxSeed = 0,
                Name = "an earth elemental lord",
                PassiveSpeed = 0.4,
                PerceptionRange = 10,
                Skills = new Dictionary<SkillName, CreatureProp>
                {
                    {SkillName.Tactics, 150},
                    {SkillName.Macing, 175},
                    {SkillName.MagicResist, 65}
                },
                StamMaxSeed = 200,
                Str = 450,
                VirtualArmor = 50
            });
        }


        [Constructible]
        public EarthElementalLord() : base(CreatureProperties.Get<EarthElementalLord>())
        {
            // Add customization here

            AddItem(new SkinningKnife
            {
                Movable = false,
                Name = "Earth Elemental Lord Weapon",
                Speed = 35,
                MaxHitPoints = 250,
                HitPoints = 250,
                HitSound = 0x10F,
                MissSound = 0x239
            });
        }

        [Constructible]
        public EarthElementalLord(Serial serial) : base(serial)
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