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
        public MassDispelSpell(Mobile caster, Item spellItem) : base(caster, spellItem) { }
        public void Target(IPoint3D p)
        {
            if (!Caster.CanSee(p))
            {
                Caster.SendLocalizedMessage(500237); // Target can not be seen.
            }
            else if (CheckSequence())
            {
                SpellHelper.Turn(Caster, p);

                SpellHelper.GetSurfaceTop(ref p);

                var targets = new List<Mobile>();

                var map = Caster.Map;

                if (map != null)
                {
                    IPooledEnumerable eable = map.GetMobilesInRange(new Point3D(p), 8);

                    foreach (Mobile m in eable)
                        if (m is BaseCreature && (m as BaseCreature).IsDispellable && Caster.CanBeHarmful(m, false))
                            targets.Add(m);

                    eable.Free();
                }

                for (var i = 0; i < targets.Count; ++i)
                {
                    var m = targets[i];

                    var bc = m as BaseCreature;

                    if (bc == null)
                        continue;

                    var dispelChance =
                        (50.0 + 100 * (Caster.Skills.Magery.Value - bc.DispelDifficulty) / (bc.DispelFocus * 2)) / 100;

                    if (dispelChance > Utility.RandomDouble())
                    {
                        Effects.SendLocationParticles(EffectItem.Create(m.Location, m.Map, EffectItem.DefaultDuration),
                            0x3728, 8, 20, 5042);
                        Effects.PlaySound(m, 0x201);

                        m.Delete();
                    }
                    else
                    {
                        Caster.DoHarmful(m);

                        m.FixedEffect(0x3779, 10, 20);
                    }
                }
            }

            FinishSequence();
        }

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
                if (!SpellHelper.ValidIndirectTarget(Caster, target) || 
                    !Caster.CanBeHarmful(target, false) ||
                    !Caster.InLOS(target)
                )
                    continue;
                
                target.DoHarmful(target, true);
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