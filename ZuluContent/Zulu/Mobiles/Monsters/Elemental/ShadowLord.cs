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
    public class ShadowLord : BaseCreature
    {
        static ShadowLord()
        {
            CreatureProperties.Register<ShadowLord>(new CreatureProperties
            {
                // CProp_BaseHpRegen = i500,
                // CProp_Permmr = i4,
                // DataElementId = shadowlord,
                // DataElementType = NpcTemplate,
                // dstart = 10,
                // Equip = shadowlord,
                // Graphic = 0x0ec4 /* Weapon */,
                // Hitscript = :combat:trielementalscript /* Weapon */,
                // HitSound = 0x283 /* Weapon */,
                // hostile = 1,
                LootTable = "79",
                LootItemChance = 15,
                LootItemLevel = 5,
                // MissSound = 0x282 /* Weapon */,
                // Parrying = 130,
                // script = killpcs,
                // Speed = 37 /* Weapon */,
                // TrueColor = 1,
                ActiveSpeed = 0.2,
                AiType = AIType.AI_Melee /* killpcs */,
                AlwaysMurderer = true,
                Body = 146,
                CanSwim = true,
                CorpseNameOverride = "corpse of a Shadow Lord",
                CreatureType = CreatureType.Elemental,
                DamageMax = 46,
                DamageMin = 16,
                Dex = 300,
                Female = false,
                FightMode = FightMode.Aggressor,
                FightRange = 1,
                HitsMax = 550,
                Hue = 1,
                Int = 200,
                ManaMaxSeed = 125,
                Name = "a Shadow Lord",
                PassiveSpeed = 0.4,
                PerceptionRange = 10,
                Resistances = new Dictionary<ElementalType, CreatureProp>
                {
                    {ElementalType.PermPoisonImmunity, 100},
                    {ElementalType.Necro, 100},
                    {ElementalType.PermMagicImmunity, 6}
                },
                SaySpellMantra = true,
                Skills = new Dictionary<SkillName, CreatureProp>
                {
                    {SkillName.MagicResist, 150},
                    {SkillName.Tactics, 100},
                    {SkillName.Fencing, 150},
                    {SkillName.DetectHidden, 130}
                },
                StamMaxSeed = 80,
                Str = 550,
                Tamable = false,
                WeaponAbility = new TriElementalStrike(),
                WeaponAbilityChance = 1.0
            });
        }


        [Constructible]
        public ShadowLord() : base(CreatureProperties.Get<ShadowLord>())
        {
            // Add customization here

            AddItem(new SkinningKnife
            {
                Movable = false,
                Name = "Shadow Lord Weapon",
                Hue = 1,
                Speed = 37,
                Skill = SkillName.Fencing,
                MaxHitPoints = 250,
                HitPoints = 250,
                HitSound = 0x283,
                MissSound = 0x282
            });

            AddItem(new BoneGloves
            {
                Movable = false,
                Name = "Blue Bone Gloves AR10",
                Hue = 0x0492,
                BaseArmorRating = 10,
                MaxHitPoints = 200,
                HitPoints = 200
            });

            AddItem(new BoneHelm
            {
                Movable = false,
                Name = "Red Bone Helm AR45",
                Hue = 0x0494,
                BaseArmorRating = 45,
                MaxHitPoints = 450,
                HitPoints = 450
            });
        }

        [Constructible]
        public ShadowLord(Serial serial) : base(serial)
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