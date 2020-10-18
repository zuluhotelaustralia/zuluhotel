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
    public class GiantFlamingSpider : BaseCreature
    {
        static GiantFlamingSpider()
        {
            CreatureProperties.Register<GiantFlamingSpider>(new CreatureProperties
            {
                // alignment_0 = evil,
                // DataElementId = flamingspider,
                // DataElementType = NpcTemplate,
                // dstart = 10,
                // Equip = flamingspider,
                // food = meat,
                // Graphic = 0x0ec4 /* Weapon */,
                // Hitscript = :combat:poisonhit /* Weapon */,
                // HitSound = 0x186 /* Weapon */,
                // hostile = 1,
                // MissSound = 0x239 /* Weapon */,
                // script = firebreather,
                // Speed = 30 /* Weapon */,
                // spell = fireball,
                // spell_0 = flamestrike,
                // spell_1 = Meteorswarm,
                // TrueColor = 232,
                AiType = AIType.AI_Melee /* firebreather */,
                AlwaysMurderer = true,
                Body = 0x1c,
                CorpseNameOverride = "corpse of a giant flaming spider",
                CreatureType = CreatureType.Animal,
                DamageMax = 35,
                DamageMin = 15,
                Dex = 80,
                Female = false,
                FightMode = FightMode.Aggressor,
                FightRange = 1,
                HasBreath = true,
                HitPoison = Poison.Regular,
                HitsMax = 250,
                Hue = 232,
                Int = 90,
                ManaMaxSeed = 40,
                MinTameSkill = 90,
                Name = "a giant flaming spider",
                PerceptionRange = 10,
                PreferredSpells = new List<Type>
                {
                    typeof(Spells.Third.FireballSpell)
                },
                ProvokeSkillOverride = 90,
                Skills = new Dictionary<SkillName, CreatureProp>
                {
                    {SkillName.Tactics, 70},
                    {SkillName.Macing, 85},
                    {SkillName.Magery, 100},
                    {SkillName.MagicResist, 80}
                },
                StamMaxSeed = 70,
                Str = 250,
                Tamable = true,
                VirtualArmor = 30
            });
        }


        [Constructible]
        public GiantFlamingSpider() : base(CreatureProperties.Get<GiantFlamingSpider>())
        {
            // Add customization here

            AddItem(new SkinningKnife
            {
                Movable = false,
                Name = "Flaming Spider Weapon",
                Speed = 30,
                MaxHitPoints = 250,
                HitPoints = 250,
                HitSound = 0x186,
                MissSound = 0x239
            });
        }

        [Constructible]
        public GiantFlamingSpider(Serial serial) : base(serial)
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