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
            
            var provokeTarget = new AsyncTarget<BaseCreature>(from,
                new TargetOptions {Range = BaseInstrument.GetBardRange(from, SkillName.Provocation)});
            from.Target = provokeTarget;
            from.RevealingAction();

            from.SendSuccessMessage(501587); // Whom do you wish to incite?

            var (provokeTargeted, _) = await provokeTarget;
            
            if (!instrument.IsChildOf(from.Backpack))
            {
                from.SendFailureMessage(1062488); // The instrument you are trying to play is no longer in your backpack!
                return Delay;
            }
            
            if (provokeTargeted == null || (provokeTargeted.BardImmune || !from.CanBeHarmful(provokeTargeted, false)))
            {
                from.SendFailureMessage(501589); // You can't incite that!
                return Delay;
            }

            if (provokeTargeted.Controlled && provokeTargeted.ControlMaster != from)
            {
                from.SendFailureMessage(501590); // They are too loyal to their master to be provoked.
                return Delay;
            }
            
            var victimTarget = new AsyncTarget<BaseCreature>(from,
                new TargetOptions {Range = BaseInstrument.GetBardRange(from, SkillName.Provocation)});
            from.Target = victimTarget;
            
            from.RevealingAction();
            instrument.PlayInstrumentWell(from);
            from.SendSuccessMessage(1008085); // You play your music and your target becomes angered.  Whom do you wish them to attack?

            var (victimTargeted, _) = await victimTarget;
            
            if (!instrument.IsChildOf( from.Backpack ))
            {
                from.SendFailureMessage(1062488); // The instrument you are trying to play is no longer in your backpack!
                return Delay;
            }
            
            if (provokeTargeted.Unprovokable)
            {
                from.SendFailureMessage(1049446); // You have no chance of provoking those creatures.
                return Delay;
            }
            
            if (victimTargeted.Unprovokable || !from.CanBeHarmful(victimTargeted, false))
            {
                from.SendFailureMessage(1049446); // You have no chance of provoking those creatures.
                return Delay;
            }

            if (provokeTargeted == victimTargeted)
            {
                from.SendFailureMessage(501593); // You can't tell someone to attack themselves!
                return Delay;
            }

            if (provokeTargeted.Map != victimTargeted.Map || !provokeTargeted.InRange(victimTargeted, BaseInstrument.GetBardRange(from, SkillName.Provocation)))
            {
                from.SendFailureMessage(1049450); // The creatures you are trying to provoke are too far away from each other for your music to have an effect.
                return Delay;
            }

            var difficulty = BaseInstrument.GetDifficulty(provokeTargeted);
            
            difficulty /= from.GetClassModifier(Skill);

            if (from.ShilCheckSkill(SkillName.Provocation, (int) difficulty, (int) (difficulty * 10)))
            {
                if (from.ShilCheckSkill(SkillName.Musicianship, (int) difficulty, (int) (difficulty * 5)))
                {
                    from.SendSuccessMessage(501602); // Your music succeeds, as you start a fight.
                    instrument.PlayInstrumentWell(from);
                    instrument.ConsumeUse(from);
                    provokeTargeted.Provoke(from, victimTargeted, true);
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
