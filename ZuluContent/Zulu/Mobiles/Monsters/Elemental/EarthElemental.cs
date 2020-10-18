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
    public class EarthElemental : BaseCreature
    {
        static EarthElemental()
        {
            CreatureProperties.Register<EarthElemental>(new CreatureProperties
            {
                // CProp_EarthProtection = i5,
                // DataElementId = earthelemental,
                // DataElementType = NpcTemplate,
                // dstart = 10,
                // Equip = earthelemental,
                // Graphic = 0x0ec4 /* Weapon */,
                // HitSound = 0x10F /* Weapon */,
                // hostile = 1,
                // lootgroup = 21,
                // MagicItemChance = 25,
                // MagicItemLevel = 3,
                // MissSound = 0x239 /* Weapon */,
                // script = killpcs,
                // Speed = 35 /* Weapon */,
                // TrueColor = 33784,
                ActiveSpeed = 0.2,
                AiType = AIType.AI_Melee /* killpcs */,
                AlwaysMurderer = true,
                Body = 0x0e,
                CorpseNameOverride = "corpse of an earth elemental",
                CreatureType = CreatureType.Elemental,
                DamageMax = 64,
                DamageMin = 8,
                Dex = 50,
                Female = false,
                FightMode = FightMode.Aggressor,
                FightRange = 1,
                HitsMax = 230,
                Hue = 33784,
                Int = 45,
                ManaMaxSeed = 35,
                Name = "an earth elemental",
                PassiveSpeed = 0.4,
                PerceptionRange = 10,
                Skills = new Dictionary<SkillName, CreatureProp>
                {
                    {SkillName.Parry, 75},
                    {SkillName.MagicResist, 40},
                    {SkillName.Tactics, 90},
                    {SkillName.Macing, 120}
                },
                StamMaxSeed = 40,
                Str = 230,
                VirtualArmor = 35
            });
        }


        [Constructible]
        public EarthElemental() : base(CreatureProperties.Get<EarthElemental>())
        {
            // Add customization here

            AddItem(new SkinningKnife
            {
                Movable = false,
                Name = "Earth Elemental Weapon",
                Speed = 35,
                MaxHitPoints = 250,
                HitPoints = 250,
                HitSound = 0x10F,
                MissSound = 0x239
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
        public EarthElemental(Serial serial) : base(serial)
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