

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
    public class AdeptMage : BaseCreature
    {
        static AdeptMage() => CreatureProperties.Register<AdeptMage>(new CreatureProperties
        {
            // cast_pct = 35,
            // DataElementId = eviladept,
            // DataElementType = NpcTemplate,
            // dstart = 10,
            // Equip = eviladept,
            // Graphic = 0x13f9 /* Weapon */,
            // HitSound = 0x13C /* Weapon */,
            // hostile = 1,
            // lootgroup = 25,
            // Macefighting = 85,
            // MagicItemChance = 75,
            // MagicItemLevel = 2,
            // MissSound = 0x239 /* Weapon */,
            // num_casts = 8,
            // script = spellkillpcs,
            // speech = 35,
            // Speed = 30 /* Weapon */,
            // spell = lightning,
            // spell_0 = curse,
            // spell_1 = weaken,
            // spell_2 = clumsy,
            // spell_3 = fireball,
            // spell_4 = magicarrow,
            // spell_5 = ebolt,
            // spell_6 = flamestrike,
            // TrueColor = 0,
            ActiveSpeed = 0.2,
            AiType = AIType.AI_Mage /* spellkillpcs */,
            AlwaysMurderer = true,
            Body = 0x190,
            CorpseNameOverride = "corpse of an adept mage",
            CreatureType = CreatureType.Human,
            DamageMax = 40,
            DamageMin = 4,
            Dex = 90,
            Female = false,
            FightMode = FightMode.Closest,
            FightRange = 1,
            HitsMax = 200,
            Hue = 0,
            Int = 295,
            ManaMaxSeed = 95,
            Name = "an adept mage",
            PassiveSpeed = 0.4,
            PerceptionRange = 10,
            PreferredSpells = new List<Type>
            {
                typeof(Spells.Fourth.LightningSpell),
                typeof(Spells.Fourth.CurseSpell),
                typeof(Spells.First.WeakenSpell),
                typeof(Spells.First.ClumsySpell),
                typeof(Spells.Third.FireballSpell),
                typeof(Spells.First.MagicArrowSpell),
                typeof(Spells.Sixth.EnergyBoltSpell),
            },
            ProvokeSkillOverride = 94,
            SaySpellMantra = true,
            Skills = new Dictionary<SkillName, CreatureProp>
            {
                { SkillName.MagicResist, 90 },
                { SkillName.Tactics, 85 },
                { SkillName.Magery, 100 },
            },
            StamMaxSeed = 50,
            Str = 200,
            VirtualArmor = 20,

        });


        [Constructible]
public AdeptMage() : base(CreatureProperties.Get<AdeptMage>())
        {
            // Add customization here

            AddItem(new LongHair(Race.RandomHairHue())
            {
                Movable = false,
            });

            AddItem(new GnarledStaff
            {
                Movable = false,
                Name = "Evil Mage Weapon",
                Speed = 30,
                MaxHitPoints = 250,
                HitPoints = 250,
                HitSound = 0x13C,
                MissSound = 0x239,
            });


        }

        [Constructible]
public AdeptMage(Serial serial) : base(serial) {}



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
