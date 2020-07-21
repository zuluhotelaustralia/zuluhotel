

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
    public class LargeHoardeDemon : BaseCreature
    {
        static LargeHoardeDemon() => CreatureProperties.Register<LargeHoardeDemon>(new CreatureProperties
        {
            // CProp_BaseHpRegen = i1000,
            // CProp_EarthProtection = i6,
            // CProp_NecroProtection = i6,
            // CProp_PermMagicImmunity = i6,
            // DataElementId = largehoardedemon,
            // DataElementType = NpcTemplate,
            // Equip = behemoth,
            // Graphic = 0x0ec4 /* Weapon */,
            // Hitscript = :combat:banishscript /* Weapon */,
            // HitSound = 0x16D /* Weapon */,
            // hostile = 1,
            // lootgroup = 9,
            // MagicItemChance = 10,
            // MagicItemLevel = 4,
            // MissSound = 0x239 /* Weapon */,
            // script = killpcsTeleporter,
            // Speed = 50 /* Weapon */,
            // TrueColor = 0,
            AiType = AIType.AI_Melee /* killpcsTeleporter */,
            AlwaysMurderer = true,
            AutoDispel = true,
            Body = 795,
            CorpseNameOverride = "corpse of a Large Hoarde Demon",
            CreatureType = CreatureType.Daemon,
            DamageMax = 60,
            DamageMin = 10,
            Dex = 200,
            Female = false,
            FightMode = FightMode.Aggressor,
            FightRange = 1,
            HitsMax = 2250,
            Hue = 0,
            Int = 200,
            ManaMaxSeed = 200,
            Name = "a Large Hoarde Demon",
            PerceptionRange = 10,
            Resistances = new Dictionary<ElementalType, CreatureProp>
            {
                { ElementalType.Poison, 100 },
                { ElementalType.Fire, 100 },
                { ElementalType.Energy, 100 },
                { ElementalType.Cold, 100 },
            },
            Skills = new Dictionary<SkillName, CreatureProp>
            {
                { SkillName.Tactics, 150 },
                { SkillName.Macing, 175 },
                { SkillName.MagicResist, 60 },
                { SkillName.DetectHidden, 200 },
            },
            StamMaxSeed = 200,
            Str = 2000,
            TargetAcquireExhaustion = true,
            VirtualArmor = 45,

        });


        [Constructible]
public LargeHoardeDemon() : base(CreatureProperties.Get<LargeHoardeDemon>())
        {
            // Add customization here

            AddItem(new SkinningKnife
            {
                Movable = false,
                Name = "Behemoth Weapon",
                Speed = 50,
                MaxHitPoints = 250,
                HitPoints = 250,
                HitSound = 0x16D,
                MissSound = 0x239,
            });


        }

        [Constructible]
public LargeHoardeDemon(Serial serial) : base(serial) {}



        public override void Serialize(IGenericWriter writer)
        {
            base.Serialize(writer);
            writer.Write((int) 0);
        }

        public override void Deserialize(IGenericReader reader)
        {
            base.Deserialize(reader);
            int version = reader.ReadInt();
        }
    }
}
