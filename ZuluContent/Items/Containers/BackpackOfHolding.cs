namespace Server.Items
{
   
    public class BackpackOfHolding : Backpack
    {
        public override int Hue { get; set; } = 1282;

        [Constructible]
        public BackpackOfHolding() : base(0xE75)
        {
            Weight = 0.0;
            MaxItems = 0;
            LootType = LootType.Newbied;
        }

        public BackpackOfHolding(Serial serial) : base(serial)
        {
        }

        public override bool CheckHold(Mobile from, Item item, bool message, bool checkItems, int plusItems, int plusWeight)
        {
            return from.AccessLevel != AccessLevel.Player && base.CheckHold(from, item, message, checkItems, plusItems, plusWeight);
        }

        public override bool IsAccessibleTo(Mobile from)
        {
            return from.AccessLevel != AccessLevel.Player && base.IsAccessibleTo(from);
        }

        public override bool CheckContentDisplay(Mobile from)
        {
            return from.AccessLevel != AccessLevel.Player && base.CheckContentDisplay(from);
        }

        public override bool OnDroppedInto(Mobile from, Container target, Point3D p)
        {
            return from.AccessLevel != AccessLevel.Player && base.OnDroppedInto(@from, target, p);
        }

        public override bool OnDragLift(Mobile from)
        {
            if (from.AccessLevel > AccessLevel.Player)
                return true;

            from.SendLocalizedMessage(500169); // You cannot pick that up.
            return false;
        }

        public override void Serialize(IGenericWriter writer)
        {
            base.Serialize(writer);

            writer.Write((int) 1); // version
        }

        public override void Deserialize(IGenericReader reader)
        {
            base.Deserialize(reader);
            int version = reader.ReadInt();
        }
    }

}