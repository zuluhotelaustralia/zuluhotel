

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
    public class StygianLiche : BaseCreature
    {
        static StygianLiche() => CreatureProperties.Register<StygianLiche>(new CreatureProperties
        {
            // cast_pct = 60,
            // CProp_NecroProtection = i8,
            // CProp_PermMagicImmunity = i8,
            // DataElementId = stygianliche,
            // DataElementType = NpcTemplate,
            // dstart = 10,
            // Equip = behemothlord,
            // Graphic = 0x0ec4 /* Weapon */,
            // Hitscript = :combat:banishscript /* Weapon */,
            // HitSound = 0x16D /* Weapon */,
            // hostile = 1,
            // lootgroup = 201,
            // MagicItemChance = 90,
            // MagicItemChance_0 = 65,
            // Magicitemlevel = 8,
            // MagicItemLevel_0 = 6,
            // MissSound = 0x239 /* Weapon */,
            // num_casts = 12,
            // script = spellkillpcs,
            // speech = 35,
            // Speed = 50 /* Weapon */,
            // spell = poison,
            // spell_0 = flamestrike,
            // spell_1 = ebolt,
            // spell_10 = abyssalflame,
            // spell_11 = icestrike,
            // spell_2 = lightning,
            // spell_3 = explosion,
            // spell_4 = wraithbreath,
            // spell_5 = wyvernstrike,
            // spell_6 = plague,
            // spell_7 = decayingray,
            // spell_8 = darkness,
            // spell_9 = kill,
            // TrueColor = 1174,
            // virtue = 4,
            ActiveSpeed = 0.2,
            AiType = AIType.AI_Mage /* spellkillpcs */,
            AlwaysMurderer = true,
            AutoDispel = true,
            Body = 0x18,
            CorpseNameOverride = "corpse of a Stygian Liche",
            CreatureType = CreatureType.Undead,
            DamageMax = 60,
            DamageMin = 10,
            Dex = 300,
            Female = false,
            FightMode = FightMode.Closest,
            FightRange = 1,
            HitsMax = 2400,
            Hue = 1174,
            Int = 2800,
            ManaMaxSeed = 2000,
            Name = "a Stygian Liche",
            PassiveSpeed = 0.4,
            PerceptionRange = 10,
            PreferredSpells = new List<Type>
            {
                typeof(Spells.Third.PoisonSpell),
                typeof(Spells.Sixth.EnergyBoltSpell),
                typeof(Spells.Fourth.LightningSpell),
                typeof(Spells.Sixth.ExplosionSpell),
                typeof(Spells.Necromancy.WraithBreathSpell),
                typeof(Spells.Necromancy.WyvernStrikeSpell),
                typeof(Spells.Necromancy.PlagueSpell),
                typeof(Spells.Necromancy.DecayingRaySpell),
                typeof(Spells.Necromancy.DarknessSpell),
                typeof(Spells.Necromancy.WyvernStrikeSpell),
                typeof(Spells.Necromancy.AbyssalFlameSpell),
                typeof(Spells.Earth.IceStrikeSpell),
            },
            Resistances = new Dictionary<ElementalType, CreatureProp>
            {
                { ElementalType.Poison, 100 },
            },
            SaySpellMantra = true,
            Skills = new Dictionary<SkillName, CreatureProp>
            {
                { SkillName.Magery, 215 },
                { SkillName.MagicResist, 200 },
                { SkillName.Tactics, 190 },
                { SkillName.Macing, 135 },
                { SkillName.DetectHidden, 200 },
            },
            StamMaxSeed = 70,
            Str = 2000,
            VirtualArmor = 65,
  
        });

        [Constructable]
        public StygianLiche() : base(CreatureProperties.Get<StygianLiche>())
        {
            // Add customization here

            AddItem(new SkinningKnife
            {
                Movable = false,
                Name = "Behemoth Weapon",
                Speed = 50,
                MaxHitPoints = 250,
                HitPoints = 250,
                HitSound = 0x16D,
                MissSound = 0x239,
            });
  
  
        }

        public StygianLiche(Serial serial) : base(serial) {}

  

        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);
            writer.Write((int) 0);
        }

        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);
            int version = reader.ReadInt();
        }
    }
}