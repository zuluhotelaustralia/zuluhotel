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
    public class DarklingSlime : BaseCreature
    {
        static DarklingSlime()
        {
            CreatureProperties.Register<DarklingSlime>(new CreatureProperties
            {
                // DataElementId = darklingslime,
                // DataElementType = NpcTemplate,
                // dstart = 10,
                // Equip = darklingslime,
                // Graphic = 0x0ec4 /* Weapon */,
                // HitSound = 0x1CB /* Weapon */,
                LootTable = "125",
                // MissSound = 0x239 /* Weapon */,
                // script = slime,
                // Speed = 35 /* Weapon */,
                // Swordsmanship = 100,
                // TrueColor = 25125,
                ActiveSpeed = 0.2,
                AiType = AIType.AI_Melee /* slime */,
                AlwaysMurderer = true,
                Body = 0x33,
                CorpseNameOverride = "corpse of a darkling slime",
                CreatureType = CreatureType.Slime,
                DamageMax = 43,
                DamageMin = 8,
                Dex = 150,
                Female = false,
                FightMode = FightMode.None,
                FightRange = 1,
                HitsMax = 200,
                Hue = 25125,
                Int = 10,
                ManaMaxSeed = 0,
                Name = "a darkling slime",
                PassiveSpeed = 0.4,
                PerceptionRange = 10,
                Resistances = new Dictionary<ElementalType, CreatureProp>
                {
                    {ElementalType.Poison, 100}
                },
                Skills = new Dictionary<SkillName, CreatureProp>
                {
                    {SkillName.MagicResist, 90},
                    {SkillName.Poisoning, 40},
                    {SkillName.Tactics, 100}
                },
                StamMaxSeed = 50,
                Str = 200,
                VirtualArmor = 30
            });
        }


        [Constructible]
        public DarklingSlime() : base(CreatureProperties.Get<DarklingSlime>())
        {
            // Add customization here

            AddItem(new SkinningKnife
            {
                Movable = false,
                Name = "Darkling Slime Weapon",
                Speed = 35,
                Skill = SkillName.Swords,
                MaxHitPoints = 250,
                HitPoints = 250,
                HitSound = 0x1CB,
                MissSound = 0x239
            });
        }

        [Constructible]
        public DarklingSlime(Serial serial) : base(serial)
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