namespace Server.Items
{
    public class Muffins : Food
    {

        [Constructible]
public Muffins() : base( 0x9eb )
        {
            Stackable = false;
            Weight = 1.0;
            FillFactor = 4;
        }

        [Constructible]
public Muffins( Serial serial ) : base( serial )
        {
        }

        public override void Serialize( IGenericWriter writer )
        {
            base.Serialize( writer );

            writer.Write( (int) 0 ); // version
        }

        public override void Deserialize( IGenericReader reader )
        {
            base.Deserialize( reader );

            int version = reader.ReadInt();
        }
    }
}
