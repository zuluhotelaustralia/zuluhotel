using System.Linq;
using System.Threading.Tasks;
using Scripts.Zulu.Engines.Classes;
using Server.Items;
using Server.Mobiles;
using Server.Multis;
using Server.Network;
using Server.Targeting;
using ZuluContent.Zulu.Engines.Magic;

namespace Server.Spells.Third
{
    public class UnlockSpell : MagerySpell, ITargetableAsyncSpell<ILockpickable>
    {
        public UnlockSpell(Mobile caster, Item spellItem) : base(caster, spellItem) { }

        public async Task OnTargetAsync(ITargetResponse<ILockpickable> response)
        {
            if (!response.HasValue || !(response.Target is IPoint3D))
            {
                switch (response.InvalidTarget)
                {
                    case Mobile:
                        // That did not need to be unlocked.
                        Caster.LocalOverheadMessage(MessageType.Regular, 0x3B2, 503101);
                        break;
                    case Item:
                        Caster.SendLocalizedMessage(501666); // You can't unlock that!
                        break;
                }

                return;
            }
            
            var lockpickable = response.Target;
            
            if (response.Target is LockableContainer {IsSecure: true})
            {
                Caster.SendLocalizedMessage(503098); // You cannot cast this on a secure item.
                return;
            }
            
            if (lockpickable.LockLevel == 0)
            {
                Caster.SendLocalizedMessage(501666); // You can't unlock that!
                return;
            }
            
            var loc = new Point3D((IPoint3D)lockpickable);

            SpellHelper.Turn(Caster, lockpickable);

            if (!lockpickable.Locked)
            {
                // That did not need to be unlocked.
                Caster.LocalOverheadMessage(MessageType.Regular, 0x3B2, 503101);
                return;
            }

            var enemyCount = Caster.Map
                .GetMobilesInRange(new Point3D(Caster), 4)
                .Count(m => SpellHelper.ValidIndirectTarget(Caster, m) && Caster.CanBeHarmful(m, false));
            
            if (enemyCount > 1)
            {
                // There are ~1_NUMBER~ enemies near.
                Caster.SendLocalizedMessage(1045105, enemyCount.ToString()); 
                // Your concentration is disturbed, thus ruining thy spell.
                Caster.SendLocalizedMessage(500641, enemyCount.ToString());
                
                Effects.PlaySound(loc, Caster.Map, 0x11E);
                return;
            }

            var bonus = (double)lockpickable.LockLevel;
            Caster.FireHook(h => h.OnModifyWithMagicEfficiency(Caster, ref bonus));
            // OnModifyWithMagicEfficiency only does + modifiers, but lower is better here
            var level = (int)(lockpickable.LockLevel - bonus);
            
            if (Caster.ShilCheckSkill(SkillName.Magery, level, 0))
            {
                lockpickable.Locked = false;
                Effects.SendLocationParticles(EffectItem.Create(new Point3D(loc), Caster.Map, EffectItem.DefaultDuration), 0x376A, 9, 32, 5024);
                Effects.PlaySound(loc, Caster.Map, 0x1FF);
            }
            else
            {
                // My spell does not seem to have an effect on that lock.
                Caster.LocalOverheadMessage(MessageType.Regular, 0x3B2, 503099);
                Effects.PlaySound(loc, Caster.Map, 0x11E);
            }
        }
    }
}