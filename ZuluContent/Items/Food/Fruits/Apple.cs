namespace Server.Items
{
    public class Apple : Food
    {

        [Constructible]
public Apple() : this( 1 )
        {
        }


        [Constructible]
public Apple( int amount ) : base( amount, 0x9D0 )
        {
            Weight = 1.0;
            FillFactor = 1;
        }

        [Constructible]
public Apple( Serial serial ) : base( serial )
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
