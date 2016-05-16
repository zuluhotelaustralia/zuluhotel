// Generated File. DO NOT MODIFY BY HAND.
namespace Server.Items
{

[FlipableAttribute( 0x1BF2, 0x1BEF )]
public class PeachblueIngot : BaseIngot
{
        [Constructable]
        public PeachblueIngot() : this( 1 )
        {
        }

        [Constructable]
        public PeachblueIngot( int amount ) : base( CraftResource.Iron, amount )
        {
        }

        public PeachblueIngot( Serial serial ) : base( serial )
        {
        }

        public override int LabelNumber
        {
                get
                {
                        return 1042692;
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
}

}
