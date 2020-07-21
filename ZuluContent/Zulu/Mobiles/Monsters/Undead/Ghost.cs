

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
    public class Ghost : BaseCreature
    {
        static Ghost() => CreatureProperties.Register<Ghost>(new CreatureProperties
        {
            // CProp_NecroProtection = i3,
            // DataElementId = ghost,
            // DataElementType = NpcTemplate,
            // dstart = 10,
            // Equip = ghost,
            // Graphic = 0x0ec4 /* Weapon */,
            // hostile = 1,
            // lootgroup = 47,
            // Macefighting_0 = 50,
            // MagicItemChance = 1,
            // script = killpcs,
            // Speed = 50 /* Weapon */,
            // Swordsmanship = 50,
            // TrueColor = 0,
            ActiveSpeed = 0.2,
            AiType = AIType.AI_Melee /* killpcs */,
            AlwaysMurderer = true,
            BaseSoundID = 382,
            Body = 0x3ca,
            CorpseNameOverride = "corpse of a ghost",
            CreatureType = CreatureType.Undead,
            DamageMax = 30,
            DamageMin = 3,
            Dex = 60,
            Female = false,
            FightMode = FightMode.Aggressor,
            FightRange = 1,
            HitsMax = 126,
            Hue = 0,
            Int = 126,
            ManaMaxSeed = 26,
            Name = "a ghost",
            PassiveSpeed = 0.4,
            PerceptionRange = 10,
            Skills = new Dictionary<SkillName, CreatureProp>
            {
                { SkillName.Fencing, 50 },
                { SkillName.Parry, 50 },
                { SkillName.Macing, 50 },
                { SkillName.Tactics, 50 },
                { SkillName.MagicResist, 50 },
            },
            StamMaxSeed = 50,
            Str = 126,
            VirtualArmor = 20,

        });


        [Constructible]
public Ghost() : base(CreatureProperties.Get<Ghost>())
        {
            // Add customization here

            AddItem(new SkinningKnife
            {
                Movable = false,
                Name = "Ghost Weapon",
                Speed = 50,
                MaxHitPoints = 250,
                HitPoints = 250,
            });


        }

        [Constructible]
public Ghost(Serial serial) : base(serial) {}



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
