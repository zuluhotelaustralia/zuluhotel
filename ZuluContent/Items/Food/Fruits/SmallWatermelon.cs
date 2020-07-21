namespace Server.Items
{
    public class SmallWatermelon : Food
    {

        [Constructible]
public SmallWatermelon() : this( 1 )
        {
        }


        [Constructible]
public SmallWatermelon( int amount ) : base( amount, 0xC5D )
        {
            Weight = 5.0;
            FillFactor = 5;
        }

        [Constructible]
public SmallWatermelon( Serial serial ) : base( serial )
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
