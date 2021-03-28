using System;
using System.Runtime.CompilerServices;
using Server.Network;
using static Server.Gumps.PolymorphEntry;
namespace Server.Gumps
{
    public class NewPolymorphGump : Gump, INotifyCompletion
    {
        private static readonly PolymorphEntry[] m_Entries =
        {
            Bird,
            Rabbit,
            Eagle,
            Cat,
            Dog,
            Wolf,
            Deer,
            Panther,
            BlackBear,
            GrizzlyBear,
            PolarBear,
            GiantSerpent,
            EarthElemental,
            FireElemental,
            WaterElemental,
            AirElemental,
            Dragon,
            Reaper,
            Wisp
        };

        private readonly Mobile m_Caster;

        public NewPolymorphGump(Mobile caster) : base(0, 0)
        {
            m_Caster = caster;

            AddPage(0);

            AddBackground(0, 0, 520, 404, 0x13BE);
            AddImageTiled(10, 10, 500, 20, 0xA40);
            AddImageTiled(10, 40, 500, 324, 0xA40);
            AddImageTiled(10, 374, 500, 20, 0xA40);
            AddAlphaRegion(10, 10, 500, 384);

            AddHtmlLocalized(14, 12, 500, 20, 1015234, 0x7FFF); // <center>Polymorph Selection Menu</center>

            AddButton(10, 374, 0xFB1, 0xFB2, 0);
            AddHtmlLocalized(45, 376, 450, 20, 1060051, 0x7FFF); // CANCEL

            for (var i = 0; i < m_Entries.Length; i++)
            {
                var entry = m_Entries[i];

                var page = i / 10 + 1;
                var pos = i % 10;

                if (pos == 0)
                {
                    if (page > 1)
                    {
                        AddButton(400, 374, 0xFA5, 0xFA7, 0, GumpButtonType.Page, page);
                        AddHtmlLocalized(440, 376, 60, 20, 1043353, 0x7FFF); // Next
                    }

                    AddPage(page);

                    if (page > 1)
                    {
                        AddButton(300, 374, 0xFAE, 0xFB0, 0, GumpButtonType.Page, 1);
                        AddHtmlLocalized(340, 376, 60, 20, 1011393, 0x7FFF); // Back
                    }
                }

                var x = pos % 2 == 0 ? 14 : 264;
                var y = pos / 2 * 64 + 44;

                AddImageTiledButton(x, y, 0x918, 0x919, i + 1, GumpButtonType.Reply, 0, entry.ArtId, 0x0, entry.X,
                    entry.Y);
                AddHtmlLocalized(x + 84, y, 250, 60, entry.LocNumber, 0x7FFF);
            }
        }

        public override void OnResponse(NetState sender, RelayInfo info)
        {
            var idx = info.ButtonID - 1;

            if (idx >= 0 && idx < m_Entries.Length) 
                BodyId = m_Entries[idx].BodyId;

            IsCompleted = true;
            Continuation?.Invoke();

            // Spell spell = new PolymorphSpell(m_Caster, m_Scroll, m_Entries[idx].BodyId);
            // spell.Cast();
        }
        
        #region INotifyCompletion
        
        protected Action Continuation;
        public bool IsCompleted { get; private set; }
        public virtual NewPolymorphGump GetAwaiter() => this;
        public int GetResult() => BodyId;
        public int BodyId { get; private set; } = -1;

        public void OnCompleted(Action continuation)
        {
            if (!IsCompleted && BodyId == -1)
            {
                Continuation = continuation;
                m_Caster?.SendGump(this);
            }
            else
            {
                continuation();
            }
        }
        
        #endregion
    }
}