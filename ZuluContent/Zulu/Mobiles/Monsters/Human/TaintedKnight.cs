using System;
using System.Collections.Generic;
using Server;
using Server.Misc;
using Server.Items;
using static Server.Mobiles.CreatureProp;
using Server.Engines.Magic;
using Server.Engines.Harvest;
using Server.Engines.Magic.HitScripts;
using Scripts.Zulu.Engines.Classes;
using Scripts.Zulu.Spells.Necromancy;

namespace Server.Mobiles
{
    public class TaintedKnight : BaseCreature
    {
        static TaintedKnight()
        {
            CreatureProperties.Register<TaintedKnight>(new CreatureProperties
            {
                // CProp_FinalDeath = i1,
                // DataElementId = taintedwarrior2,
                // DataElementType = NpcTemplate,
                // dstart = 10,
                // Equip = taintedwarrior2,
                // Graphic = 0xF60 /* Weapon */,
                // Hitscript = :combat:blindingscript /* Weapon */,
                // HitSound = 0x23C /* Weapon */,
                // hostile = 1,
                LootTable = "131",
                LootItemChance = 50,
                LootItemLevel = 4,
                // MissSound = 0x23A /* Weapon */,
                // Parrying = 130,
                // script = killpcs,
                // Speed = 50 /* Weapon */,
                // Swordsmanship = 140,
                // TrueColor = 1302,
                ActiveSpeed = 0.2,
                AiType = AIType.AI_Melee /* killpcs */,
                AlwaysMurderer = true,
                Body = 0x22A,
                ClassLevel = 3,
                ClassType = ZuluClassType.Warrior,
                CorpseNameOverride = "corpse of <random> the Tainted Knight",
                CreatureType = CreatureType.Human,
                DamageMax = 45,
                DamageMin = 25,
                Dex = 250,
                Female = false,
                FightMode = FightMode.Aggressor,
                FightRange = 1,
                HitsMax = 400,
                Hue = 1302,
                Int = 200,
                ManaMaxSeed = 100,
                Name = "<random> the Tainted Knight",
                PassiveSpeed = 0.4,
                PerceptionRange = 10,
                RiseCreatureDelay = TimeSpan.FromSeconds(2),
                RiseCreatureType = typeof(EvisceratedCarcass),
                Skills = new Dictionary<SkillName, CreatureProp>
                {
                    {SkillName.Tactics, 120},
                    {SkillName.MagicResist, 100}
                },
                StamMaxSeed = 50,
                Str = 400,
                VirtualArmor = 50,
                WeaponAbility = new SpellStrike(typeof(DarknessSpell)),
                WeaponAbilityChance = 1.0
            });
        }


        [Constructible]
        public TaintedKnight() : base(CreatureProperties.Get<TaintedKnight>())
        {
            // Add customization here

            AddItem(new Longsword
            {
                Movable = false,
                Name = "a stygian-bladed sword",
                Hue = 1283,
                Speed = 50,
                Skill = SkillName.Swords,
                MaxHitPoints = 250,
                HitPoints = 250,
                HitSound = 0x23C,
                MissSound = 0x23A,
                Animation = (WeaponAnimation) 0x0009
            });

            AddItem(new BoneHelm
            {
                Movable = false,
                Name = "the bones of the damned",
                Hue = 1302,
                BaseArmorRating = 1,
                MaxHitPoints = 500,
                HitPoints = 500
            });

            AddItem(new BoneGloves
            {
                Movable = false,
                Name = "the bones of the damned",
                Hue = 1302,
                BaseArmorRating = 1,
                MaxHitPoints = 500,
                HitPoints = 500
            });

            AddItem(new BoneLegs
            {
                Movable = false,
                Name = "the bones of the damned",
                Hue = 1302,
                BaseArmorRating = 1,
                MaxHitPoints = 500,
                HitPoints = 500
            });

            AddItem(new BoneChest
            {
                Movable = false,
                Name = "the bones of the damned",
                Hue = 1302,
                BaseArmorRating = 1,
                MaxHitPoints = 300,
                HitPoints = 300
            });

            AddItem(new MetalShield
            {
                Movable = false,
                Name = "a shield of stygian darkness",
                BaseArmorRating = 30,
                MaxHitPoints = 500,
                HitPoints = 500
            });
        }

        [Constructible]
        public TaintedKnight(Serial serial) : base(serial)
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