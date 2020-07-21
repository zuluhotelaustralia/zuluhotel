namespace Server.Items
{
    public class DupreSuit : BaseSuit
	{

		[Constructible]
public DupreSuit() : base( AccessLevel.GameMaster, 0x0, 0x2050 )
		{
		}

		[Constructible]
public DupreSuit( Serial serial ) : base( serial )
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
