// Generated File. DO NOT MODIFY BY HAND.
namespace Server.Items
{

public class DarkSableRubyOre : BaseOre
{
        [Constructable]
        public DarkSableRubyOre() : this( 1 )
        {
        }

        [Constructable]
        public DarkSableRubyOre( int amount ) : base( CraftResource.DarkSableRuby, amount )
        {
        }

        public DarkSableRubyOre( Serial serial ) : base( serial )
        {
        }

        public overring string Name
        {
                get
                {
                        return "Dark Sable Ruby ore";
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
                return new DarkSableRubyIngot();
        }

        public override void OnSingleClick( Mobile from )
        {
                from.SendMessage("DarkSableRuby ore");
        }
}

}
