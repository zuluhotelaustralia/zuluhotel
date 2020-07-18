

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
    public class LizardShaman : BaseCreature
    {
        static LizardShaman() => CreatureProperties.Register<LizardShaman>(new CreatureProperties
        {
            // cast_pct = 50,
            // count_casts = 1,
            // DataElementId = lizardmanshaman,
            // DataElementType = NpcTemplate,
            // dstart = 10,
            // Equip = lizardmanshaman,
            // Graphic = 0x0ec4 /* Weapon */,
            // HitSound = 0x1A5 /* Weapon */,
            // hostile = 1,
            // lootgroup = 54,
            // MagicItemChance = 50,
            // MagicItemLevel = 1,
            // MissSound = 0x239 /* Weapon */,
            // num_casts = 10,
            // script = critterhealer,
            // Speed = 30 /* Weapon */,
            // spell = summonfire,
            // spell_0 = ebolt,
            // spell_1 = curse,
            // TrueColor = 0x04C2,
            ActiveSpeed = 0.2,
            AiType = AIType.AI_Mage /* critterhealer */,
            AlwaysMurderer = true,
            BaseSoundID = 417,
            Body = 0x21,
            CorpseNameOverride = "corpse of <random> the Lizard Shaman",
            CreatureType = CreatureType.Dragonkin,
            DamageMax = 32,
            DamageMin = 4,
            Dex = 110,
            Female = false,
            FightMode = FightMode.Closest,
            FightRange = 1,
            Hides = 5,
            HideType = HideType.Lizard,
            HitsMax = 170,
            Hue = 0x04C2,
            Int = 280,
            ManaMaxSeed = 100,
            Name = "<random> the Lizard Shaman",
            PassiveSpeed = 0.4,
            PerceptionRange = 10,
            PreferredSpells = new List<Type>
            {
                typeof(Spells.Sixth.EnergyBoltSpell),
                typeof(Spells.Fourth.CurseSpell),
            },
            ProvokeSkillOverride = 105,
            SaySpellMantra = true,
            Skills = new Dictionary<SkillName, CreatureProp>
            {
                { SkillName.Tactics, 80 },
                { SkillName.Macing, 75 },
                { SkillName.MagicResist, 90 },
                { SkillName.Magery, 100 },
            },
            StamMaxSeed = 100,
            Str = 170,
            VirtualArmor = 10,
  
        });

        [Constructable]
        public LizardShaman() : base(CreatureProperties.Get<LizardShaman>())
        {
            // Add customization here

            AddItem(new SkinningKnife
            {
                Movable = false,
                Name = "Lizardman Shaman Weapon",
                Speed = 30,
                MaxHitPoints = 250,
                HitPoints = 250,
                HitSound = 0x1A5,
                MissSound = 0x239,
            });
  
  
        }

        public LizardShaman(Serial serial) : base(serial) {}

  

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