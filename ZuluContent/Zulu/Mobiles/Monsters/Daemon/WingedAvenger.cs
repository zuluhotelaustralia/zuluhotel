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
    public class WingedAvenger : BaseCreature
    {
        static WingedAvenger()
        {
            CreatureProperties.Register<WingedAvenger>(new CreatureProperties
            {
                // cast_pct = 40,
                // DataElementId = WingedAvenger,
                // DataElementType = NpcTemplate,
                // dstart = 10,
                // Equip = WingedAvenger,
                // Graphic = 0x0ec4 /* Weapon */,
                // HitSound = 0x195 /* Weapon */,
                // hostile = 1,
                LootTable = "46",
                LootItemChance = 50,
                LootItemLevel = 2,
                // MissSound = 0x239 /* Weapon */,
                // num_casts = 8,
                // script = spellkillpcs,
                // Speed = 35 /* Weapon */,
                // spell = flamestrike,
                // spell_0 = ebolt,
                // spell_1 = lightning,
                // spell_2 = harm,
                // spell_3 = mindblast,
                // spell_4 = magicarrow,
                // spell_5 = fireball,
                // spell_6 = chainlightning,
                // spell_7 = weaken,
                // spell_8 = masscurse,
                // spell_9 = magicreflect,
                // TrueColor = 0x0455,
                ActiveSpeed = 0.2,
                AiType = AIType.AI_Mage /* spellkillpcs */,
                AlwaysMurderer = true,
                Body = 0x01e,
                CorpseNameOverride = "corpse of a Winged Avenger",
                CreatureType = CreatureType.Daemon,
                DamageMax = 43,
                DamageMin = 8,
                Dex = 350,
                Female = false,
                FightMode = FightMode.Closest,
                FightRange = 1,
                HitsMax = 305,
                Hue = 0x0455,
                Int = 295,
                ManaMaxSeed = 200,
                Name = "a Winged Avenger",
                PassiveSpeed = 0.4,
                PerceptionRange = 10,
                PreferredSpells = new List<Type>
                {
                    typeof(Spells.Sixth.EnergyBoltSpell),
                    typeof(Spells.Fourth.LightningSpell),
                    typeof(Spells.Second.HarmSpell),
                    typeof(Spells.Fifth.MindBlastSpell),
                    typeof(Spells.First.MagicArrowSpell),
                    typeof(Spells.Third.FireballSpell),
                    typeof(Spells.First.WeakenSpell),
                    typeof(Spells.Sixth.MassCurseSpell)
                },
                ProvokeSkillOverride = 75,
                Skills = new Dictionary<SkillName, CreatureProp>
                {
                    {SkillName.Tactics, 140},
                    {SkillName.Macing, 200},
                    {SkillName.MagicResist, 90},
                    {SkillName.Magery, 85}
                },
                StamMaxSeed = 50,
                Str = 305,
                VirtualArmor = 30
            });
        }


        [Constructible]
        public WingedAvenger() : base(CreatureProperties.Get<WingedAvenger>())
        {
            // Add customization here

            AddItem(new SkinningKnife
            {
                Movable = false,
                Name = "Winged Avenger Weapon",
                Speed = 35,
                MaxHitPoints = 250,
                HitPoints = 250,
                HitSound = 0x195,
                MissSound = 0x239
            });
        }

        [Constructible]
        public WingedAvenger(Serial serial) : base(serial)
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