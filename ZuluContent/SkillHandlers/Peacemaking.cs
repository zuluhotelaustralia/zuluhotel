using System;
using System.Threading.Tasks;
using Scripts.Zulu.Engines.Classes;
using Scripts.Zulu.Utilities;
using Server.Targeting;
using Server.Mobiles;
using Server.Items;
using ZuluContent.Zulu.Skills;

namespace Server.SkillHandlers
{
    public class Peacemaking : BaseSkillHandler
    {
        public override SkillName Skill => SkillName.Peacemaking;

        public override async Task<TimeSpan> OnUse(Mobile from)
        {
            var instrument = BaseInstrument.PickInstrument(from);

            if (instrument == null)
            {
                from.SendFailureMessage("You don't have an instrument to play!");
                return Delay;
            }
            
            var target = new AsyncTarget<Mobile>(from,
                new TargetOptions {Range = BaseInstrument.GetBardRange(from, SkillName.Provocation)});
            from.Target = target;
            from.RevealingAction();

            from.SendSuccessMessage(1049525); // Whom do you wish to calm?

            var (targeted, _) = await target;
            
            if (targeted == null)
            {
                from.SendFailureMessage(1049528); // You cannot calm that!
                return Delay;
            }
            
            if (!instrument.IsChildOf(from.Backpack))
            {
                from.SendFailureMessage(1062488); // The instrument you are trying to play is no longer in your backpack!
                return Delay;
            }

            if (targeted == from)
            {
                instrument.PlayMusicEffect(from, 0x5A);

                // Standard mode : reset combatants for everyone in the area
                var map = from.Map;

                if (map != null)
                {
                    var range = BaseInstrument.GetBardRange(from, SkillName.Peacemaking);

                    var calmed = false;

                    var eable = from.GetMobilesInRange(range);

                    foreach (var m in eable)
                    {
                        if (m is BaseCreature {Uncalmable: true} ||
                            m is BaseCreature {AreaPeaceImmune: true} || m == from ||
                            !from.CanBeHarmful(m, false))
                            continue;

                        var difficulty  = m is BaseCreature creature ? BaseInstrument.GetDifficulty(creature) : m.Int;

                        var points = calmed ? 0 : difficulty * 10;

                        if (from.ShilCheckSkill(SkillName.Peacemaking, difficulty, points))
                        {
                            if (from.ShilCheckSkill(SkillName.Musicianship, difficulty, points / 3))
                            {
                                calmed = true;

                                m.SendSuccessMessage(500616); // You hear lovely music, and forget to continue battling!
                                m.Combatant = null;
                                m.Warmode = false;

                                if (m is BaseCreature {BardPacified: false} baseCreature)
                                    baseCreature.Pacify(from, DateTime.Now + TimeSpan.FromSeconds(5.0));
                            }
                        }
                    }
                    
                    eable.Free();

                    if (!calmed)
                    {
                        from.SendFailureMessage(500612); // You play poorly, and there is no effect.
                        instrument.PlayInstrumentBadly(from);
                        instrument.ConsumeUse(from);
                    }
                    else
                    {
                        from.SendSuccessMessage(500615); // You play your hypnotic music, stopping the battle.
                        instrument.PlayInstrumentWell(from);
                        instrument.ConsumeUse(from);
                    }
                }
            }
            else
            {
                // Target mode : pacify a single target for a longer duration
                if (!from.CanBeHarmful(targeted, false))
                {
                    from.SendFailureMessage(1049528);
                }
                else if (targeted is BaseCreature { Uncalmable: true })
                {
                    from.SendFailureMessage(1049526); // You have no chance of calming that creature.
                }
                else if (targeted is BaseCreature { BardPacified: true })
                {
                    from.SendFailureMessage(1049527); // That creature is already being calmed.
                }
                else
                {
                    var difficulty = targeted is BaseCreature creature ? BaseInstrument.GetDifficulty(creature) : targeted.Int;
                    var points = difficulty * 10;
                    
                    if (from.ShilCheckSkill(SkillName.Peacemaking, difficulty, points))
                    {
                        if (from.ShilCheckSkill(SkillName.Musicianship, difficulty, points / 3))
                        {
                            instrument.PlayInstrumentWell(from);
                            instrument.ConsumeUse(from);
                            
                            if (targeted is BaseCreature bc)
                            {
                                from.SendSuccessMessage(1049532); // You play hypnotic music, calming your target.

                                targeted.Combatant = null;
                                targeted.Warmode = false;

                                var seconds = 100 - difficulty / 1.5;

                                seconds = seconds switch
                                {
                                    > 120 => 120,
                                    < 10 => 10,
                                    _ => seconds
                                };

                                bc.Pacify(from, DateTime.Now + TimeSpan.FromSeconds(seconds));
                            }
                            else
                            {
                                from.SendSuccessMessage(1049532); // You play hypnotic music, calming your target.

                                targeted.SendSuccessMessage(500616); // You hear lovely music, and forget to continue battling!
                                targeted.Combatant = null;
                                targeted.Warmode = false;
                            }
                        }
                        else
                        {
                            from.SendFailureMessage(500612); // You play poorly, and there is no effect.
                            instrument.PlayInstrumentBadly( from );
                            instrument.ConsumeUse(from);
                        }
                    }
                    else
                    {
                        from.SendFailureMessage(1049531); // // You attempt to calm your target, but fail.
                        instrument.PlayInstrumentBadly(from);
                        instrument.ConsumeUse(from);
                    }
                }
            }

            return Delay;
        }
    }
}
