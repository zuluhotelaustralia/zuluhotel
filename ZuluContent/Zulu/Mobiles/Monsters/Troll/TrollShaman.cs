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
    public class TrollShaman : BaseCreature
    {
        static TrollShaman()
        {
            CreatureProperties.Register<TrollShaman>(new CreatureProperties
            {
                // cast_pct = 50,
                // DataElementId = trollshaman,
                // DataElementType = NpcTemplate,
                // dstart = 10,
                // Equip = trollshaman,
                // Graphic = 0x0ec4 /* Weapon */,
                // HitSound = 0x1B3 /* Weapon */,
                // hostile = 1,
                LootTable = "54",
                LootItemChance = 60,
                LootItemLevel = 2,
                // MissSound = 0x239 /* Weapon */,
                // num_casts = 3,
                // script = critterhealer,
                // Speed = 35 /* Weapon */,
                // spell = summonearth,
                // spell_0 = explosion,
                // TrueColor = 0x0220,
                ActiveSpeed = 0.2,
                AiType = AIType.AI_Mage /* critterhealer */,
                AlwaysMurderer = true,
                Body = 0x36,
                CorpseNameOverride = "corpse of a Troll Shaman",
                CreatureType = CreatureType.Troll,
                DamageMax = 64,
                DamageMin = 8,
                Dex = 170,
                Female = false,
                FightMode = FightMode.Closest,
                FightRange = 1,
                Hides = 4,
                HideType = HideType.Troll,
                HitsMax = 225,
                Hue = 0x0220,
                Int = 285,
                ManaMaxSeed = 85,
                Name = "a Troll Shaman",
                PassiveSpeed = 0.4,
                PerceptionRange = 10,
                PreferredSpells = new List<Type>
                {
                    typeof(Spells.Sixth.ExplosionSpell)
                },
                ProvokeSkillOverride = 100,
                SaySpellMantra = true,
                Skills = new Dictionary<SkillName, CreatureProp>
                {
                    {SkillName.MagicResist, 60},
                    {SkillName.Tactics, 60},
                    {SkillName.Macing, 90},
                    {SkillName.Magery, 90}
                },
                StamMaxSeed = 50,
                Str = 225,
                VirtualArmor = 20
            });
        }


        [Constructible]
        public TrollShaman() : base(CreatureProperties.Get<TrollShaman>())
        {
            // Add customization here

            AddItem(new SkinningKnife
            {
                Movable = false,
                Name = "Troll Shaman Weapon",
                Speed = 35,
                MaxHitPoints = 250,
                HitPoints = 250,
                HitSound = 0x1B3,
                MissSound = 0x239
            });
        }

        [Constructible]
        public TrollShaman(Serial serial) : base(serial)
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