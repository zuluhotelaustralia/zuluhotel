// Generated File. DO NOT MODIFY BY HAND.
namespace Server.Items
{

[FlipableAttribute( 0x1BF2, 0x1BEF )]
public class SpectralIngot : BaseIngot
{
        [Constructable]
        public SpectralIngot() : this( 1 )
        {
        }

        [Constructable]
        public SpectralIngot( int amount ) : base( CraftResource.Spectral, amount )
        {
        }

        public SpectralIngot( Serial serial ) : base( serial )
        {
        }

        public override string DefaultName
        {
                get
                {
                        return "Spectral ingot";
                }
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

        public override void OnSingleClick( Mobile from )
        {
                from.SendMessage("Spectral ingot");
        }
}

}