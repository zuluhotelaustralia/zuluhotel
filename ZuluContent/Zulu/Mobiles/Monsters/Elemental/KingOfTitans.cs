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
    public class KingOfTitans : BaseCreature
    {
        static KingOfTitans()
        {
            CreatureProperties.Register<KingOfTitans>(new CreatureProperties
            {
                // cast_pct = 95,
                // DataElementId = hiddenkingoftitans,
                // DataElementType = NpcTemplate,
                // dstart = 10,
                // Equip = kingoftitans,
                // Graphic = 0x0ec4 /* Weapon */,
                // hiding = 170,
                // HitSound = 0x25F /* Weapon */,
                LootTable = "201",
                LootItemChance = 100,
                LootItemLevel = 9,
                // MissSound = 0x169 /* Weapon */,
                // mount = 0,
                // num_casts = 30,
                // script = firebreatherspells,
                // Speed = 40 /* Weapon */,
                // spell = TitanStomp,
                // TrueColor = 1172,
                AiType = AIType.AI_Melee /* firebreatherspells */,
                AlwaysMurderer = true,
                Body = 189,
                CorpseNameOverride = "corpse of a King of Titans",
                CreatureType = CreatureType.Elemental,
                DamageMax = 144,
                DamageMin = 12,
                Dex = 300,
                Female = false,
                FightMode = FightMode.None,
                FightRange = 1,
                HasBreath = true,
                HitsMax = 6750,
                Hue = 1172,
                Int = 1255,
                ManaMaxSeed = 500,
                Name = "a King of Titans",
                PerceptionRange = 10,
                Resistances = new Dictionary<ElementalType, CreatureProp>
                {
                    {ElementalType.Fire, 100},
                    {ElementalType.Air, 100},
                    {ElementalType.Water, 75},
                    {ElementalType.Poison, 1},
                    {ElementalType.Necro, 100},
                    {ElementalType.Earth, 100},
                    {ElementalType.PermMagicImmunity, 8}
                },
                SaySpellMantra = true,
                Skills = new Dictionary<SkillName, CreatureProp>
                {
                    {SkillName.Tactics, 200},
                    {SkillName.Macing, 200},
                    {SkillName.Magery, 170},
                    {SkillName.MagicResist, 170}
                },
                StamMaxSeed = 5000,
                Str = 2050,
                VirtualArmor = 100
            });
        }


        [Constructible]
        public KingOfTitans() : base(CreatureProperties.Get<KingOfTitans>())
        {
            // Add customization here

            AddItem(new SkinningKnife
            {
                Movable = false,
                Name = "King of Titans Weapon",
                Speed = 40,
                MaxHitPoints = 250,
                HitPoints = 250,
                HitSound = 0x25F,
                MissSound = 0x169
            });
        }

        [Constructible]
        public KingOfTitans(Serial serial) : base(serial)
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