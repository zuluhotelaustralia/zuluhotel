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
    public class BalronSpawn : BaseCreature
    {
        static BalronSpawn()
        {
            CreatureProperties.Register<BalronSpawn>(new CreatureProperties
            {
                // CProp_BaseHpRegen = i1000,
                // CProp_EarthProtection = i8,
                // CProp_NoReactiveArmour = i1,
                // CProp_PermMagicImmunity = i4,
                // DataElementId = balronspawn,
                // DataElementType = NpcTemplate,
                // dstart = 10,
                // Equip = BalronSpawn,
                // Graphic = 0x0ec4 /* Weapon */,
                // Hitscript = :combat:lifedrainscript /* Weapon */,
                // HitSound = 0x25A /* Weapon */,
                // hostile = 1,
                LootTable = "69",
                LootItemChance = 80,
                LootItemLevel = 4,
                // MissSound = 0x239 /* Weapon */,
                // script = killpcsTeleporter,
                // Speed = 35 /* Weapon */,
                // TrueColor = 17969,
                AiType = AIType.AI_Melee /* killpcsTeleporter */,
                AlwaysMurderer = true,
                Body = 74,
                CorpseNameOverride = "corpse of a Balron Spawn",
                CreatureType = CreatureType.Daemon,
                DamageMax = 73,
                DamageMin = 33,
                Dex = 400,
                Female = false,
                FightMode = FightMode.Aggressor,
                FightRange = 1,
                HitsMax = 600,
                Hue = 17969,
                Int = 400,
                ManaMaxSeed = 400,
                Name = "a Balron Spawn",
                PerceptionRange = 10,
                Resistances = new Dictionary<ElementalType, CreatureProp>
                {
                    {ElementalType.PermPoisonImmunity, 100}
                },
                Skills = new Dictionary<SkillName, CreatureProp>
                {
                    {SkillName.Tactics, 130},
                    {SkillName.Macing, 130},
                    {SkillName.MagicResist, 130}
                },
                StamMaxSeed = 400,
                Str = 600,
                Tamable = false,
                TargetAcquireExhaustion = true
            });
        }


        [Constructible]
        public BalronSpawn() : base(CreatureProperties.Get<BalronSpawn>())
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

            // if (Weapon is BaseWeapon baseWeapon)
            //     baseWeapon.WeaponAttributes.HitLeechHits = 50;
        }

        [Constructible]
        public BalronSpawn(Serial serial) : base(serial)
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