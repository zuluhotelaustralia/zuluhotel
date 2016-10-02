// Generated File. DO NOT MODIFY BY HAND.
namespace Server.Items
{

public class MalachiteOre : BaseOre
{
        [Constructable]
        public MalachiteOre() : this( 1 )
        {
        }

        [Constructable]
        public MalachiteOre( int amount ) : base( CraftResource.Malachite, amount )
        {
        }

        public MalachiteOre( Serial serial ) : base( serial )
        {
        }

        public overring string Name
        {
                get
                {
                        return "Malachite ore";
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
                return new MalachiteIngot();
        }

        public override void OnSingleClick( Mobile from )
        {
                from.SendMessage("Malachite ore");
        }
}

}
