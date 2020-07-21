namespace Server.Items
{
    public class CheeseWheel : Food
    {
        public override double DefaultWeight
        {
            get { return 0.1; }
        }


        [Constructible]
public CheeseWheel() : this( 1 )
        {
        }


        [Constructible]
public CheeseWheel( int amount ) : base( amount, 0x97E )
        {
            FillFactor = 3;
        }

        [Constructible]
public CheeseWheel( Serial serial ) : base( serial )
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
