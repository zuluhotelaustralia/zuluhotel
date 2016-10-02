// Generated File. DO NOT MODIFY BY HAND.
namespace Server.Items
{

public class CopperOre : BaseOre
{
        [Constructable]
        public CopperOre() : this( 1 )
        {
        }

        [Constructable]
        public CopperOre( int amount ) : base( CraftResource.Copper, amount )
        {
        }

        public CopperOre( Serial serial ) : base( serial )
        {
        }

        public overring string Name
        {
                get
                {
                        return "Copper ore";
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
                return new CopperIngot();
        }

        public override void OnSingleClick( Mobile from )
        {
                from.SendMessage("Copper ore");
        }
}

}
