namespace Server.Items
{
    public class GlassBottle : Item
    {

        [Constructible]
public GlassBottle() : base( 0xe2b )
        {
            Weight = 0.3;
        }

        [Constructible]
public GlassBottle( Serial serial ) : base( serial )
        {
        }

        public override void Serialize( IGenericWriter writer )
        {
            base.Serialize( writer );

            writer.Write( (int) 0 ); // version
        }

        public override void Deserialize( IGenericReader reader )
        {
            base.Deserialize( reader );

            int version = reader.ReadInt();
        }
    }
}
