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
    public class SkitteringHopper : BaseCreature
    {
        static SkitteringHopper()
        {
            CreatureProperties.Register<SkitteringHopper>(new CreatureProperties
            {
                // CProp_BaseHpRegen = i1000,
                // CProp_NoReactiveArmour = i1,


                // DataElementId = skitteringhopper,
                // DataElementType = NpcTemplate,
                // dstart = 10,
                // Equip = BalronSpawn,
                // Graphic = 0x0ec4 /* Weapon */,
                // Hitscript = :combat:lifedrainscript /* Weapon */,
                // HitSound = 0x25A /* Weapon */,
                // hostile = 1,
                LootTable = "69",
                LootItemChance = 70,
                LootItemLevel = 3,
                // MissSound = 0x239 /* Weapon */,
                // script = killpcsTeleporter,
                // Speed = 35 /* Weapon */,
                // TrueColor = 33784,
                AiType = AIType.AI_Melee /* killpcsTeleporter */,
                AlwaysMurderer = true,
                Body = 302,
                CorpseNameOverride = "corpse of a Skittering Hopper",
                CreatureType = CreatureType.Daemon,
                DamageMax = 73,
                DamageMin = 33,
                Dex = 200,
                Female = false,
                FightMode = FightMode.Aggressor,
                FightRange = 1,
                HitsMax = 300,
                Hue = 33784,
                Int = 200,
                ManaMaxSeed = 400,
                Name = "a Skittering Hopper",
                PerceptionRange = 10,
                Resistances = new Dictionary<ElementalType, CreatureProp>
                {
                    {ElementalType.PermPoisonImmunity, 100},
                    {ElementalType.Earth, 75},
                    {ElementalType.PermMagicImmunity, 4}
                },
                Skills = new Dictionary<SkillName, CreatureProp>
                {
                    {SkillName.Tactics, 110},
                    {SkillName.Macing, 110},
                    {SkillName.MagicResist, 110}
                },
                StamMaxSeed = 400,
                Str = 200,
                Tamable = false,
                TargetAcquireExhaustion = true
            });
        }


        [Constructible]
        public SkitteringHopper() : base(CreatureProperties.Get<SkitteringHopper>())
        {
            // Add customization here

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
        public SkitteringHopper(Serial serial) : base(serial)
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