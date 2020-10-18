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
    public class EnergyVortex : BaseCreature
    {
        static EnergyVortex()
        {
            CreatureProperties.Register<EnergyVortex>(new CreatureProperties
            {
                // CProp_nocorpse = i1,
                // DataElementId = vortex1,
                // DataElementType = NpcTemplate,
                // dstart = 10,
                // Equip = vortex1,
                // food = meat,
                // Graphic = 0x0ec4 /* Weapon */,
                // Hitscript = :combat:poisonhit /* Weapon */,
                // HitSound = 0x212 /* Weapon */,
                // hostile = 1,
                // MissSound = 0x239 /* Weapon */,
                // script = vortexai,
                // Speed = 25 /* Weapon */,
                // TrueColor = 205,
                ActiveSpeed = 0.2,
                AiType = AIType.AI_Melee /* vortexai */,
                AlwaysMurderer = true,
                Body = 0x23d,
                CanSwim = true,
                CorpseNameOverride = "corpse of an energy vortex",
                CreatureType = CreatureType.Elemental,
                DamageMax = 20,
                DamageMin = 5,
                Dex = 190,
                Female = false,
                FightMode = FightMode.Aggressor,
                FightRange = 1,
                HitPoison = Poison.Regular,
                HitsMax = 350,
                Hue = 0,
                Int = 110,
                ManaMaxSeed = 125,
                MinTameSkill = 130,
                Name = "an energy vortex",
                PassiveSpeed = 0.4,
                PerceptionRange = 10,
                Skills = new Dictionary<SkillName, CreatureProp>
                {
                    {SkillName.Poisoning, 100},
                    {SkillName.MagicResist, 150},
                    {SkillName.Tactics, 125},
                    {SkillName.Macing, 200}
                },
                StamMaxSeed = 80,
                Str = 350,
                Tamable = true,
                VirtualArmor = 30
            });
        }


        [Constructible]
        public EnergyVortex() : base(CreatureProperties.Get<EnergyVortex>())
        {
            // Add customization here

            AddItem(new SkinningKnife
            {
                Movable = false,
                Name = "Vortex Weapon",
                Speed = 25,
                MaxHitPoints = 250,
                HitPoints = 250,
                HitSound = 0x212,
                MissSound = 0x239
            });
        }

        [Constructible]
        public EnergyVortex(Serial serial) : base(serial)
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