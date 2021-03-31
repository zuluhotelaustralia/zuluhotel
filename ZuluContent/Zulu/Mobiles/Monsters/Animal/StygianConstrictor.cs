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
    public class StygianConstrictor : BaseCreature
    {
        static StygianConstrictor()
        {
            CreatureProperties.Register<StygianConstrictor>(new CreatureProperties
            {
                // DataElementId = stygianconstrictor,
                // DataElementType = NpcTemplate,
                // dstart = 10,
                // Equip = stygianconstrictor,
                // food = meat,
                // Graphic = 0x0ec4 /* Weapon */,
                // HitSound = 0x0DD /* Weapon */,
                // hostile = 1,
                // MissSound = 0x0DC /* Weapon */,
                // noloot = 1,
                // script = killpcs,
                // Speed = 35 /* Weapon */,
                // Swordsmanship = 100,
                // TrueColor = 1157,
                ActiveSpeed = 0.2,
                AiType = AIType.AI_Melee /* killpcs */,
                AlwaysMurderer = true,
                Body = 0x15,
                CorpseNameOverride = "corpse of a Stygian Constrictor",
                CreatureType = CreatureType.Animal,
                DamageMax = 69,
                DamageMin = 13,
                Dex = 110,
                Female = false,
                FightMode = FightMode.Aggressor,
                FightRange = 1,
                Hides = 5,
                HideType = HideType.Serpent,
                HitsMax = 275,
                Hue = 1157,
                Int = 175,
                ManaMaxSeed = 70,
                Name = "a Stygian Constrictor",
                PassiveSpeed = 0.4,
                PerceptionRange = 10,
                Resistances = new Dictionary<ElementalType, CreatureProp>
                {
                    {ElementalType.Poison, 3}
                },
                Skills = new Dictionary<SkillName, CreatureProp>
                {
                    {SkillName.MagicResist, 70},
                    {SkillName.Tactics, 90},
                    {SkillName.Poisoning, 90}
                },
                StamMaxSeed = 75,
                Str = 275,
                VirtualArmor = 40
            });
        }


        [Constructible]
        public StygianConstrictor() : base(CreatureProperties.Get<StygianConstrictor>())
        {
            // Add customization here

            AddItem(new SkinningKnife
            {
                Movable = false,
                Name = "Stygian Constrictor Weapon",
                Speed = 35,
                Skill = SkillName.Swords,
                MaxHitPoints = 250,
                HitPoints = 250,
                HitSound = 0x0DD,
                MissSound = 0x0DC
            });
        }

        [Constructible]
        public StygianConstrictor(Serial serial) : base(serial)
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