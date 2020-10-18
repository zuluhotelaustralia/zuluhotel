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
    public class GwennoUndeadBard : BaseCreature
    {
        static GwennoUndeadBard()
        {
            CreatureProperties.Register<GwennoUndeadBard>(new CreatureProperties
            {
                // AttackAttribute = MaceFighting,
                // AttackSpeed = 80,
                // cast_pct = 100,
                // CProp_PermMagicImmunity = i4,
                // DataElementId = gwenno,
                // DataElementType = NpcTemplate,
                // dstart = 10,
                // dstart_0 = 10,
                // Equip = florist,
                // Equip_0 = florist,
                // guardignore = 1,
                // hostile = 1,
                // lootgroup = 35,
                // MagicItemChance = 70,
                // MagicItemLevel = 5,
                // num_casts = 50,
                // script = spellkillpcs,
                // spell = paralyze,
                // spell_0 = massdispel,
                // spell_1 = curse,
                // spell_2 = manadrain,
                // TrueColor = 0,
                ActiveSpeed = 0.2,
                AiType = AIType.AI_Mage /* spellkillpcs */,
                AlwaysMurderer = true,
                Body = 0x191,
                CorpseNameOverride = "corpse of Gwenno the Undead Bard",
                CreatureType = CreatureType.Undead,
                DamageMax = 50,
                DamageMin = 5,
                Dex = 510,
                Female = false,
                FightMode = FightMode.Closest,
                FightRange = 1,
                HitsMax = 510,
                Hue = 0,
                Int = 510,
                ManaMaxSeed = 500,
                Name = "Gwenno the Undead Bard",
                PassiveSpeed = 0.4,
                PerceptionRange = 10,
                PreferredSpells = new List<Type>
                {
                    typeof(Spells.Fifth.ParalyzeSpell),
                    typeof(Spells.Seventh.MassDispelSpell),
                    typeof(Spells.Fourth.CurseSpell),
                    typeof(Spells.Fourth.ManaDrainSpell)
                },
                Resistances = new Dictionary<ElementalType, CreatureProp>
                {
                    {ElementalType.Poison, 100}
                },
                Skills = new Dictionary<SkillName, CreatureProp>
                {
                    {SkillName.MagicResist, 80},
                    {SkillName.Macing, 100}
                },
                StamMaxSeed = 500,
                Str = 510
            });
        }


        [Constructible]
        public GwennoUndeadBard() : base(CreatureProperties.Get<GwennoUndeadBard>())
        {
            // Add customization here
        }

        [Constructible]
        public GwennoUndeadBard(Serial serial) : base(serial)
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