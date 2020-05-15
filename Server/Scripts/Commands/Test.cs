using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using Server;
using Server.Items;
using Server.Network;
using Server.Targets;
using Server.Targeting;
using CPA = Server.CommandPropertyAttribute;

namespace Server.Commands
{
    public class Test
    {
        public static void Initialize()
        {
            CommandSystem.Register("testbook", AccessLevel.GameMaster, new CommandEventHandler(TestBook_OnCommand));
        }

        [Usage("testbook")]
        [Description("random test code")]
        public static void TestBook_OnCommand(CommandEventArgs e)
        {
            Mobile who = e.Mobile;

            Item pen = who.Backpack.FindItemByType(typeof(PenAndInk), true);

            if (pen == null)
            {
                who.SendMessage("I cannot write a book without ink and a pen!");
                return;
            }

            e.Mobile.Target = new SimpleTarget(-1, false, TargetFlags.None, (Mobile from, object target) =>
            {
                if (!(target is BaseBook))
                {
                    from.SendMessage("Target a book.");
                    return;
                }

                BaseBook book = (BaseBook)target;

                if (!book.Writable)
                {
                    from.SendMessage("I cannot write to that!");
                    return;
                }

                from.PlaySound(0x249);
                from.Emote("*scribble scribble*");

                new SimpleTimeout(new TimeSpan(0, 0, 5), () =>
                {
                    if (!pen.IsAccessibleTo(from))
                    {
                        from.SendMessage("Where'd my pen go?!");
                        return;
                    }

                    if (!book.IsAccessibleTo(from))
                    {
                        from.SendMessage("Where'd my book go?!");
                        return;
                    }

                    book.Author = from.Name;
                    book.Title = "Resources Field Report";

                    book.Pages[0].Lines = new string[] {
                "~~ Iron ~~",
                "Quantity: 20%"
                };
                    book.Pages[1].Lines = new string[] {
                "~~ Gold ~~",
                "Quantity: 11%"
                };
                    book.Pages[2].Lines = new string[] {
                "~~ Unknown ~~",
                "I detect some other",
                "materials that I cannot",
                "quite identify."
                };

                    from.SendMessage("Perfect.");
                }).Start();
            });
        }
    }
}
