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
    public class Daemon : BaseCreature
    {
        static Daemon()
        {
            CreatureProperties.Register<Daemon>(new CreatureProperties
            {
                // cast_pct = 20,
                // DataElementId = daemon,
                // DataElementType = NpcTemplate,
                // dstart = 10,
                // Equip = daemon,
                // Graphic = 0x0ec4 /* Weapon */,
                // HitSound = 0x168 /* Weapon */,
                // hostile = 1,
                LootTable = "22",
                LootItemChance = 40,
                LootItemLevel = 5,
                // MissSound = 0x239 /* Weapon */,
                // num_casts = 4,
                // script = spellkillpcs,
                // speech = 35,
                // Speed = 45 /* Weapon */,
                // spell = fstrike,
                // spell_0 = ebolt,
                // spell_1 = lightning,
                // spell_10 = summonskel,
                // spell_2 = harm,
                // spell_3 = mindblast,
                // spell_4 = magicarrow,
                // spell_5 = chainlightning,
                // spell_6 = fireball,
                // spell_7 = explosion,
                // spell_8 = masscurse,
                // spell_9 = meteorswarm,
                // TrueColor = 33784,
                // virtue = 2,
                ActiveSpeed = 0.2,
                AiType = AIType.AI_Mage /* spellkillpcs */,
                AlwaysMurderer = true,
                BaseSoundID = 357,
                Body = 0x09,
                CanFly = true,
                CorpseNameOverride = "corpse of <random> the Daemon",
                CreatureType = CreatureType.Daemon,
                DamageMax = 40,
                DamageMin = 10,
                Female = false,
                FightMode = FightMode.Closest,
                FightRange = 1,
                HitsMax = 450,
                Hue = 33784,
                Str = 450,
                Int = 350,
                Dex = 80,
                StamMaxSeed = 70,
                VirtualArmor = 30,
                ManaMaxSeed = 200,
                Name = "<random> the Daemon",
                PassiveSpeed = 0.4,
                PerceptionRange = 10,
                PreferredSpells = new List<Type>
                {
                    typeof(Spells.Seventh.FlameStrikeSpell),
                    typeof(Spells.Sixth.EnergyBoltSpell),
                    typeof(Spells.Fourth.LightningSpell),
                    typeof(Spells.Second.HarmSpell),
                    typeof(Spells.Fifth.MindBlastSpell),
                    typeof(Spells.First.MagicArrowSpell),
                    typeof(Spells.Third.FireballSpell),
                    typeof(Spells.Sixth.ExplosionSpell),
                    typeof(Spells.Sixth.MassCurseSpell)
                },
                SaySpellMantra = true,
                Resistances = new Dictionary<ElementalType, CreatureProp>()
                {
                    {ElementalType.Earth, 75},
                    {ElementalType.MagicImmunity, 3}
                },
                Skills = new Dictionary<SkillName, CreatureProp>
                {
                    {SkillName.Parry, 65},
                    {SkillName.MagicResist, 90},
                    {SkillName.Tactics, 100},
                    {SkillName.Macing, 120},
                    {SkillName.Magery, 100},
                    {SkillName.EvalInt, 100}
                },
            });
        }


        [Constructible]
        public Daemon() : base(CreatureProperties.Get<Daemon>())
        {
            // Add customization here

            AddItem(new SkinningKnife
            {
                Movable = false,
                Name = "Daemon Weapon",
                Speed = 45,
                MaxHitPoints = 250,
                HitPoints = 250,
                HitSound = 0x168,
                MissSound = 0x239
            });
        }

        [Constructible]
        public Daemon(Serial serial) : base(serial)
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