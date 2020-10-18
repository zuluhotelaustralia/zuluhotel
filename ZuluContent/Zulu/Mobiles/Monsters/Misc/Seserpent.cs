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
    public class Seserpent : BaseCreature
    {
        static Seserpent()
        {
            CreatureProperties.Register<Seserpent>(new CreatureProperties
            {
                // DataElementId = seaserpent,
                // DataElementType = NpcTemplate,
                // dstart = 10,
                // Equip = seaserpent,
                // Graphic = 0x0ec4 /* Weapon */,
                // HitSound = 0x1C1 /* Weapon */,
                // lootgroup = 50,
                // MissSound = 0x239 /* Weapon */,
                // script = killpcs,
                // Speed = 30 /* Weapon */,
                // TrueColor = 33784,
                ActiveSpeed = 0.2,
                AiType = AIType.AI_Melee /* killpcs */,
                AlwaysAttackable = true,
                BaseSoundID = 446,
                Body = 0x96,
                CanSwim = true,
                CorpseNameOverride = "corpse of a sea serpent",
                DamageMax = 16,
                DamageMin = 2,
                Dex = 180,
                Female = false,
                FightMode = FightMode.None,
                FightRange = 1,
                Hides = 5,
                HideType = HideType.Serpent,
                HitsMax = 250,
                Hue = 33784,
                Int = 35,
                ManaMaxSeed = 0,
                Name = "a sea serpent",
                PassiveSpeed = 0.4,
                PerceptionRange = 10,
                ProvokeSkillOverride = 60,
                Skills = new Dictionary<SkillName, CreatureProp>
                {
                    {SkillName.Parry, 70},
                    {SkillName.Tactics, 50},
                    {SkillName.Macing, 150},
                    {SkillName.MagicResist, 90},
                    {SkillName.Poisoning, 80}
                },
                StamMaxSeed = 50,
                Str = 250,
                VirtualArmor = 15
            });
        }


        [Constructible]
        public Seserpent() : base(CreatureProperties.Get<Seserpent>())
        {
            // Add customization here

            AddItem(new SkinningKnife
            {
                Movable = false,
                Name = "Sea Serpent Weapon",
                Speed = 30,
                MaxHitPoints = 250,
                HitPoints = 250,
                HitSound = 0x1C1,
                MissSound = 0x239
            });
        }

        [Constructible]
        public Seserpent(Serial serial) : base(serial)
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