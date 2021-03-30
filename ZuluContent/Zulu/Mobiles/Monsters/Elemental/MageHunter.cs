using System;
using System.Collections.Generic;
using Server;
using Server.Misc;
using Server.Items;
using static Server.Mobiles.CreatureProp;
using Server.Engines.Magic;
using Server.Engines.Harvest;
using Server.Engines.Magic.HitScripts;

namespace Server.Mobiles
{
    public class MageHunter : BaseCreature
    {
        static MageHunter()
        {
            CreatureProperties.Register<MageHunter>(new CreatureProperties
            {
                // CProp_AttackTypeImmunities = i639,
                // DataElementId = hiddenmagehunter,
                // DataElementType = NpcTemplate,
                // dstart = 10,
                // Equip = magehunter,
                // Graphic = 0x0ec4 /* Weapon */,
                // hiding = 130,
                // Hitscript = :combat:blackrockscript /* Weapon */,
                // HitSound = 0x23D /* Weapon */,
                // hostile = 1,
                // MissSound = 0x239 /* Weapon */,
                // script = killpcs,
                // Speed = 32 /* Weapon */,
                // TrueColor = 1170,
                ActiveSpeed = 0.2,
                AiType = AIType.AI_Melee /* killpcs */,
                AlwaysMurderer = true,
                Body = 0x23e,
                CorpseNameOverride = "corpse of a Mage Hunter",
                CreatureType = CreatureType.Elemental,
                DamageMax = 10,
                DamageMin = 1,
                Dex = 150,
                Female = false,
                FightMode = FightMode.Aggressor,
                FightRange = 1,
                HitsMax = 300,
                Hue = 1170,
                Int = 25,
                ManaMaxSeed = 0,
                Name = "a Mage Hunter",
                PassiveSpeed = 0.4,
                PerceptionRange = 10,
                Resistances = new Dictionary<ElementalType, CreatureProp>
                {
                    {ElementalType.PermPoisonImmunity, 100},
                    {ElementalType.PermMagicImmunity, 8}
                },
                Skills = new Dictionary<SkillName, CreatureProp>
                {
                    {SkillName.Poisoning, 100},
                    {SkillName.Tactics, 130},
                    {SkillName.Macing, 150},
                    {SkillName.MagicResist, 160},
                    {SkillName.DetectHidden, 130}
                },
                StamMaxSeed = 50,
                Str = 300,
                VirtualArmor = 30,
                WeaponAbility = new BlackrockStrike(),
                WeaponAbilityChance = 1.0
            });
        }


        [Constructible]
        public MageHunter() : base(CreatureProperties.Get<MageHunter>())
        {
            // Add customization here

            AddItem(new SkinningKnife
            {
                Movable = false,
                Name = "Mage Hunter Weapon",
                Speed = 32,
                MaxHitPoints = 250,
                HitPoints = 250,
                HitSound = 0x23D,
                MissSound = 0x239
            });
        }

        [Constructible]
        public MageHunter(Serial serial) : base(serial)
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