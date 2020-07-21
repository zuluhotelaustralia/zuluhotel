namespace Server.Items
{
    [Flipable( 0x170d, 0x170e )]
    public class Sandals : BaseShoes
    {
        public override CraftResource DefaultResource{ get{ return CraftResource.RegularLeather; } }


        [Constructible]
public Sandals() : this( 0 )
        {
        }


        [Constructible]
public Sandals( int hue ) : base( 0x170D, hue )
        {
            Weight = 1.0;
        }

        [Constructible]
public Sandals( Serial serial ) : base( serial )
        {
        }

        public override bool Dye( Mobile from, DyeTub sender )
        {
            return false;
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
