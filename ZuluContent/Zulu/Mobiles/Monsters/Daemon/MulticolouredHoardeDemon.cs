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
    public class MulticolouredHoardeDemon : BaseCreature
    {
        static MulticolouredHoardeDemon()
        {
            CreatureProperties.Register<MulticolouredHoardeDemon>(new CreatureProperties
            {
                // CProp_AtDeathScript = s:death:hoardedemon,
                // CProp_BaseHpRegen = i1000,
                // CProp_EarthProtection = i8,
                // CProp_NecroProtection = i8,
                // CProp_PermMagicImmunity = i8,
                // DataElementId = multicolouredhoardedemon,
                // DataElementType = NpcTemplate,
                // Equip = behemoth,
                // Graphic = 0x0ec4 /* Weapon */,
                // Hitscript = :combat:banishscript /* Weapon */,
                // HitSound = 0x16D /* Weapon */,
                // hostile = 1,
                // lootgroup = 9,
                // MagicItemChance = 70,
                // MagicItemLevel = 6,
                // MissSound = 0x239 /* Weapon */,
                // script = killpcsTeleporter,
                // Speed = 50 /* Weapon */,
                // TrueColor = 0,
                AiType = AIType.AI_Melee /* killpcsTeleporter */,
                AlwaysMurderer = true,
                AutoDispel = true,
                Body = 999,
                CorpseNameOverride = "corpse of a Multicoloured Hoarde Demon",
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
                Name = "a Multicoloured Hoarde Demon",
                PerceptionRange = 10,
                Resistances = new Dictionary<ElementalType, CreatureProp>
                {
                    {ElementalType.Poison, 100},
                    {ElementalType.Fire, 100},
                    {ElementalType.Air, 100},
                    {ElementalType.Water, 100}
                },
                Skills = new Dictionary<SkillName, CreatureProp>
                {
                    {SkillName.Tactics, 150},
                    {SkillName.Macing, 175},
                    {SkillName.MagicResist, 60},
                    {SkillName.DetectHidden, 200}
                },
                StamMaxSeed = 200,
                Str = 2000,
                TargetAcquireExhaustion = true,
                VirtualArmor = 45
            });
        }


        [Constructible]
        public MulticolouredHoardeDemon() : base(CreatureProperties.Get<MulticolouredHoardeDemon>())
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
                MissSound = 0x239
            });
        }

        [Constructible]
        public MulticolouredHoardeDemon(Serial serial) : base(serial)
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