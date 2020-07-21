

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
    public class TerathanMatriarch : BaseCreature
    {
        static TerathanMatriarch() => CreatureProperties.Register<TerathanMatriarch>(new CreatureProperties
        {
            // CProp_PermMagicImmunity = i6,
            // DataElementId = terathanmatriarch,
            // DataElementType = NpcTemplate,
            // dstart = 10,
            // Equip = terathanmatriarch,
            // EvaluateIntelligence = 175,
            // Graphic = 0x0ec4 /* Weapon */,
            // HitSound = 0x257 /* Weapon */,
            // hostile = 1,
            // lootgroup = 65,
            // MagicItemChance = 50,
            // MagicItemLevel = 4,
            // MissSound = 0x258 /* Weapon */,
            // script = spellkillpcs,
            // Speed = 40 /* Weapon */,
            // spell = flamestrike,
            // spell_0 = poison,
            // spell_1 = ebolt,
            // spell_2 = explosion,
            // TrueColor = 0,
            // virtue = 2,
            ActiveSpeed = 0.2,
            AiType = AIType.AI_Mage /* spellkillpcs */,
            AlwaysMurderer = true,
            Body = 0x48,
            CorpseNameOverride = "corpse of a Terathan Matriarch",
            CreatureType = CreatureType.Terathan,
            DamageMax = 60,
            DamageMin = 20,
            Dex = 70,
            Female = false,
            FightMode = FightMode.Closest,
            FightRange = 1,
            HitsMax = 350,
            Hue = 0,
            Int = 650,
            ManaMaxSeed = 200,
            Name = "a Terathan Matriarch",
            PassiveSpeed = 0.4,
            PerceptionRange = 10,
            PreferredSpells = new List<Type>
            {
                typeof(Spells.Third.PoisonSpell),
                typeof(Spells.Sixth.EnergyBoltSpell),
                typeof(Spells.Sixth.ExplosionSpell),
            },
            SaySpellMantra = true,
            Skills = new Dictionary<SkillName, CreatureProp>
            {
                { SkillName.Parry, 65 },
                { SkillName.Tactics, 90 },
                { SkillName.Macing, 110 },
                { SkillName.Magery, 175 },
                { SkillName.MagicResist, 150 },
            },
            StamMaxSeed = 50,
            Str = 350,
            VirtualArmor = 35,

        });


        [Constructible]
public TerathanMatriarch() : base(CreatureProperties.Get<TerathanMatriarch>())
        {
            // Add customization here

            AddItem(new SkinningKnife
            {
                Movable = false,
                Name = "Terathan Matriarch Weapon",
                Speed = 40,
                MaxHitPoints = 250,
                HitPoints = 250,
                HitSound = 0x257,
                MissSound = 0x258,
            });


        }

        [Constructible]
public TerathanMatriarch(Serial serial) : base(serial) {}



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
