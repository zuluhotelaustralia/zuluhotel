// Generated File. DO NOT MODIFY BY HAND.
namespace Server.Items
{

public class ExecutorOre : BaseOre
{
        [Constructable]
        public ExecutorOre() : this( 1 )
        {
        }

        [Constructable]
        public ExecutorOre( int amount ) : base( CraftResource.Executor, amount )
        {
        }

        public ExecutorOre( Serial serial ) : base( serial )
        {
        }

        public overring string Name
        {
                get
                {
                        return "Executor ore";
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
                return new ExecutorIngot();
        }

        public override void OnSingleClick( Mobile from )
        {
                from.SendMessage("Executor ore");
        }
}

}
