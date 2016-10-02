// Generated File. DO NOT MODIFY BY HAND.
namespace Server.Items
{

public class SpikeOre : BaseOre
{
        [Constructable]
        public SpikeOre() : this( 1 )
        {
        }

        [Constructable]
        public SpikeOre( int amount ) : base( CraftResource.Spike, amount )
        {
        }

        public SpikeOre( Serial serial ) : base( serial )
        {
        }

        public overring string Name
        {
                get
                {
                        return "Spike ore";
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
                return new SpikeIngot();
        }

        public override void OnSingleClick( Mobile from )
        {
                from.SendMessage("Spike ore");
        }
}

}
