using System.Threading.Tasks;
using Server.Network;

namespace Server.Spells.Sixth
{
    public class RevealSpell : MagerySpell, IAsyncSpell
    {
        public RevealSpell(Mobile caster, Item spellItem) : base(caster, spellItem) { }

        public async Task CastAsync()
        {
            var target = SpellHelper.GetSurfaceTop(Caster.Location);

            var range = (int) (Caster.Skills[SkillName.Magery].Value / 10.0 - 5);

            var found = 0;
            var eable = Caster.Map.GetMobilesInRange(target, range > 1 ? range : 1);
            foreach (var hider in eable)
            {
                if (!hider.Hidden)
                    continue;
                
                var skill = SpellHelper.TryResistDamage(Caster, hider, Circle, (int)Caster.Skills[SkillName.Magery].Value);
                var resist = hider.Skills[SkillName.MagicResist].Value / 1.5;

                if (skill > resist)
                {
                    hider.RevealingAction();

                    hider.FixedParticles(0x375A, 9, 20, 5049, EffectLayer.Head);
                    hider.PlaySound(0x1FD);
                    
                    // You have been revealed!
                    hider.PrivateOverheadMessage(
                        MessageType.Regular, 
                        ZhConfig.Messaging.FailureHue, 
                        500814,
                        hider.NetState
                    );
                    
                    hider.PrivateOverheadMessage(
                        MessageType.Regular, 
                        ZhConfig.Messaging.SuccessHue, 
                        true,
                        "Ah ha!!",
                        Caster.NetState
                    );
                    found++;
                }
            }
            eable.Free();

            if (found == 0) 
                Caster.SendLocalizedMessage(500817); // You can see nothing hidden there.
        }
    }
}