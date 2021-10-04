using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using Scripts.Zulu.Engines.Classes;
using Scripts.Zulu.Utilities;
using Server.Misc;
using Server.Targeting;
using Server.Network;
using Server.Mobiles;
using ZuluContent.Zulu.Engines.Magic;
using ZuluContent.Zulu.Skills;
using static Scripts.Zulu.Utilities.ZuluUtil;
namespace Server.SkillHandlers
{
    public class AnimalTaming : BaseSkillHandler
    {
        private const int MaxDistance = 20;
        private const int PrevTamedMinus = 20;
        private const int PointMultiplier = 15;
        private static readonly TimeSpan DelayBetweenSpeech = TimeSpan.FromSeconds(3.0);
        private static readonly TimeSpan UnresponsiveTime = TimeSpan.FromSeconds(300.0);
        private static readonly Dictionary<Serial, Serial> BeingTamed = new();

        private static readonly string[] SpeechLines = {
            "What a nice {0}",
            "I've always wanted a {0} like you.",
            "{0}, will you be my friend?"
        };

        public override SkillName Skill => SkillName.AnimalTaming;

        private static readonly TargetOptions TargetOptions = new()
        {
            Range = 12,
        };

        public override async Task<TimeSpan> OnUse(Mobile from)
        {
            var target = new AsyncTarget<BaseCreature>(from, TargetOptions);
            from.Target = target;
            from.RevealingAction();

            from.SendSuccessMessage(502789); // Tame which animal?

            var (creature, responseType) = await target;

            if (responseType != TargetResponseType.Success || creature == null)
                return Delay;
            
            if (!creature.Tamable)
                return FinishTaming(from, creature, "You can't tame that!");

            if (creature.Controlled)
                return FinishTaming(from, creature, "That creature looks pretty tame already.");

            var difficulty = GetCreatureDifficulty(creature);

            if (from.Skills[SkillName.AnimalTaming].Value < difficulty)
                return FinishTaming(from, creature, "You have no chance of taming this creature!");

            if (BeingTamed.ContainsKey(creature.Serial))
            {
                // Someone else is already taming this.
                return FinishTaming(from, creature, 502802);
            }

            difficulty += 10;

            var timesPreviouslyTamed = creature.Owners.Count;

            difficulty -= PrevTamedMinus * timesPreviouslyTamed;
            if (difficulty < 1)
                difficulty = 1;

            var calmingDifficulty = difficulty + 10;

            if (creature.UnresponsiveToTamingEndTime < Core.TickCount)
            {
                if (from.ShilCheckSkill(SkillName.AnimalLore, calmingDifficulty, 0) && creature.Warmode)
                    CalmBeast(creature, from);
                else if (creature.CreatureType == CreatureType.Dragonkin && AngerBeast(creature, from))
                    return FinishTaming(from, creature);
            }
            else
            {
                AngerBeast(creature, from);
                return FinishTaming(from, creature, "The creature is unresponsive to taming at this time.");
            }

            BeingTamed[creature.Serial] = from.Serial;

            foreach (var line in SpeechLines)
            {
                if (!from.InRange(creature, MaxDistance))
                    return FinishTaming(from, creature, "You are too far away to continue taming.");

                if (!from.CheckAlive())
                {
                    BeingTamed.Remove(creature.Serial);
                    return FinishTaming(from, creature, "You are dead and cannot continue taming.");
                }

                if (!from.CanSee(creature) || !from.InLOS(creature) || !CanPath(from, creature))
                    return FinishTaming(from, creature,
                        "You do not have a clear path to the animal you are taming, and must cease your attempt.");

                if (!creature.Tamable)
                    return FinishTaming(from, creature, "That creature cannot be tamed.");

                if (creature.Controlled)
                    return FinishTaming(from, creature, $"{creature.Name} belongs to someone else!");

                from.SendSuccessPublicOverHeadMessage(string.Format(line, TrimIndefiniteArticle(creature.Name)));
                await Timer.Pause(DelayBetweenSpeech);
            }
            
            if (Core.TickCount < creature.UnresponsiveToTamingEndTime)
                return FinishTaming(from, creature, "You failed to tame the creature.");

            creature.UnresponsiveToTamingEndTime = Core.TickCount;

            if (from.ShilCheckSkill(SkillName.AnimalTaming, difficulty, difficulty * PointMultiplier))
            {
                from.RevealingAction();
                from.SendSuccessMessage($"You successfully tame the {TrimIndefiniteArticle(creature.Name)}");

                // It seems to accept you as master.
                creature.PrivateOverheadMessage(MessageType.Regular, 0x3B2, 502799, from.NetState);
                creature.Owners.Add(from);
                creature.SetControlMaster(from);
                if (creature.Combatant != null || from.Combatant == creature)
                    PacifyBeast(creature, from);
            }
            else
            {
                from.SendFailureMessage("You failed to tame the creature.");
                var chance = 80 - (int) ((from.Skills[SkillName.AnimalTaming].Value - difficulty + 20) * 2);
                from.FireHook(h => h.OnAnimalTaming(from, creature, ref chance));

                if (chance < 1)
                    chance = 1;

                if (Utility.Random(100) <= chance)
                {
                    creature.UnresponsiveToTamingEndTime = Core.TickCount + (int) UnresponsiveTime.TotalMilliseconds;
                    return FinishTaming(from, creature, "And have made the creature unresponsive to taming.");
                }
            }
            
            return FinishTaming(from, creature);
        }

        private static TimeSpan FinishTaming(Mobile from, BaseCreature targeted, TextDefinition message = null)
        {
            if(BeingTamed.ContainsKey(targeted.Serial))
                BeingTamed.Remove(targeted.Serial);
            
            if(message != null)
                from.SendFailureMessage(message);
            
            return Delay;
        }

        private static int GetCreatureDifficulty(BaseCreature creature)
        {
            var difficulty = (int) creature.MinTameSkill;

            if (difficulty <= 0)
                difficulty = creature.GetCreatureScore();

            return difficulty;
        }

        private static bool CanPath(IEntity from, Mobile targeted)
        {
            if (targeted.InRange(from, 1))
                return true;

            var path = new MovementPath(targeted, from.Location);
            return path.Success;
        }

        private static bool AngerBeast(BaseCreature creature, Mobile from)
        {
            var chance = 75;
            from.FireHook(h => h.OnAnimalTaming(from, creature, ref chance));

            if (Utility.Random(100) < chance)
            {
                creature.PublicOverheadMessage(MessageType.Regular, 0x3B2, true,
                    $"{from.Name} has angered the beast!");
                creature.PlaySound(creature.GetAngerSound());
                creature.Direction = creature.GetDirectionTo(from);
                creature.BardEndTime = DateTime.Now;
                creature.BardPacified = false;
                creature.DoHarmful(from, true);
                return true;
            }

            return false;
        }

        private static void CalmBeast(BaseCreature creature, Mobile from)
        {
            PacifyBeast(creature, from);
            creature.PublicOverheadMessage(MessageType.Regular, 0x3B2, true,
                $"{from.Name} has calmed the beast!");
        }

        private static void PacifyBeast(BaseCreature creature, Mobile from)
        {
            creature.Combatant = null;
            creature.Warmode = false;
            from.Combatant = null;
            creature.Pacify(from, DateTime.Now + TimeSpan.FromSeconds(1.0));
        }
    }
}