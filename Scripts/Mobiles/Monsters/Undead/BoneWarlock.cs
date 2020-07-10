

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
    public class BoneWarlock : BaseCreature
    {
        static BoneWarlock() => CreatureProperties.Register<BoneWarlock>(new CreatureProperties
        {
            // cast_pct = 40,
            // DataElementId = bonewarlock,
            // DataElementType = NpcTemplate,
            // dstart = 10,
            // Equip = bonewarlock,
            // Graphic = 0x0ec4 /* Weapon */,
            // HitSound = 0x16C /* Weapon */,
            // hostile = 1,
            // lootgroup = 23,
            // MagicItemChance = 50,
            // MagicItemLevel = 2,
            // MissSound = 0x239 /* Weapon */,
            // num_casts = 10,
            // script = spellkillpcs,
            // speech = 54,
            // Speed = 30 /* Weapon */,
            // spell = poison,
            // spell_0 = lightning,
            // spell_1 = fireball,
            // spell_2 = summonskel,
            // spell_3 = paralyze,
            // spell_4 = ebolt,
            // TrueColor = 0,
            ActiveSpeed = 0.2,
            AiType = AIType.AI_Mage /* spellkillpcs */,
            AlwaysMurderer = true,
            Body = 0x38,
            CorpseNameOverride = "corpse of a bone warlock",
            CreatureType = CreatureType.Undead,
            DamageMax = 32,
            DamageMin = 4,
            Dex = 94,
            Female = false,
            FightMode = FightMode.Closest,
            FightRange = 1,
            HitsMax = 270,
            Hue = 0,
            Int = 295,
            ManaMaxSeed = 120,
            Name = "a bone warlock",
            PassiveSpeed = 0.4,
            PerceptionRange = 10,
            PreferredSpells = new List<Type>
            {
                typeof(Spells.Third.PoisonSpell),
                typeof(Spells.Fourth.LightningSpell),
                typeof(Spells.Third.FireballSpell),
                typeof(Spells.Fifth.ParalyzeSpell),
                typeof(Spells.Sixth.EnergyBoltSpell),
            },
            Resistances = new Dictionary<ElementalType, CreatureProp>
            {
                { ElementalType.Poison, 100 },
            },
            SaySpellMantra = true,
            Skills = new Dictionary<SkillName, CreatureProp>
            {
                { SkillName.MagicResist, 76 },
                { SkillName.Tactics, 100 },
                { SkillName.Macing, 110 },
                { SkillName.Magery, 100 },
            },
            StamMaxSeed = 50,
            Str = 270,
            VirtualArmor = 25,
  
        });

        [Constructable]
        public BoneWarlock() : base(CreatureProperties.Get<BoneWarlock>())
        {
            // Add customization here

            AddItem(new SkinningKnife
            {
                Movable = false,
                Name = "Bone Warlock Weapon",
                Speed = 30,
                MaxHitPoints = 250,
                HitPoints = 250,
                HitSound = 0x16C,
                MissSound = 0x239,
            });
  
  
        }

        public BoneWarlock(Serial serial) : base(serial) {}

  

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