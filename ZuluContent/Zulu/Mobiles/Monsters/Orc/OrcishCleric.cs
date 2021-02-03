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
    public class OrcishCleric : BaseCreature
    {
        static OrcishCleric()
        {
            CreatureProperties.Register<OrcishCleric>(new CreatureProperties
            {
                // cast_pct = 25,
                // DataElementId = orccleric,
                // DataElementType = NpcTemplate,
                // dstart = 10,
                // Equip = orccleric,
                // Graphic = 0x0ec4 /* Weapon */,
                // HitSound = 0x1B4 /* Weapon */,
                // hostile = 1,
                LootTable = "31",
                LootItemChance = 60,
                LootItemLevel = 1,
                // MissSound = 0x239 /* Weapon */,
                // num_casts = 5,
                // script = critterhealer,
                // speech = 6,
                // Speed = 30 /* Weapon */,
                // spell = summonwater,
                // spell_0 = lightning,
                // spell_1 = chainlightning,
                // TrueColor = 0x0579,
                ActiveSpeed = 0.2,
                AiType = AIType.AI_Mage /* critterhealer */,
                AlwaysMurderer = true,
                Body = 0x11,
                CorpseNameOverride = "corpse of <random> the Orcish Cleric",
                CreatureType = CreatureType.Orc,
                DamageMax = 32,
                DamageMin = 4,
                Dex = 90,
                Female = false,
                FightMode = FightMode.Closest,
                FightRange = 1,
                HitsMax = 215,
                Hue = 0x0579,
                Int = 290,
                ManaMaxSeed = 90,
                Name = "<random> the Orcish Cleric",
                PassiveSpeed = 0.4,
                PerceptionRange = 10,
                PreferredSpells = new List<Type>
                {
                    typeof(Spells.Fourth.LightningSpell)
                },
                ProvokeSkillOverride = 115,
                SaySpellMantra = true,
                Skills = new Dictionary<SkillName, CreatureProp>
                {
                    {SkillName.MagicResist, 80},
                    {SkillName.Tactics, 50},
                    {SkillName.Macing, 75},
                    {SkillName.Magery, 100}
                },
                StamMaxSeed = 80,
                Str = 215,
                VirtualArmor = 15
            });
        }


        [Constructible]
        public OrcishCleric() : base(CreatureProperties.Get<OrcishCleric>())
        {
            // Add customization here

            AddItem(new SkinningKnife
            {
                Movable = false,
                Name = "Orc Cleric Weapon",
                Speed = 30,
                MaxHitPoints = 250,
                HitPoints = 250,
                HitSound = 0x1B4,
                MissSound = 0x239
            });
        }

        [Constructible]
        public OrcishCleric(Serial serial) : base(serial)
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