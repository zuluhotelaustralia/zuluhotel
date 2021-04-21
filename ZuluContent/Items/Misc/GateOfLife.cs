using Scripts.Zulu.Utilities;

namespace Server.Items
{
    public class GateOfLife : Item
    {
        public override bool HandlesOnMovement => true;

        public override string DefaultName => "Gate of Life";

        public override bool OnMoveOver(Mobile mobile)
        {
            if (mobile.Player)
            {
                if (!mobile.Alive)
                    mobile.Resurrect();
                else
                    mobile.SendSuccessMessage("Your body begins to feel warm.");
            }

            return true;
        }
        
        [Constructible]
        public GateOfLife() : base(0x1FE7)
        {
            Movable = false;
        }
        
        [Constructible]
        public GateOfLife(Serial serial) : base(serial)
        {
        }

        public override void Serialize(IGenericWriter writer)
        {
            base.Serialize(writer);

            writer.Write(0); // version
        }

        public override void Deserialize(IGenericReader reader)
        {
            base.Deserialize(reader);

            int version = reader.ReadInt();
        }
    }
}