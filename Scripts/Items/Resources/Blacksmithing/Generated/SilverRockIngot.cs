// Generated File. DO NOT MODIFY BY HAND.
namespace Server.Items
{

[FlipableAttribute( 0x1BF2, 0x1BEF )]
public class SilverRockIngot : BaseIngot
{
        [Constructable]
        public SilverRockIngot() : this( 1 )
        {
        }

        [Constructable]
        public SilverRockIngot( int amount ) : base( CraftResource.SilverRock, amount )
        {
        }

        public SilverRockIngot( Serial serial ) : base( serial )
        {
        }

        public override string Name
        {
                get
                {
                        return "Silver Rock ingot";
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
                from.SendMessage("SilverRock ingot");
        }
}

}
