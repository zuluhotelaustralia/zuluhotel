namespace Server.Items
{
    public class WoodenBowlOfTomatoSoup : Food
    {

        [Constructible]
public WoodenBowlOfTomatoSoup() : base( 0x1606 )
        {
            Stackable = false;
            Weight = 2.0;
            FillFactor = 2;
        }

        public override bool Eat( Mobile from )
        {
            if ( !base.Eat( from ) )
                return false;

            from.AddToBackpack( new EmptyWoodenTub() );
            return true;
        }

        [Constructible]
public WoodenBowlOfTomatoSoup( Serial serial ) : base( serial )
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
