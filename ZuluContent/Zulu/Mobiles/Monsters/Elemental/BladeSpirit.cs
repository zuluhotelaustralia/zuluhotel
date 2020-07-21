

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
    public class BladeSpirit : BaseCreature
    {
        static BladeSpirit() => CreatureProperties.Register<BladeSpirit>(new CreatureProperties
        {
            // CProp_guardkill = 1,
            // DataElementId = bladespirit,
            // DataElementType = NpcTemplate,
            // dstart = 10,
            // Equip = bladespirit,
            // Graphic = 0x0ec4 /* Weapon */,
            // HitSound = 0x23D /* Weapon */,
            // hostile = 1,
            // MissSound = 0x239 /* Weapon */,
            // script = vortexai,
            // Speed = 20 /* Weapon */,
            // TrueColor = 33784,
            ActiveSpeed = 0.2,
            AiType = AIType.AI_Melee /* vortexai */,
            AlwaysMurderer = true,
            Body = 0x23e,
            CorpseNameOverride = "corpse of a blade spirit",
            CreatureType = CreatureType.Elemental,
            DamageMax = 8,
            DamageMin = 2,
            Dex = 200,
            Female = false,
            FightMode = FightMode.Aggressor,
            FightRange = 1,
            HitsMax = 200,
            Hue = 33784,
            Int = 25,
            ManaMaxSeed = 0,
            Name = "a blade spirit",
            PassiveSpeed = 0.4,
            PerceptionRange = 10,
            Skills = new Dictionary<SkillName, CreatureProp>
            {
                { SkillName.Poisoning, 100 },
                { SkillName.Tactics, 50 },
                { SkillName.Macing, 125 },
                { SkillName.MagicResist, 110 },
            },
            StamMaxSeed = 50,
            Str = 200,
            VirtualArmor = 15,

        });


        [Constructible]
public BladeSpirit() : base(CreatureProperties.Get<BladeSpirit>())
        {
            // Add customization here

            AddItem(new SkinningKnife
            {
                Movable = false,
                Name = "Blade Spirit Weapon",
                Speed = 20,
                MaxHitPoints = 250,
                HitPoints = 250,
                HitSound = 0x23D,
                MissSound = 0x239,
            });


        }

        [Constructible]
public BladeSpirit(Serial serial) : base(serial) {}



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
