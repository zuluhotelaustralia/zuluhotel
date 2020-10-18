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
    public class EvilMage : BaseCreature
    {
        static EvilMage()
        {
            CreatureProperties.Register<EvilMage>(new CreatureProperties
            {
                // cast_pct = 45,
                // DataElementId = evilmage,
                // DataElementType = NpcTemplate,
                // dstart = 10,
                // Equip = evilmage,
                // Graphic = 0x13f9 /* Weapon */,
                // HitSound = 0x13C /* Weapon */,
                // lootgroup = 57,
                // MagicItemChance = 2,
                // MissSound = 0x239 /* Weapon */,
                // num_casts = 10,
                // script = spellkillpcs,
                // speech = 35,
                // Speed = 30 /* Weapon */,
                // spell = flamestrike,
                // spell_0 = ebolt,
                // spell_1 = lightning,
                // spell_2 = harm,
                // spell_3 = summonfire,
                // spell_4 = fireball,
                // spell_5 = paralyze,
                // spell_6 = explosion,
                // TrueColor = 0,
                ActiveSpeed = 0.2,
                AiType = AIType.AI_Mage /* spellkillpcs */,
                AlwaysAttackable = true,
                Body = 0x190,
                CorpseNameOverride = "corpse of <random> the Evil Mage",
                CreatureType = CreatureType.Human,
                DamageMax = 40,
                DamageMin = 4,
                Dex = 90,
                Female = false,
                FightMode = FightMode.None,
                FightRange = 1,
                HitsMax = 180,
                Hue = 0,
                Int = 385,
                ManaMaxSeed = 105,
                Name = "<random> the Evil Mage",
                PassiveSpeed = 0.4,
                PerceptionRange = 10,
                PreferredSpells = new List<Type>
                {
                    typeof(Spells.Sixth.EnergyBoltSpell),
                    typeof(Spells.Fourth.LightningSpell),
                    typeof(Spells.Second.HarmSpell),
                    typeof(Spells.Third.FireballSpell),
                    typeof(Spells.Fifth.ParalyzeSpell),
                    typeof(Spells.Sixth.ExplosionSpell)
                },
                ProvokeSkillOverride = 94,
                SaySpellMantra = true,
                Skills = new Dictionary<SkillName, CreatureProp>
                {
                    {SkillName.MagicResist, 170},
                    {SkillName.Tactics, 380},
                    {SkillName.Macing, 95},
                    {SkillName.Magery, 100}
                },
                StamMaxSeed = 80,
                Str = 180,
                VirtualArmor = 20
            });
        }


        [Constructible]
        public EvilMage() : base(CreatureProperties.Get<EvilMage>())
        {
            // Add customization here

            AddItem(new LongHair(Race.RandomHairHue())
            {
                Movable = false
            });

            AddItem(new GnarledStaff
            {
                Movable = false,
                Name = "Evil Mage Weapon",
                Speed = 30,
                MaxHitPoints = 250,
                HitPoints = 250,
                HitSound = 0x13C,
                MissSound = 0x239
            });
        }

        [Constructible]
        public EvilMage(Serial serial) : base(serial)
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