

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
    public class OphidianZealot : BaseCreature
    {
        static OphidianZealot() => CreatureProperties.Register<OphidianZealot>(new CreatureProperties
        {
            // cast_pct = 50,
            // count_cast = 1,
            // DataElementId = ophidianzealot,
            // DataElementType = NpcTemplate,
            // dstart = 10,
            // Equip = ophidianzealot,
            // Graphic = 0x0ec4 /* Weapon */,
            // HitSound = 0x168 /* Weapon */,
            // hostile = 1,
            // lootgroup = 66,
            // MissSound = 0x169 /* Weapon */,
            // num_casts = 3,
            // script = spellkillpcs,
            // Speed = 30 /* Weapon */,
            // spell = summonfire,
            // spell_0 = lightning,
            // spell_1 = ebolt,
            // spell_2 = magicreflection,
            // spell_3 = fireball,
            // spell_4 = explosion,
            // spell_5 = flamestrike,
            // TrueColor = 0,
            // virtue = 2,
            ActiveSpeed = 0.2,
            AiType = AIType.AI_Mage /* spellkillpcs */,
            AlwaysMurderer = true,
            Body = 85,
            CorpseNameOverride = "corpse of an Ophidian Zealot",
            CreatureType = CreatureType.Ophidian,
            DamageMax = 44,
            DamageMin = 8,
            Dex = 510,
            Female = false,
            FightMode = FightMode.Closest,
            FightRange = 1,
            Hides = 5,
            HideType = HideType.Serpent,
            HitsMax = 375,
            Hue = 0,
            Int = 400,
            ManaMaxSeed = 400,
            Name = "an Ophidian Zealot",
            PassiveSpeed = 0.4,
            PerceptionRange = 10,
            PreferredSpells = new List<Type>
            {
                typeof(Spells.Fourth.LightningSpell),
                typeof(Spells.Sixth.EnergyBoltSpell),
                typeof(Spells.Third.FireballSpell),
                typeof(Spells.Sixth.ExplosionSpell),
            },
            ProvokeSkillOverride = 120,
            Skills = new Dictionary<SkillName, CreatureProp>
            {
                { SkillName.Parry, 70 },
                { SkillName.Magery, 110 },
                { SkillName.Macing, 70 },
                { SkillName.Tactics, 70 },
                { SkillName.MagicResist, 50 },
                { SkillName.EvalInt, 90 },
            },
            StamMaxSeed = 70,
            Str = 375,
            VirtualArmor = 25,
  
        });

        [Constructable]
        public OphidianZealot() : base(CreatureProperties.Get<OphidianZealot>())
        {
            // Add customization here

            AddItem(new SkinningKnife
            {
                Movable = false,
                Name = "Ophidian Zealot Weapon",
                Speed = 30,
                MaxHitPoints = 250,
                HitPoints = 250,
                HitSound = 0x168,
                MissSound = 0x169,
            });
  
  
        }

        public OphidianZealot(Serial serial) : base(serial) {}

  

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