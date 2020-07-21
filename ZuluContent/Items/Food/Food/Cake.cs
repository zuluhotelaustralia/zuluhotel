namespace Server.Items
{
    public class Cake : Food
    {

        [Constructible]
public Cake() : base( 0x9E9 )
        {
            Stackable = false;
            Weight = 1.0;
            FillFactor = 10;
        }

        [Constructible]
public Cake( Serial serial ) : base( serial )
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
