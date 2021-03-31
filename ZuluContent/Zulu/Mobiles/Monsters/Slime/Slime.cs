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
    public class Slime : BaseCreature
    {
        static Slime()
        {
            CreatureProperties.Register<Slime>(new CreatureProperties
            {
                // DataElementId = slime,
                // DataElementType = NpcTemplate,
                // dstart = 10,
                // Equip = slime,
                // Graphic = 0x0ec4 /* Weapon */,
                // guardignore = 1,
                // HitSound = 0x1CB /* Weapon */,
                LootTable = "62",
                // MissSound = 0x239 /* Weapon */,
                // script = slime,
                // Speed = 25 /* Weapon */,
                // Swordsmanship = 30,
                // TrueColor = 0,
                ActiveSpeed = 0.2,
                AiType = AIType.AI_Melee /* slime */,
                AlwaysMurderer = true,
                Body = 0x33,
                CorpseNameOverride = "corpse of a slime",
                CreatureType = CreatureType.Slime,
                DamageMax = 10,
                DamageMin = 2,
                Dex = 50,
                Female = false,
                FightMode = FightMode.None,
                FightRange = 1,
                HitsMax = 40,
                Hue = 0,
                Int = 15,
                ManaMaxSeed = 0,
                MinTameSkill = 20,
                Name = "a slime",
                PassiveSpeed = 0.4,
                PerceptionRange = 10,
                ProvokeSkillOverride = 40,
                Resistances = new Dictionary<ElementalType, CreatureProp>
                {
                    {ElementalType.Poison, 6}
                },
                Skills = new Dictionary<SkillName, CreatureProp>
                {
                    {SkillName.Parry, 18},
                    {SkillName.MagicResist, 20},
                    {SkillName.Poisoning, 40},
                    {SkillName.Tactics, 50}
                },
                StamMaxSeed = 50,
                Str = 40,
                Tamable = true,
                VirtualArmor = 5
            });
        }


        [Constructible]
        public Slime() : base(CreatureProperties.Get<Slime>())
        {
            // Add customization here

            AddItem(new SkinningKnife
            {
                Movable = false,
                Name = "Slime Weapon",
                Speed = 25,
                Skill = SkillName.Swords,
                MaxHitPoints = 250,
                HitPoints = 250,
                HitSound = 0x1CB,
                MissSound = 0x239
            });
        }

        [Constructible]
        public Slime(Serial serial) : base(serial)
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