

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
    public class OphidianAvengerErrant : BaseCreature
    {
        static OphidianAvengerErrant() => CreatureProperties.Register<OphidianAvengerErrant>(new CreatureProperties
        {
            // DataElementId = ophidianknighterrant,
            // DataElementType = NpcTemplate,
            // dstart = 10,
            // Equip = ophidianknighterrant,
            // Graphic = 0x0ec4 /* Weapon */,
            // Hitscript = :combat:poisonhit /* Weapon */,
            // HitSound = 0x168 /* Weapon */,
            // hostile = 1,
            // lootgroup = 71,
            // MissSound = 0x169 /* Weapon */,
            // script = killpcssprinters,
            // Speed = 37 /* Weapon */,
            // TrueColor = 0,
            // virtue = 3,
            ActiveSpeed = 0.150,
            AiType = AIType.AI_Melee /* killpcssprinters */,
            AlwaysMurderer = true,
            Body = 0x56,
            CorpseNameOverride = "corpse of an Ophidian Knight-Errant",
            CreatureType = CreatureType.Ophidian,
            DamageMax = 43,
            DamageMin = 8,
            Dex = 310,
            Female = false,
            FightMode = FightMode.Aggressor,
            FightRange = 1,
            Hides = 5,
            HideType = HideType.Serpent,
            HitPoison = Poison.Regular,
            HitsMax = 1750,
            Hue = 0,
            Int = 160,
            ManaMaxSeed = 0,
            Name = "an Ophidian Knight-Errant",
            PassiveSpeed = 0.300,
            PerceptionRange = 10,
            ProvokeSkillOverride = 120,
            Skills = new Dictionary<SkillName, CreatureProp>
            {
                { SkillName.Parry, 130 },
                { SkillName.Magery, 100 },
                { SkillName.Macing, 100 },
                { SkillName.Tactics, 110 },
                { SkillName.MagicResist, 110 },
            },
            StamMaxSeed = 1070,
            Str = 750,
            VirtualArmor = 30,

        });


        [Constructible]
public OphidianAvengerErrant() : base(CreatureProperties.Get<OphidianAvengerErrant>())
        {
            // Add customization here

            AddItem(new SkinningKnife
            {
                Movable = false,
                Name = "Ophidian Knight Errant Weapon",
                Speed = 37,
                MaxHitPoints = 250,
                HitPoints = 250,
                HitSound = 0x168,
                MissSound = 0x169,
            });


        }

        [Constructible]
public OphidianAvengerErrant(Serial serial) : base(serial) {}



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
