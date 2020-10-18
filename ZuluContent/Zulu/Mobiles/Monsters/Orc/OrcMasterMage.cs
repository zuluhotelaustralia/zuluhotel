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
    public class OrcMasterMage : BaseCreature
    {
        static OrcMasterMage()
        {
            CreatureProperties.Register<OrcMasterMage>(new CreatureProperties
            {
                // cast_pct = 50,
                // CProp_PermMagicImmunity = i3,
                // DataElementId = orcmastermage,
                // DataElementType = NpcTemplate,
                // dstart = 10,
                // Equip = orcmastermage,
                // Graphic = 0x0ec4 /* Weapon */,
                // HitSound = 0x1B3 /* Weapon */,
                // hostile = 1,
                // lootgroup = 31,
                // MagicItemChance = 75,
                // MagicItemLevel = 3,
                // MissSound = 0x239 /* Weapon */,
                // num_casts = 10,
                // script = spellkillpcs,
                // speech = 6,
                // Speed = 30 /* Weapon */,
                // spell = flamestrike,
                // spell_0 = ebolt,
                // spell_1 = lightning,
                // spell_2 = harm,
                // spell_3 = fireball,
                // spell_4 = paralyze,
                // spell_5 = masscurse,
                // spell_6 = summonwater,
                // spell_7 = explosion,
                // spell_8 = chainlightning,
                // TrueColor = 0x06122,
                ActiveSpeed = 0.2,
                AiType = AIType.AI_Mage /* spellkillpcs */,
                AlwaysMurderer = true,
                Body = 0x11,
                CorpseNameOverride = "corpse of <random> the Orc Master Mage",
                CreatureType = CreatureType.Orc,
                DamageMax = 43,
                DamageMin = 8,
                Dex = 90,
                Female = false,
                FightMode = FightMode.Closest,
                FightRange = 1,
                HitsMax = 255,
                Hue = 0x0612,
                Int = 255,
                ManaMaxSeed = 205,
                Name = "<random> the Orc Master Mage",
                PassiveSpeed = 0.4,
                PerceptionRange = 10,
                PreferredSpells = new List<Type>
                {
                    typeof(Spells.Sixth.EnergyBoltSpell),
                    typeof(Spells.Fourth.LightningSpell),
                    typeof(Spells.Second.HarmSpell),
                    typeof(Spells.Third.FireballSpell),
                    typeof(Spells.Fifth.ParalyzeSpell),
                    typeof(Spells.Sixth.MassCurseSpell),
                    typeof(Spells.Sixth.ExplosionSpell)
                },
                ProvokeSkillOverride = 100,
                SaySpellMantra = true,
                Skills = new Dictionary<SkillName, CreatureProp>
                {
                    {SkillName.MagicResist, 85},
                    {SkillName.Tactics, 80},
                    {SkillName.Macing, 100},
                    {SkillName.Magery, 120}
                },
                StamMaxSeed = 80,
                Str = 255,
                VirtualArmor = 40
            });
        }


        [Constructible]
        public OrcMasterMage() : base(CreatureProperties.Get<OrcMasterMage>())
        {
            // Add customization here

            AddItem(new SkinningKnife
            {
                Movable = false,
                Name = "Orc Master Mage Weapon",
                Speed = 30,
                MaxHitPoints = 250,
                HitPoints = 250,
                HitSound = 0x1B3,
                MissSound = 0x239
            });
        }

        [Constructible]
        public OrcMasterMage(Serial serial) : base(serial)
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