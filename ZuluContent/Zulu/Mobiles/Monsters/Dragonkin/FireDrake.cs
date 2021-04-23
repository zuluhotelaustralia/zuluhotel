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
    public class FireDrake : BaseCreature
    {
        static FireDrake()
        {
            CreatureProperties.Register<FireDrake>(new CreatureProperties
            {
                // DataElementId = firedrake,
                // DataElementType = NpcTemplate,
                // dstart = 10,
                // Equip = firedrake,
                // food = meat,
                // Graphic = 0x0ec4 /* Weapon */,
                // Hitscript = :combat:spellstrikescript /* Weapon */,
                // HitSound = 0x16D /* Weapon */,
                // hostile = 1,
                LootTable = "36",
                LootItemChance = 25,
                LootItemLevel = 4,
                // MissSound = 0x239 /* Weapon */,
                // noloot = 1,
                // script = firebreather,
                // Speed = 50 /* Weapon */,
                // TrueColor = 1158,
                // virtue = 7,
                AiType = AIType.AI_Melee /* firebreather */,
                AlwaysMurderer = true,
                BaseSoundID = 362,
                Body = 0x3d,
                CorpseNameOverride = "corpse of a Fire Drake",
                CreatureType = CreatureType.Dragonkin,
                DamageMax = 73,
                DamageMin = 33,
                Dex = 110,
                Female = false,
                FightMode = FightMode.Aggressor,
                FightRange = 1,
                HasBreath = true,
                Hides = 5,
                HideType = HideType.Lava,
                HitsMax = 350,
                Hue = 1158,
                Int = 90,
                ManaMaxSeed = 80,
                MinTameSkill = 115,
                Name = "a Fire Drake",
                PerceptionRange = 10,
                ProvokeSkillOverride = 130,
                Resistances = new Dictionary<ElementalType, CreatureProp>
                {
                    {ElementalType.Fire, 100},
                    {ElementalType.MagicImmunity, 3}
                },
                Skills = new Dictionary<SkillName, CreatureProp>
                {
                    {SkillName.Parry, 70},
                    {SkillName.MagicResist, 110},
                    {SkillName.Tactics, 100},
                    {SkillName.Macing, 130},
                    {SkillName.DetectHidden, 130}
                },
                StamMaxSeed = 130,
                Str = 350,
                Tamable = true,
                VirtualArmor = 30,
                WeaponAbility = new SpellStrike(typeof(Spells.Sixth.ExplosionSpell)),
                WeaponAbilityChance = 0.5
            });
        }


        [Constructible]
        public FireDrake() : base(CreatureProperties.Get<FireDrake>())
        {
            // Add customization here

            AddItem(new SkinningKnife
            {
                Movable = false,
                Name = "Fire Drake Weapon",
                Speed = 50,
                MaxHitPoints = 250,
                HitPoints = 250,
                HitSound = 0x16D,
                MissSound = 0x239
            });
        }

        [Constructible]
        public FireDrake(Serial serial) : base(serial)
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