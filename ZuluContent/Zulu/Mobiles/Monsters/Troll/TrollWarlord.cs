

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
    public class TrollWarlord : BaseCreature
    {
        static TrollWarlord() => CreatureProperties.Register<TrollWarlord>(new CreatureProperties
        {
            // DataElementId = trollwarlord,
            // DataElementType = NpcTemplate,
            // dstart = 10,
            // Equip = trollwarlord,
            // Graphic = 0x0ec4 /* Weapon */,
            // HitSound = 0x233 /* Weapon */,
            // hostile = 1,
            // lootgroup = 19,
            // MagicItemChance = 25,
            // MagicItemLevel = 3,
            // MissSound = 0x23B /* Weapon */,
            // script = killpcs,
            // Speed = 35 /* Weapon */,
            // Swordsmanship = 160,
            // TrueColor = 0x0455,
            ActiveSpeed = 0.2,
            AiType = AIType.AI_Melee /* killpcs */,
            AlwaysMurderer = true,
            Body = 0x35,
            CorpseNameOverride = "corpse of a Troll Warlord",
            CreatureType = CreatureType.Troll,
            DamageMax = 45,
            DamageMin = 21,
            Dex = 190,
            Female = false,
            FightMode = FightMode.Aggressor,
            FightRange = 1,
            Hides = 4,
            HideType = HideType.Troll,
            HitsMax = 350,
            Hue = 0x0455,
            Int = 60,
            ManaMaxSeed = 0,
            Name = "a Troll Warlord",
            PassiveSpeed = 0.4,
            PerceptionRange = 10,
            ProvokeSkillOverride = 120,
            Skills = new Dictionary<SkillName, CreatureProp>
            {
                { SkillName.Tactics, 140 },
                { SkillName.MagicResist, 80 },
            },
            StamMaxSeed = 90,
            Str = 350,
            VirtualArmor = 30,

        });


        [Constructible]
public TrollWarlord() : base(CreatureProperties.Get<TrollWarlord>())
        {
            // Add customization here

            AddItem(new SkinningKnife
            {
                Movable = false,
                Name = "Troll Warlord Weapon",
                Speed = 35,
                MaxHitPoints = 250,
                HitPoints = 250,
                HitSound = 0x233,
                MissSound = 0x23B,
            });


        }

        [Constructible]
public TrollWarlord(Serial serial) : base(serial) {}



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
