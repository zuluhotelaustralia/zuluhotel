using System;
using System.Collections.Generic;
using Server;
using Server.Misc;
using Server.Items;
using static Server.Mobiles.CreatureProp;
using Server.Engines.Magic;
using Server.Engines.Harvest;
using Scripts.Zulu.Engines.Classes;

namespace Server.Mobiles
{
    public class TwistedAnimation : BaseCreature
    {
        static TwistedAnimation()
        {
            CreatureProperties.Register<TwistedAnimation>(new CreatureProperties
            {
                // DataElementId = twistedanimation,
                // DataElementType = NpcTemplate,
                // dstart = 10,
                // Equip = twistedanimation,
                // Graphic = 0xF60 /* Weapon */,
                // Hitscript = :combat:banishscript /* Weapon */,
                // HitSound = 0x23C /* Weapon */,
                // hostile = 1,
                // lootgroup = 132,
                // MagicItemChance = 75,
                // Magicitemlevel = 5,
                // MissSound = 0x23A /* Weapon */,
                // Parrying = 150,
                // script = killpcs,
                // Speed = 50 /* Weapon */,
                // Swordsmanship = 140,
                // TrueColor = 49408,
                ActiveSpeed = 0.2,
                AiType = AIType.AI_Melee /* killpcs */,
                AlwaysMurderer = true,
                AutoDispel = true,
                Body = 0x245,
                ClassLevel = 4,
                ClassType = ZuluClassType.Warrior,
                CorpseNameOverride = "corpse of a Twisted Animation",
                CreatureType = CreatureType.Elemental,
                DamageMax = 73,
                DamageMin = 33,
                Dex = 100,
                Female = false,
                FightMode = FightMode.Aggressor,
                FightRange = 1,
                HitsMax = 700,
                Hue = 49408,
                Int = 200,
                ManaMaxSeed = 100,
                Name = "a Twisted Animation",
                PassiveSpeed = 0.4,
                PerceptionRange = 10,
                Skills = new Dictionary<SkillName, CreatureProp>
                {
                    {SkillName.Tactics, 120},
                    {SkillName.MagicResist, 150}
                },
                StamMaxSeed = 50,
                Str = 700,
                VirtualArmor = 50
            });
        }


        [Constructible]
        public TwistedAnimation() : base(CreatureProperties.Get<TwistedAnimation>())
        {
            // Add customization here

            AddItem(new Longsword
            {
                Movable = false,
                Name = "an animation's blade",
                Hue = 33870,
                Speed = 50,
                Skill = SkillName.Swords,
                MaxHitPoints = 250,
                HitPoints = 250,
                HitSound = 0x23C,
                MissSound = 0x23A,
                Animation = (WeaponAnimation) 0x0009
            });

            AddItem(new GoldBracelet
            {
                Movable = false,
                Name = "a animation's shield"
            });
        }

        [Constructible]
        public TwistedAnimation(Serial serial) : base(serial)
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