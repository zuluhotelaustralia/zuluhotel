using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Scripts.Zulu.Engines.Classes;
using Server.Engines.Magic;
using Server.Items;
using Server.Mobiles;
using Server.Network;
using Server.Targeting;
using ZuluContent.Zulu.Engines.Magic;

namespace Server.Spells.Seventh
{
    public class MassDispelSpell : MagerySpell, ITargetableAsyncSpell<IPoint3D>
    {
        public MassDispelSpell(Mobile caster, Item spellItem = null) : base(caster, spellItem) { }

        public async Task OnTargetAsync(ITargetResponse<IPoint3D> response)
        {
            if (!response.HasValue)
                return;
            
            var point = SpellHelper.GetSurfaceTop(response.Target);
            
            var magery = Caster.Skills[SkillName.Magery].Value;
            var range = magery / 15.0;
            Caster.FireHook(h => h.OnModifyWithMagicEfficiency(Caster, ref range));
            
            var eable = Caster.Map.GetMobilesInRange(point, (int)range);
            foreach (var target in eable)
            {
                if (!Caster.InLOS(target))
                    continue;
                
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
                    EffectItem.Create(target.Location, target.Map, EffectItem.DefaultDuration),
                    0x3728, 8, 20, 5042
                );
                Effects.PlaySound(target, 0x201);
            }
            eable.Free();
        }
    }
}