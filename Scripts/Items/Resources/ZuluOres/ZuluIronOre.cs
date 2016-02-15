// Generated File. DO NOT MODIFY BY HAND.
namespace Server.Items
{

public class ZuluIronOre : BaseOre
{
        [Constructable]
        public ZuluIronOre() : this( 1 )
        {
        }

        [Constructable]
        public ZuluIronOre( int amount ) : base( CraftResource.Iron, amount )
        {
        }

        public ZuluIronOre( Serial serial ) : base( serial )
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
        
        public override BaseIngot GetIngot()
        {
                return new ZuluIronIngot();
        }
}

}
