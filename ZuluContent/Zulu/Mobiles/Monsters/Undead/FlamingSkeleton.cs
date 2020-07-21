

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
    public class FlamingSkeleton : BaseCreature
    {
        static FlamingSkeleton() => CreatureProperties.Register<FlamingSkeleton>(new CreatureProperties
        {
            // cast_pct = 25,
            // CProp_NecroProtection = i3,
            // DataElementId = flamingskeleton,
            // DataElementType = NpcTemplate,
            // dstart = 10,
            // Equip = flamingskeleton,
            // Graphic = 0x0ec4 /* Weapon */,
            // HitSound = 0x1C7 /* Weapon */,
            // hostile = 1,
            // lootgroup = 23,
            // MagicItemChance = 1,
            // MissSound = 0x239 /* Weapon */,
            // num_casts = 3,
            // script = spellkillpcs,
            // Speed = 35 /* Weapon */,
            // spell = flamestrike,
            // spell_0 = fireball,
            // spell_1 = explosion,
            // targetText = "Ego apokteinou",
            // TrueColor = 0x04b1,
            ActiveSpeed = 0.2,
            AiType = AIType.AI_Mage /* spellkillpcs */,
            AlwaysMurderer = true,
            Body = 0x39,
            CorpseNameOverride = "corpse of a flaming skeleton",
            CreatureType = CreatureType.Undead,
            DamageMax = 40,
            DamageMin = 5,
            Dex = 180,
            Female = false,
            FightMode = FightMode.Closest,
            FightRange = 1,
            HitsMax = 170,
            Hue = 0x04b1,
            Int = 180,
            ManaMaxSeed = 80,
            Name = "a flaming skeleton",
            PassiveSpeed = 0.4,
            PerceptionRange = 10,
            PreferredSpells = new List<Type>
            {
                typeof(Spells.Third.FireballSpell),
                typeof(Spells.Sixth.ExplosionSpell),
            },
            Resistances = new Dictionary<ElementalType, CreatureProp>
            {
                { ElementalType.Poison, 100 },
            },
            SaySpellMantra = true,
            Skills = new Dictionary<SkillName, CreatureProp>
            {
                { SkillName.MagicResist, 80 },
                { SkillName.Tactics, 50 },
                { SkillName.Macing, 120 },
                { SkillName.Magery, 75 },
            },
            StamMaxSeed = 50,
            Str = 170,
            VirtualArmor = 20,

        });


        [Constructible]
public FlamingSkeleton() : base(CreatureProperties.Get<FlamingSkeleton>())
        {
            // Add customization here

            AddItem(new SkinningKnife
            {
                Movable = false,
                Name = "Flaming Skeleton Weapon",
                Speed = 35,
                MaxHitPoints = 250,
                HitPoints = 250,
                HitSound = 0x1C7,
                MissSound = 0x239,
            });


        }

        [Constructible]
public FlamingSkeleton(Serial serial) : base(serial) {}



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
