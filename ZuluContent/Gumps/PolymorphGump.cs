using System;
using System.Runtime.CompilerServices;
using Server.Network;

namespace Server.Gumps
{
    public record PolymorphCategory
    {
        public PolymorphCategory(int num, params PolymorphEntry[] entries)
        {
            LocNumber = num;
            Entries = entries;
        }

        public PolymorphEntry[] Entries { get; }

        public int LocNumber { get; }
    }
    public record PolymorphEntry
    {
        public static readonly PolymorphEntry Bird = new(8430, 6, 1072458, 17, 10);
        public static readonly PolymorphEntry Slime = new(8424, 51, 1015246, 5, 10);
        public static readonly PolymorphEntry Eagle = new(8434, 5, 1072461, 5, 10);
        public static readonly PolymorphEntry Mongbat = new(8441, 39, 1072441, 5, 10);
        public static readonly PolymorphEntry Headless = new(8458, 31, 1018124, 5, 10);
        public static readonly PolymorphEntry Gorilla = new(8437, 29, 1015240, 23, 10);
        public static readonly PolymorphEntry Ratman = new(8419, 42, 1072421, 23, 10);
        public static readonly PolymorphEntry GiantSpider = new(8445, 28, 1072424, 23, 10);
        public static readonly PolymorphEntry Scorpion = new(8420, 48, 1018140, 23, 10);
        public static readonly PolymorphEntry Orc = new(8416, 17, 1015247, 29, 10);

        public static readonly PolymorphEntry LizardMan = new(8414, 33, 1015248, 26, 10);
        public static readonly PolymorphEntry Ghoul = new(8457, 26, 1018245, 23, 10);
        public static readonly PolymorphEntry Harpy = new(8412, 30, 1018123, 23, 10);
        public static readonly PolymorphEntry Ettin = new(8408, 2, 1015252, 25, 8);
        public static readonly PolymorphEntry Corpser = new(8402, 8, 1018101, 23, 10);
        public static readonly PolymorphEntry Gazer = new(8436, 22, 1018123, 23, 10);
        public static readonly PolymorphEntry EarthElemental = new(8407, 14, 1018107, 23, 10);
        public static readonly PolymorphEntry WaterElemental = new(8459, 16, 1018109, 23, 10);
        public static readonly PolymorphEntry FireElemental = new(8435, 15, 1018108, 23, 10);
        public static readonly PolymorphEntry AirElemental = new(8429, 13, 1018106, 23, 10);
        public static readonly PolymorphEntry Ogre = new(8415, 1, 1015250, 24, 9);
        public static readonly PolymorphEntry Gargoyle = new(8409, 4, 1015249, 22, 10);
        public static readonly PolymorphEntry Liche = new(8440, 24, 1028440, 23, 10);
        public static readonly PolymorphEntry Daemon = new(8403, 9, 1015253, 25, 8);
        public static readonly PolymorphEntry Dragon = new(8406, 12, 1028440, 23, 10);
        public static readonly PolymorphEntry Wisp = new(8448, 58, 1028440, 23, 10);

        
        public static readonly PolymorphEntry Chicken = new(8401, 208, 1015236, 15, 10);
        public static readonly PolymorphEntry Dog = new(8405, 217, 1015237, 17, 10);
        public static readonly PolymorphEntry Wolf = new(8426, 225, 1015238, 18, 10);
        public static readonly PolymorphEntry Panther = new(8473, 214, 1015239, 20, 14);
        public static readonly PolymorphEntry BlackBear = new(8399, 211, 1015241, 22, 10);
        public static readonly PolymorphEntry GrizzlyBear = new(8411, 212, 1015242, 22, 12);
        public static readonly PolymorphEntry PolarBear = new(8417, 213, 1015243, 26, 10);
        public static readonly PolymorphEntry Troll = new(8425, 54, 1015251, 25, 9);
        
        
        public static readonly PolymorphEntry HumanMale = new(8397, 400, 1015244, 29, 8);
        public static readonly PolymorphEntry HumanFemale = new(8398, 401, 1015254, 29, 10);
        
        // Missing ArtId, Cliloc, x, y
        public static readonly PolymorphEntry Zombie = new(0, 3, 0, 0, 0);
        public static readonly PolymorphEntry DaemonWithSword = new(0, 10, 0, 0, 0);
        public static readonly PolymorphEntry GiantSerpent = new(1, 21, 0, 0, 0);
        public static readonly PolymorphEntry SeaSerpent = new(1, 150, 0, 0, 0);
        public static readonly PolymorphEntry Ent = new(1, 47, 0, 0, 0);



        private PolymorphEntry(int art, int body, int locNum, int x, int y)
        {
            ArtId = art;
            BodyId = body;
            LocNumber = locNum;
            X = x;
            Y = y;
        }

        public int ArtId { get; }
        public int BodyId { get; }
        public int LocNumber { get; }
        public int X { get; }
        public int Y { get; }
    }
    
    public class PolymorphGump : Gump, INotifyCompletion
    {
        public PolymorphCategory[] Categories { get; }
        private readonly Mobile m_Caster;
        protected int BodyId = -1;

        public PolymorphGump(Mobile caster, PolymorphCategory[] categories) : base(50, 50)
        {
            Categories = categories;
            m_Caster = caster;

            int x, y;
            AddPage(0);
            AddBackground(0, 0, 585, 393, 5054);
            AddBackground(195, 36, 387, 275, 3000);
            AddHtmlLocalized(0, 0, 510, 18, 1015234); // <center>Polymorph Selection Menu</center>
            AddHtmlLocalized(60, 355, 150, 18, 1011036); // OKAY
            AddButton(25, 355, 4005, 4007, 1, GumpButtonType.Reply, 1);
            AddHtmlLocalized(320, 355, 150, 18, 1011012); // CANCEL
            AddButton(285, 355, 4005, 4007, 0, GumpButtonType.Reply, 2);

            y = 35;
            for (var i = 0; i < Categories.Length; i++)
            {
                var cat = Categories[i];
                AddHtmlLocalized(5, y, 150, 25, cat.LocNumber, true);
                AddButton(155, y, 4005, 4007, 0, GumpButtonType.Page, i + 1);
                y += 25;
            }

            for (var i = 0; i < Categories.Length; i++)
            {
                var cat = Categories[i];
                AddPage(i + 1);

                for (var c = 0; c < cat.Entries.Length; c++)
                {
                    var entry = cat.Entries[c];
                    x = 198 + c % 3 * 129;
                    y = 38 + c / 3 * 67;

                    AddHtmlLocalized(x, y, 100, 18, entry.LocNumber);
                    AddItem(x + 20, y + 25, entry.ArtId);
                    AddRadio(x, y + 20, 210, 211, false, (c << 8) + i);
                }
            }
        }

        public override void OnResponse(NetState state, RelayInfo info)
        {
            if (info.ButtonID == 1 && info.Switches.Length > 0)
            {
                var cnum = info.Switches[0];
                var cat = cnum % 256;
                var ent = cnum >> 8;

                if (cat >= 0 && cat < Categories.Length)
                {
                    if (ent >= 0 && ent < Categories[cat].Entries.Length)
                    {
                        BodyId = Categories[cat].Entries[ent].BodyId;
                        IsCompleted = true;
                        Continuation?.Invoke();
                    }
                }
            }
        }

        #region INotifyCompletion
        
        protected Action Continuation;
        public bool IsCompleted { get; private set; }
        public virtual PolymorphGump GetAwaiter() => this;
        public int GetResult() => BodyId;
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