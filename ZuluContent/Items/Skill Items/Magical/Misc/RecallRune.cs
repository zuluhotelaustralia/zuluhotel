using System;
using Server.Prompts;
using Server.Multis;
using Server.Regions;

namespace Server.Items
{
    [FlipableAttribute(0x1f14, 0x1f15, 0x1f16, 0x1f17)]
    public class RecallRune : Item
    {
        private const string RuneFormat = "a recall rune for {0}";

        [CommandProperty(AccessLevel.Counselor, AccessLevel.GameMaster)]
        public BaseHouse House => BaseHouse.FindHouseAt(Target, TargetMap);

        [CommandProperty(AccessLevel.Counselor, AccessLevel.GameMaster)]
        public string Description { get; set; }

        [CommandProperty(AccessLevel.Counselor, AccessLevel.GameMaster)]
        public bool Marked { get; set; }

        [CommandProperty(AccessLevel.Counselor, AccessLevel.GameMaster)]
        public Point3D Target { get; set; }

        [CommandProperty(AccessLevel.Counselor, AccessLevel.GameMaster)]
        public Map TargetMap { get; set; }
        
        
        [Constructible]
        public RecallRune() : base(0x1F14)
        {
            Weight = 1.0;
            CalculateHue();
        }

        [Constructible]
        public RecallRune(Serial serial) : base(serial)
        {
        }

        private void CalculateHue()
        {
            if (!Marked)
                Hue = 0;
            else if (TargetMap == Map.Felucca)
                Hue = House != null ? 0x66D : 0;
        }

        public void Mark(Mobile m)
        {
            Marked = true;
            Target = m.Location;
            TargetMap = m.Map;
            Description = BaseRegion.GetRuneNameFor(Region.Find(Target, TargetMap));

            CalculateHue();
        }


        public override void OnSingleClick(Mobile from)
        {
            if (Marked)
            {
                string desc;
                if ((desc = Description) == null || (desc = desc.Trim()).Length == 0)
                    desc = "an unknown location";

                LabelTo(from, $"a recall rune for {desc}{(House != null ? " (House)" : "")}");
            }
            else
            {
                LabelTo(from, "an unmarked recall rune");
            }
        }

        public override void OnDoubleClick(Mobile from)
        {
            int number;

            if (!IsChildOf(from.Backpack))
            {
                number = 1042001; // That must be in your pack for you to use it.
            }
            else if (Marked)
            {
                number = 501804; // Please enter a description for this marked object.
                from.Prompt = new RenamePrompt(this);
            }
            else
            {
                number = 501805; // That rune is not yet marked.
            }

            from.SendLocalizedMessage(number);
        }

        private class RenamePrompt : Prompt
        {
            private readonly RecallRune m_Rune;
            public RenamePrompt(RecallRune rune) => m_Rune = rune;

            public override void OnResponse(Mobile from, string text)
            {
                m_Rune.Description = text.Length > 50 ? text.Substring(0, 50) : text;
                from.SendLocalizedMessage(1010474); // The etching on the rune has been changed.
            }
        }
        
        
        public override void Serialize(IGenericWriter writer)
        {
            base.Serialize(writer);

            writer.Write(0); // version
            writer.Write(Description);
            writer.Write(Marked);
            writer.Write(Target);
            writer.Write(TargetMap);
        }

        public override void Deserialize(IGenericReader reader)
        {
            base.Deserialize(reader);
            switch (reader.ReadInt())
            {
                case 0:
                {
                    Description = reader.ReadString();
                    Marked = reader.ReadBool();
                    Target = reader.ReadPoint3D();
                    TargetMap = reader.ReadMap();

                    CalculateHue();

                    break;
                }
            }
        }
    }
}