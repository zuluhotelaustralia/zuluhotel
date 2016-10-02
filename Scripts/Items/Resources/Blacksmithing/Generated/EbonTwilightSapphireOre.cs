// Generated File. DO NOT MODIFY BY HAND.
namespace Server.Items
{

public class EbonTwilightSapphireOre : BaseOre
{
        [Constructable]
        public EbonTwilightSapphireOre() : this( 1 )
        {
        }

        [Constructable]
        public EbonTwilightSapphireOre( int amount ) : base( CraftResource.EbonTwilightSapphire, amount )
        {
        }

        public EbonTwilightSapphireOre( Serial serial ) : base( serial )
        {
        }

        public overring string Name
        {
                get
                {
                        return "Ebon Twilight Sapphire ore";
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
                return new EbonTwilightSapphireIngot();
        }

        public override void OnSingleClick( Mobile from )
        {
                from.SendMessage("EbonTwilightSapphire ore");
        }
}

}
