// Generated File. DO NOT MODIFY BY HAND.
namespace Server.Items
{

[FlipableAttribute( 0x1BF2, 0x1BEF )]
public class ZuluIronIngot : BaseIngot
{
        [Constructable]
        public ZuluIronIngot() : this( 1 )
        {
        }

        [Constructable]
        public ZuluIronIngot( int amount ) : base( CraftResource.Iron, amount )
        {
        }

        public ZuluIronIngot( Serial serial ) : base( serial )
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
