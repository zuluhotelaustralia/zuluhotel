// Generated File. DO NOT MODIFY BY HAND.
namespace Server.Items
{

public class IceRockOre : BaseOre
{
        [Constructable]
        public IceRockOre() : this( 1 )
        {
        }

        [Constructable]
        public IceRockOre( int amount ) : base( CraftResource.IceRock, amount )
        {
        }

        public IceRockOre( Serial serial ) : base( serial )
        {
        }

        public override string DefaultName
        {
                get
                {
                        return "Ice Rock ore";
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
                return new IceRockIngot();
        }

        public override void OnSingleClick( Mobile from )
        {
                from.SendMessage("IceRock ore");
        }
}

}
