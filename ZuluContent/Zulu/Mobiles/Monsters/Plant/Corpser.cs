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
    public class Corpser : BaseCreature
    {
        static Corpser()
        {
            CreatureProperties.Register<Corpser>(new CreatureProperties
            {
                // DataElementId = corpser,
                // DataElementType = NpcTemplate,
                // dstart = 10,
                // Equip = corpser,
                // Graphic = 0x0ec4 /* Weapon */,
                // Hitscript = :combat:paralyzehit /* Weapon */,
                // HitSound = 0x163 /* Weapon */,
                // hostile = 1,
                LootTable = "32",
                // Macefighting = 105,
                // MissSound = 0x239 /* Weapon */,
                // script = killpcs,
                // Speed = 60 /* Weapon */,
                // TrueColor = 0,
                ActiveSpeed = 0.2,
                AiType = AIType.AI_Melee /* killpcs */,
                AlwaysMurderer = true,
                Body = 0x8,
                CorpseNameOverride = "corpse of a corpser",
                CreatureType = CreatureType.Plant,
                DamageMax = 30,
                DamageMin = 5,
                Dex = 105,
                Female = false,
                FightMode = FightMode.Aggressor,
                FightRange = 1,
                HitsMax = 150,
                Hue = 0,
                Int = 45,
                ManaMaxSeed = 35,
                Name = "a corpser",
                PassiveSpeed = 0.4,
                PerceptionRange = 10,
                Resistances = new Dictionary<ElementalType, CreatureProp>
                {
                    {ElementalType.Earth, 100}
                },
                Skills = new Dictionary<SkillName, CreatureProp>
                {
                    {SkillName.Parry, 20},
                    {SkillName.Hiding, 75},
                    {SkillName.Tactics, 90},
                    {SkillName.MagicResist, 20}
                },
                StamMaxSeed = 95,
                Str = 150,
                VirtualArmor = 20
            });
        }


        [Constructible]
        public Corpser() : base(CreatureProperties.Get<Corpser>())
        {
            // Add customization here

            AddItem(new SkinningKnife
            {
                Movable = false,
                Name = "Corpser Weapon",
                Speed = 60,
                MaxHitPoints = 250,
                HitPoints = 250,
                HitSound = 0x163,
                MissSound = 0x239
            });
        }

        [Constructible]
        public Corpser(Serial serial) : base(serial)
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