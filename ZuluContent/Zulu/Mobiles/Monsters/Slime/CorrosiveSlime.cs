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
    public class CorrosiveSlime : BaseCreature
    {
        static CorrosiveSlime()
        {
            CreatureProperties.Register<CorrosiveSlime>(new CreatureProperties
            {
                // DataElementId = corrosiveslime,
                // DataElementType = NpcTemplate,
                // dstart = 10,
                // Equip = corrosiveslime,
                // Graphic = 0x0ec4 /* Weapon */,
                // Hitscript = :combat:acidscript /* Weapon */,
                // HitSound = 0x04F /* Weapon */,
                LootTable = "125",
                // MissSound = 0x239 /* Weapon */,
                // script = killpcsTeleporter,
                // Speed = 10 /* Weapon */,
                // Swordsmanship = 130,
                // TrueColor = 1296,
                AiType = AIType.AI_Melee /* killpcsTeleporter */,
                AlwaysMurderer = true,
                Body = 0x33,
                CorpseNameOverride = "corpse of a corrosive slime",
                CreatureType = CreatureType.Slime,
                DamageMax = 60,
                DamageMin = 15,
                Dex = 150,
                Female = false,
                FightMode = FightMode.None,
                FightRange = 7,
                HitsMax = 500,
                Hue = 1296,
                Int = 10,
                ManaMaxSeed = 0,
                Name = "a corrosive slime",
                PerceptionRange = 10,
                Resistances = new Dictionary<ElementalType, CreatureProp>
                {
                    {ElementalType.Poison, 100},
                    {ElementalType.Fire, 100}
                },
                Skills = new Dictionary<SkillName, CreatureProp>
                {
                    {SkillName.MagicResist, 90},
                    {SkillName.Poisoning, 40},
                    {SkillName.Tactics, 130}
                },
                StamMaxSeed = 150,
                Str = 600,
                TargetAcquireExhaustion = true,
                VirtualArmor = 50
            });
        }


        [Constructible]
        public CorrosiveSlime() : base(CreatureProperties.Get<CorrosiveSlime>())
        {
            // Add customization here

            AddItem(new SkinningKnife
            {
                Movable = false,
                Name = "Corrosive Slime Weapon",
                Hue = 1296,
                Speed = 10,
                Skill = SkillName.Swords,
                MaxHitPoints = 250,
                HitPoints = 250,
                HitSound = 0x04F,
                MissSound = 0x239,
                MaxRange = 7,
                Animation = (WeaponAnimation) 0x0009
            });
        }

        [Constructible]
        public CorrosiveSlime(Serial serial) : base(serial)
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