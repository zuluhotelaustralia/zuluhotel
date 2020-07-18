

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
    public class Necromancer : BaseCreature
    {
        static Necromancer() => CreatureProperties.Register<Necromancer>(new CreatureProperties
        {
            // cast_pct = 40,
            // CProp_EarthProtection = i3,
            // CProp_NecroProtection = i3,
            // DataElementId = necromancer,
            // DataElementType = NpcTemplate,
            // dstart = 10,
            // Equip = necromancer,
            // Graphic = 0x0df0 /* Weapon */,
            // HitSound = 0x14B /* Weapon */,
            // lootgroup = 46,
            // MagicItemChance = 50,
            // MagicItemLevel = 3,
            // MissSound = 0x239 /* Weapon */,
            // num_casts = 5,
            // script = spellkillpcs,
            // speech = 35,
            // Speed = 50 /* Weapon */,
            // spell = flamestrike,
            // spell_0 = ebolt,
            // spell_1 = lightning,
            // spell_2 = summonskel,
            // spell_3 = fireball,
            // spell_4 = masscurse,
            // spell_5 = explosion,
            // spell_6 = wraithform,
            // spell_7 = liche,
            // spell_8 = darkness,
            // spell_9 = decayingray,
            // TrueColor = 0,
            ActiveSpeed = 0.2,
            AiType = AIType.AI_Mage /* spellkillpcs */,
            AlwaysAttackable = true,
            Body = 0x190,
            CorpseNameOverride = "corpse of <random> the Necromancer",
            CreatureType = CreatureType.Human,
            DamageMax = 64,
            DamageMin = 8,
            Dex = 90,
            Female = false,
            FightMode = FightMode.None,
            FightRange = 1,
            Hides = 3,
            HideType = HideType.Necromancer,
            HitsMax = 130,
            Hue = 0,
            Int = 300,
            ManaMaxSeed = 95,
            Name = "<random> the Necromancer",
            PassiveSpeed = 0.4,
            PerceptionRange = 10,
            PreferredSpells = new List<Type>
            {
                typeof(Spells.Sixth.EnergyBoltSpell),
                typeof(Spells.Fourth.LightningSpell),
                typeof(Spells.Third.FireballSpell),
                typeof(Spells.Sixth.MassCurseSpell),
                typeof(Spells.Sixth.ExplosionSpell),
                typeof(RunZH.Scripts.Zulu.Spells.Necromancy.WraithFormSpell),
                typeof(RunZH.Scripts.Zulu.Spells.Necromancy.LicheFormSpell),
                typeof(RunZH.Scripts.Zulu.Spells.Necromancy.DarknessSpell),
                typeof(RunZH.Scripts.Zulu.Spells.Necromancy.DecayingRaySpell),
            },
            ProvokeSkillOverride = 90,
            Resistances = new Dictionary<ElementalType, CreatureProp>
            {
                { ElementalType.Fire, 75 },
            },
            RiseCreatureDelay = TimeSpan.FromSeconds(8),
            SaySpellMantra = true,
            Skills = new Dictionary<SkillName, CreatureProp>
            {
                { SkillName.MagicResist, 80 },
                { SkillName.Tactics, 100 },
                { SkillName.Macing, 100 },
                { SkillName.Magery, 150 },
            },
            StamMaxSeed = 50,
            Str = 130,
            VirtualArmor = 25,
  
        });

        [Constructable]
        public Necromancer() : base(CreatureProperties.Get<Necromancer>())
        {
            // Add customization here

            AddItem(new LongHair(Utility.RandomHairHue())
            {
                Movable = false,
            });
  
            AddItem(new BlackStaff
            {
                Movable = false,
                Name = "Necromancer Weapon",
                Speed = 50,
                MaxHitPoints = 250,
                HitPoints = 250,
                HitSound = 0x14B,
                MissSound = 0x239,
            });
  
  
        }

        public Necromancer(Serial serial) : base(serial) {}

  

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