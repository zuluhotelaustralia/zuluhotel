

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
    public class FireElemental : BaseCreature
    {
        static FireElemental() => CreatureProperties.Register<FireElemental>(new CreatureProperties
        {
            // cast_pct = 40,
            // CProp_nocorpse = i1,
            // DataElementId = fireelemental,
            // DataElementType = NpcTemplate,
            // dstart = 10,
            // Equip = fireelemental,
            // Graphic = 0x0ec4 /* Weapon */,
            // HitSound = 0x114 /* Weapon */,
            // hostile = 1,
            // lootgroup = 20,
            // lootgroup_0 = 124,
            // MagicItemChance = 25,
            // MagicItemLevel = 3,
            // MissSound = 0x239 /* Weapon */,
            // num_casts = 10,
            // script = spellkillpcs,
            // Speed = 45 /* Weapon */,
            // spell = magicarrow,
            // spell_0 = fireball,
            // spell_1 = reflect,
            // spell_2 = explosion,
            // spell_3 = fstrike,
            // Swordsmanship = 100,
            // TrueColor = 33784,
            ActiveSpeed = 0.2,
            AiType = AIType.AI_Mage /* spellkillpcs */,
            AlwaysMurderer = true,
            Body = 0x0f,
            CorpseNameOverride = "corpse of a fire elemental",
            CreatureType = CreatureType.Elemental,
            DamageMax = 45,
            DamageMin = 21,
            Dex = 80,
            Female = false,
            FightMode = FightMode.Closest,
            FightRange = 1,
            HitsMax = 180,
            Hue = 33784,
            Int = 205,
            ManaMaxSeed = 100,
            Name = "a fire elemental",
            PassiveSpeed = 0.4,
            PerceptionRange = 10,
            PreferredSpells = new List<Type>
            {
                typeof(Spells.First.MagicArrowSpell),
                typeof(Spells.Third.FireballSpell),
                typeof(Spells.Fifth.MagicReflectSpell),
                typeof(Spells.Sixth.ExplosionSpell),
                typeof(Spells.Seventh.FlameStrikeSpell),
            },
            Resistances = new Dictionary<ElementalType, CreatureProp>
            {
                { ElementalType.Fire, 100 },
            },
            Skills = new Dictionary<SkillName, CreatureProp>
            {
                { SkillName.Parry, 55 },
                { SkillName.Tactics, 100 },
                { SkillName.MagicResist, 70 },
                { SkillName.Magery, 100 },
                { SkillName.EvalInt, 65 },
            },
            StamMaxSeed = 50,
            Str = 180,
            VirtualArmor = 20,

        });


        [Constructible]
public FireElemental() : base(CreatureProperties.Get<FireElemental>())
        {
            // Add customization here

            AddItem(new SkinningKnife
            {
                Movable = false,
                Name = "Fire Elemental Weapon",
                Speed = 45,
                Skill = SkillName.Swords,
                MaxHitPoints = 250,
                HitPoints = 250,
                HitSound = 0x114,
                MissSound = 0x239,
            });


        }

        [Constructible]
public FireElemental(Serial serial) : base(serial) {}



        public override void Serialize(IGenericWriter writer)
        {
            base.Serialize(writer);
            writer.Write((int) 0);
        }

        public override void Deserialize(IGenericReader reader)
        {
            base.Deserialize(reader);
            int version = reader.ReadInt();
        }
    }
}
