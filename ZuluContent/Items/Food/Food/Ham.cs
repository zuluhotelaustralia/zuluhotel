namespace Server.Items
{
    public class Ham : Food
    {

        [Constructible]
public Ham() : this( 1 )
        {
        }


        [Constructible]
public Ham( int amount ) : base( amount, 0x9C9 )
        {
            Weight = 1.0;
            FillFactor = 5;
        }

        [Constructible]
public Ham( Serial serial ) : base( serial )
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
