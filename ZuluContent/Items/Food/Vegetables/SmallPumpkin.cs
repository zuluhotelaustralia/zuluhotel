namespace Server.Items
{
    public class SmallPumpkin : Food
    {

        [Constructible]
public SmallPumpkin() : this( 1 )
        {
        }


        [Constructible]
public SmallPumpkin( int amount ) : base( amount, 0xC6C )
        {
            Weight = 1.0;
            FillFactor = 8;
        }

        [Constructible]
public SmallPumpkin( Serial serial ) : base( serial )
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
