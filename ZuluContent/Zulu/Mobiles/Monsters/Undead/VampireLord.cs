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
    public class VampireLord : BaseCreature
    {
        static VampireLord()
        {
            CreatureProperties.Register<VampireLord>(new CreatureProperties
            {
                // CProp_PermMagicImmunity = i4,
                // DataElementId = vampirelord,
                // DataElementType = NpcTemplate,
                // dstart = 10,
                // Equip = VampireLord,
                // Graphic = 0x0ec4 /* Weapon */,
                // guardignore = 1,
                // Hitscript = :combat:lifedrainscript /* Weapon */,
                // HitSound = 0x25A /* Weapon */,
                // hostile = 1,
                LootTable = "9",
                LootItemChance = 100,
                LootItemLevel = 7,
                // MissSound = 0x239 /* Weapon */,
                // script = killpcsTeleporter,
                // Speed = 35 /* Weapon */,
                // TrueColor = 0,
                AiType = AIType.AI_Melee /* killpcsTeleporter */,
                AlwaysMurderer = true,
                Body = 0x191,
                CorpseNameOverride = "corpse of a Vampire Lord",
                CreatureType = CreatureType.Undead,
                DamageMax = 73,
                DamageMin = 33,
                Dex = 500,
                Female = false,
                FightMode = FightMode.Aggressor,
                FightRange = 1,
                HitsMax = 5000,
                Hue = 0,
                Int = 1500,
                ManaMaxSeed = 350,
                Name = "a Vampire Lord",
                PerceptionRange = 10,
                ProvokeSkillOverride = 160,
                Resistances = new Dictionary<ElementalType, CreatureProp>
                {
                    {ElementalType.Poison, 100},
                    {ElementalType.Fire, 100},
                    {ElementalType.Air, 100},
                    {ElementalType.Water, 50}
                },
                Skills = new Dictionary<SkillName, CreatureProp>
                {
                    {SkillName.Tactics, 800},
                    {SkillName.Macing, 150},
                    {SkillName.MagicResist, 250}
                },
                StamMaxSeed = 350,
                Str = 1500,
                Tamable = false,
                TargetAcquireExhaustion = true,
                VirtualArmor = 40
            });
        }


        [Constructible]
        public VampireLord() : base(CreatureProperties.Get<VampireLord>())
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
                Name = "Dracula Weapon",
                Speed = 35,
                MaxHitPoints = 250,
                HitPoints = 250,
                HitSound = 0x25A,
                MissSound = 0x239
            });
        }

        [Constructible]
        public VampireLord(Serial serial) : base(serial)
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