

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
    public class FireElementalLord : BaseCreature
    {
        static FireElementalLord() => CreatureProperties.Register<FireElementalLord>(new CreatureProperties
        {
            // CProp_nocorpse = i1,
            // DataElementId = fireelementallord,
            // DataElementType = NpcTemplate,
            // Equip = fireelementallord,
            // Graphic = 0x0ec4 /* Weapon */,
            // HitSound = 0x114 /* Weapon */,
            // hostile = 1,
            // lootgroup = 73,
            // MagicItemChance = 60,
            // MagicItemLevel = 4,
            // MissSound = 0x239 /* Weapon */,
            // script = spellkillpcs,
            // Speed = 40 /* Weapon */,
            // spell = flamestrike,
            // spell_0 = ebolt,
            // spell_1 = lightning,
            // spell_2 = fireball,
            // spell_3 = summonfire,
            // spell_4 = explosion,
            // TrueColor = 137,
            ActiveSpeed = 0.2,
            AiType = AIType.AI_Mage /* spellkillpcs */,
            AlwaysMurderer = true,
            Body = 0x0f,
            CorpseNameOverride = "corpse of a fire elemental lord",
            CreatureType = CreatureType.Elemental,
            DamageMax = 36,
            DamageMin = 6,
            Dex = 300,
            Female = false,
            FightMode = FightMode.Closest,
            FightRange = 1,
            HitsMax = 300,
            Hue = 137,
            Int = 400,
            ManaMaxSeed = 900,
            Name = "a fire elemental lord",
            PassiveSpeed = 0.4,
            PerceptionRange = 10,
            PreferredSpells = new List<Type>
            {
                typeof(Spells.Sixth.EnergyBoltSpell),
                typeof(Spells.Fourth.LightningSpell),
                typeof(Spells.Third.FireballSpell),
                typeof(Spells.Sixth.ExplosionSpell),
            },
            Resistances = new Dictionary<ElementalType, CreatureProp>
            {
                { ElementalType.Fire, 100 },
            },
            Skills = new Dictionary<SkillName, CreatureProp>
            {
                { SkillName.Tactics, 130 },
                { SkillName.Macing, 175 },
                { SkillName.Magery, 100 },
                { SkillName.MagicResist, 75 },
            },
            StamMaxSeed = 100,
            Str = 300,
            VirtualArmor = 40,

        });


        [Constructible]
public FireElementalLord() : base(CreatureProperties.Get<FireElementalLord>())
        {
            // Add customization here

            AddItem(new SkinningKnife
            {
                Movable = false,
                Name = "Fire Elemental Lord Weapon",
                Speed = 40,
                MaxHitPoints = 250,
                HitPoints = 250,
                HitSound = 0x114,
                MissSound = 0x239,
            });


        }

        [Constructible]
public FireElementalLord(Serial serial) : base(serial) {}



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
