using System.Threading.Tasks;
using Server.Items;
using Server.Multis;
using Server.Network;
using Server.Targeting;
using ZuluContent.Zulu.Engines.Magic;

namespace Server.Spells.Third
{
    public class MagicLockSpell : MagerySpell, ITargetableAsyncSpell<ILockpickable>
    {
        public MagicLockSpell(Mobile caster, Item spellItem) : base(caster, spellItem) { }
        
        public async Task OnTargetAsync(ITargetResponse<ILockpickable> response)
        {
            if (!response.HasValue || !(response.Target is Item item))
            {
                Caster.SendLocalizedMessage(501764); // Hmmm...I can't lock that.
                return;
            }

            var loc = item.GetWorldLocation();
            var lockpickable = response.Target;
            
            if (item is BaseDoor && SpellHelper.IsTown(loc, Caster) || BaseHouse.CheckLockedDownOrSecured(item))
            {
                Caster.SendLocalizedMessage(501671); // You cannot currently lock that.
                return;
            }
            
            if (lockpickable.Locked)
            {
                // Target must be an unlocked chest.
                Caster.SendLocalizedMessage(501762);
                return;
            }
            
            SpellHelper.Turn(Caster, item);
            
            Caster.LocalOverheadMessage(MessageType.Regular, 0x3B2, 1048000); // You lock it.
            var difficulty = Caster.Skills[SkillName.Magery].Value;
            Caster.FireHook(h => h.OnModifyWithMagicEfficiency(Caster, ref difficulty));
            
            Effects.SendLocationParticles(
                EffectItem.Create(loc, item.Map, EffectItem.DefaultDuration),
                0x376A, 9, 32, 5020
            );

            Effects.PlaySound(loc, item.Map, 0x1FA);
            
            lockpickable.LockLevel = (int) difficulty;
            lockpickable.RequiredSkill = (int) difficulty;
            lockpickable.Locked = true;
        }
    }
}