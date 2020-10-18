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
    public class InvisibleMan : BaseCreature
    {
        static InvisibleMan()
        {
            CreatureProperties.Register<InvisibleMan>(new CreatureProperties
            {
                // DataElementId = invisibleman,
                // DataElementType = NpcTemplate,
                // dstart = 10,
                // Equip = invisibleman,
                // Graphic = 0x0ec4 /* Weapon */,
                // hostile = 1,
                // lootgroup = 47,
                // Macefighting_0 = 50,
                // MagicItemChance = 1,
                // MissSound = 0x239 /* Weapon */,
                // script = killpcs,
                // Speed = 50 /* Weapon */,
                // Swordsmanship = 50,
                // TrueColor = 0,
                ActiveSpeed = 0.2,
                AiType = AIType.AI_Melee /* killpcs */,
                AlwaysMurderer = true,
                Body = 0x1a8,
                CorpseNameOverride = "corpse of an invisible man",
                CreatureType = CreatureType.Human,
                DamageMax = 30,
                DamageMin = 3,
                Dex = 60,
                Female = false,
                FightMode = FightMode.Aggressor,
                FightRange = 1,
                HitsMax = 176,
                Hue = 0,
                Int = 126,
                ManaMaxSeed = 26,
                Name = "an invisible man",
                PassiveSpeed = 0.4,
                PerceptionRange = 10,
                Skills = new Dictionary<SkillName, CreatureProp>
                {
                    {SkillName.Fencing, 50},
                    {SkillName.Parry, 50},
                    {SkillName.Macing, 50},
                    {SkillName.Tactics, 50},
                    {SkillName.MagicResist, 75}
                },
                StamMaxSeed = 50,
                Str = 176,
                VirtualArmor = 20
            });
        }


        [Constructible]
        public InvisibleMan() : base(CreatureProperties.Get<InvisibleMan>())
        {
            // Add customization here

            AddItem(new SkinningKnife
            {
                Movable = false,
                Name = "Invisibleman Weapon",
                Speed = 50,
                MaxHitPoints = 250,
                HitPoints = 250,
                MissSound = 0x239
            });
        }

        [Constructible]
        public InvisibleMan(Serial serial) : base(serial)
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