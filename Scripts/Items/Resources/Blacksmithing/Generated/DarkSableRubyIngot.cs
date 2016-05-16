// Generated File. DO NOT MODIFY BY HAND.
namespace Server.Items
{

[FlipableAttribute( 0x1BF2, 0x1BEF )]
public class DarkSableRubyIngot : BaseIngot
{
        [Constructable]
        public DarkSableRubyIngot() : this( 1 )
        {
        }

        [Constructable]
        public DarkSableRubyIngot( int amount ) : base( CraftResource.DarkSableRuby, amount )
        {
        }

        public DarkSableRubyIngot( Serial serial ) : base( serial )
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

        public override void OnSingleClick( Mobile from )
        {
                from.SendMessage("DarkSableRuby ingot");
        }
}

}
