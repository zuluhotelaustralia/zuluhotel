namespace Server.Items
{
    public class SplitCoconut : Food
    {

        [Constructible]
public SplitCoconut() : this( 1 )
        {
        }


        [Constructible]
public SplitCoconut( int amount ) : base( amount, 0x1725 )
        {
            Weight = 1.0;
            FillFactor = 1;
        }

        [Constructible]
public SplitCoconut( Serial serial ) : base( serial )
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
