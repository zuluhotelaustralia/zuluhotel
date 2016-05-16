// Generated File. DO NOT MODIFY BY HAND.
namespace Server.Items
{

public class UndeadOre : BaseOre
{
        [Constructable]
        public UndeadOre() : this( 1 )
        {
        }

        [Constructable]
        public UndeadOre( int amount ) : base( CraftResource.Undead, amount )
        {
        }

        public UndeadOre( Serial serial ) : base( serial )
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
                return new UndeadIngot();
        }

        public override void OnSingleClick( Mobile from )
        {
                from.SendMessage("Undead ore");
        }
}

}
