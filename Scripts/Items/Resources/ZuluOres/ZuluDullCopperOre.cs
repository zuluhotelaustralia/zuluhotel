// Generated File. DO NOT MODIFY BY HAND.
namespace Server.Items
{

public class ZuluDullCopperOre : BaseOre
{
        [Constructable]
        public ZuluDullCopperOre() : this( 1 )
        {
        }

        [Constructable]
        public ZuluDullCopperOre( int amount ) : base( CraftResource.Iron, amount )
        {
        }

        public ZuluDullCopperOre( Serial serial ) : base( serial )
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
                return new ZuluDullCopperIngot();
        }
}

}
