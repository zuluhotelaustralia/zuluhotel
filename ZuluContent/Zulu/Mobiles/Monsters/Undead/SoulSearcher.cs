using System;
using System.Collections.Generic;
using Scripts.Zulu.Spells.Necromancy;
using Server;
using Server.Misc;
using Server.Items;
using static Server.Mobiles.CreatureProp;
using Server.Engines.Magic;
using Server.Engines.Harvest;

namespace Server.Mobiles
{
    public class SoulSearcher : BaseCreature
    {
        static SoulSearcher()
        {
            CreatureProperties.Register<SoulSearcher>(new CreatureProperties
            {
                // cast_pct = 32,
                // CProp_NecroProtection = i8,
                // CProp_PermMagicImmunity = i8,
                // DataElementId = soulsearcher,
                // DataElementType = NpcTemplate,
                // dstart = 10,
                // Equip = undeadflayer,
                // Graphic = 0x0ec4 /* Weapon */,
                // Hitscript = :combat:manadrainscript /* Weapon */,
                // HitSound = 0x16D /* Weapon */,
                // hostile = 1,
                // lootgroup = 9,
                // MagicItemChance = 100,
                // MagicItemChance_0 = 100,
                // Magicitemlevel = 7,
                // MagicItemLevel_0 = 7,
                // MissSound = 0x239 /* Weapon */,
                // num_casts = 7,
                // script = spellkillpcs,
                // speech = 35,
                // Speed = 50 /* Weapon */,
                // spell = flamestrike,
                // spell_0 = ebolt,
                // spell_1 = explosion,
                // spell_2 = wraithbreath,
                // spell_3 = plague,
                // spell_4 = darkness,
                // spell_5 = kill,
                // spell_6 = abyssalflame,
                // TrueColor = 1170,
                // virtue = 4,
                ActiveSpeed = 0.2,
                AiType = AIType.AI_Mage /* spellkillpcs */,
                AlwaysMurderer = true,
                Body = 0x3ca,
                CorpseNameOverride = "corpse of a Soul Searcher",
                CreatureType = CreatureType.Undead,
                DamageMax = 60,
                DamageMin = 10,
                Dex = 310,
                Female = false,
                FightMode = FightMode.Closest,
                FightRange = 1,
                HitsMax = 1200,
                Hue = 1170,
                Int = 2750,
                ManaMaxSeed = 2750,
                Name = "a Soul Searcher",
                PassiveSpeed = 0.4,
                PerceptionRange = 10,
                PreferredSpells = new List<Type>
                {
                    typeof(Spells.Sixth.EnergyBoltSpell),
                    typeof(Spells.Sixth.ExplosionSpell),
                    typeof(WraithBreathSpell),
                    typeof(PlagueSpell),
                    typeof(DarknessSpell),
                    typeof(WyvernStrikeSpell),
                    typeof(AbyssalFlameSpell)
                },
                Resistances = new Dictionary<ElementalType, CreatureProp>
                {
                    {ElementalType.Poison, 100}
                },
                SaySpellMantra = true,
                Skills = new Dictionary<SkillName, CreatureProp>
                {
                    {SkillName.Magery, 195},
                    {SkillName.MagicResist, 150},
                    {SkillName.Tactics, 150},
                    {SkillName.Macing, 160},
                    {SkillName.DetectHidden, 200}
                },
                StamMaxSeed = 50,
                Str = 1200,
                VirtualArmor = 45
            });
        }


        [Constructible]
        public SoulSearcher() : base(CreatureProperties.Get<SoulSearcher>())
        {
            // Add customization here

            AddItem(new SkinningKnife
            {
                Movable = false,
                Name = "Undead Flayer Weapon",
                Speed = 50,
                MaxHitPoints = 250,
                HitPoints = 250,
                HitSound = 0x16D,
                MissSound = 0x239
            });
        }

        [Constructible]
        public SoulSearcher(Serial serial) : base(serial)
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