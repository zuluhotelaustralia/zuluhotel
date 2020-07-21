

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
    public class Defender : BaseCreature
    {
        static Defender() => CreatureProperties.Register<Defender>(new CreatureProperties
        {
            // DataElementId = lizardmandefender,
            // DataElementType = NpcTemplate,
            // dstart = 10,
            // Equip = lizardmandefender,
            // Graphic = 0x0ec4 /* Weapon */,
            // HitSound = 0x1A2 /* Weapon */,
            // hostile = 1,
            // lootgroup = 56,
            // MagicItemChance = 2,
            // MissSound = 0x1A3 /* Weapon */,
            // script = killpcs,
            // Speed = 35 /* Weapon */,
            // TrueColor = 0x05AE,
            ActiveSpeed = 0.2,
            AiType = AIType.AI_Melee /* killpcs */,
            AlwaysMurderer = true,
            BaseSoundID = 417,
            Body = 0x21,
            CorpseNameOverride = "corpse of <random> the Defender",
            CreatureType = CreatureType.Dragonkin,
            DamageMax = 43,
            DamageMin = 8,
            Dex = 180,
            Female = false,
            FightMode = FightMode.Aggressor,
            FightRange = 1,
            Hides = 5,
            HideType = HideType.Lizard,
            HitsMax = 275,
            Hue = 0x05AE,
            Int = 45,
            ManaMaxSeed = 35,
            Name = "<random> the Defender",
            PassiveSpeed = 0.4,
            PerceptionRange = 10,
            ProvokeSkillOverride = 100,
            Skills = new Dictionary<SkillName, CreatureProp>
            {
                { SkillName.Tactics, 90 },
                { SkillName.Macing, 100 },
                { SkillName.MagicResist, 80 },
            },
            StamMaxSeed = 50,
            Str = 275,
            VirtualArmor = 15,

        });


        [Constructible]
public Defender() : base(CreatureProperties.Get<Defender>())
        {
            // Add customization here

            AddItem(new SkinningKnife
            {
                Movable = false,
                Name = "Lizardman Defender Weapon",
                Speed = 35,
                MaxHitPoints = 250,
                HitPoints = 250,
                HitSound = 0x1A2,
                MissSound = 0x1A3,
            });


        }

        [Constructible]
public Defender(Serial serial) : base(serial) {}



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
