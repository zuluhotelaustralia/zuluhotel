namespace Server.Items
{
    public class CheeseSlice : Food
    {
        public override double DefaultWeight
        {
            get { return 0.1; }
        }


        [Constructible]
public CheeseSlice() : this( 1 )
        {
        }


        [Constructible]
public CheeseSlice( int amount ) : base( amount, 0x97C )
        {
            FillFactor = 1;
        }

        [Constructible]
public CheeseSlice( Serial serial ) : base( serial )
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
