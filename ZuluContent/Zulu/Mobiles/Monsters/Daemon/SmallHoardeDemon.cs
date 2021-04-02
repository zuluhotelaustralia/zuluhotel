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
    public class SmallHoardeDemon : BaseCreature
    {
        static SmallHoardeDemon()
        {
            CreatureProperties.Register<SmallHoardeDemon>(new CreatureProperties
            {
                // DataElementId = smallhoardedemon,
                // DataElementType = NpcTemplate,
                // dstart = 10,
                // Equip = dracula,
                // Graphic = 0x0ec4 /* Weapon */,
                // Hitscript = :combat:lifedrainscript /* Weapon */,
                // HitSound = 0x25A /* Weapon */,
                // hostile = 1,
                LootTable = "69",
                LootItemChance = 50,
                LootItemLevel = 5,
                // MissSound = 0x239 /* Weapon */,
                // script = killpcs,
                // Speed = 35 /* Weapon */,
                // TrueColor = 0,
                ActiveSpeed = 0.2,
                AiType = AIType.AI_Melee /* killpcs */,
                AlwaysMurderer = true,
                Body = 776,
                CorpseNameOverride = "corpse of a Small Hoarde Demon",
                CreatureType = CreatureType.Daemon,
                DamageMax = 73,
                DamageMin = 33,
                Dex = 500,
                Female = false,
                FightMode = FightMode.Aggressor,
                FightRange = 1,
                HitsMax = 600,
                Hue = 0,
                Int = 100,
                ManaMaxSeed = 100,
                Name = "a Small Hoarde Demon",
                PassiveSpeed = 0.4,
                PerceptionRange = 10,
                Resistances = new Dictionary<ElementalType, CreatureProp>
                {
                    {ElementalType.Poison, 1},
                    {ElementalType.MagicImmunity, 1}
                },
                Skills = new Dictionary<SkillName, CreatureProp>
                {
                    {SkillName.Tactics, 130},
                    {SkillName.Macing, 130},
                    {SkillName.MagicResist, 130}
                },
                StamMaxSeed = 400,
                Str = 600,
                VirtualArmor = 35
            });
        }


        [Constructible]
        public SmallHoardeDemon() : base(CreatureProperties.Get<SmallHoardeDemon>())
        {
            // Add customization here

            AddItem(new ShortHair(Race.RandomHairHue())
            {
                Movable = false,
                Hue = 0x1
            });

            AddItem(new SkinningKnife
            {
                Movable = false,
                Name = "Dracula Weapon",
                Speed = 35,
                MaxHitPoints = 250,
                HitPoints = 250,
                HitSound = 0x25A,
                MissSound = 0x239
            });
        }

        [Constructible]
        public SmallHoardeDemon(Serial serial) : base(serial)
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