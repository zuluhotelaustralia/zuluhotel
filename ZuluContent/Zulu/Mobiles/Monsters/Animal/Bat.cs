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
    public class Bat : BaseCreature
    {
        static Bat()
        {
            CreatureProperties.Register<Bat>(new CreatureProperties
            {
                // cast_pct = 20,
                // DataElementId = bat,
                // DataElementType = NpcTemplate,
                // dstart = 10,
                // Equip = bat,
                // food = veggie,
                // Graphic = 0x0ec4 /* Weapon */,
                // guardignore = 1,
                // Hitscript = :combat:poisonhit /* Weapon */,
                // num_casts = 2,
                // script = spellkillpcs,
                // Speed = 45 /* Weapon */,
                // spell = poison,
                // spell_0 = manadrain,
                // TrueColor = 5555,
                ActiveSpeed = 0.2,
                AiType = AIType.AI_Mage /* spellkillpcs */,
                AlwaysAttackable = true,
                Body = 0x06,
                CorpseNameOverride = "corpse of a bat",
                CreatureType = CreatureType.Animal,
                DamageMax = 9,
                DamageMin = 3,
                Dex = 80,
                Female = false,
                FightMode = FightMode.None,
                FightRange = 1,
                HitPoison = Poison.Lesser,
                HitsMax = 30,
                Hue = 5555,
                Int = 60,
                ManaMaxSeed = 50,
                MinTameSkill = 35,
                Name = "a bat",
                PassiveSpeed = 0.4,
                PerceptionRange = 10,
                PreferredSpells = new List<Type>
                {
                    typeof(Spells.Third.PoisonSpell),
                    typeof(Spells.Fourth.ManaDrainSpell)
                },
                ProvokeSkillOverride = 10,
                Skills = new Dictionary<SkillName, CreatureProp>
                {
                    {SkillName.MagicResist, 20},
                    {SkillName.Tactics, 70},
                    {SkillName.Macing, 70},
                    {SkillName.Magery, 50}
                },
                StamMaxSeed = 70,
                Str = 30,
                Tamable = true,
                VirtualArmor = 10
            });
        }


        [Constructible]
        public Bat() : base(CreatureProperties.Get<Bat>())
        {
            // Add customization here

            AddItem(new SkinningKnife
            {
                Movable = false,
                Name = "Bat Weapon",
                Speed = 45,
                MaxHitPoints = 250,
                HitPoints = 250
            });
        }

        [Constructible]
        public Bat(Serial serial) : base(serial)
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