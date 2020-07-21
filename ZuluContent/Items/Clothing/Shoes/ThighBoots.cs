namespace Server.Items
{
    [Flipable]
    public class ThighBoots : BaseShoes
    {
        public override CraftResource DefaultResource{ get{ return CraftResource.RegularLeather; } }


        [Constructible]
public ThighBoots() : this( 0 )
        {
        }


        [Constructible]
public ThighBoots( int hue ) : base( 0x1711, hue )
        {
            Weight = 4.0;
        }

        [Constructible]
public ThighBoots( Serial serial ) : base( serial )
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
