

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
    public class Ent : BaseCreature
    {
        static Ent() => CreatureProperties.Register<Ent>(new CreatureProperties
        {
            // CProp_EarthProtection = i5,
            // CProp_PermMagicImmunity = i6,
            // DataElementId = ent,
            // DataElementType = NpcTemplate,
            // dstart = 10,
            // Equip = ent,
            // Graphic = 0x0ec4 /* Weapon */,
            // HitSound = 0x1BD /* Weapon */,
            // hostile = 1,
            // lootgroup = 34,
            // MagicItemChance = 1,
            // MissSound = 0x1BC /* Weapon */,
            // script = killpcs,
            // Speed = 25 /* Weapon */,
            // TrueColor = 0,
            ActiveSpeed = 0.2,
            AiType = AIType.AI_Melee /* killpcs */,
            AlwaysMurderer = true,
            Body = 0x2f,
            CorpseNameOverride = "corpse of an ent",
            CreatureType = CreatureType.Plant,
            DamageMax = 44,
            DamageMin = 24,
            Dex = 160,
            Female = false,
            FightMode = FightMode.Aggressor,
            FightRange = 1,
            HitsMax = 300,
            Hue = 0,
            Int = 75,
            ManaMaxSeed = 0,
            Name = "an ent",
            PassiveSpeed = 0.4,
            PerceptionRange = 10,
            Skills = new Dictionary<SkillName, CreatureProp>
            {
                { SkillName.Tactics, 150 },
                { SkillName.Macing, 100 },
                { SkillName.MagicResist, 200 },
            },
            StamMaxSeed = 50,
            Str = 300,
            VirtualArmor = 20,

        });


        [Constructible]
public Ent() : base(CreatureProperties.Get<Ent>())
        {
            // Add customization here

            AddItem(new SkinningKnife
            {
                Movable = false,
                Name = "Ent Weapon",
                Speed = 25,
                MaxHitPoints = 250,
                HitPoints = 250,
                HitSound = 0x1BD,
                MissSound = 0x1BC,
            });


        }

        [Constructible]
public Ent(Serial serial) : base(serial) {}



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
