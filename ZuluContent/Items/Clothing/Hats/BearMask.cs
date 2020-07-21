namespace Server.Items
{
    public class BearMask : BaseHat
    {
        public override int InitMinHits{ get{ return 20; } }
        public override int InitMaxHits{ get{ return 30; } }


        [Constructible]
public BearMask() : this( 0 )
        {
        }


        [Constructible]
public BearMask( int hue ) : base( 0x1545, hue )
        {
            Weight = 5.0;
        }

        public override bool Dye( Mobile from, DyeTub sender )
        {
            from.SendLocalizedMessage( sender.FailMessage );
            return false;
        }

        [Constructible]
public BearMask( Serial serial ) : base( serial )
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
