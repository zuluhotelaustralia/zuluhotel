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
    public class Humuc : BaseCreature
    {
        static Humuc()
        {
            CreatureProperties.Register<Humuc>(new CreatureProperties
            {
                ActiveSpeed = 0.2,
                AiType = AIType.AI_Familiar,
                InitialInnocent = true,
                AlwaysMurderer = false,
                BaseSoundID = 422,
                CanFly = true,
                SaySpellMantra = true,
                Body = 0x27,
                CorpseNameOverride = "corpse of a totem",
                CreatureType = CreatureType.Daemon,
                DamageMax = 2,
                DamageMin = 13,
                Dex = 100,
                Female = false,
                FightMode = FightMode.Aggressor,
                FightRange = 1,
                HitsMax = 20,
                Hue = 746,
                Int = 75,
                ManaMaxSeed = 75,
                MinTameSkill = 0,
                Name = "totem",
                PassiveSpeed = 0.5,
                PerceptionRange = 10,
                ProvokeSkillOverride = 150,
                StamMaxSeed = 50,
                Str = 200,
                Tamable = false,
                VirtualArmor = 100,
                Skills = new Dictionary<SkillName, CreatureProp>
                {
                    {SkillName.Macing, 150},
                    {SkillName.Tactics, 50},
                    {SkillName.MagicResist, 160},
                    {SkillName.Magery, 75}
                },
                Resistances = new Dictionary<ElementalType, CreatureProp>
                {
                    {ElementalType.PermMagicImmunity, 100},
                },
            });
        }


        [Constructible]
        public Humuc() : base(CreatureProperties.Get<Humuc>())
        {
            // Add customization here

            AddItem(new SkinningKnife
            {
                Movable = false,
                Name = "Humuc Weapon",
                Speed = 100,
                Skill = SkillName.Macing,
                MaxHitPoints = 250,
                HitPoints = 250,
                HitSound = 0x1A9,
                MissSound = 0x239
            });

            AddItem(new HeaterShield
            {
                Movable = false,
                Name = "Shield AR50",
                BaseArmorRating = 50,
                MaxHitPoints = 400,
                HitPoints = 400
            });

            Backpack?.Delete();
            var pack = new StrongBackpack {Movable = false};
            AddItem(pack);

            RemoveIfUntamed = true;
        }

        public override bool CanDrop { get; } = true;
        public override bool PlayerRangeSensitive => ControlMaster == null || ControlMaster.Map == Map.Internal;

        [Constructible]
        public Humuc(Serial serial) : base(serial)
        {
        }

        public override bool IsFriend(Mobile m)
        {
            return false;
        }

        public override bool CanBeControlledBy(Mobile m)
        {
            return m == ControlMaster;
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