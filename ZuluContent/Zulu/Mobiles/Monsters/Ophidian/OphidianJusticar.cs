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
    public class OphidianJusticar : BaseCreature
    {
        static OphidianJusticar()
        {
            CreatureProperties.Register<OphidianJusticar>(new CreatureProperties
            {
                // cast_pct = 70,
                // count_cast = 1,
                // DataElementId = ophidianjusticar,
                // DataElementType = NpcTemplate,
                // dstart = 10,
                // Equip = ophidianjusticar,
                // Graphic = 0x0ec4 /* Weapon */,
                // HitSound = 0x168 /* Weapon */,
                // hostile = 1,
                LootTable = "66",
                // MissSound = 0x169 /* Weapon */,
                // num_casts = 5,
                // script = spellkillpcs,
                // Speed = 35 /* Weapon */,
                // spell = summonfire,
                // spell_0 = lightning,
                // spell_1 = ebolt,
                // spell_2 = magicreflection,
                // spell_3 = fireball,
                // spell_4 = explosion,
                // spell_5 = flamestrike,
                // TrueColor = 0,
                // virtue = 3,
                ActiveSpeed = 0.2,
                AiType = AIType.AI_Mage /* spellkillpcs */,
                AlwaysMurderer = true,
                Body = 85,
                CorpseNameOverride = "corpse of an Ophidian Justicar",
                CreatureType = CreatureType.Ophidian,
                DamageMax = 43,
                DamageMin = 8,
                Dex = 210,
                Female = false,
                FightMode = FightMode.Closest,
                FightRange = 1,
                Hides = 5,
                HideType = HideType.Serpent,
                HitsMax = 500,
                Hue = 0,
                Int = 1000,
                ManaMaxSeed = 1000,
                Name = "an Ophidian Justicar",
                PassiveSpeed = 0.4,
                PerceptionRange = 10,
                PreferredSpells = new List<Type>
                {
                    typeof(Spells.Fourth.LightningSpell),
                    typeof(Spells.Sixth.EnergyBoltSpell),
                    typeof(Spells.Third.FireballSpell),
                    typeof(Spells.Sixth.ExplosionSpell)
                },
                ProvokeSkillOverride = 110,
                Skills = new Dictionary<SkillName, CreatureProp>
                {
                    {SkillName.Parry, 100},
                    {SkillName.Magery, 150},
                    {SkillName.Macing, 110},
                    {SkillName.Tactics, 100},
                    {SkillName.MagicResist, 130},
                    {SkillName.EvalInt, 80}
                },
                StamMaxSeed = 700,
                Str = 500,
                VirtualArmor = 30
            });
        }


        [Constructible]
        public OphidianJusticar() : base(CreatureProperties.Get<OphidianJusticar>())
        {
            // Add customization here

            AddItem(new SkinningKnife
            {
                Movable = false,
                Name = "Ophidian Justicar Weapon",
                Speed = 35,
                MaxHitPoints = 250,
                HitPoints = 250,
                HitSound = 0x168,
                MissSound = 0x169
            });
        }

        [Constructible]
        public OphidianJusticar(Serial serial) : base(serial)
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