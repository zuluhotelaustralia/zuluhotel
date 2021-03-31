using System;
using System.Collections.Generic;
using Scripts.Zulu.Spells.Necromancy;
using Server;
using Server.Misc;
using Server.Items;
using static Server.Mobiles.CreatureProp;
using Server.Engines.Magic;
using Server.Engines.Harvest;

namespace Server.Mobiles
{
    public class UndeadDrake : BaseCreature
    {
        static UndeadDrake()
        {
            CreatureProperties.Register<UndeadDrake>(new CreatureProperties
            {
                // cast_pct = 20,

                // DataElementId = undeaddrake,
                // DataElementType = NpcTemplate,
                // dstart = 10,
                // Equip = undeaddrake,
                // food = meat,
                // Graphic = 0x0ec4 /* Weapon */,
                // Hitscript = :combat:lifedrainscript /* Weapon */,
                // HitSound = 0x16D /* Weapon */,
                // hostile = 1,
                LootTable = "37",
                LootItemChance = 25,
                LootItemLevel = 4,
                // MissSound = 0x239 /* Weapon */,
                // noloot = 1,
                // num_casts = 4,
                // script = spellkillpcs,
                // Speed = 45 /* Weapon */,
                // spell = flamestrike,
                // spell_0 = kill,
                // spell_1 = abyssalflame,
                // spell_2 = ebolt,
                // spell_3 = sorcerersbane,
                // spell_4 = decayingray,
                // spell_5 = darkness,
                // TrueColor = 1109,
                // virtue = 7,
                ActiveSpeed = 0.2,
                AiType = AIType.AI_Mage /* spellkillpcs */,
                AlwaysMurderer = true,
                BaseSoundID = 362,
                Body = 0x3c,
                CorpseNameOverride = "corpse of an Undead Drake",
                CreatureType = CreatureType.Undead,
                DamageMax = 73,
                DamageMin = 33,
                Dex = 110,
                Female = false,
                FightMode = FightMode.Closest,
                FightRange = 1,
                Hides = 5,
                HideType = HideType.Liche,
                HitsMax = 300,
                Hue = 1109,
                Int = 400,
                ManaMaxSeed = 80,
                MinTameSkill = 115,
                Name = "an Undead Drake",
                PassiveSpeed = 0.4,
                PerceptionRange = 10,
                PreferredSpells = new List<Type>
                {
                    typeof(WyvernStrikeSpell),
                    typeof(AbyssalFlameSpell),
                    typeof(Spells.Sixth.EnergyBoltSpell),
                    typeof(SorcerersBaneSpell),
                    typeof(DecayingRaySpell),
                    typeof(DarknessSpell)
                },
                ProvokeSkillOverride = 120,
                Resistances = new Dictionary<ElementalType, CreatureProp>
                {
                    {ElementalType.Poison, 6},
                    {ElementalType.Necro, 100},
                    {ElementalType.PermMagicImmunity, 4}
                },
                Skills = new Dictionary<SkillName, CreatureProp>
                {
                    {SkillName.Parry, 70},
                    {SkillName.MagicResist, 100},
                    {SkillName.Tactics, 100},
                    {SkillName.Macing, 130},
                    {SkillName.EvalInt, 100},
                    {SkillName.DetectHidden, 130}
                },
                StamMaxSeed = 100,
                Str = 300,
                Tamable = true,
                VirtualArmor = 30
            });
        }


        [Constructible]
        public UndeadDrake() : base(CreatureProperties.Get<UndeadDrake>())
        {
            // Add customization here

            AddItem(new SkinningKnife
            {
                Movable = false,
                Name = "Undead Drake Weapon",
                Speed = 45,
                MaxHitPoints = 250,
                HitPoints = 250,
                HitSound = 0x16D,
                MissSound = 0x239
            });
        }

        [Constructible]
        public UndeadDrake(Serial serial) : base(serial)
        {
        }


        public override void Serialize(IGenericWriter writer)
        {
            base.Serialize(writer);
            writer.Write((int) 0);
        }

        public override void Deserialize(IGenericReader reader)
        {
            base.Deserialize(reader);
            var version = reader.ReadInt();
        }
    }
}