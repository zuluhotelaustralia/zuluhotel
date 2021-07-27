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
    public class Provocation : BaseSkillHandler
    {
        public override SkillName Skill => SkillName.Provocation;

        public override async Task<TimeSpan> OnUse(Mobile from)
        {
            var instrument = BaseInstrument.PickInstrument(from);

            if (instrument == null)
            {
                from.SendFailureMessage("You don't have an instrument to play!");
                return Delay;
            }
            
            var target1 = new AsyncTarget<BaseCreature>(from,
                new TargetOptions {Range = BaseInstrument.GetBardRange(from, SkillName.Provocation)});
            from.Target = target1;
            from.RevealingAction();

            from.SendSuccessMessage( 501587 ); // Whom do you wish to incite?

            var (targeted1, _) = await target1;
            
            if (!instrument.IsChildOf(from.Backpack))
            {
                from.SendFailureMessage(1062488); // The instrument you are trying to play is no longer in your backpack!
                return Delay;
            }
            
            if (targeted1 == from || (targeted1.BardImmune || !from.CanBeHarmful(targeted1, false)))
            {
                from.SendFailureMessage(501589); // You can't incite that!
                return Delay;
            }

            if (targeted1.Controlled && targeted1.ControlMaster != from)
            {
                from.SendFailureMessage(501590); // They are too loyal to their master to be provoked.
                return Delay;
            }
            
            var target2 = new AsyncTarget<BaseCreature>(from,
                new TargetOptions {Range = BaseInstrument.GetBardRange(from, SkillName.Provocation)});
            from.Target = target2;
            
            from.RevealingAction();
            instrument.PlayInstrumentWell(from);
            from.SendSuccessMessage(1008085); // You play your music and your target becomes angered.  Whom do you wish them to attack?

            var (targeted2, _) = await target2;
            
            if (!instrument.IsChildOf( from.Backpack ))
            {
                from.SendFailureMessage(1062488); // The instrument you are trying to play is no longer in your backpack!
                return Delay;
            }
            
            if (targeted1.Unprovokable)
            {
                from.SendFailureMessage(1049446); // You have no chance of provoking those creatures.
                return Delay;
            }
            
            if (targeted2.Unprovokable || !from.CanBeHarmful(targeted2, false))
            {
                from.SendFailureMessage(1049446); // You have no chance of provoking those creatures.
                return Delay;
            }

            if (targeted1 == targeted2)
            {
                from.SendFailureMessage(501593); // You can't tell someone to attack themselves!
                return Delay;
            }

            if (targeted1.Map != targeted2.Map || !targeted1.InRange(targeted2, BaseInstrument.GetBardRange(from, SkillName.Provocation)))
            {
                from.SendFailureMessage(1049450); // The creatures you are trying to provoke are too far away from each other for your music to have an effect.
                return Delay;
            }

            var difficulty = targeted1.ProvokeSkillOverride;
            if (difficulty == 0)
                difficulty = 100;

            if (from.ShilCheckSkill(SkillName.Provocation, difficulty, difficulty * 10))
            {
                if (from.ShilCheckSkill(SkillName.Musicianship, difficulty, difficulty * 5))
                {
                    from.SendSuccessMessage( 501602 ); // Your music succeeds, as you start a fight.
                    instrument.PlayInstrumentWell(from);
                    instrument.ConsumeUse(from);
                    targeted1.Provoke(from, targeted2, true);
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
                from.SendFailureMessage(501599); // Your music fails to incite enough anger.
                instrument.PlayInstrumentBadly(from);
                instrument.ConsumeUse(from);
            }

            return Delay;
        }
    }
}
