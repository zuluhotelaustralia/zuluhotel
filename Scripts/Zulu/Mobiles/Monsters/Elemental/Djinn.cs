

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
    public class Djinn : BaseCreature
    {
        static Djinn() => CreatureProperties.Register<Djinn>(new CreatureProperties
        {
            // cast_pct = 60,
            // CProp_nocorpse = i1,
            // CProp_PermMagicImmunity = i3,
            // DataElementId = djinn,
            // DataElementType = NpcTemplate,
            // dstart = 10,
            // Equip = djinn,
            // Graphic = 0x0ec4 /* Weapon */,
            // HitSound = 0x25F /* Weapon */,
            // lootgroup = 46,
            // MagicItemChance = 60,
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
            Body = 0x4c,
            CorpseNameOverride = "corpse of a Djinn",
            CreatureType = CreatureType.Elemental,
            DamageMax = 60,
            DamageMin = 6,
            Dex = 100,
            Female = false,
            FightMode = FightMode.None,
            FightRange = 1,
            Hides = 4,
            HideType = HideType.Lava,
            HitsMax = 450,
            Hue = 0x04b9,
            Int = 255,
            ManaMaxSeed = 160,
            Name = "a Djinn",
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
                { SkillName.Tactics, 125 },
                { SkillName.Macing, 175 },
                { SkillName.Magery, 100 },
                { SkillName.MagicResist, 130 },
            },
            StamMaxSeed = 90,
            Str = 450,
            VirtualArmor = 45,
  
        });

        [Constructable]
        public Djinn() : base(CreatureProperties.Get<Djinn>())
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

        public Djinn(Serial serial) : base(serial) {}

  

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