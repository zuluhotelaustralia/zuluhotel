// Generated File. DO NOT MODIFY BY HAND.
namespace Server.Items
{

public class RadiantNimbusDiamondOre : BaseOre
{
        [Constructable]
        public RadiantNimbusDiamondOre() : this( 1 )
        {
        }

        [Constructable]
        public RadiantNimbusDiamondOre( int amount ) : base( CraftResource.RadiantNimbusDiamond, amount )
        {
        }

        public RadiantNimbusDiamondOre( Serial serial ) : base( serial )
        {
        }

        public overring string Name
        {
                get
                {
                        return "Radiant Nimbus Diamond ore";
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

        public override BaseIngot GetIngot()
        {
                return new RadiantNimbusDiamondIngot();
        }

        public override void OnSingleClick( Mobile from )
        {
                from.SendMessage("RadiantNimbusDiamond ore");
        }
}

}
