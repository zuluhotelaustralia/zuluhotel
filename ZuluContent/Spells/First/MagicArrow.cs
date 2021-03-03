using System.Threading.Tasks;
using Server.Targeting;

namespace Server.Spells.First
{
    public class MagicArrowSpell : MagerySpell
    {
        public MagicArrowSpell(Mobile caster, Item spellItem) : base(caster, spellItem)
        {
        }


        public override bool DelayedDamageStacking
        {
            get { return true; }
        }

        public override bool DelayedDamage
        {
            get { return true; }
        }

        public readonly TargetOptions Options = new()
        {
            Range = 12,
            AllowGround = false,
            Flags = TargetFlags.Harmful
        };

        public override async Task OnCastAsync()
        {
            var target = new AsyncTarget<Mobile>(Caster, Options);
            Caster.Target = target;

            var response = await target;

            if (response.Type != TargetResponseType.Success)
                return;
            
            response.Target.SendMessage($"Hello from {nameof(OnCastAsync)}");
            Target(response.Target);
        }

        public override void OnCast()
        {
            //Caster.Target = new AwaitableSpellTarget<Mobile>(this, Options);
        }

        public void Target(Mobile m)
        {
            if (!Caster.CanSee(m))
            {
                Caster.SendLocalizedMessage(500237); // Target can not be seen.
            }
            else if (CheckHSequence(m))
            {
                var source = Caster;

                SpellHelper.Turn(source, m);

                SpellHelper.CheckReflect((int) Circle, ref source, ref m);

                double damage = Utility.Random(4, 4);

                if (CheckResisted(m))
                {
                    damage *= 0.75;

                    m.SendLocalizedMessage(501783); // You feel yourself resisting magical energy.
                }

                damage *= GetDamageScalar(m);

                source.MovingParticles(m, 0x36E4, 5, 0, false, false, 3006, 0, 0);
                source.PlaySound(0x1E5);

                SpellHelper.Damage(damage, m, Caster, this);
            }

            FinishSequence();
        }
    }
}