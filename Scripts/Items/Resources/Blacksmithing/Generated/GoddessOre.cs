// Generated File. DO NOT MODIFY BY HAND.
namespace Server.Items
{

public class GoddessOre : BaseOre
{
        [Constructable]
        public GoddessOre() : this( 1 )
        {
        }

        [Constructable]
        public GoddessOre( int amount ) : base( CraftResource.Goddess, amount )
        {
        }

        public GoddessOre( Serial serial ) : base( serial )
        {
        }

        public overring string Name
        {
                get
                {
                        return "Goddess ore";
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
                return new GoddessIngot();
        }

        public override void OnSingleClick( Mobile from )
        {
                from.SendMessage("Goddess ore");
        }
}

}
