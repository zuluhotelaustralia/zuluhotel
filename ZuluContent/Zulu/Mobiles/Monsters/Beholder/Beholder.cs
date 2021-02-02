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
    public class Beholder : BaseCreature
    {
        static Beholder()
        {
            CreatureProperties.Register<Beholder>(new CreatureProperties
            {
                // cast_pct = 50,
                // DataElementId = beholder,
                // DataElementType = NpcTemplate,
                // dstart = 10,
                // Equip = beholder,
                // Graphic = 0x0ec4 /* Weapon */,
                // HitSound = 0x17C /* Weapon */,
                // hostile = 1,
                LootTable = "57",
                LootItemChance = 75,
                LootItemLevel = 2,
                // MissSound = 0x239 /* Weapon */,
                // num_casts = 40,
                // script = spellkillpcs,
                // Speed = 33 /* Weapon */,
                // spell = paralyze,
                // spell_0 = fireball,
                // spell_1 = lightning,
                // spell_2 = chainlightning,
                // spell_3 = MassCurse,
                // spell_4 = flamestrike,
                // spell_5 = Magicreflect,
                // TrueColor = 0x8455,
                ActiveSpeed = 0.2,
                AiType = AIType.AI_Mage /* spellkillpcs */,
                AlwaysMurderer = true,
                Body = 0x16,
                CorpseNameOverride = "corpse of a beholder",
                CreatureType = CreatureType.Beholder,
                DamageMax = 34,
                DamageMin = 14,
                Dex = 10,
                Female = false,
                FightMode = FightMode.Closest,
                FightRange = 1,
                HitsMax = 160,
                Hue = 0x8455,
                Int = 600,
                ManaMaxSeed = 220,
                Name = "a beholder",
                PassiveSpeed = 0.4,
                PerceptionRange = 10,
                PreferredSpells = new List<Type>
                {
                    typeof(Spells.Fifth.ParalyzeSpell),
                    typeof(Spells.Third.FireballSpell),
                    typeof(Spells.Fourth.LightningSpell),
                    typeof(Spells.Sixth.MassCurseSpell)
                },
                ProvokeSkillOverride = 75,
                SaySpellMantra = true,
                Skills = new Dictionary<SkillName, CreatureProp>
                {
                    {SkillName.MagicResist, 100},
                    {SkillName.Tactics, 100},
                    {SkillName.Macing, 100},
                    {SkillName.Magery, 125}
                },
                StamMaxSeed = 65,
                Str = 160,
                VirtualArmor = 20
            });
        }


        [Constructible]
        public Beholder() : base(CreatureProperties.Get<Beholder>())
        {
            // Add customization here

            AddItem(new SkinningKnife
            {
                Movable = false,
                Name = "Beholder Weapon",
                Speed = 33,
                MaxHitPoints = 250,
                HitPoints = 250,
                HitSound = 0x17C,
                MissSound = 0x239
            });
        }

        [Constructible]
        public Beholder(Serial serial) : base(serial)
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