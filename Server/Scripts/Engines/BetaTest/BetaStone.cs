using System;
using Server;

namespace Server.Items
{
    public class InviteStone : Item
    {

        [Constructable]
        public InviteStone() : base(0xF8B)
        {
            this.Name = "A fiery moonstone";
            this.Hue = 2747; //lavarock 
        }

        public InviteStone(Serial serial) : base(serial)
        {
            this.Name = "A fiery moonstone";
            this.Hue = 2747;
        }

        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);
        }

        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);
        }

        public override void OnDoubleClick(Mobile from)
        {
            from.SendMessage("Whatever magic this stone once held has faded, and it does not respond to you.");
        }
    }
}
