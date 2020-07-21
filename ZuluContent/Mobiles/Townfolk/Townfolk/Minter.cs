namespace Server.Mobiles
{
    public class Minter : Banker
	{
		public override NpcGuild NpcGuild{ get{ return NpcGuild.MerchantsGuild; } }


		[Constructible]
public Minter()
		{
			Title = "the minter";
		}

		[Constructible]
public Minter( Serial serial ) : base( serial )
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
