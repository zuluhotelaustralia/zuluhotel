using System;
using System.Threading.Tasks;
using Scripts.Zulu.Engines.Classes;
using Server;
using Server.Spells;
using ZuluContent.Zulu.Engines.Magic.Enchantments.Buffs;

namespace Scripts.Zulu.Spells.Necromancy
{
    public class LicheFormSpell : NecromancerSpell, IAsyncSpell
    {
        public LicheFormSpell(Mobile caster, Item spellItem = null) : base(caster, spellItem) { }

        public async Task CastAsync()
        {
            if (!Caster.CanBuff(Caster, true, BuffIcon.Polymorph, BuffIcon.LichForm))
                return;

            var casterSkill = Caster.Skills.Magery.Value;
            var duration = casterSkill * 5;

            var strMod = 0 - Caster.Str / 2.0;
            var dexMod = 0 - Caster.Dex / 2.0;
            var intMod = (double)Caster.Int;

            var classBonus = Caster.GetClassModifier(SkillName.Magery);
            if (classBonus > 1.0)
            {
                duration *= classBonus;
                strMod /= classBonus;
                dexMod /= classBonus;
                intMod *= classBonus;
            }
            
            Caster.FixedParticles(0x375B, 1, 16, 5044, EffectLayer.Waist);
            Caster.PlaySound(0x209);

            const int bodyId = 0x18;
            const int hueMod = 0;
            
            Caster.TryAddBuff(new Polymorph
            {
                Title = "Liche Form",
                Description = $"<br>Str: {strMod}<br>Dex: {dexMod}<br>Int: +{intMod}",
                Icon = BuffIcon.LichForm,
                BodyMods = (bodyId, hueMod),
                StatMods = (StrMod: (int)strMod, DexMod: (int)dexMod, IntMod: (int)intMod),
                Duration = TimeSpan.FromSeconds(duration),
            });
        }
    }
}