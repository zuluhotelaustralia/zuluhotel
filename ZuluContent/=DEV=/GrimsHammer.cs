using Server.Engines.Craft;
using Server.Network;
using Server.Menus.ItemLists;

namespace Server.Items
{
    [FlipableAttribute(0x13B3, 0x13B4)]
    public class GrimsHammer : BaseTool
    {
        public override CraftSystem CraftSystem
        {
            get { return DefBlacksmithy.CraftSystem; }
        }


        [Constructible]
        public GrimsHammer() : base(0x13B3)
        {
            Hue = 1283;
            Layer = Layer.OneHanded;
        }


        [Constructible]
        public GrimsHammer(Serial serial) : base(serial)
        {
        }

        public override void OnSingleClick(Mobile from)
        {
            from.NetState.SendMessage(Serial, ItemID, MessageType.Label, 0, 3, true, null, "", "Crafting Menu [Test]");
        }

        public override void OnDoubleClick(Mobile from)
        {
            from.SendMenu(new BlacksmithMenuTest(from, BlacksmithMenuTest.Main(from), "Main"));
        }

        public override void Serialize(IGenericWriter writer)
        {
            base.Serialize(writer);
            writer.Write((int) 0); // version
        }

        public override void Deserialize(IGenericReader reader)
        {
            base.Deserialize(reader);
            int version = reader.ReadInt();
        }
    }
}