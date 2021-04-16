using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Scripts.Zulu.Engines.Classes;
using Scripts.Zulu.Utilities;
using Server.Misc;
using Server.Targeting;
using Server.Items;
using Server.Network;
using ZuluContent.Zulu.Skills;

namespace Server.SkillHandlers
{
    public class Begging : BaseSkillHandler
    {
        private const int BegRange = 2;
        private const int BegDelayMs = 2000;
        private static readonly int BegCooldown = (int)TimeSpan.FromMinutes(30).TotalMilliseconds;
        private static readonly Dictionary<uint, long> MobileBegCooldown = new();

        private static readonly string[] BeggingLines =
        {
            "Give me something please...",
            "I need something to eat!",
            "I've got four children, please help me!",
            "Could thee spare a dime?",
            "Some thieves stole me everything, I'm broken now..."
        };

        private static readonly string[] ExitLines =
        {
            "I have to go!",
            "I must leave!!",
            "no....ehm...bye!",
            "Bye!!",
            "I must flee!!",
            "I gotta go!",
        };


        private static readonly TargetOptions TargetOptions = new()
        {
            Range = 1,
        };

        public override SkillName Skill => SkillName.Begging;

        public override async Task<TimeSpan> OnUse(Mobile from)
        {
            var target = new AsyncTarget<Mobile>(from, TargetOptions);
            from.Target = target;
            from.RevealingAction();

            from.SendLocalizedMessage(500397); // To whom do you wish to grovel?

            var (targeted, responseType) = await target;

            if (responseType != TargetResponseType.Success || targeted == null || !targeted.Body.IsHuman)
            {
                from.SendFailureMessage(500399); // There is little chance of getting money from that!
                return Delay;
            }

            if (targeted.Player) // We can't beg from players
            {
                from.SendFailureMessage(500398); // Perhaps just asking would work better.
                return Delay;
            }
            
            if (!CheckRange(from, targeted))
                return Delay;

            if (from.Mounted) // If we're on a mount, who would give us money?
            {
                from.SendFailureMessage(500404); // They seem unwilling to give you any money.
                return Delay;
            }

            if (!await DoSpeech(from, targeted))
            {
                // Exit message
                from.PublicOverheadMessage(MessageType.Regular, from.SpeechHue, true, ExitLines.RandomElement());
                return Delay;
            }
            
            if (MobileBegCooldown.TryGetValue(targeted.Serial, out var nextBegTime) && nextBegTime > Core.TickCount)
            {
                targeted.PublicOverheadMessage(MessageType.Regular, targeted.SpeechHue, true, "Hey!! I seem a bank?");
                return Delay;
            }

            var theirPack = targeted.Backpack;
            var badKarmaChance = 0.5 - (double) from.Karma / 8570;

            if (theirPack == null)
            {
                from.SendFailureMessage(500404); // They seem unwilling to give you any money.
                return Delay;
            }

            if (from.Karma < 0 && badKarmaChance > Utility.RandomDouble())
            {
                // Thou dost not look trustworthy... no gold for thee today!
                targeted.PublicOverheadMessage(MessageType.Regular, targeted.SpeechHue, 500406);
                return Delay;
            }

            if (!from.ShilCheckSkill(SkillName.Begging))
            {
                targeted.SendFailureMessage(500404); // They seem unwilling to give you any money.
                return Delay;
            }

            var toConsume = Utility.Random((int) from.Skills.Begging.Value) + Utility.Random(10) + 3;
            var consumed = theirPack.ConsumeUpTo(typeof(Gold), toConsume);

            if (consumed <= 0)
            {
                // I have not enough money to give thee any!
                targeted.PublicOverheadMessage(MessageType.Regular, targeted.SpeechHue, 500407);
                return Delay;
            }

            // I feel sorry for thee...
            targeted.PublicOverheadMessage(MessageType.Regular, targeted.SpeechHue, 500405);

            var gold = new Gold(consumed);

            from.AddToBackpack(gold);
            from.PlaySound(gold.GetDropSound());

            if (from.Karma > Titles.KarmaCriminalLimit) 
                Titles.AwardKarma(from, -25, true);

            MobileBegCooldown[targeted.Serial] = Core.TickCount + BegCooldown;

            return Delay;
        }

        public static bool CheckRange(Mobile from, Mobile targeted)
        {
            if (!from.InRange(targeted, BegRange))
            {
                from.SendFailureMessage(targeted.Female
                        ? 500402 // You are too far away to beg from her.
                        : 500401 // You are too far away to beg from him.
                );
                return false;
            }

            return true;
        }

        private static async Task<bool> DoSpeech(Mobile from, Mobile targeted)
        {
            // Face each other
            from.Direction = from.GetDirectionTo(targeted);
            targeted.Direction = targeted.GetDirectionTo(from);

            var speechLines = new[]
            {
                $"Sorry... {(targeted.Female ? "lady" : "sir")}!",
                BeggingLines.RandomElement(),
                "Pleeeease!"
            };

            foreach (var line in speechLines)
            {
                from.Animate(32, 5, 1, true, false, 0); // Bow
                from.PublicOverheadMessage(MessageType.Regular, from.SpeechHue, true, line);
                await Timer.Pause(BegDelayMs);

                if (!CheckRange(from, targeted))
                    return false;
            }

            return true;
        }
    }
}