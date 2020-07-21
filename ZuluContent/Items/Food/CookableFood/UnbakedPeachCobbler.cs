namespace Server.Items
{
    public class UnbakedPeachCobbler : CookableFood
    {
        public override int LabelNumber{ get{ return 1041335; } } // unbaked peach cobbler


        [Constructible]
public UnbakedPeachCobbler() : base( 0x1042, 25 )
        {
            Weight = 1.0;
        }

        [Constructible]
public UnbakedPeachCobbler( Serial serial ) : base( serial )
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
            return new PeachCobbler();
        }
    }
}
