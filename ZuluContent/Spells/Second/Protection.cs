using System;
using System.Threading.Tasks;
using Server.Spells.First;
using Server.Targeting;

namespace Server.Spells.Second
{
    public class ProtectionSpell : MagerySpell, ITargetableAsyncSpell<Mobile>
    {
        public ProtectionSpell(Mobile caster, Item spellItem) : base(caster, spellItem) { }
        
        public async Task OnTargetAsync(ITargetResponse<Mobile> response)
        {
            if (!response.HasValue)
                return;

            var m = response.Target;
            
            if (!m.BeginAction<ProtectionSpell>())
            {
                Caster.SendLocalizedMessage(1005559); // This spell is already in effect.
                return;
            }

            var amount = SpellHelper.GetModAmount(Caster, m);
            var duration = SpellHelper.GetDuration(Caster, m);

            m.VirtualArmorMod += amount;
            m.FixedParticles(0x373B, 9, 20, 5027, EffectLayer.Waist);
            m.PlaySound(0x1ED);

            EndActionAsync(m, amount, duration);
        }
        
        private static async void EndActionAsync(Mobile mobile, int amount, TimeSpan duration)
        {
            await Timer.Pause(duration);

            if (!mobile.CanBeginAction<ProtectionSpell>())
            {
                mobile.VirtualArmorMod -= amount;
                mobile.SendLocalizedMessage(1005550); // Your protection spell has been nullified.
                mobile.EndAction<ProtectionSpell>();
            }
        }
    }
}