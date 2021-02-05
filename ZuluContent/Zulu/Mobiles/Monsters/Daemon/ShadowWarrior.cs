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
    public class ShadowWarrior : BaseCreature
    {
        static ShadowWarrior()
        {
            CreatureProperties.Register<ShadowWarrior>(new CreatureProperties
            {
                // CProp_PermMagicImmunity = i4,
                // CProp_summoned = i1,
                // DataElementId = shadowwarrior,
                // DataElementType = NpcTemplate,
                // dstart = 10,
                // Equip = dracula,
                // Graphic = 0x0ec4 /* Weapon */,
                // guardignore = 1,
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
                Body = 0x190,
                CorpseNameOverride = "corpse of Shadow Warrior",
                CreatureType = CreatureType.Daemon,
                DamageMax = 73,
                DamageMin = 33,
                Dex = 1000,
                Female = false,
                FightMode = FightMode.Aggressor,
                FightRange = 1,
                HitsMax = 1000,
                Hue = 0,
                Int = 400,
                ManaMaxSeed = 400,
                Name = "Shadow Warrior",
                PassiveSpeed = 0.4,
                PerceptionRange = 10,
                Resistances = new Dictionary<ElementalType, CreatureProp>
                {
                    {ElementalType.PermPoisonImmunity, 100}
                },
                Skills = new Dictionary<SkillName, CreatureProp>
                {
                    {SkillName.Tactics, 150},
                    {SkillName.Macing, 150},
                    {SkillName.MagicResist, 150}
                },
                StamMaxSeed = 400,
                Str = 1200,
                VirtualArmor = 35
            });
        }


        [Constructible]
        public ShadowWarrior() : base(CreatureProperties.Get<ShadowWarrior>())
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
        public ShadowWarrior(Serial serial) : base(serial)
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