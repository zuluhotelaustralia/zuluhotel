namespace Server.Items
{
    [TypeAlias( "Server.Items.EmptyLargePewterBowl" )]
    public class EmptyPewterTub : Item
    {

        [Constructible]
public EmptyPewterTub() : base( 0x1603 )
        {
            Weight = 2.0;
        }

        [Constructible]
public EmptyPewterTub( Serial serial ) : base( serial )
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
