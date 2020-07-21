namespace Server.Items
{
    public class EmptyPewterBowl : Item
    {

        [Constructible]
public EmptyPewterBowl() : base( 0x15FD )
        {
            Weight = 1.0;
        }

        [Constructible]
public EmptyPewterBowl( Serial serial ) : base( serial )
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
