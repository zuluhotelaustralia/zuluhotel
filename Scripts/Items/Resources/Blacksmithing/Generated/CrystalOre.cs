// Generated File. DO NOT MODIFY BY HAND.
namespace Server.Items
{

public class CrystalOre : BaseOre
{
        [Constructable]
        public CrystalOre() : this( 1 )
        {
        }

        [Constructable]
        public CrystalOre( int amount ) : base( CraftResource.Crystal, amount )
        {
        }

        public CrystalOre( Serial serial ) : base( serial )
        {
        }

        public overring string Name
        {
                get
                {
                        return "Crystal ore";
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
                return new CrystalIngot();
        }

        public override void OnSingleClick( Mobile from )
        {
                from.SendMessage("Crystal ore");
        }
}

}
