

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
    public class UndeadDrake : BaseCreature
    {
        static UndeadDrake() => CreatureProperties.Register<UndeadDrake>(new CreatureProperties
        {
            // cast_pct = 20,
            // CProp_NecroProtection = i8,
            // CProp_PermMagicImmunity = i4,
            // DataElementId = undeaddrake,
            // DataElementType = NpcTemplate,
            // dstart = 10,
            // Equip = undeaddrake,
            // food = meat,
            // Graphic = 0x0ec4 /* Weapon */,
            // Hitscript = :combat:lifedrainscript /* Weapon */,
            // HitSound = 0x16D /* Weapon */,
            // hostile = 1,
            // lootgroup = 37,
            // MagicItemChance = 25,
            // MagicItemLevel = 4,
            // MissSound = 0x239 /* Weapon */,
            // noloot = 1,
            // num_casts = 4,
            // script = spellkillpcs,
            // Speed = 45 /* Weapon */,
            // spell = flamestrike,
            // spell_0 = kill,
            // spell_1 = abyssalflame,
            // spell_2 = ebolt,
            // spell_3 = sorcerersbane,
            // spell_4 = decayingray,
            // spell_5 = darkness,
            // TrueColor = 1109,
            // virtue = 7,
            ActiveSpeed = 0.2,
            AiType = AIType.AI_Mage /* spellkillpcs */,
            AlwaysMurderer = true,
            BaseSoundID = 362,
            Body = 0x3c,
            CorpseNameOverride = "corpse of an Undead Drake",
            CreatureType = CreatureType.Undead,
            DamageMax = 73,
            DamageMin = 33,
            Dex = 110,
            Female = false,
            FightMode = FightMode.Closest,
            FightRange = 1,
            Hides = 5,
            HideType = HideType.Liche,
            HitsMax = 300,
            Hue = 1109,
            Int = 400,
            ManaMaxSeed = 80,
            MinTameSkill = 115,
            Name = "an Undead Drake",
            PassiveSpeed = 0.4,
            PerceptionRange = 10,
            PreferredSpells = new List<Type>
            {
                typeof(RunZH.Scripts.Zulu.Spells.Necromancy.WyvernStrikeSpell),
                typeof(RunZH.Scripts.Zulu.Spells.Necromancy.AbyssalFlameSpell),
                typeof(Spells.Sixth.EnergyBoltSpell),
                typeof(RunZH.Scripts.Zulu.Spells.Necromancy.SorcerorsBaneSpell),
                typeof(RunZH.Scripts.Zulu.Spells.Necromancy.DecayingRaySpell),
                typeof(RunZH.Scripts.Zulu.Spells.Necromancy.DarknessSpell),
            },
            ProvokeSkillOverride = 120,
            Resistances = new Dictionary<ElementalType, CreatureProp>
            {
                { ElementalType.Poison, 100 },
            },
            Skills = new Dictionary<SkillName, CreatureProp>
            {
                { SkillName.Parry, 70 },
                { SkillName.MagicResist, 100 },
                { SkillName.Tactics, 100 },
                { SkillName.Macing, 130 },
                { SkillName.EvalInt, 100 },
                { SkillName.DetectHidden, 130 },
            },
            StamMaxSeed = 100,
            Str = 300,
            Tamable = true,
            VirtualArmor = 30,
  
        });

        [Constructable]
        public UndeadDrake() : base(CreatureProperties.Get<UndeadDrake>())
        {
            // Add customization here

            AddItem(new SkinningKnife
            {
                Movable = false,
                Name = "Undead Drake Weapon",
                Speed = 45,
                MaxHitPoints = 250,
                HitPoints = 250,
                HitSound = 0x16D,
                MissSound = 0x239,
            });
  
  
        }

        public UndeadDrake(Serial serial) : base(serial) {}

  

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