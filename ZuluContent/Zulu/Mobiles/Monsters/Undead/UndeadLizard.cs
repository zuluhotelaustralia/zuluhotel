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
    public class UndeadLizard : BaseCreature
    {
        static UndeadLizard()
        {
            CreatureProperties.Register<UndeadLizard>(new CreatureProperties
            {
                // cast_pct = 42,
                // CProp_PermMagicImmunity = i3,
                // DataElementId = deadlizard,
                // DataElementType = NpcTemplate,
                // dstart = 10,
                // Equip = deadlizard,
                // food = meat,
                // Graphic = 0x0ec4 /* Weapon */,
                // HitSound = 0x19F /* Weapon */,
                // hostile = 1,
                // lootgroup = 46,
                // Macefighting = 125,
                // MagicItemChance = 75,
                // MagicItemLevel = 3,
                // MissSound = 0x239 /* Weapon */,
                // num_casts = 8,
                // script = spellkillpcs,
                // speech = 35,
                // Speed = 40 /* Weapon */,
                // spell = magicarrow,
                // spell_0 = magicreflect,
                // spell_1 = ebolt,
                // spell_2 = flamestrike,
                // spell_3 = explosion,
                // spell_4 = chainlightning,
                // spell_5 = masscurse,
                // spell_6 = fireball,
                // TrueColor = 0x4631,
                ActiveSpeed = 0.2,
                AiType = AIType.AI_Mage /* spellkillpcs */,
                AlwaysMurderer = true,
                BaseSoundID = 417,
                Body = 0x21,
                CorpseNameOverride = "corpse of an Undead Lizard",
                CreatureType = CreatureType.Undead,
                DamageMax = 43,
                DamageMin = 8,
                Dex = 85,
                Female = false,
                FightMode = FightMode.Closest,
                FightRange = 1,
                Hides = 5,
                HideType = HideType.Lizard,
                HitsMax = 290,
                Hue = 0x4631,
                Int = 300,
                ManaMaxSeed = 250,
                Name = "an Undead Lizard",
                PassiveSpeed = 0.4,
                PerceptionRange = 10,
                PreferredSpells = new List<Type>
                {
                    typeof(Spells.First.MagicArrowSpell),
                    typeof(Spells.Sixth.EnergyBoltSpell),
                    typeof(Spells.Sixth.ExplosionSpell),
                    typeof(Spells.Sixth.MassCurseSpell),
                    typeof(Spells.Third.FireballSpell)
                },
                Resistances = new Dictionary<ElementalType, CreatureProp>
                {
                    {ElementalType.Poison, 100}
                },
                SaySpellMantra = true,
                Skills = new Dictionary<SkillName, CreatureProp>
                {
                    {SkillName.MagicResist, 60},
                    {SkillName.Tactics, 70},
                    {SkillName.Magery, 90}
                },
                StamMaxSeed = 80,
                Str = 290,
                VirtualArmor = 30
            });
        }


        [Constructible]
        public UndeadLizard() : base(CreatureProperties.Get<UndeadLizard>())
        {
            // Add customization here

            AddItem(new SkinningKnife
            {
                Movable = false,
                Name = "Dead Lizard Weapon",
                Speed = 40,
                MaxHitPoints = 250,
                HitPoints = 250,
                HitSound = 0x19F,
                MissSound = 0x239
            });
        }

        [Constructible]
        public UndeadLizard(Serial serial) : base(serial)
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