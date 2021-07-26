using System;
using Scripts.Zulu.Utilities;

namespace Server.Items
{
    public class SpiderWeb : Item
    {
        public override bool HandlesOnMovement => true;

        public override bool Decays => true;

        public override string DefaultName => "Sticky Web";

        public override bool OnMoveOver(Mobile mobile)
        {
            Stick(mobile);

            return true;
        }

        public static void Stick(Mobile mobile)
        {
            mobile.Paralyze(TimeSpan.FromSeconds(10));
            mobile.SendFailureMessage("You are trapped in a spider web!");
        }
        
        public SpiderWeb(Point3D loc, Map map, TimeSpan duration) : base(0xEE4)
        {
            Movable = false;
            MoveToWorld(loc, map);

            Timer.DelayCall(duration, Delete);
        }
        
        public SpiderWeb(Serial serial) : base(serial)
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

            Delete();
        }
    }
}