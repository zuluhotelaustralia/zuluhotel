

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
    public class MasterOfWind : BaseCreature
    {
        static MasterOfWind() => CreatureProperties.Register<MasterOfWind>(new CreatureProperties
        {
            // cast_pct = 65,
            // CProp_PermMagicImmunity = i4,
            // DataElementId = airmaster,
            // DataElementType = NpcTemplate,
            // dstart = 10,
            // Equip = airmaster,
            // Graphic = 0x13F9 /* Weapon */,
            // HitSound = 0x168 /* Weapon */,
            // hostile = 1,
            // lootgroup = 57,
            // Macefighting = 100,
            // MagicItemChance = 66,
            // MagicItemLevel = 3,
            // MissSound = 0x239 /* Weapon */,
            // num_casts = 5,
            // script = spellkillpcs,
            // speech = 35,
            // Speed = 35 /* Weapon */,
            // spell = summonair,
            // spell_0 = lightning,
            // spell_1 = chainlightning,
            // TrueColor = 0,
            ActiveSpeed = 0.2,
            AiType = AIType.AI_Mage /* spellkillpcs */,
            AlwaysMurderer = true,
            Body = 0x190,
            CorpseNameOverride = "corpse of a master of the wind",
            CreatureType = CreatureType.Human,
            DamageMax = 60,
            DamageMin = 6,
            Dex = 90,
            Female = false,
            FightMode = FightMode.Closest,
            FightRange = 1,
            HitsMax = 160,
            Hue = 0,
            Int = 295,
            ManaMaxSeed = 95,
            Name = "a master of the wind",
            PassiveSpeed = 0.4,
            PerceptionRange = 10,
            PreferredSpells = new List<Type>
            {
                typeof(Spells.Fourth.LightningSpell),
            },
            ProvokeSkillOverride = 94,
            Resistances = new Dictionary<ElementalType, CreatureProp>
            {
                { ElementalType.Energy, 100 },
            },
            SaySpellMantra = true,
            Skills = new Dictionary<SkillName, CreatureProp>
            {
                { SkillName.MagicResist, 120 },
                { SkillName.Tactics, 100 },
                { SkillName.Magery, 150 },
            },
            StamMaxSeed = 50,
            Str = 160,
            VirtualArmor = 30,

        });


        [Constructible]
public MasterOfWind() : base(CreatureProperties.Get<MasterOfWind>())
        {
            // Add customization here

            AddItem(new LongHair(Race.RandomHairHue())
            {
                Movable = false,
            });

            AddItem(new GnarledStaff
            {
                Movable = false,
                Name = "Air Master Weapon",
                Speed = 35,
                MaxHitPoints = 250,
                HitPoints = 250,
                HitSound = 0x168,
                MissSound = 0x239,
            });


        }

        [Constructible]
public MasterOfWind(Serial serial) : base(serial) {}



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
