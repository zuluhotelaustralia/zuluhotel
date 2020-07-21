namespace Server.Items
{
    public class Limes : Food
    {

        [Constructible]
public Limes() : this( 1 )
        {
        }


        [Constructible]
public Limes( int amount ) : base( amount, 0x172B )
        {
            Weight = 1.0;
            FillFactor = 1;
        }

        [Constructible]
public Limes( Serial serial ) : base( serial )
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
