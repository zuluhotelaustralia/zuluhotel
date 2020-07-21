namespace Server.Items
{
    public class WoodenBowl : Item
    {

        [Constructible]
public WoodenBowl() : base( 0x15f8 )
        {
            Weight = 1.0;
        }

        [Constructible]
public WoodenBowl( Serial serial ) : base( serial )
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
