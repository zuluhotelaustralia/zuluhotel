

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
    public class HellHound : BaseCreature
    {
        static HellHound() => CreatureProperties.Register<HellHound>(new CreatureProperties
        {
            // cast_pct = 35,
            // DataElementId = hellhound,
            // DataElementType = NpcTemplate,
            // dstart = 10,
            // Equip = hellhound,
            // food = meat,
            // Graphic = 0x0ec4 /* Weapon */,
            // HitSound = 0xE8 /* Weapon */,
            // hostile = 1,
            // lootgroup = 61,
            // MissSound = 0x239 /* Weapon */,
            // num_casts = 5,
            // script = spellkillpcs,
            // Speed = 35 /* Weapon */,
            // spell = fireball,
            // TrueColor = 232,
            ActiveSpeed = 0.2,
            AiType = AIType.AI_Mage /* spellkillpcs */,
            AlwaysMurderer = true,
            Body = 0xe1,
            CorpseNameOverride = "corpse of a hell hound",
            CreatureType = CreatureType.Daemon,
            DamageMax = 64,
            DamageMin = 8,
            Dex = 185,
            Female = false,
            FightMode = FightMode.Closest,
            FightRange = 1,
            Hides = 5,
            HideType = HideType.Wolf,
            HitsMax = 170,
            Hue = 232,
            Int = 60,
            ManaMaxSeed = 50,
            MinTameSkill = 95,
            Name = "a hell hound",
            PassiveSpeed = 0.4,
            PerceptionRange = 10,
            PreferredSpells = new List<Type>
            {
                typeof(Spells.Third.FireballSpell),
            },
            ProvokeSkillOverride = 80,
            Skills = new Dictionary<SkillName, CreatureProp>
            {
                { SkillName.Tactics, 100 },
                { SkillName.Macing, 110 },
                { SkillName.Magery, 90 },
                { SkillName.MagicResist, 70 },
            },
            StamMaxSeed = 70,
            Str = 170,
            Tamable = true,
            VirtualArmor = 25,

        });


        [Constructible]
public HellHound() : base(CreatureProperties.Get<HellHound>())
        {
            // Add customization here

            AddItem(new SkinningKnife
            {
                Movable = false,
                Name = "Hellhound Weapon",
                Speed = 35,
                MaxHitPoints = 250,
                HitPoints = 250,
                HitSound = 0xE8,
                MissSound = 0x239,
            });


        }

        [Constructible]
public HellHound(Serial serial) : base(serial) {}



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
