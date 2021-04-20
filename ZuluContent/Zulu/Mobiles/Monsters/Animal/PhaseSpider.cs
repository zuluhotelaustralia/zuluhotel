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
    public class PhaseSpider : BaseCreature
    {
        static PhaseSpider()
        {
            CreatureProperties.Register<PhaseSpider>(new CreatureProperties
            {
                // DataElementId = phasespider,
                // DataElementType = NpcTemplate,
                // dstart = 10,
                // Equip = phasespider,
                // food = meat,
                // Graphic = 0x0ec4 /* Weapon */,
                // Hitscript = :combat:poisonhit /* Weapon */,
                // HitSound = 0x186 /* Weapon */,
                // hostile = 1,
                // MissSound = 0x239 /* Weapon */,
                // script = killpcsTeleporter,
                // Speed = 30 /* Weapon */,
                // spell = flamestrike,
                // spell_0 = ebolt,
                // spell_1 = lightning,
                // spell_2 = harm,
                // spell_3 = magicarrow,
                // TrueColor = 25125,
                AiType = AIType.AI_Melee /* killpcsTeleporter */,
                AlwaysMurderer = true,
                Body = 0x1c,
                CorpseNameOverride = "corpse of a phase spider",
                CreatureType = CreatureType.Animal,
                DamageMax = 32,
                DamageMin = 4,
                Dex = 60,
                Female = false,
                FightMode = FightMode.Aggressor,
                FightRange = 1,
                HasWebs = true,
                HitPoison = Poison.Regular,
                HitsMax = 250,
                Hue = 25125,
                Int = 350,
                ManaMaxSeed = 0,
                MinTameSkill = 150,
                Name = "a phase spider",
                PerceptionRange = 10,
                PreferredSpells = new List<Type>
                {
                    typeof(Spells.Sixth.EnergyBoltSpell),
                    typeof(Spells.Fourth.LightningSpell),
                    typeof(Spells.Second.HarmSpell),
                    typeof(Spells.First.MagicArrowSpell)
                },
                ProvokeSkillOverride = 90,
                Resistances = new Dictionary<ElementalType, CreatureProp>
                {
                    {ElementalType.Poison, 2}
                },
                Skills = new Dictionary<SkillName, CreatureProp>
                {
                    {SkillName.MagicResist, 90},
                    {SkillName.Tactics, 70},
                    {SkillName.Macing, 125},
                    {SkillName.Magery, 90}
                },
                StamMaxSeed = 50,
                Str = 700,
                Tamable = true,
                TargetAcquireExhaustion = true,
                VirtualArmor = 25
            });
        }


        [Constructible]
        public PhaseSpider() : base(CreatureProperties.Get<PhaseSpider>())
        {
            // Add customization here

            AddItem(new SkinningKnife
            {
                Movable = false,
                Name = "Phase Spider Weapon",
                Speed = 30,
                MaxHitPoints = 250,
                HitPoints = 250,
                HitSound = 0x186,
                MissSound = 0x239
            });
        }

        [Constructible]
        public PhaseSpider(Serial serial) : base(serial)
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