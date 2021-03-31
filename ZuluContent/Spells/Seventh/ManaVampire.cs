using System.Threading.Tasks;
using Server.Network;
using Server.Targeting;
using ZuluContent.Zulu.Engines.Magic;

namespace Server.Spells.Seventh
{
    public class ManaVampireSpell : MagerySpell, ITargetableAsyncSpell<Mobile>
    {
        public ManaVampireSpell(Mobile caster, Item spellItem = null) : base(caster, spellItem) { }
        
        public async Task OnTargetAsync(ITargetResponse<Mobile> response)
        {
            if (!response.HasValue)
                return;
            
            var target = response.Target;
            
            SpellHelper.Turn(Caster, target);
            
            target.Spell?.OnCasterHurt();
            target.Paralyzed = false;
            
            var magery = Caster.Skills[SkillName.Magery].Value;
            var resist = target.Skills[SkillName.MagicResist].Value;

            Caster.FireHook(h => h.OnModifyWithMagicEfficiency(Caster, ref magery));

            string message;
            if (resist >= magery + 5)
                message = $"{target.Name}'s will is too strong!";
            else
            {
                var amount = SpellHelper.TryResistDamage(Caster, target, Circle, (int)magery);
                if (amount > target.Mana)
                    amount = target.Mana;

                target.Mana -= amount;
                Caster.Mana += amount;
                
                message = $"You drained {amount} mana out of {target.Name}'s aura!";
            }

            target.PrivateOverheadMessage(MessageType.Regular, 0x3B2, true, message, Caster.NetState);
            
            target.FixedParticles(0x374A, 10, 15, 5054, EffectLayer.Head);
            target.PlaySound(0x1F9);

        }
    }
}