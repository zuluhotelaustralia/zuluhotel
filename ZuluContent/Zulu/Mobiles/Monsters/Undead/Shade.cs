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
    public class Shade : BaseCreature
    {
        static Shade()
        {
            CreatureProperties.Register<Shade>(new CreatureProperties
            {
                // DataElementId = shade,
                // DataElementType = NpcTemplate,
                // dstart = 10,
                // Equip = shade,
                // equip_0 = bewitchedsword,
                // Graphic = 0x0ec4 /* Weapon */,
                // HitSound = 0x181 /* Weapon */,
                // hostile = 1,
                LootTable = "25",
                LootItemChance = 50,
                LootItemLevel = 1,
                // MissSound = 0x239 /* Weapon */,
                // script = killpcs,
                // Speed = 35 /* Weapon */,
                // TrueColor = 17969,
                ActiveSpeed = 0.2,
                AiType = AIType.AI_Melee /* killpcs */,
                AlwaysMurderer = true,
                Body = 0x190,
                CorpseNameOverride = "corpse of a shade",
                CreatureType = CreatureType.Undead,
                DamageMax = 44,
                DamageMin = 8,
                Dex = 90,
                Female = false,
                FightMode = FightMode.Aggressor,
                FightRange = 1,
                HitsMax = 300,
                Hue = 17969,
                Int = 35,
                ManaMaxSeed = 25,
                Name = "a shade",
                PassiveSpeed = 0.4,
                PerceptionRange = 10,
                Resistances = new Dictionary<ElementalType, CreatureProp>
                {
                    {ElementalType.Poison, 6}
                },
                Skills = new Dictionary<SkillName, CreatureProp>
                {
                    {SkillName.MagicResist, 60},
                    {SkillName.Tactics, 80},
                    {SkillName.Macing, 105}
                },
                StamMaxSeed = 50,
                Str = 300,
                VirtualArmor = 20
            });
        }


        [Constructible]
        public Shade() : base(CreatureProperties.Get<Shade>())
        {
            // Add customization here

            AddItem(new SkinningKnife
            {
                Movable = false,
                Name = "Shade Weapon",
                Speed = 35,
                MaxHitPoints = 250,
                HitPoints = 250,
                HitSound = 0x181,
                MissSound = 0x239
            });
        }

        [Constructible]
        public Shade(Serial serial) : base(serial)
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