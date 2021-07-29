using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Scripts.Zulu.Engines.Classes;
using Scripts.Zulu.Utilities;
using Server.Items;
using Server.Targeting;
using Server.Mobiles;
using ZuluContent.Zulu.Skills;

namespace Server.SkillHandlers
{
    public class Discordance : BaseSkillHandler
    {
        private static readonly Dictionary<Serial, DiscordanceInfo> ActiveDiscords = new();

        public override SkillName Skill { get; } = SkillName.Discordance;

        public override async Task<TimeSpan> OnUse(Mobile from)
        {
            from.RevealingAction();

            var instrument = BaseInstrument.PickInstrument(from);

            if (instrument == null)
            {
                from.SendFailureMessage("You don't have an instrument to play!");
                return Delay;
            }

            from.SendSuccessMessage(1049541); // Choose the target for your song of discordance.
            var target = new AsyncTarget<BaseCreature>(from,
                new TargetOptions {Range = BaseInstrument.GetBardRange(from, SkillName.Provocation)});
            from.Target = target;

            var (creature, _) = await target;
            
            if (!instrument.IsChildOf(from.Backpack))
            {
                // The instrument you are trying to play is no longer in your backpack!
                from.SendFailureMessage(1062488);
                return Delay;
            }

            if (creature == from || (creature.BardImmune || !from.CanBeHarmful(creature, false)) && creature.ControlMaster != from)
            {
                from.SendFailureMessage(1049535); // A song of discord would have no effect on that.
                return Delay;
            }

            if (ActiveDiscords.ContainsKey(creature.Serial)) // Already discorded
            {
                from.SendFailureMessage(1049537); // Your target is already in discord.
                return Delay;
            }

            var difficulty = BaseInstrument.GetDifficulty(creature);

            difficulty /= from.GetClassModifier(Skill);

            if (!from.ShilCheckSkill(SkillName.Musicianship, (int) difficulty, (int) (difficulty * 10)))
            {
                from.SendFailureMessage(500612); // You play poorly, and there is no effect.
                instrument.PlayInstrumentBadly(from);
                instrument.ConsumeUse(from);
                return Delay;
            }

            if (!from.ShilCheckSkill(SkillName.Discordance, (int) difficulty, (int) (difficulty * 10)))
            {
                from.SendFailureMessage(1049540); // You fail to disrupt your target
                instrument.PlayInstrumentBadly(from);
                instrument.ConsumeUse(from);
                return Delay;
            }

            from.SendSuccessMessage(1049539); // You play the song suppressing your targets strength
            instrument.PlayInstrumentWell(from);
            instrument.ConsumeUse(from);


            var effect = (int) (from.Skills[SkillName.Discordance].Value / -5.0);
            var scalar = effect * 0.01 * from.GetClassModifier(Skill);

            var mods = new List<object>
            {
                new StatMod(StatType.Str, "DiscordanceStr", (int) (creature.RawStr * scalar), TimeSpan.Zero),
                new StatMod(StatType.Int, "DiscordanceInt", (int) (creature.RawInt * scalar), TimeSpan.Zero),
                new StatMod(StatType.Dex, "DiscordanceDex", (int) (creature.RawDex * scalar), TimeSpan.Zero)
            };

            for (var i = 0; i < creature.Skills.Length; ++i)
            {
                if (creature.Skills[i].Value > 0)
                    mods.Add(new DefaultSkillMod((SkillName) i, true, creature.Skills[i].Value * scalar));
            }

            var info = new DiscordanceInfo(from, creature, mods);
            info.Timer = Timer.DelayCall(TimeSpan.Zero, TimeSpan.FromSeconds(1.25), ProcessDiscordance, info);

            ActiveDiscords[creature.Serial] = info;

            return Delay;
        }
        
        private class DiscordanceInfo
        {
            public readonly Mobile From;
            public readonly Mobile Creature;
            public long EndTime;
            public bool Ending;
            public Timer Timer;
            public readonly List<object> Mods;

            public DiscordanceInfo(Mobile from, Mobile creature, List<object> mods)
            {
                From = from;
                Creature = creature;
                EndTime = Core.TickCount;
                Ending = false;
                Mods = mods;

                Apply();
            }

            public void Apply()
            {
                foreach (var mod in Mods)
                {
                    switch (mod)
                    {
                        case StatMod statMod:
                            Creature.AddStatMod(statMod);
                            break;
                        case SkillMod skillMod:
                            Creature.AddSkillMod(skillMod);
                            break;
                    }
                }
            }

            public void Clear()
            {
                foreach (var mod in Mods)
                {
                    switch (mod)
                    {
                        case StatMod statMod:
                            Creature.RemoveStatMod(statMod.Name);
                            break;
                        case SkillMod skillMod:
                            Creature.RemoveSkillMod(skillMod);
                            break;
                    }
                }
            }
        }

        private static void ProcessDiscordance(DiscordanceInfo info)
        {
            var from = info.From;
            var creature = info.Creature;
            var ends = false;

            // According to uoherald bard must remain alive, visible, and
            // within range of the target or the effect ends in 15 seconds.
            if (!creature.Alive || creature.Deleted || !from.Alive || from.Hidden)
            {
                ends = true;
            }
            else
            {
                var range = (int) creature.GetDistanceToSqrt(from);
                var maxRange = BaseInstrument.GetBardRange(from, SkillName.Discordance);

                if (from.Map != creature.Map || range > maxRange)
                    ends = true;
            }

            if (ends && info.Ending && info.EndTime < Core.TickCount)
            {
                info.Timer?.Stop();
                info.Clear();
                ActiveDiscords.Remove(creature.Serial);
            }
            else
            {
                switch (ends)
                {
                    case true when !info.Ending:
                        info.Ending = true;
                        info.EndTime = Core.TickCount + 15000;
                        break;
                    case false:
                        info.Ending = false;
                        info.EndTime = Core.TickCount;
                        break;
                }

                creature.FixedEffect(0x376A, 1, 32);
            }
        }
    }
}