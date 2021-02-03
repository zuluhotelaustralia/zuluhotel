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
    public class Gazer : BaseCreature
    {
        static Gazer()
        {
            CreatureProperties.Register<Gazer>(new CreatureProperties
            {
                // cast_pct = 40,
                // CProp_PermMagicImmunity = i2,
                // DataElementId = gazer,
                // DataElementType = NpcTemplate,
                // dstart = 10,
                // Equip = gazer,
                // Graphic = 0x0ec4 /* Weapon */,
                // HitSound = 0x17C /* Weapon */,
                // hostile = 1,
                LootTable = "30",
                LootItemChance = 100,
                LootItemLevel = 2,
                // MissSound = 0x239 /* Weapon */,
                // num_casts = 8,
                // script = spellkillpcs,
                // Speed = 30 /* Weapon */,
                // spell = ebolt,
                // spell_0 = lightning,
                // spell_1 = harm,
                // spell_2 = mindblast,
                // spell_3 = magicarrow,
                // spell_4 = fireball,
                // spell_5 = weaken,
                // Swordsmanship = 75,
                // TrueColor = 0,
                ActiveSpeed = 0.2,
                AiType = AIType.AI_Mage /* spellkillpcs */,
                AlwaysMurderer = true,
                BaseSoundID = 377,
                Body = 0x16,
                CanFly = true,
                CorpseNameOverride = "corpse of a gazer",
                CreatureType = CreatureType.Beholder,
                DamageMax = 32,
                DamageMin = 4,
                Dex = 90,
                Female = false,
                FightMode = FightMode.Closest,
                FightRange = 1,
                HitsMax = 150,
                Hue = 0,
                Int = 205,
                ManaMaxSeed = 195,
                Name = "a gazer",
                PassiveSpeed = 0.4,
                PerceptionRange = 10,
                PreferredSpells = new List<Type>
                {
                    typeof(Spells.Sixth.EnergyBoltSpell),
                    typeof(Spells.Fourth.LightningSpell),
                    typeof(Spells.Second.HarmSpell),
                    typeof(Spells.Fifth.MindBlastSpell),
                    typeof(Spells.First.MagicArrowSpell),
                    typeof(Spells.Third.FireballSpell),
                    typeof(Spells.First.WeakenSpell)
                },
                ProvokeSkillOverride = 95,
                Skills = new Dictionary<SkillName, CreatureProp>
                {
                    {SkillName.Parry, 65},
                    {SkillName.MagicResist, 65},
                    {SkillName.Tactics, 50},
                    {SkillName.Magery, 90},
                    {SkillName.EvalInt, 70}
                },
                StamMaxSeed = 80,
                Str = 150,
                VirtualArmor = 10
            });
        }


        [Constructible]
        public Gazer() : base(CreatureProperties.Get<Gazer>())
        {
            // Add customization here

            AddItem(new SkinningKnife
            {
                Movable = false,
                Name = "Gazer Weapon",
                Speed = 30,
                Skill = SkillName.Swords,
                MaxHitPoints = 250,
                HitPoints = 250,
                HitSound = 0x17C,
                MissSound = 0x239
            });
        }

        [Constructible]
        public Gazer(Serial serial) : base(serial)
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