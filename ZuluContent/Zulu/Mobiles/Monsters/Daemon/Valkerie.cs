

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
    public class Valkerie : BaseCreature
    {
        static Valkerie() => CreatureProperties.Register<Valkerie>(new CreatureProperties
        {
            // DataElementId = valkerie,
            // DataElementType = NpcTemplate,
            // dstart = 10,
            // Equip = valkerie,
            // Graphic = 0x0ec4 /* Weapon */,
            // HitSound = 0x195 /* Weapon */,
            // lootgroup = 29,
            // MissSound = 0x239 /* Weapon */,
            // script = killpcs,
            // Speed = 30 /* Weapon */,
            // TrueColor = 148,
            ActiveSpeed = 0.2,
            AiType = AIType.AI_Melee /* killpcs */,
            AlwaysAttackable = true,
            BaseSoundID = 402,
            Body = 0x1e,
            CorpseNameOverride = "corpse of a Valkerie",
            CreatureType = CreatureType.Daemon,
            DamageMax = 24,
            DamageMin = 4,
            Dex = 180,
            Female = false,
            FightMode = FightMode.None,
            FightRange = 1,
            HitsMax = 180,
            Hue = 148,
            Int = 80,
            ManaMaxSeed = 0,
            Name = "a Valkerie",
            PassiveSpeed = 0.4,
            PerceptionRange = 10,
            ProvokeSkillOverride = 100,
            Skills = new Dictionary<SkillName, CreatureProp>
            {
                { SkillName.Tactics, 90 },
                { SkillName.Macing, 95 },
                { SkillName.MagicResist, 60 },
            },
            StamMaxSeed = 70,
            Str = 180,
            VirtualArmor = 15,

        });


        [Constructible]
public Valkerie() : base(CreatureProperties.Get<Valkerie>())
        {
            // Add customization here

            AddItem(new SkinningKnife
            {
                Movable = false,
                Name = "Valkerie Weapon",
                Speed = 30,
                MaxHitPoints = 250,
                HitPoints = 250,
                HitSound = 0x195,
                MissSound = 0x239,
            });


        }

        [Constructible]
public Valkerie(Serial serial) : base(serial) {}



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
