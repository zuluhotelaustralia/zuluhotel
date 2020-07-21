namespace Server.Items
{
    public class CookieMix : CookableFood
    {

        [Constructible]
public CookieMix() : base( 0x103F, 20 )
        {
            Weight = 1.0;
        }

        [Constructible]
public CookieMix( Serial serial ) : base( serial )
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

        public override Food Cook()
        {
            return new Cookies();
        }
    }
}
