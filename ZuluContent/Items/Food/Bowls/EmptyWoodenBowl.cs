namespace Server.Items
{
    public class EmptyWoodenBowl : Item
	{

		[Constructible]
public EmptyWoodenBowl() : base( 0x15F8 )
		{
			Weight = 1.0;
		}

		[Constructible]
public EmptyWoodenBowl( Serial serial ) : base( serial )
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
