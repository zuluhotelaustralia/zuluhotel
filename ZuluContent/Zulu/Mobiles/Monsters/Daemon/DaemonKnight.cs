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
    public class DaemonKnight : BaseCreature
    {
        static DaemonKnight()
        {
            CreatureProperties.Register<DaemonKnight>(new CreatureProperties
            {
                // CProp_NecroProtection = i5,
                // CProp_PermMagicImmunity = i3,
                // DataElementId = daemonknight,
                // DataElementType = NpcTemplate,
                // dstart = 10,
                // Equip = daemonknight,
                // Graphic = 0x0ec4 /* Weapon */,
                // HitSound = 0x168 /* Weapon */,
                // hostile = 1,
                // lootgroup = 22,
                // MagicItemChance = 60,
                // Magicitemlevel = 5,
                // MissSound = 0x239 /* Weapon */,
                // script = spellkillpcs,
                // speech = 35,
                // Speed = 35 /* Weapon */,
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
                // Swordsmanship = 150,
                // TrueColor = 33784,
                // virtue = 3,
                ActiveSpeed = 0.2,
                AiType = AIType.AI_Mage /* spellkillpcs */,
                AlwaysMurderer = true,
                BaseSoundID = 357,
                Body = 0x0A,
                CanFly = true,
                CorpseNameOverride = "corpse of <random> the Daemon Knight",
                CreatureType = CreatureType.Daemon,
                DamageMax = 45,
                DamageMin = 21,
                Dex = 80,
                Female = false,
                FightMode = FightMode.Closest,
                FightRange = 1,
                HitsMax = 450,
                Hue = 33784,
                Int = 300,
                ManaMaxSeed = 200,
                Name = "<random> the Daemon Knight",
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
                Skills = new Dictionary<SkillName, CreatureProp>
                {
                    {SkillName.Parry, 65},
                    {SkillName.MagicResist, 90},
                    {SkillName.Tactics, 100},
                    {SkillName.Macing, 120},
                    {SkillName.Magery, 100},
                    {SkillName.EvalInt, 80}
                },
                StamMaxSeed = 70,
                Str = 450,
                VirtualArmor = 30
            });
        }


        [Constructible]
        public DaemonKnight() : base(CreatureProperties.Get<DaemonKnight>())
        {
            // Add customization here

            AddItem(new SkinningKnife
            {
                Movable = false,
                Name = "Daemonknight Weapon",
                Speed = 35,
                MaxHitPoints = 250,
                HitPoints = 250,
                HitSound = 0x168,
                MissSound = 0x239
            });
        }

        [Constructible]
        public DaemonKnight(Serial serial) : base(serial)
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