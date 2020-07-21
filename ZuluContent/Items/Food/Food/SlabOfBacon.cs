namespace Server.Items
{
    public class SlabOfBacon : Food
    {

        [Constructible]
public SlabOfBacon() : this( 1 )
        {
        }


        [Constructible]
public SlabOfBacon( int amount ) : base( amount, 0x976 )
        {
            Weight = 1.0;
            FillFactor = 3;
        }

        [Constructible]
public SlabOfBacon( Serial serial ) : base( serial )
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
