// Generated File. DO NOT MODIFY BY HAND.
namespace Server.Items
{

public class DoomOre : BaseOre
{
        [Constructable]
        public DoomOre() : this( 1 )
        {
        }

        [Constructable]
        public DoomOre( int amount ) : base( CraftResource.Doom, amount )
        {
        }

        public DoomOre( Serial serial ) : base( serial )
        {
        }

        public override string Name
        {
                get
                {
                        return "Doom ore";
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
                return new DoomIngot();
        }

        public override void OnSingleClick( Mobile from )
        {
                from.SendMessage("Doom ore");
        }
}

}
