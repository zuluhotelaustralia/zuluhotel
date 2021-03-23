using System;
using System.Collections;
using System.Threading.Tasks;
using Server.Network;
using Server.Items;
using Server.Mobiles;
using Scripts.Zulu.Engines.Classes;
using Server;
using Server.Targeting;
using Server.Spells;
using ZuluContent.Zulu.Engines.Magic.Enchantments.Buffs;

namespace Scripts.Zulu.Spells.Earth
{
    public class EarthsBlessingSpell : EarthSpell, IAsyncSpell
    {
        public override TimeSpan CastDelayBase => TimeSpan.FromSeconds(0);
    
        public override double RequiredSkill => 60.0;
    
        public override int RequiredMana => 5;

        public EarthsBlessingSpell(Mobile caster, Item spellItem) : base(caster, spellItem)
        {
        }

        public async Task CastAsync()
        {
            if (!Caster.CanBuff(Caster, true, BuffIcon.Curse, BuffIcon.Bless))
                return;

            var modAmount = (int) (SpellHelper.GetModAmount(Caster, Caster, StatType.All) * 1.5);
            var duration = SpellHelper.GetDuration(Caster, Caster) * 1.5;
            
            Caster.TryAddBuff(new StatBuff(StatType.All)
            {
                Value = modAmount,
                Duration = duration
            });

            Caster.FixedParticles(0x373A, 10, 15, 5018, EffectLayer.Waist);
            Caster.PlaySound(0x1EA);
            
            if (!Caster.CanBuff(Caster, true, BuffIcon.Protection, BuffIcon.ArchProtection))
                return;
            
            Caster.TryAddBuff(new ArmorBuff
            {
                Value = (int) (modAmount * 0.75 + 1),
                Duration = duration
            });
        }
    }
}