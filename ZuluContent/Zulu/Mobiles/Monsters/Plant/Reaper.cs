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
    public class Reaper : BaseCreature
    {
        static Reaper()
        {
            CreatureProperties.Register<Reaper>(new CreatureProperties
            {
                // DataElementId = reaper,
                // DataElementType = NpcTemplate,
                // dstart = 10,
                // Equip = reaper,
                // Graphic = 0x0ec4 /* Weapon */,
                // HitSound = 0x1BD /* Weapon */,
                // hostile = 1,
                LootTable = "34",
                // MissSound = 0x239 /* Weapon */,
                // script = killpcs,
                // Speed = 20 /* Weapon */,
                // TrueColor = 0,
                ActiveSpeed = 0.2,
                AiType = AIType.AI_Melee /* killpcs */,
                AlwaysMurderer = true,
                Body = 0x2f,
                CorpseNameOverride = "corpse of a reaper",
                CreatureType = CreatureType.Plant,
                DamageMax = 50,
                DamageMin = 5,
                Dex = 110,
                Female = false,
                FightMode = FightMode.Aggressor,
                FightRange = 1,
                HitsMax = 210,
                Hue = 0,
                Int = 35,
                ManaMaxSeed = 25,
                Name = "a reaper",
                PassiveSpeed = 0.4,
                PerceptionRange = 10,
                Resistances = new Dictionary<ElementalType, CreatureProp>
                {
                    {ElementalType.PermPoisonImmunity, 100},
                    {ElementalType.Water, 100},
                    {ElementalType.Earth, 100}
                },
                Skills = new Dictionary<SkillName, CreatureProp>
                {
                    {SkillName.Parry, 60},
                    {SkillName.Magery, 100},
                    {SkillName.Tactics, 100},
                    {SkillName.Macing, 150},
                    {SkillName.MagicResist, 75}
                },
                StamMaxSeed = 100,
                Str = 210,
                VirtualArmor = 30
            });
        }


        [Constructible]
        public Reaper() : base(CreatureProperties.Get<Reaper>())
        {
            // Add customization here

            AddItem(new SkinningKnife
            {
                Movable = false,
                Name = "Reaper Weapon",
                Speed = 20,
                MaxHitPoints = 250,
                HitPoints = 250,
                HitSound = 0x1BD,
                MissSound = 0x239
            });
        }

        [Constructible]
        public Reaper(Serial serial) : base(serial)
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