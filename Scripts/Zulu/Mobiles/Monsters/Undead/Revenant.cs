

using System;
using System.Collections.Generic;
using Server;

using Server.Misc;
using Server.Items;
using static Server.Mobiles.CreatureProp;
using Server.Engines.Magic;
using Server.Engines.Harvest;
using Server.Engines.Magic.HitScripts;

namespace Server.Mobiles
{
    public class Revenant : BaseCreature
    {
        static Revenant() => CreatureProperties.Register<Revenant>(new CreatureProperties
        {
            // CProp_NecroProtection = i6,
            // DataElementId = revenant,
            // DataElementType = NpcTemplate,
            // dstart = 10,
            // Equip = revenant,
            // Graphic = 0x0ec4 /* Weapon */,
            // Hitscript = :combat:blindingscript /* Weapon */,
            // HitSound = 0x1DA /* Weapon */,
            // hostile = 1,
            // lootgroup = 14,
            // MagicItemChance = 0,
            // Magicitemlevel = 0,
            // MissSound = 0x239 /* Weapon */,
            // script = killpcs,
            // Speed = 45 /* Weapon */,
            // Swordsmanship = 95,
            // TrueColor = 1285,
            ActiveSpeed = 0.2,
            AiType = AIType.AI_Melee /* killpcs */,
            AlwaysMurderer = true,
            Body = 0x03,
            CorpseNameOverride = "corpse of a Revenant",
            CreatureType = CreatureType.Undead,
            DamageMax = 64,
            DamageMin = 8,
            Dex = 120,
            Female = false,
            FightMode = FightMode.Aggressor,
            FightRange = 1,
            HitsMax = 135,
            Hue = 1285,
            Int = 15,
            ManaMaxSeed = 5,
            Name = "a Revenant",
            PassiveSpeed = 0.4,
            PerceptionRange = 10,
            Resistances = new Dictionary<ElementalType, CreatureProp>
            {
                { ElementalType.Poison, 100 },
            },
            Skills = new Dictionary<SkillName, CreatureProp>
            {
                { SkillName.MagicResist, 60 },
                { SkillName.Tactics, 90 },
            },
            StamMaxSeed = 30,
            Str = 135,
            VirtualArmor = 20,
            WeaponAbility = new SpellStrike<RunZH.Scripts.Zulu.Spells.Necromancy.DarknessSpell>(),
            WeaponAbilityChance = 1.0,
  
        });

        [Constructable]
        public Revenant() : base(CreatureProperties.Get<Revenant>())
        {
            // Add customization here

            AddItem(new SkinningKnife
            {
                Movable = false,
                Name = "Revenant Weapon",
                Speed = 45,
                Skill = SkillName.Swords,
                MaxHitPoints = 250,
                HitPoints = 250,
                HitSound = 0x1DA,
                MissSound = 0x239,
            });
  
  
        }

        public Revenant(Serial serial) : base(serial) {}

  

        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);
            writer.Write((int) 0);
        }

        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);
            int version = reader.ReadInt();
        }
    }
}