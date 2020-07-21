

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
    public class Ettin : BaseCreature
    {
        static Ettin() => CreatureProperties.Register<Ettin>(new CreatureProperties
        {
            // DataElementId = ettin2,
            // DataElementType = NpcTemplate,
            // dstart = 10,
            // Equip = ettin2,
            // Graphic = 0x0ec4 /* Weapon */,
            // HitSound = 0x170 /* Weapon */,
            // hostile = 1,
            // lootgroup = 123,
            // MagicItemChance = 2,
            // Magicitemlevel = 1,
            // MissSound = 0x239 /* Weapon */,
            // script = killpcs,
            // Speed = 20 /* Weapon */,
            // TrueColor = 33784,
            ActiveSpeed = 0.2,
            AiType = AIType.AI_Melee /* killpcs */,
            AlwaysMurderer = true,
            BaseSoundID = 367,
            Body = 0x12,
            CorpseNameOverride = "corpse of an ettin",
            CreatureType = CreatureType.Giantkin,
            DamageMax = 40,
            DamageMin = 8,
            Dex = 130,
            Female = false,
            FightMode = FightMode.Aggressor,
            FightRange = 1,
            HitsMax = 140,
            Hue = 33784,
            Int = 10,
            ManaMaxSeed = 10,
            Name = "an ettin",
            PassiveSpeed = 0.4,
            PerceptionRange = 10,
            ProvokeSkillOverride = 80,
            Skills = new Dictionary<SkillName, CreatureProp>
            {
                { SkillName.Parry, 60 },
                { SkillName.Tactics, 70 },
                { SkillName.Macing, 80 },
                { SkillName.MagicResist, 40 },
            },
            StamMaxSeed = 120,
            Str = 140,
            VirtualArmor = 15,

        });


        [Constructible]
public Ettin() : base(CreatureProperties.Get<Ettin>())
        {
            // Add customization here

            AddItem(new SkinningKnife
            {
                Movable = false,
                Name = "Ettin2 Weapon",
                Speed = 20,
                MaxHitPoints = 250,
                HitPoints = 250,
                HitSound = 0x170,
                MissSound = 0x239,
            });


        }

        [Constructible]
public Ettin(Serial serial) : base(serial) {}



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
