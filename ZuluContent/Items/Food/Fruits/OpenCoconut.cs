namespace Server.Items
{
    public class OpenCoconut : Food
    {

        [Constructible]
public OpenCoconut() : this( 1 )
        {
        }


        [Constructible]
public OpenCoconut( int amount ) : base( amount, 0x1723 )
        {
            Weight = 1.0;
            FillFactor = 1;
        }

        [Constructible]
public OpenCoconut( Serial serial ) : base( serial )
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
