using System;
using System.Threading.Tasks;
using Scripts.Zulu.Engines.Classes;
using Scripts.Zulu.Utilities;
using Server;
using Server.Network;
using Server.Targeting;
using Server.Mobiles;
using Server.Spells;
using ZuluContent.Zulu.Engines.Magic;

namespace Scripts.Zulu.Spells.Necromancy
{
    public class SpellbindSpell : NecromancerSpell, ITargetableAsyncSpell<BaseCreature>
    {
        public SpellbindSpell(Mobile caster, Item spellItem) : base(caster, spellItem) { }

        public async Task OnTargetAsync(ITargetResponse<BaseCreature> response)
        {
            if (!response.HasValue)
                return;
            
            var target = response.Target;
            SpellHelper.Turn(Caster, target);
            
            if (!target.Tamable || target.IsInvulnerable || target is BaseVendor)
            {
                // That creature cannot be tamed.
                target.PrivateOverheadMessage(MessageType.Regular, 0x3B2, 1049655, Caster.NetState);
                return;
            }
            
            var difficulty = target.MinTameSkill;

            difficulty = target.Str / 2.0;
            if (target.Int / 2.0 > difficulty) 
                difficulty = target.Int / 2.0;

            if (target.Dex / 2.0 > difficulty) 
                difficulty = target.Dex / 2.0;

            var magery = Caster.Skills.Magery.Value;
            Caster.FireHook(h => h.OnModifyWithMagicEfficiency(Caster, ref magery));

            var victimResist = target.Skills.MagicResist.Value;
            var duration = magery - victimResist / 2.0;
            
            var resist = target.GetResist(Info.DamageType);
            if (resist > 0)
            {
                duration -= duration * resist * 0.25;
                if (duration <= 0 || resist >= 100)
                    return;
            }

            if (magery < difficulty)
            {
                target.PrivateOverheadMessage(
                    MessageType.Regular,
                    0x3B2,
                    true,
                    "The creature's will is too strong!",
                    Caster.NetState
                );
                Caster.DoHarmful(target);
                return;
            }

            if (target.Controlled)
            {
                if (target.ControlMaster == Caster)
                {
                    Caster.SendFailureMessage("This creature is already under your control");
                    return;
                }

                if (target.ControlMaster is {} owner)
                {
                    var ownerTaming = owner.Skills.AnimalTaming.Value;
                    var ownerMagery = owner.Skills.Magery.Value;

                    if (owner.GetClassBonus(SkillName.AnimalTaming) > 1.0)
                        ownerTaming *= owner.GetClassBonus(SkillName.AnimalTaming);

                    if (owner.GetClassBonus(SkillName.Magery) > 1.0)
                        ownerMagery *= owner.GetClassBonus(SkillName.Magery);

                    if (ownerTaming > magery || ownerMagery > magery)
                    {
                        Caster.SendFailureMessage("The control of the owner over their pet is stronger than your spell!");
                        owner.SendMessage($"{Caster.Name} tried to take control of {target.Name}!");
                        Caster.DoHarmful(target);
                        return;
                    }
                }
            }
            
            target.PrivateOverheadMessage(
                MessageType.Regular,
                0x3B2,
                true,
                $"You successfully take control of {target.Name}'s mind.",
                Caster.NetState
            );
            
            target.SetControlMaster(Caster);
            target.ControlOrder = OrderType.Follow;
            target.SpellBound = true;

            duration = 10.0;
            ReleaseControlAfterDelay(target, Caster, TimeSpan.FromSeconds(duration));
        }

        private static async void ReleaseControlAfterDelay(BaseCreature creature, Mobile caster, TimeSpan delay)
        {
            await Timer.Pause(delay);
            if (creature?.Deleted == false && creature.ControlMaster == caster)
            {
                creature.ControlOrder = OrderType.Release;
                creature.SpellBound = false;
                
                if (creature is BaseMount {Rider: { }} mount) 
                    mount.Rider = null;

                var magery = caster.Skills.Magery.Value;
                caster.FireHook(h => h.OnModifyWithMagicEfficiency(caster, ref magery));
                var evalInt = caster.Skills.EvalInt.Value;
                caster.FireHook(h => h.OnModifyWithMagicEfficiency(caster, ref evalInt));

                if (creature.Int > magery / 2 + 15 && 
                    (creature.Int > magery + 20 || Utility.RandomMinMax(1, 100) <= (creature.Int - evalInt) / 2))
                {
                    await Timer.Pause(500);
                    creature.Attack(caster);
                }
            }
        }
    }
}