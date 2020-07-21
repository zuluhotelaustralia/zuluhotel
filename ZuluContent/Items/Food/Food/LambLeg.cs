namespace Server.Items
{
    public class LambLeg : Food
    {

        [Constructible]
public LambLeg() : this( 1 )
        {
        }


        [Constructible]
public LambLeg( int amount ) : base( amount, 0x160a )
        {
            Weight = 2.0;
            FillFactor = 5;
        }

        [Constructible]
public LambLeg( Serial serial ) : base( serial )
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
