

using System;
using System.Collections.Generic;
using Server;

using Server.Misc;
using Server.Items;
using static Server.Mobiles.CreatureProp;
using Server.Engines.Magic;
using Server.Engines.Harvest;
using Scripts.Zulu.Engines.Classes;

namespace Server.Mobiles
{
    public class GoblinGuard : BaseCreature
    {
        static GoblinGuard() => CreatureProperties.Register<GoblinGuard>(new CreatureProperties
        {
            // CProp_NoReactiveArmour = i1,
            // CProp_PermMagicImmunity = i1,
            // CProp_untamemountatdeath = i1,
            // DataElementId = goblinguardmounted,
            // DataElementType = NpcTemplate,
            // dstart = 10,
            // Equip = orcguard,
            // Graphic = 0x0ec4 /* Weapon */,
            // guardignore = 1,
            // HitSound = 0x1B3 /* Weapon */,
            // hostile = 1,
            // lootgroup = 59,
            // MissSound = 0x1B1 /* Weapon */,
            // mount = 0x3ea4 264,
            // mounttemplate = frenziedpoisonostard,
            // script = killpcsTeleporter,
            // Speed = 45 /* Weapon */,
            // Swordsmanship = 200,
            // TrueColor = 34186,
            AiType = AIType.AI_Melee /* killpcsTeleporter */,
            AlwaysMurderer = true,
            BardImmune = true,
            Body = 0x190,
            ClassLevel = 4,
            ClassSpec = SpecName.Warrior,
            CorpseNameOverride = "corpse of <random> the Goblin Guard",
            CreatureType = CreatureType.Orc,
            DamageMax = 64,
            DamageMin = 8,
            Dex = 300,
            Female = false,
            FightMode = FightMode.Aggressor,
            FightRange = 1,
            HitsMax = 500,
            Hue = 34186,
            Int = 210,
            ManaMaxSeed = 200,
            Name = "<random> the Goblin Guard",
            PerceptionRange = 10,
            Resistances = new Dictionary<ElementalType, CreatureProp>
            {
                { ElementalType.Physical, 25 },
                { ElementalType.Poison, 25 },
            },
            Skills = new Dictionary<SkillName, CreatureProp>
            {
                { SkillName.Tactics, 150 },
                { SkillName.MagicResist, 80 },
            },
            StamMaxSeed = 200,
            Str = 500,
            TargetAcquireExhaustion = true,
            VirtualArmor = 60,

        });


        [Constructible]
public GoblinGuard() : base(CreatureProperties.Get<GoblinGuard>())
        {
            // Add customization here

            AddItem(new OrcHelm
            {
                Movable = false,
                Name = "Goblin Helmet",
                Hue = 1418,
                BaseArmorRating = 18,
                MaxHitPoints = 400,
                HitPoints = 400,
            });

            AddItem(new SkinningKnife
            {
                Movable = false,
                Name = "Orc Defender Weapon",
                Speed = 45,
                MaxHitPoints = 250,
                HitPoints = 250,
                HitSound = 0x1B3,
                MissSound = 0x1B1,
            });


        }

        [Constructible]
public GoblinGuard(Serial serial) : base(serial) {}



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
