using System;
using Server;
using Server.Items;

namespace Server.Items
{
    public class BagOfNecroReagents : Bag
    {

        [Constructible]
public BagOfNecroReagents() : this(50)
        {
        }


        [Constructible]
public BagOfNecroReagents(int amount)
        {
            DropItem(new BatWing(amount));
            //DropItem( new GraveDust  ( amount ) );
            DropItem(new DaemonBlood(amount));
            DropItem(new NoxCrystal(amount));
            DropItem(new PigIron(amount));
        }

        [Constructible]
public BagOfNecroReagents(Serial serial) : base(serial)
        {
        }

        public override void Serialize(IGenericWriter writer)
        {
            base.Serialize(writer);

            writer.Write((int)0); // version
        }

        public override void Deserialize(IGenericReader reader)
        {
            base.Deserialize(reader);

            int version = reader.ReadInt();
        }
    }
}
