namespace Server.Items
{
    public class Lemons : Food
    {

        [Constructible]
public Lemons() : this( 1 )
        {
        }


        [Constructible]
public Lemons( int amount ) : base( amount, 0x1729 )
        {
            Weight = 1.0;
            FillFactor = 1;
        }

        [Constructible]
public Lemons( Serial serial ) : base( serial )
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
