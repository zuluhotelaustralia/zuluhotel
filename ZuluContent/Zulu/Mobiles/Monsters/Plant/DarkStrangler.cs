

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
    public class DarkStrangler : BaseCreature
    {
        static DarkStrangler() => CreatureProperties.Register<DarkStrangler>(new CreatureProperties
        {
            // ammoamount = 300,
            // ammotype = 0xEED,
            // CProp_EarthProtection = i5,
            // DataElementId = darkstrangler,
            // DataElementType = NpcTemplate,
            // dstart = 10,
            // Equip = darkstrangler,
            // graphic = 0x13B2 /* Weapon */,
            // Hitscript = :combat:poisonhit /* Weapon */,
            // HitSound = 0x1CB /* Weapon */,
            // hostile = 1,
            // lootgroup = 128,
            // Macefighting = 150,
            // MagicItemChance = 50,
            // Magicitemlevel = 4,
            // missileweapon = xbowman,
            // MissSound = 0x1CA /* Weapon */,
            // script = explosionkillpcs,
            // Speed = 20 /* Weapon */,
            // TrueColor = 1285,
            ActiveSpeed = 0.2,
            AiType = AIType.AI_Archer /* explosionkillpcs */,
            AlwaysMurderer = true,
            Body = 0x8,
            CorpseNameOverride = "corpse of a Dark Strangler",
            CreatureType = CreatureType.Plant,
            DamageMax = 46,
            DamageMin = 16,
            Dex = 200,
            Female = false,
            FightMode = FightMode.Closest,
            FightRange = 7,
            HitPoison = Poison.Regular,
            HitsMax = 400,
            Hue = 1285,
            Int = 45,
            ManaMaxSeed = 35,
            Name = "a Dark Strangler",
            PassiveSpeed = 0.4,
            PerceptionRange = 10,
            Skills = new Dictionary<SkillName, CreatureProp>
            {
                { SkillName.Parry, 90 },
                { SkillName.Hiding, 125 },
                { SkillName.Tactics, 110 },
                { SkillName.MagicResist, 75 },
                { SkillName.Archery, 150 },
            },
            StamMaxSeed = 95,
            Str = 400,
            VirtualArmor = 40,

        });


        [Constructible]
public DarkStrangler() : base(CreatureProperties.Get<DarkStrangler>())
        {
            // Add customization here

            AddItem(new Bow
            {
                Movable = false,
                Name = "Dark Strangler Weapon",
                Hue = 0x0493,
                Speed = 20,
                Skill = SkillName.Archery,
                EffectID = 0x113A,
                MissSound = 0x1CA,
                HitSound = 0x1CB,
                MaxHitPoints = 300,
                HitPoints = 300,
                MaxRange = 7,
            });

            AddItem(new HeaterShield
            {
                Movable = false,
                Name = "Shield AR30",
                BaseArmorRating = 30,
                MaxHitPoints = 500,
                HitPoints = 500,
            });


        }

        [Constructible]
public DarkStrangler(Serial serial) : base(serial) {}



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
