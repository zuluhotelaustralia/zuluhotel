namespace Server.Items
{
    [Flipable( 0x170b, 0x170c )]
    public class Boots : BaseShoes
    {
        public override CraftResource DefaultResource{ get{ return CraftResource.RegularLeather; } }


        [Constructible]
public Boots() : this( 0 )
        {
        }


        [Constructible]
public Boots( int hue ) : base( 0x170B, hue )
        {
            Weight = 3.0;
        }

        [Constructible]
public Boots( Serial serial ) : base( serial )
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
