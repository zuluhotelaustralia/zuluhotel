

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
    public class SpectralDrake : BaseCreature
    {
        static SpectralDrake() => CreatureProperties.Register<SpectralDrake>(new CreatureProperties
        {
            // cast_pct = 17,
            // CProp_PermMagicImmunity = i4,
            // DataElementId = spectraldrake,
            // DataElementType = NpcTemplate,
            // dstart = 10,
            // Equip = spectraldrake,
            // food = meat,
            // Graphic = 0x0ec4 /* Weapon */,
            // HitSound = 0x16D /* Weapon */,
            // hostile = 1,
            // lootgroup = 36,
            // MagicItemChance = 70,
            // MagicItemLevel = 4,
            // MissSound = 0x239 /* Weapon */,
            // num_casts = 3,
            // script = spellkillpcs,
            // Speed = 45 /* Weapon */,
            // spell = ebolt,
            // spell_0 = flamestrike,
            // spell_1 = meteorswarm,
            // spell_2 = lightning,
            // spell_3 = chainlightning,
            // TrueColor = 0x4631,
            // virtue = 6,
            ActiveSpeed = 0.2,
            AiType = AIType.AI_Mage /* spellkillpcs */,
            AlwaysMurderer = true,
            BaseSoundID = 362,
            Body = 0x3c,
            CorpseNameOverride = "corpse of a spectral drake",
            CreatureType = CreatureType.Dragonkin,
            DamageMax = 73,
            DamageMin = 33,
            Dex = 105,
            Female = false,
            FightMode = FightMode.Closest,
            FightRange = 1,
            Hides = 5,
            HideType = HideType.Dragon,
            HitsMax = 550,
            Hue = 0x4631,
            Int = 360,
            ManaMaxSeed = 150,
            MinTameSkill = 120,
            Name = "a spectral drake",
            PassiveSpeed = 0.4,
            PerceptionRange = 10,
            PreferredSpells = new List<Type>
            {
                typeof(Spells.Sixth.EnergyBoltSpell),
                typeof(Spells.Fourth.LightningSpell),
            },
            ProvokeSkillOverride = 130,
            Skills = new Dictionary<SkillName, CreatureProp>
            {
                { SkillName.Tactics, 120 },
                { SkillName.Macing, 140 },
                { SkillName.Magery, 125 },
                { SkillName.MagicResist, 90 },
            },
            StamMaxSeed = 50,
            Str = 550,
            Tamable = true,
            VirtualArmor = 40,
  
        });

        [Constructable]
        public SpectralDrake() : base(CreatureProperties.Get<SpectralDrake>())
        {
            // Add customization here

            AddItem(new SkinningKnife
            {
                Movable = false,
                Name = "Spectral Drake Weapon",
                Speed = 45,
                MaxHitPoints = 250,
                HitPoints = 250,
                HitSound = 0x16D,
                MissSound = 0x239,
            });
  
  
        }

        public SpectralDrake(Serial serial) : base(serial) {}

  

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