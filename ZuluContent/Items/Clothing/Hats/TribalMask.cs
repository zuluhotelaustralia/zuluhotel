namespace Server.Items
{
    public class TribalMask : BaseHat
    {
        public override int InitMinHits{ get{ return 20; } }
        public override int InitMaxHits{ get{ return 30; } }


        [Constructible]
public TribalMask() : this( 0 )
        {
        }


        [Constructible]
public TribalMask( int hue ) : base( 0x154B, hue )
        {
            Weight = 2.0;
        }

        public override bool Dye( Mobile from, DyeTub sender )
        {
            from.SendLocalizedMessage( sender.FailMessage );
            return false;
        }

        [Constructible]
public TribalMask( Serial serial ) : base( serial )
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
