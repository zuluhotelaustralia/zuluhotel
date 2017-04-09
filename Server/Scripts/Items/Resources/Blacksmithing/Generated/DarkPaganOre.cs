// Generated File. DO NOT MODIFY BY HAND.
namespace Server.Items
{

public class DarkPaganOre : BaseOre
{
        [Constructable]
        public DarkPaganOre() : this( 1 )
        {
        }

        [Constructable]
        public DarkPaganOre( int amount ) : base( CraftResource.DarkPagan, amount )
        {
        }

        public DarkPaganOre( Serial serial ) : base( serial )
        {
        }

        public override string DefaultName
        {
                get
                {
                        return "Dark Pagan ore";
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
                return new DarkPaganIngot();
        }

        public override void OnSingleClick( Mobile from )
        {
                from.SendMessage("DarkPagan ore");
        }
}

}
