namespace Server.Items
{
    public class Lime : Food
    {

        [Constructible]
public Lime() : this( 1 )
        {
        }


        [Constructible]
public Lime( int amount ) : base( amount, 0x172a )
        {
            Weight = 1.0;
            FillFactor = 1;
        }

        [Constructible]
public Lime( Serial serial ) : base( serial )
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
