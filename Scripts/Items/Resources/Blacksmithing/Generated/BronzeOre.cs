// Generated File. DO NOT MODIFY BY HAND.
namespace Server.Items
{

public class BronzeOre : BaseOre
{
        [Constructable]
        public BronzeOre() : this( 1 )
        {
        }

        [Constructable]
        public BronzeOre( int amount ) : base( CraftResource.Iron, amount )
        {
        }

        public BronzeOre( Serial serial ) : base( serial )
        {
        }

        public override int LabelNumber
        {
                get
                {
                        return 1042853;
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
                return new BronzeIngot();
        }
}

}
