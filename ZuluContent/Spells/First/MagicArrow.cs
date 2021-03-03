using System.Threading.Tasks;
using Server.Targeting;
#pragma warning disable 1998

namespace Server.Spells.First
{
    public class MagicArrowSpell : MagerySpell
    {
        public override bool DelayedDamageStacking => true;

        public override bool DelayedDamage => true;
        
        public MagicArrowSpell(Mobile caster, Item spellItem) : base(caster, spellItem) { }
        
        public override async Task OnCastAsync(TargetResponse<object> response = null)
        {
            if (!(response?.Target is Mobile mobile))
                return;

            if (!CheckHarmfulSequence(mobile))
                return;

            var source = Caster;

            SpellHelper.Turn(source, mobile);
            SpellHelper.CheckReflect((int) Circle, ref source, ref mobile);

            double damage = Utility.Random(4, 4);

            if (CheckResisted(mobile))
            {
                damage *= 0.75;
                mobile.SendLocalizedMessage(501783); // You feel yourself resisting magical energy.
            }

            damage *= GetDamageScalar(mobile);

            source.MovingParticles(mobile, 0x36E4, 5, 0, false, false, 3006, 0, 0);
            source.PlaySound(0x1E5);

            SpellHelper.Damage(damage, mobile, Caster, this);
        }
    }
}