using System;
using System.Collections;
using System.Threading.Tasks;
using Server;
using Server.Engines.Magic;
using Server.Network;
using Server.Items;
using Server.Targeting;
using Server.Mobiles;
using Server.Spells;
using ZuluContent.Zulu.Engines.Magic;
using static Server.Engines.Magic.IElementalResistible;

namespace Scripts.Zulu.Spells.Necromancy
{
    public class ControlUndeadSpell : NecromancerSpell, ITargetableAsyncSpell<BaseCreature>
    {
        public ControlUndeadSpell(Mobile caster, Item spellItem) : base(caster, spellItem) { }
        
        public async Task OnTargetAsync(ITargetResponse<BaseCreature> response)
        {
            if (!response.HasValue)
                return;
            
            var target = response.Target;
            SpellHelper.Turn(Caster, target);

            if (target.CreatureType != CreatureType.Undead || target.Summoned || target.ControlMaster != null)
            {
                // That creature cannot be tamed.
                target.PrivateOverheadMessage(MessageType.Regular, 0x3B2, 1049655, Caster.NetState);
                return;
            }

            var duration = Caster.Skills[SkillName.Magery].Value * 3.0;
            Caster.FireHook(h => h.OnModifyWithMagicEfficiency(Caster, ref duration));
            var resist = target.GetResist(Info.DamageType);
            
            if (resist > 0)
            {
                duration -= duration * resist * 0.25;
                if(duration < 1)
                {
                    target.PrivateOverheadMessage(
                        MessageType.Regular, 
                        0x3B2, 
                        true, 
                        "This undead is immune to your spell!", 
                        Caster.NetState
                    );
                    return;
                }
            }

            target.SetControlMaster(Caster);
            target.SummonMaster = Caster;
            target.ControlOrder = OrderType.Follow;
            target.SpellBound = true;
            
            ReleaseControlAfterDelay(target, Caster, TimeSpan.FromSeconds(duration));
        }

        private static async void ReleaseControlAfterDelay(BaseCreature creature, Mobile caster, TimeSpan delay)
        {
            await Timer.Pause(delay);
            if (creature?.Deleted == false && creature.ControlMaster == caster)
            {
                creature.ControlOrder = OrderType.Release;
                creature.SpellBound = false;

                await Timer.Pause(500);
                creature.Attack(caster);
            }
        }
    }
}