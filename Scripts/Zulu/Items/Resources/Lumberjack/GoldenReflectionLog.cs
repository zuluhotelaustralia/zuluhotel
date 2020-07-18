// Generated File. DO NOT MODIFY BY HAND.
namespace Server.Items
{

    public class GoldenReflectionLog : Log
    {
        [Constructable]
        public GoldenReflectionLog() : this(1) { }

        [Constructable]
        public GoldenReflectionLog(int amount) : base(CraftResource.GoldenReflection, amount)
        {
            this.Hue = 48;
        }

        public GoldenReflectionLog(Serial serial) : base(serial) { }

        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);
            writer.Write((int)0); // version
        }

        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);
            int version = reader.ReadInt();
        }

        public override bool Axe(Mobile from, BaseAxe axe)
        {
            if (!TryCreateBoards(from, 59, new GoldenReflectionBoard()))
            {
                return false;
            }
            return true;
        }
    }
}
