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
    public class Anaconda : BaseCreature
    {
        static Anaconda()
        {
            CreatureProperties.Register<Anaconda>(new CreatureProperties
            {
                // CProp_BaseHpRegen = i1000,
                // CProp_EarthProtection = i8,
                // CProp_NecroProtection = i8,
                // CProp_PermMagicImmunity = i8,
                // DataElementId = anaconda,
                // DataElementType = NpcTemplate,
                // dstart = 10,
                // Equip = behemoth,
                // Graphic = 0x0ec4 /* Weapon */,
                // Hitscript = :combat:banishscript /* Weapon */,
                // HitSound = 0x16D /* Weapon */,
                // hostile = 1,
                // MissSound = 0x239 /* Weapon */,
                // script = killpcsTeleporter,
                // Speed = 50 /* Weapon */,
                // Swordsmanship = 155,
                // TrueColor = 1157  ,
                AiType = AIType.AI_Melee /* killpcsTeleporter */,
                AlwaysMurderer = true,
                AutoDispel = true,
                Body = 0x15,
                CorpseNameOverride = "corpse of the Anaconda",
                CreatureType = CreatureType.Animal,
                DamageMax = 60,
                DamageMin = 10,
                Dex = 500,
                Female = false,
                FightMode = FightMode.Aggressor,
                FightRange = 1,
                HitsMax = 2250,
                Hue = 1157,
                Int = 0,
                ManaMaxSeed = 0,
                Name = "the Anaconda",
                PerceptionRange = 10,
                Resistances = new Dictionary<ElementalType, CreatureProp>
                {
                    {ElementalType.Fire, 100},
                    {ElementalType.Air, 100},
                    {ElementalType.Water, 100}
                },
                Skills = new Dictionary<SkillName, CreatureProp>
                {
                    {SkillName.MagicResist, 200},
                    {SkillName.Tactics, 250},
                    {SkillName.Poisoning, 95},
                    {SkillName.DetectHidden, 200},
                    {SkillName.Hiding, 200}
                },
                StamMaxSeed = 400,
                Str = 2500,
                TargetAcquireExhaustion = true,
                VirtualArmor = 45
            });
        }


        [Constructible]
        public Anaconda() : base(CreatureProperties.Get<Anaconda>())
        {
            // Add customization here

            AddItem(new SkinningKnife
            {
                Movable = false,
                Name = "Behemoth Weapon",
                Speed = 50,
                MaxHitPoints = 250,
                HitPoints = 250,
                HitSound = 0x16D,
                MissSound = 0x239
            });
        }

        [Constructible]
        public Anaconda(Serial serial) : base(serial)
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