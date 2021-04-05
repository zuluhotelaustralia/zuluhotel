using System;
using System.Collections;
using Scripts.Zulu.Utilities;
using Server.Targeting;
using Server.Items;

namespace Server.SkillHandlers
{
    public class Inscribe
    {
        private static readonly long TargetTimeout = (int) TimeSpan.FromMinutes(1.0).TotalMilliseconds;
        
        public static void Initialize()
        {
            SkillInfo.Table[(int) SkillName.Inscribe].Callback = OnUse;
        }

        public static TimeSpan OnUse(Mobile m)
        {
            Target target = new InternalTargetSrc();
            m.Target = target;
            m.SendAsciiMessage("What would you like to inscribe?");
            target.BeginTimeout(m, TargetTimeout);

            return TimeSpan.FromSeconds(1.0);
        }

        private static Hashtable m_UseTable = new Hashtable();

        private static void SetUser(BaseBook book, Mobile mob)
        {
            m_UseTable[book] = mob;
        }

        private static void CancelUser(BaseBook book)
        {
            m_UseTable.Remove(book);
        }

        public static Mobile GetUser(BaseBook book)
        {
            return (Mobile) m_UseTable[book];
        }

        public static bool IsEmpty(BaseBook book)
        {
            foreach (BookPageInfo page in book.Pages)
            {
                foreach (string line in page.Lines)
                {
                    if (line.Trim().Length != 0)
                        return false;
                }
            }

            return true;
        }

        public static void Copy(BaseBook bookSrc, BaseBook bookDst)
        {
            bookDst.Title = bookSrc.Title;
            bookDst.Author = bookSrc.Author;

            BookPageInfo[] pagesSrc = bookSrc.Pages;
            BookPageInfo[] pagesDst = bookDst.Pages;
            for (int i = 0; i < pagesSrc.Length && i < pagesDst.Length; i++)
            {
                BookPageInfo pageSrc = pagesSrc[i];
                BookPageInfo pageDst = pagesDst[i];

                int length = pageSrc.Lines.Length;
                pageDst.Lines = new string[length];

                for (int j = 0; j < length; j++)
                    pageDst.Lines[j] = pageSrc.Lines[j];
            }
        }

        private class InternalTargetSrc : Target
        {
            public InternalTargetSrc() : base(3, false, TargetFlags.None)
            {
            }

            protected override void OnTarget(Mobile from, object targeted)
            {
                if (targeted is BaseBook book)
                {
                    if (IsEmpty(book))
                        from.SendLocalizedMessage(501611); // Can't copy an empty book.
                    else if (GetUser(book) != null)
                        from.SendLocalizedMessage(501621); // Someone else is inscribing that item.
                    else
                    {
                        Target target = new InternalTargetBookDst(book);
                        from.Target = target;
                        from.SendLocalizedMessage(501612); // Select a book to copy this to.
                        target.BeginTimeout(from, 60000);
                        SetUser(book, from);
                    }
                }
                else if (targeted is CustomSpellScroll customScroll)
                {
                    Target target = new InternalTargetScrollDst(customScroll);
                    from.Target = target;
                    from.SendAsciiMessage("Select a book to inscribe this to.");
                    target.BeginTimeout(from, 60000);
                }
                else
                {
                    from.SendFailureMessage("That is not a valid selection.");
                }
            }

            protected override void OnTargetCancel(Mobile from, TargetCancelType cancelType)
            {
                if (cancelType == TargetCancelType.Timeout)
                    from.SendLocalizedMessage(
                        501619); // You have waited too long to make your inscribe selection, your inscription attempt has timed out.
            }
        }

        private class InternalTargetBookDst : Target
        {
            private BaseBook m_BookSrc;

            public InternalTargetBookDst(BaseBook bookSrc) : base(3, false, TargetFlags.None)
            {
                m_BookSrc = bookSrc;
            }

            protected override void OnTarget(Mobile from, object targeted)
            {
                if (m_BookSrc.Deleted)
                    return;

                BaseBook bookDst = targeted as BaseBook;

                if (bookDst == null)
                    from.SendLocalizedMessage(1046296); // That is not a book
                else if (IsEmpty(m_BookSrc))
                    from.SendLocalizedMessage(501611); // Can't copy an empty book.
                else if (bookDst == m_BookSrc)
                    from.SendLocalizedMessage(501616); // Cannot copy a book onto itself.
                else if (!bookDst.Writable)
                    from.SendLocalizedMessage(501614); // Cannot write into that book.
                else if (GetUser(bookDst) != null)
                    from.SendLocalizedMessage(501621); // Someone else is inscribing that item.
                else
                {
                    if (from.CheckTargetSkill(SkillName.Inscribe, bookDst, 0, 50))
                    {
                        Copy(m_BookSrc, bookDst);

                        from.SendLocalizedMessage(501618); // You make a copy of the book.
                        from.PlaySound(0x249);
                    }
                    else
                    {
                        from.SendLocalizedMessage(501617); // You fail to make a copy of the book.
                    }
                }
            }

            protected override void OnTargetCancel(Mobile from, TargetCancelType cancelType)
            {
                if (cancelType == TargetCancelType.Timeout)
                    from.SendLocalizedMessage(
                        501619); // You have waited too long to make your inscribe selection, your inscription attempt has timed out.
            }

            protected override void OnTargetFinish(Mobile from)
            {
                CancelUser(m_BookSrc);
            }
        }

        private class InternalTargetScrollDst : Target
        {
            private CustomSpellScroll m_ScrollSrc;

            public InternalTargetScrollDst(CustomSpellScroll scrollSrc) : base(3, false, TargetFlags.None)
            {
                m_ScrollSrc = scrollSrc;
            }

            protected override void OnTarget(Mobile from, object targeted)
            {
                if (m_ScrollSrc.Deleted)
                    return;

                if (targeted is CustomSpellbook customBook)
                {
                    if (!customBook.CanAddEntry(from, m_ScrollSrc))
                        return;

                    if (from.CheckTargetSkill(SkillName.Inscribe, customBook, 100, 150))
                    {
                        customBook.AddEntry(m_ScrollSrc);
                        from.SendSuccessMessage("You have successfully inscribed that spell.");
                        from.PlaySound(0x249);
                    }
                    else
                    {
                        m_ScrollSrc.Delete();
                        from.SendFailureMessage("You fail to inscribe that scroll.");
                    }
                }
                else
                {
                    from.SendFailureMessage("You cannot inscribe to that.");
                }
            }

            protected override void OnTargetCancel(Mobile from, TargetCancelType cancelType)
            {
                if (cancelType == TargetCancelType.Timeout)
                    from.SendLocalizedMessage(
                        501619); // You have waited too long to make your inscribe selection, your inscription attempt has timed out.
            }
        }
    }
}