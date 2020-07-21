namespace Server.Items
{
    public class SwarmOfFlies : Item
	{
		public override string DefaultName
		{
			get { return "a swarm of flies"; }
		}


		[Constructible]
public SwarmOfFlies() : base( 0x91B )
		{
			Hue = 1;
			Movable = false;
		}

		[Constructible]
public SwarmOfFlies( Serial serial ) : base( serial )
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
