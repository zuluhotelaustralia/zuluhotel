

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
    public class FireCyclops : BaseCreature
    {
        static FireCyclops() => CreatureProperties.Register<FireCyclops>(new CreatureProperties
        {
            // cast_pct = 60,
            // CProp_PermMagicImmunity = i5,
            // DataElementId = firecyclops,
            // DataElementType = NpcTemplate,
            // dstart = 10,
            // Equip = djinn,
            // Graphic = 0x0ec4 /* Weapon */,
            // HitSound = 0x25F /* Weapon */,
            // lootgroup = 9,
            // MagicItemChance = 100,
            // MagicItemLevel = 5,
            // MissSound = 0x169 /* Weapon */,
            // num_casts = 12,
            // script = spellkillpcs,
            // Speed = 30 /* Weapon */,
            // spell = flamestrike,
            // spell_0 = fireball,
            // spell_1 = risingfire,
            // spell_2 = abyssalflame,
            // TrueColor = 0x04b9,
            ActiveSpeed = 0.2,
            AiType = AIType.AI_Mage /* spellkillpcs */,
            AlwaysMurderer = true,
            Body = 0x4b,
            CorpseNameOverride = "corpse of a Fire Cyclops",
            CreatureType = CreatureType.Elemental,
            DamageMax = 60,
            DamageMin = 6,
            Dex = 300,
            Female = false,
            FightMode = FightMode.None,
            FightRange = 1,
            HitsMax = 2000,
            Hue = 0x04b9,
            Int = 255,
            ManaMaxSeed = 1600,
            Name = "a Fire Cyclops",
            PassiveSpeed = 0.4,
            PerceptionRange = 10,
            PreferredSpells = new List<Type>
            {
                typeof(Spells.Third.FireballSpell),
                typeof(RunZH.Scripts.Zulu.Spells.Earth.RisingFireSpell),
                typeof(RunZH.Scripts.Zulu.Spells.Necromancy.AbyssalFlameSpell),
            },
            Resistances = new Dictionary<ElementalType, CreatureProp>
            {
                { ElementalType.Fire, 100 },
            },
            SaySpellMantra = true,
            Skills = new Dictionary<SkillName, CreatureProp>
            {
                { SkillName.Tactics, 180 },
                { SkillName.Macing, 180 },
                { SkillName.Magery, 180 },
                { SkillName.MagicResist, 180 },
            },
            StamMaxSeed = 90,
            Str = 1000,
            VirtualArmor = 45,
  
        });

        [Constructable]
        public FireCyclops() : base(CreatureProperties.Get<FireCyclops>())
        {
            // Add customization here

            AddItem(new SkinningKnife
            {
                Movable = false,
                Name = "Djinn Weapon",
                Speed = 30,
                MaxHitPoints = 250,
                HitPoints = 250,
                HitSound = 0x25F,
                MissSound = 0x169,
            });
  
  
        }

        public FireCyclops(Serial serial) : base(serial) {}

  

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