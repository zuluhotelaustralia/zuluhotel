// Generated File. DO NOT MODIFY BY HAND.
namespace Server.Items
{

public class VirginityOre : BaseOre
{
        [Constructable]
        public VirginityOre() : this( 1 )
        {
        }

        [Constructable]
        public VirginityOre( int amount ) : base( CraftResource.Virginity, amount )
        {
        }

        public VirginityOre( Serial serial ) : base( serial )
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
                return new VirginityIngot();
        }

        public override void OnSingleClick( Mobile from )
        {
                from.SendMessage("Virginity ore");
        }
}

}
