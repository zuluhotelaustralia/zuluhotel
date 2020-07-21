namespace Server.Items
{
    public class Bacon : Food
    {

        [Constructible]
public Bacon() : this( 1 )
        {
        }


        [Constructible]
public Bacon( int amount ) : base( amount, 0x979 )
        {
            Weight = 1.0;
            FillFactor = 1;
        }

        [Constructible]
public Bacon( Serial serial ) : base( serial )
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
