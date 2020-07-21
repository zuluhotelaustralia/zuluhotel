namespace Server.Items
{
    [TypeAlias( "Server.Items.Pizza" )]
    public class CheesePizza : Food
    {
        public override int LabelNumber{ get{ return 1044516; } } // cheese pizza


        [Constructible]
public CheesePizza() : base( 0x1040 )
        {
            Stackable = false;
            Weight = 1.0;
            FillFactor = 6;
        }

        [Constructible]
public CheesePizza( Serial serial ) : base( serial )
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
