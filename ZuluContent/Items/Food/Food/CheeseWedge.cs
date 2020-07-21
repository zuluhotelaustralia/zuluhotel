namespace Server.Items
{
    public class CheeseWedge : Food
    {
        public override double DefaultWeight
        {
            get { return 0.1; }
        }


        [Constructible]
public CheeseWedge() : this( 1 )
        {
        }


        [Constructible]
public CheeseWedge( int amount ) : base( amount, 0x97D )
        {
            FillFactor = 3;
        }

        [Constructible]
public CheeseWedge( Serial serial ) : base( serial )
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
