using System;
using System.Collections.Generic;
using Server;
using Server.Misc;
using Server.Items;
using static Server.Mobiles.CreatureProp;
using Server.Engines.Magic;
using Server.Engines.Harvest;
using Scripts.Zulu.Engines.Classes;
using Scripts.Zulu.Spells.Necromancy;

namespace Server.Mobiles
{
    public class TerathanQueen : BaseCreature
    {
        static TerathanQueen()
        {
            CreatureProperties.Register<TerathanQueen>(new CreatureProperties
            {
                // DataElementId = terathanqueen,
                // DataElementType = NpcTemplate,
                // dstart = 10,
                // Equip = terathanmatriarch,
                // EvaluateIntelligence = 200,
                // Graphic = 0x0ec4 /* Weapon */,
                // HitSound = 0x257 /* Weapon */,
                // hostile = 1,
                LootTable = "9",
                LootItemChance = 70,
                LootItemLevel = 6,
                // MissSound = 0x258 /* Weapon */,
                // script = spellkillpcs,
                // Speed = 40 /* Weapon */,
                // spell = flamestrike,
                // spell_0 = poison,
                // spell_1 = ebolt,
                // spell_2 = explosion,
                // spell_3 = mindblast,
                // spell_4 = spectrestouch,
                // spell_5 = decayingray,
                // spell_6 = kill,
                // TrueColor = 1177,
                // virtue = 2,
                ActiveSpeed = 0.2,
                AiType = AIType.AI_Mage /* spellkillpcs */,
                AlwaysMurderer = true,
                Body = 0x48,
                ClassLevel = 4,
                ClassType = ZuluClassType.Mage,
                CorpseNameOverride = "corpse of a Terathan Queen",
                CreatureType = CreatureType.Terathan,
                DamageMax = 60,
                DamageMin = 20,
                Dex = 70,
                Female = false,
                FightMode = FightMode.Closest,
                FightRange = 1,
                HitsMax = 2350,
                Hue = 1177,
                Int = 2000,
                ManaMaxSeed = 2000,
                Name = "a Terathan Queen",
                PassiveSpeed = 0.4,
                PerceptionRange = 10,
                Resistances = new Dictionary<ElementalType, CreatureProp>()
                {
                    {ElementalType.MagicImmunity, 8}
                },
                PreferredSpells = new List<Type>
                {
                    typeof(Spells.Third.PoisonSpell),
                    typeof(Spells.Sixth.EnergyBoltSpell),
                    typeof(Spells.Sixth.ExplosionSpell),
                    typeof(Spells.Fifth.MindBlastSpell),
                    typeof(DecayingRaySpell),
                    typeof(WyvernStrikeSpell)
                },
                SaySpellMantra = true,
                Skills = new Dictionary<SkillName, CreatureProp>
                {
                    {SkillName.Parry, 100},
                    {SkillName.Tactics, 120},
                    {SkillName.Macing, 110},
                    {SkillName.Magery, 200},
                    {SkillName.MagicResist, 200}
                },
                StamMaxSeed = 50,
                Str = 1350,
                VirtualArmor = 35
            });
        }


        [Constructible]
        public TerathanQueen() : base(CreatureProperties.Get<TerathanQueen>())
        {
            // Add customization here

            AddItem(new SkinningKnife
            {
                Movable = false,
                Name = "Terathan Matriarch Weapon",
                Speed = 40,
                MaxHitPoints = 250,
                HitPoints = 250,
                HitSound = 0x257,
                MissSound = 0x258
            });
        }

        [Constructible]
        public TerathanQueen(Serial serial) : base(serial)
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