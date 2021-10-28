using System.Threading.Tasks;
using Scripts.Zulu.Engines.Classes;
using Scripts.Zulu.Utilities;
using Server.Items;
using Server.Mobiles;
using Server.Network;
using Server.Targeting;
using ZuluContent.Zulu.Engines.Magic;

namespace Server.Spells.Sixth
{
    public class DispelSpell : MagerySpell, ITargetableAsyncSpell<Mobile>
    {
        public DispelSpell(Mobile caster, Item spellItem = null) : base(caster, spellItem)
        {
        }

        public async Task OnTargetAsync(ITargetResponse<Mobile> response)
        {
            var magery = Caster.Skills[SkillName.Magery].Value;
            Caster.FireHook(h => h.OnModifyWithMagicEfficiency(Caster, ref magery));

            if (!response.HasValue)
            {
                if (response.InvalidTarget is IDispellable {Dispellable: true} item)
                {
                    Effects.SendLocationParticles(
                        EffectItem.Create(item.Location, item.Map, EffectItem.DefaultDuration),
                        0x3729, 9, 20, 5042);
                    Effects.PlaySound(item.Location, Caster.Map, 0x201);

                    if (!Caster.ShilCheckSkill(SkillName.MagicResist, (int) magery, 25))
                        item.Delete();
                    else
                        Caster.SendFailureMessage("You failed to dispel the field.");
                }
                else
                {
                    Caster.SendLocalizedMessage(1005049); // That cannot be dispelled.
                }

                return;
            }


            var target = response.Target;
            var loc = SpellHelper.GetSurfaceTop(target.Location);

            SpellHelper.Turn(Caster, loc);

            (target as IBuffable)?.BuffManager.DispelBuffs();

            if (target is BaseCreature {Summoned: true} creature)
            {
                if (!creature.ShilCheckSkill(SkillName.MagicResist, (int) magery, 50))
                {
                    // Your summoned creature has been dispelled.
                    (creature.ControlMaster as PlayerMobile)?.SendLocalizedMessage(1153193);
                    creature.Delete();
                }
                else
                {
                    target.PrivateOverheadMessage(
                        MessageType.Regular, 
                        ZhConfig.Messaging.FailureHue, 
                        1010084,
                        Caster.NetState
                    );
                }
            }

            Effects.SendLocationParticles(
                EffectItem.Create(loc, Caster.Map, EffectItem.DefaultDuration),
                0x3728, 8, 20, 5042
            );
            Effects.PlaySound(loc, Caster.Map, 0x201);
        }
    }
}