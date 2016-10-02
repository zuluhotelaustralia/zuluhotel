// Generated File. DO NOT MODIFY BY HAND.
namespace Server.Items
{

[FlipableAttribute( 0x1BF2, 0x1BEF )]
public class IronIngot : BaseIngot
{
        [Constructable]
        public IronIngot() : this( 1 )
        {
        }

        [Constructable]
        public IronIngot( int amount ) : base( CraftResource.Iron, amount )
        {
        }

        public IronIngot( Serial serial ) : base( serial )
        {
        }

        public override string Name
        {
                get
                {
                        return "Iron ingot";
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
                from.SendMessage("Iron ingot");
        }
}

}
