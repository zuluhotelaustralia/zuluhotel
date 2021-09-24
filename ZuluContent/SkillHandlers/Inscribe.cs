using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Scripts.Zulu.Engines.Classes;
using Scripts.Zulu.Utilities;
using Server.Targeting;
using Server.Items;
using ZuluContent.Zulu.Skills;

namespace Server.SkillHandlers
{
    public class Inscribe : BaseSkillHandler
    {
        public override SkillName Skill => SkillName.Inscribe;

        private static readonly TargetOptions TargetOptions = new()
        {
            Range = 3,
            CheckLos = true
        };

        private static readonly int PointMultiplier = 15;

        private static readonly Dictionary<Serial, Serial> Users = new();

        private static void SetUser(Serial itemSerial, Mobile mob)
        {
            Users[itemSerial] = mob.Serial;
        }

        private static void CancelUser(Serial itemSerial)
        {
            Users.Remove(itemSerial);
        }

        private static bool GetUser(Serial itemSerial)
        {
            return Users.ContainsKey(itemSerial);
        }

        public override async Task<TimeSpan> OnUse(Mobile from)
        {
            var target = new TimeoutAsyncTarget(from, TargetOptions);
            from.Target = target;

            from.SendSuccessMessage(501620); // What would you like to inscribe?

            var (targeted, responseType) = await target;

            if (responseType != TargetResponseType.Success)
                return Delay;

            if (targeted is ISpellbook spellbook)
            {
                if (GetUser(spellbook.Serial))
                {
                    from.SendFailureMessage(501621); // Someone else is inscribing that item.
                    return Delay;
                }

                var emptySpellbookTarget = new TimeoutAsyncTarget(from, TargetOptions);
                from.Target = emptySpellbookTarget;

                SetUser(spellbook.Serial, from);

                from.SendSuccessMessage($"Select an empty {spellbook.Name} to copy to.");

                var (targetedEmptySpellbook, responseTypeEmptySpellbook) = await emptySpellbookTarget;

                CancelUser(spellbook.Serial);

                if (responseTypeEmptySpellbook != TargetResponseType.Success)
                    return Delay;

                if (GetUser(targetedEmptySpellbook.Serial))
                {
                    from.SendFailureMessage(501621); // Someone else is inscribing that item.
                    return Delay;
                }

                if (targetedEmptySpellbook is not ISpellbook emptySpellBook ||
                    emptySpellBook.BookOffset != spellbook.BookOffset)
                {
                    from.SendFailureMessage($"You must target a {spellbook.Name}.");
                    return Delay;
                }

                if (emptySpellBook.Content != 0)
                {
                    from.SendFailureMessage($"You must target an empty {spellbook.Name}.");
                    return Delay;
                }

                SetUser(emptySpellBook.Serial, from);

                var difficulty = spellbook switch
                {
                    Spellbook => 110.0,
                    Earthbook => 150.0,
                    Codex => 160.0,
                    _ => 0.0
                };
                var manaCost = difficulty;
                manaCost /= from.GetClassModifier(Skill);

                if (from.Mana < manaCost)
                {
                    from.SendFailureMessage("You don't have enough mana.");
                    from.Mana = 0;
                    CancelUser(emptySpellBook.Serial);
                    return Delay;
                }

                from.Mana -= (int)manaCost;

                if (!from.ShilCheckSkill(Skill, (int)difficulty, (int)(difficulty * PointMultiplier)))
                {
                    from.SendFailureMessage($"You fail to copy the {spellbook.Name}.");
                    CancelUser(emptySpellBook.Serial);
                    targetedEmptySpellbook.Delete();
                    return Delay;
                }

                from.PlaySound(0x249);
                emptySpellBook.Content = spellbook.Content;
                from.SendSuccessMessage($"You copied the {spellbook.Name} successfully.");
                CancelUser(emptySpellBook.Serial);
            }
            else if (targeted is CustomSpellScroll customScroll)
            {
                var spellbookTarget = new TimeoutAsyncTarget(from, TargetOptions);
                from.Target = spellbookTarget;

                SetUser(customScroll.Serial, from);

                from.SendSuccessMessage("Select a book to inscribe this to.");

                var (targetedSpellbook, responseTypeSpellbook) = await spellbookTarget;

                CancelUser(customScroll.Serial);

                if (responseTypeSpellbook != TargetResponseType.Success)
                    return Delay;

                if (customScroll.Deleted)
                    return Delay;

                if (targetedSpellbook is CustomSpellbook customBook)
                {
                    if (!customBook.CanAddEntry(from, customScroll))
                        return Delay;

                    var scrollData =
                        ZhConfig.Crafting.Inscription.CraftEntries.First(e => e.ItemType == customScroll.GetType());
                    var difficulty = scrollData.Skill2 ?? 80.0;

                    if (from.ShilCheckSkill(Skill, (int)difficulty, (int)(difficulty * PointMultiplier + 5)))
                    {
                        customBook.AddEntry(customScroll);
                        from.SendSuccessMessage($"You inscribe the spell into your {customBook.Name}.");
                        from.PlaySound(0x249);
                    }
                    else
                    {
                        customScroll.Consume();
                        from.SendFailureMessage("You fail, and destroy the scroll.");
                    }
                }
                else
                {
                    from.SendFailureMessage("You cannot inscribe to that.");
                }
            }
            else
            {
                from.SendFailureMessage(501624); // Can't inscribe that item.
            }

            return Delay;
        }

        private class TimeoutAsyncTarget : AsyncTarget<Item>
        {
            public TimeoutAsyncTarget(Mobile from, TargetOptions opts) : base(from, opts)
            {
            }

            protected override void OnTargetCancel(Mobile from, TargetCancelType cancelType)
            {
                if (cancelType == TargetCancelType.Timeout)
                    from.SendFailureMessage(
                        501619); // You have waited too long to make your inscribe selection, your inscription attempt has timed out.
            }
        }
    }
}