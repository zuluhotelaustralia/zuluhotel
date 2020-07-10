namespace Server.Items
{
    public class Turnip : Food
    {
        [Constructable]
        public Turnip() : this( 1 )
        {
        }

        [Constructable]
        public Turnip( int amount ) : base( amount, 0xD3A )
        {
            Weight = 1.0;
            FillFactor = 1;
        }

        public Turnip( Serial serial ) : base( serial )
        {
        }

        public override void Serialize( GenericWriter writer )
        {
            base.Serialize( writer );

            writer.Write( (int) 0 ); // version
        }

        public override void Deserialize( GenericReader reader )
        {
            base.Deserialize( reader );

            int version = reader.ReadInt();
        }
    }
}