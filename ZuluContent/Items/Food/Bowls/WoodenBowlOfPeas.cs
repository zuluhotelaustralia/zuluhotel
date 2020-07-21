namespace Server.Items
{
    public class WoodenBowlOfPeas : Food
    {

        [Constructible]
public WoodenBowlOfPeas() : base( 0x15FC )
        {
            Stackable = false;
            Weight = 1.0;
            FillFactor = 2;
        }

        public override bool Eat( Mobile from )
        {
            if ( !base.Eat( from ) )
                return false;

            from.AddToBackpack( new EmptyWoodenBowl() );
            return true;
        }

        [Constructible]
public WoodenBowlOfPeas( Serial serial ) : base( serial )
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
