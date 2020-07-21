namespace Server.Items
{
    public class SheafOfHay : Item
    {

        [Constructible]
public SheafOfHay() : base( 0xF36 )
        {
            Weight = 10.0;
        }

        [Constructible]
public SheafOfHay( Serial serial ) : base( serial )
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
