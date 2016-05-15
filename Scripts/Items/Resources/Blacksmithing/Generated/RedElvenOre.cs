// Generated File. DO NOT MODIFY BY HAND.
namespace Server.Items
{

public class RedElvenOre : BaseOre
{
        [Constructable]
        public RedElvenOre() : this( 1 )
        {
        }

        [Constructable]
        public RedElvenOre( int amount ) : base( CraftResource.Iron, amount )
        {
        }

        public RedElvenOre( Serial serial ) : base( serial )
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
                return new RedElvenIngot();
        }
}

}
