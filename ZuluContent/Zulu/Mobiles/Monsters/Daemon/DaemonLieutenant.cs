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
    public class DaemonLieutenant : BaseCreature
    {
        static DaemonLieutenant()
        {
            CreatureProperties.Register<DaemonLieutenant>(new CreatureProperties
            {
                // cast_pct = 12,
                // DataElementId = daemonlieutenant,
                // DataElementType = NpcTemplate,
                // dstart = 10,
                // Equip = daemonlieutenant,
                // Graphic = 0x0ec4 /* Weapon */,
                // HitSound = 0x166 /* Weapon */,
                // hostile = 1,
                LootTable = "22",
                LootItemChance = 50,
                LootItemLevel = 4,
                // MissSound = 0x239 /* Weapon */,
                // num_casts = 3,
                // script = spellkillpcs,
                // speech = 35,
                // Speed = 40 /* Weapon */,
                // spell = poison,
                // spell_0 = flamestrike,
                // spell_1 = ebolt,
                // spell_2 = lightning,
                // spell_3 = explosion,
                // spell_4 = meteorswarm,
                // spell_5 = chainlightning,
                // spell_6 = summondaemon,
                // spell_7 = paralyze,
                // spell_8 = masscurse,
                // spell_9 = summonskel,
                // TrueColor = 0x04b9,
                // virtue = 8,
                ActiveSpeed = 0.2,
                AiType = AIType.AI_Mage /* spellkillpcs */,
                AlwaysMurderer = true,
                BaseSoundID = 357,
                Body = 0x18,
                CorpseNameOverride = "corpse of a Daemon Lieutenant",
                CreatureType = CreatureType.Daemon,
                DamageMax = 65,
                DamageMin = 20,
                Dex = 350,
                Female = false,
                FightMode = FightMode.Closest,
                FightRange = 1,
                Hides = 3,
                HideType = HideType.Liche,
                HitsMax = 550,
                Hue = 0x04b9,
                Int = 350,
                ManaMaxSeed = 200,
                Name = "a Daemon Lieutenant",
                PassiveSpeed = 0.4,
                PerceptionRange = 10,
                PreferredSpells = new List<Type>
                {
                    typeof(Spells.Third.PoisonSpell),
                    typeof(Spells.Sixth.EnergyBoltSpell),
                    typeof(Spells.Fourth.LightningSpell),
                    typeof(Spells.Sixth.ExplosionSpell),
                    typeof(Spells.Fifth.ParalyzeSpell),
                    typeof(Spells.Sixth.MassCurseSpell)
                },
                Resistances = new Dictionary<ElementalType, CreatureProp>
                {
                    {ElementalType.Poison, 100}
                },
                SaySpellMantra = true,
                Skills = new Dictionary<SkillName, CreatureProp>
                {
                    {SkillName.MagicResist, 90},
                    {SkillName.Tactics, 100},
                    {SkillName.Macing, 160},
                    {SkillName.Magery, 100}
                },
                StamMaxSeed = 50,
                Str = 550,
                VirtualArmor = 35
            });
        }


        [Constructible]
        public DaemonLieutenant() : base(CreatureProperties.Get<DaemonLieutenant>())
        {
            // Add customization here

            AddItem(new SkinningKnife
            {
                Movable = false,
                Name = "Daemon Lieutenant Weapon",
                Speed = 40,
                MaxHitPoints = 250,
                HitPoints = 250,
                HitSound = 0x166,
                MissSound = 0x239
            });
        }

        [Constructible]
        public DaemonLieutenant(Serial serial) : base(serial)
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