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
    public class Titan : BaseCreature
    {
        static Titan()
        {
            CreatureProperties.Register<Titan>(new CreatureProperties
            {
                // CProp_EarthProtection = i3,
                // CProp_HolyProtection = i3,
                // CProp_PermMagicImmunity = i3,
                // DataElementId = titan,
                // DataElementType = NpcTemplate,
                // dstart = 10,
                // Equip = titan,
                // Graphic = 0x0ec4 /* Weapon */,
                // HitSound = 0x25F /* Weapon */,
                // hostile = 1,
                LootTable = "22",
                LootItemChance = 50,
                LootItemLevel = 4,
                // MissSound = 0x169 /* Weapon */,
                // script = spellkillpcs,
                // Speed = 60 /* Weapon */,
                // spell = poison,
                // spell_0 = flamestrike,
                // spell_1 = ebolt,
                // spell_2 = lightning,
                // spell_3 = harm,
                // spell_4 = mindblast,
                // spell_5 = magicarrow,
                // spell_6 = explosion,
                // spell_7 = meteorswarm,
                // spell_8 = chainlightning,
                // spell_9 = paralyze,
                // TrueColor = 0,
                // virtue = 2,
                ActiveSpeed = 0.2,
                AiType = AIType.AI_Mage /* spellkillpcs */,
                AlwaysMurderer = true,
                Body = 0x4c,
                CorpseNameOverride = "corpse of a Titan",
                CreatureType = CreatureType.Giantkin,
                DamageMax = 43,
                DamageMin = 8,
                Dex = 180,
                Female = false,
                FightMode = FightMode.Closest,
                FightRange = 1,
                HitsMax = 1000,
                Hue = 0,
                Int = 210,
                ManaMaxSeed = 200,
                Name = "a Titan",
                PassiveSpeed = 0.4,
                PerceptionRange = 10,
                PreferredSpells = new List<Type>
                {
                    typeof(Spells.Third.PoisonSpell),
                    typeof(Spells.Sixth.EnergyBoltSpell),
                    typeof(Spells.Fourth.LightningSpell),
                    typeof(Spells.Second.HarmSpell),
                    typeof(Spells.Fifth.MindBlastSpell),
                    typeof(Spells.First.MagicArrowSpell),
                    typeof(Spells.Sixth.ExplosionSpell),
                    typeof(Spells.Fifth.ParalyzeSpell)
                },
                ProvokeSkillOverride = 100,
                Skills = new Dictionary<SkillName, CreatureProp>
                {
                    {SkillName.Parry, 50},
                    {SkillName.Magery, 120},
                    {SkillName.Tactics, 100},
                    {SkillName.Macing, 110},
                    {SkillName.MagicResist, 90}
                },
                StamMaxSeed = 300,
                Str = 600,
                VirtualArmor = 40
            });
        }


        [Constructible]
        public Titan() : base(CreatureProperties.Get<Titan>())
        {
            // Add customization here

            AddItem(new SkinningKnife
            {
                Movable = false,
                Name = "Titan Weapon",
                Speed = 60,
                MaxHitPoints = 250,
                HitPoints = 250,
                HitSound = 0x25F,
                MissSound = 0x169
            });
        }

        [Constructible]
        public Titan(Serial serial) : base(serial)
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