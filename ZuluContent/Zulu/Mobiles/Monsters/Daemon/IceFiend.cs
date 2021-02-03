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
    public class IceFiend : BaseCreature
    {
        static IceFiend()
        {
            CreatureProperties.Register<IceFiend>(new CreatureProperties
            {
                // CProp_PermMagicImmunity = i5,
                // DataElementId = icefiend,
                // DataElementType = NpcTemplate,
                // dstart = 10,
                // Equip = icefiend,
                // Graphic = 0x0ec4 /* Weapon */,
                // HitSound = 0x168 /* Weapon */,
                // hostile = 1,
                LootTable = "22",
                LootItemChance = 50,
                LootItemLevel = 4,
                // MissSound = 0x239 /* Weapon */,
                // script = spellkillpcs,
                // Speed = 45 /* Weapon */,
                // spell = poison,
                // spell_0 = lightning,
                // spell_1 = manadrain,
                // spell_2 = flamestrike,
                // spell_3 = ebolt,
                // TrueColor = 1170,
                ActiveSpeed = 0.2,
                AiType = AIType.AI_Mage /* spellkillpcs */,
                AlwaysMurderer = true,
                Body = 43,
                CorpseNameOverride = "corpse of <random> the Ice Fiend",
                CreatureType = CreatureType.Daemon,
                DamageMax = 40,
                DamageMin = 10,
                Dex = 300,
                Female = false,
                FightMode = FightMode.Closest,
                FightRange = 1,
                Hides = 5,
                HideType = HideType.IceCrystal,
                HitsMax = 500,
                Hue = 1170,
                Int = 500,
                ManaMaxSeed = 500,
                Name = "<random> the Ice Fiend",
                PassiveSpeed = 0.4,
                PerceptionRange = 10,
                PreferredSpells = new List<Type>
                {
                    typeof(Spells.Third.PoisonSpell),
                    typeof(Spells.Fourth.LightningSpell),
                    typeof(Spells.Fourth.ManaDrainSpell),
                    typeof(Spells.Sixth.EnergyBoltSpell)
                },
                ProvokeSkillOverride = 130,
                SaySpellMantra = true,
                Skills = new Dictionary<SkillName, CreatureProp>
                {
                    {SkillName.MagicResist, 90},
                    {SkillName.Tactics, 125},
                    {SkillName.Macing, 175},
                    {SkillName.Magery, 110},
                    {SkillName.EvalInt, 110}
                },
                StamMaxSeed = 95,
                Str = 500,
                VirtualArmor = 35
            });
        }


        [Constructible]
        public IceFiend() : base(CreatureProperties.Get<IceFiend>())
        {
            // Add customization here

            AddItem(new SkinningKnife
            {
                Movable = false,
                Name = "Daemon Weapon",
                Speed = 45,
                MaxHitPoints = 250,
                HitPoints = 250,
                HitSound = 0x168,
                MissSound = 0x239
            });
        }

        [Constructible]
        public IceFiend(Serial serial) : base(serial)
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