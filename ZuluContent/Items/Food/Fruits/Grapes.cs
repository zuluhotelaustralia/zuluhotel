namespace Server.Items
{
    public class Grapes : Food
    {

        [Constructible]
public Grapes() : this( 1 )
        {
        }


        [Constructible]
public Grapes( int amount ) : base( amount, 0x9D1 )
        {
            Weight = 1.0;
            FillFactor = 1;
        }

        [Constructible]
public Grapes( Serial serial ) : base( serial )
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
