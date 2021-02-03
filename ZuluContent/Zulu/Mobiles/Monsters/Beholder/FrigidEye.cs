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
    public class FrigidEye : BaseCreature
    {
        static FrigidEye()
        {
            CreatureProperties.Register<FrigidEye>(new CreatureProperties
            {
                // cast_pct = 50,
                // DataElementId = frostgazer,
                // DataElementType = NpcTemplate,
                // dstart = 10,
                // Equip = frostgazer,
                // Graphic = 0x0ec4 /* Weapon */,
                // HitSound = 0x17C /* Weapon */,
                // hostile = 1,
                LootTable = "45",
                LootItemChance = 2,
                // MissSound = 0x239 /* Weapon */,
                // num_casts = 10,
                // script = spellkillpcs,
                // Speed = 33 /* Weapon */,
                // spell = chainlightning,
                // spell_0 = explosion,
                // spell_1 = masscurse,
                // spell_2 = weaken,
                // spell_3 = paralyze,
                // spell_4 = fireball,
                // spell_5 = lightning,
                // TrueColor = 1154,
                ActiveSpeed = 0.2,
                AiType = AIType.AI_Mage /* spellkillpcs */,
                AlwaysMurderer = true,
                BaseSoundID = 377,
                Body = 0x16,
                CorpseNameOverride = "corpse of a frigid eye",
                CreatureType = CreatureType.Beholder,
                DamageMax = 35,
                DamageMin = 5,
                Dex = 100,
                Female = false,
                FightMode = FightMode.Closest,
                FightRange = 1,
                HitsMax = 250,
                Hue = 1154,
                Int = 450,
                ManaMaxSeed = 120,
                Name = "a frigid eye",
                PassiveSpeed = 0.4,
                PerceptionRange = 10,
                PreferredSpells = new List<Type>
                {
                    typeof(Spells.Sixth.ExplosionSpell),
                    typeof(Spells.Sixth.MassCurseSpell),
                    typeof(Spells.First.WeakenSpell),
                    typeof(Spells.Fifth.ParalyzeSpell),
                    typeof(Spells.Third.FireballSpell),
                    typeof(Spells.Fourth.LightningSpell)
                },
                ProvokeSkillOverride = 95,
                Skills = new Dictionary<SkillName, CreatureProp>
                {
                    {SkillName.MagicResist, 80},
                    {SkillName.Tactics, 65},
                    {SkillName.Macing, 100},
                    {SkillName.Magery, 95}
                },
                StamMaxSeed = 65,
                Str = 250,
                VirtualArmor = 20
            });
        }


        [Constructible]
        public FrigidEye() : base(CreatureProperties.Get<FrigidEye>())
        {
            // Add customization here

            AddItem(new SkinningKnife
            {
                Movable = false,
                Name = "Frost Gazer Weapon",
                Speed = 33,
                MaxHitPoints = 250,
                HitPoints = 250,
                HitSound = 0x17C,
                MissSound = 0x239
            });
        }

        [Constructible]
        public FrigidEye(Serial serial) : base(serial)
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