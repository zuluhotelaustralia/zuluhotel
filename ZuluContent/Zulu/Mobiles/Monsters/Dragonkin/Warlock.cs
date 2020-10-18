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
    public class Warlock : BaseCreature
    {
        static Warlock()
        {
            CreatureProperties.Register<Warlock>(new CreatureProperties
            {
                // cast_pct = 50,
                // CProp_PermMagicImmunity = i2,
                // DataElementId = lizardmanwarlock,
                // DataElementType = NpcTemplate,
                // dstart = 10,
                // Equip = lizardmanwarlock,
                // Graphic = 0x0ec4 /* Weapon */,
                // HitSound = 0x1A5 /* Weapon */,
                // hostile = 1,
                // lootgroup = 55,
                // MagicItemChance = 50,
                // MagicItemLevel = 2,
                // MissSound = 0x239 /* Weapon */,
                // num_casts = 10,
                // script = spellkillpcs,
                // Speed = 35 /* Weapon */,
                // spell = ebolt,
                // spell_0 = lightning,
                // spell_1 = chainlightning,
                // spell_2 = fireball,
                // spell_3 = flamestrike,
                // spell_4 = curse,
                // spell_5 = paralyze,
                // TrueColor = 0x04B1,
                ActiveSpeed = 0.2,
                AiType = AIType.AI_Mage /* spellkillpcs */,
                AlwaysMurderer = true,
                BaseSoundID = 417,
                Body = 0x21,
                CorpseNameOverride = "corpse of <random> the Warlock",
                CreatureType = CreatureType.Dragonkin,
                DamageMax = 32,
                DamageMin = 4,
                Dex = 180,
                Female = false,
                FightMode = FightMode.Closest,
                FightRange = 1,
                Hides = 5,
                HideType = HideType.Lizard,
                HitsMax = 225,
                Hue = 0x04B1,
                Int = 295,
                ManaMaxSeed = 155,
                Name = "<random> the Warlock",
                PassiveSpeed = 0.4,
                PerceptionRange = 10,
                PreferredSpells = new List<Type>
                {
                    typeof(Spells.Sixth.EnergyBoltSpell),
                    typeof(Spells.Fourth.LightningSpell),
                    typeof(Spells.Third.FireballSpell),
                    typeof(Spells.Fourth.CurseSpell),
                    typeof(Spells.Fifth.ParalyzeSpell)
                },
                ProvokeSkillOverride = 105,
                SaySpellMantra = true,
                Skills = new Dictionary<SkillName, CreatureProp>
                {
                    {SkillName.Tactics, 50},
                    {SkillName.Macing, 50},
                    {SkillName.MagicResist, 90},
                    {SkillName.Magery, 110}
                },
                StamMaxSeed = 80,
                Str = 225,
                VirtualArmor = 20
            });
        }


        [Constructible]
        public Warlock() : base(CreatureProperties.Get<Warlock>())
        {
            // Add customization here

            AddItem(new SkinningKnife
            {
                Movable = false,
                Name = "Lizardman Warlock Weapon",
                Speed = 35,
                MaxHitPoints = 250,
                HitPoints = 250,
                HitSound = 0x1A5,
                MissSound = 0x239
            });
        }

        [Constructible]
        public Warlock(Serial serial) : base(serial)
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