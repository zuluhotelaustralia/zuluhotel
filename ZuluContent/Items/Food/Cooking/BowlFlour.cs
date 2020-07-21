namespace Server.Items
{
    public class BowlFlour : Item
    {

        [Constructible]
public BowlFlour() : base( 0xa1e )
        {
            Weight = 1.0;
        }

        [Constructible]
public BowlFlour( Serial serial ) : base( serial )
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
