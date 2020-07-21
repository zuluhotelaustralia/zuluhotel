namespace Server.Items
{
    public class Pear : Food
    {

        [Constructible]
public Pear() : this( 1 )
        {
        }


        [Constructible]
public Pear( int amount ) : base( amount, 0x994 )
        {
            Weight = 1.0;
            FillFactor = 1;
        }

        [Constructible]
public Pear( Serial serial ) : base( serial )
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
