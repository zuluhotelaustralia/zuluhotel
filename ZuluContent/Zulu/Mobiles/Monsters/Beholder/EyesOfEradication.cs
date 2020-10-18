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
    public class EyesOfEradication : BaseCreature
    {
        static EyesOfEradication()
        {
            CreatureProperties.Register<EyesOfEradication>(new CreatureProperties
            {
                // cast_pct = 50,
                // DataElementId = eyesoferadication,
                // DataElementType = NpcTemplate,
                // Detect = Hidden 200,
                // dstart = 10,
                // Equip = beholder,
                // Graphic = 0x0ec4 /* Weapon */,
                // HitSound = 0x17C /* Weapon */,
                // hostile = 1,
                // lootgroup = 57,
                // MagicItemChance = 75,
                // MagicItemLevel = 4,
                // MissSound = 0x239 /* Weapon */,
                // num_casts = 40,
                // script = spellkillpcs,
                // Speed = 33 /* Weapon */,
                // spell = paralyze,
                // spell_0 = fireball,
                // spell_1 = lightning,
                // spell_2 = chainlightning,
                // spell_3 = MassCurse,
                // spell_4 = flamestrike,
                // spell_5 = Magicreflect,
                // TrueColor = 0x494,
                ActiveSpeed = 0.2,
                AiType = AIType.AI_Mage /* spellkillpcs */,
                AlwaysMurderer = true,
                Body = 0x16,
                CorpseNameOverride = "corpse of Eyes of Eradication",
                CreatureType = CreatureType.Beholder,
                DamageMax = 34,
                DamageMin = 14,
                Dex = 500,
                Female = false,
                FightMode = FightMode.Closest,
                FightRange = 1,
                HitsMax = 500,
                Hue = 0x494,
                Int = 600,
                ManaMaxSeed = 500,
                Name = "Eyes of Eradication",
                PassiveSpeed = 0.4,
                PerceptionRange = 10,
                PreferredSpells = new List<Type>
                {
                    typeof(Spells.Fifth.ParalyzeSpell),
                    typeof(Spells.Third.FireballSpell),
                    typeof(Spells.Fourth.LightningSpell),
                    typeof(Spells.Sixth.MassCurseSpell)
                },
                ProvokeSkillOverride = 160,
                SaySpellMantra = true,
                Skills = new Dictionary<SkillName, CreatureProp>
                {
                    {SkillName.MagicResist, 200},
                    {SkillName.Tactics, 200},
                    {SkillName.Macing, 150},
                    {SkillName.Magery, 300}
                },
                StamMaxSeed = 500,
                Str = 500,
                Tamable = false,
                VirtualArmor = 20
            });
        }


        [Constructible]
        public EyesOfEradication() : base(CreatureProperties.Get<EyesOfEradication>())
        {
            // Add customization here

            AddItem(new SkinningKnife
            {
                Movable = false,
                Name = "Beholder Weapon",
                Speed = 33,
                MaxHitPoints = 250,
                HitPoints = 250,
                HitSound = 0x17C,
                MissSound = 0x239
            });
        }

        [Constructible]
        public EyesOfEradication(Serial serial) : base(serial)
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