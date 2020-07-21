namespace Server.Items
{
    public class RawChickenLeg : CookableFood
    {

        [Constructible]
public RawChickenLeg() : base( 0x1607, 10 )
        {
            Weight = 1.0;
            Stackable = true;
        }

        [Constructible]
public RawChickenLeg( Serial serial ) : base( serial )
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
            return new ChickenLeg();
        }
    }
}
