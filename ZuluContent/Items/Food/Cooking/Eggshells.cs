namespace Server.Items
{
    public class Eggshells : Item
    {

        [Constructible]
public Eggshells() : base( 0x9b4 )
        {
            Weight = 0.5;
        }

        [Constructible]
public Eggshells( Serial serial ) : base( serial )
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
