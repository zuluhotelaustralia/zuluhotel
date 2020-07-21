namespace Server.Items
{
    [TypeAlias( "Server.Items.EmptyLargeWoodenBowl" )]
    public class EmptyWoodenTub : Item
    {

        [Constructible]
public EmptyWoodenTub() : base( 0x1605 )
        {
            Weight = 2.0;
        }

        [Constructible]
public EmptyWoodenTub( Serial serial ) : base( serial )
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
