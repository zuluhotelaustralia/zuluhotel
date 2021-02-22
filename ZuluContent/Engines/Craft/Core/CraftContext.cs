using System.Collections.Generic;

namespace Server.Engines.Craft
{
    public enum CraftMarkOption
    {
        MarkItem,
        DoNotMark,
        PromptForMark
    }

    public class CraftContext
    {
        private bool m_DoNotColor;

        public List<CraftItem> Items { get; }

        public int LastResourceIndex { get; set; }

        public int LastResourceIndex2 { get; set; }

        public int LastGroupIndex { get; set; }

        public int CraftNumber { get; set; }

        public bool DoNotColor
        {
            get
            {
                return false; //m_DoNotColor;
            }
            set { m_DoNotColor = value; }
        }

        public CraftMarkOption MarkOption { get; set; }

        public CraftContext()
        {
            Items = new List<CraftItem>();
            LastResourceIndex = -1;
            LastResourceIndex2 = -1;
            LastGroupIndex = -1;
            CraftNumber = 1;
        }

        public CraftItem LastMade
        {
            get
            {
                if (Items.Count > 0)
                    return Items[0];

                return null;
            }
        }

        public void OnMade(CraftItem item)
        {
            Items.Remove(item);

            if (Items.Count == 10)
                Items.RemoveAt(9);

            Items.Insert(0, item);
        }
    }
}