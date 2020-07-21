namespace Server.Items
{
    public class PieceWhiteChecker : BasePiece
	{
		public override string DefaultName
		{
			get { return "white checker"; }
		}

		public PieceWhiteChecker( BaseBoard board ) : base( 0x3584, board )
		{
		}

		public PieceWhiteChecker( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( IGenericWriter writer )
		{
			base.Serialize( writer );
			writer.Write( (int) 0 );
		}

		public override void Deserialize( IGenericReader reader )
		{
			base.Deserialize( reader );
			int version = reader.ReadInt();
		}
	}

	public class PieceBlackChecker : BasePiece
	{
		public override string DefaultName
		{
			get { return "black checker"; }
		}

		public PieceBlackChecker( BaseBoard board ) : base( 0x358B, board )
		{
		}

		public PieceBlackChecker( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( IGenericWriter writer )
		{
			base.Serialize( writer );
			writer.Write( (int) 0 );
		}

		public override void Deserialize( IGenericReader reader )
		{
			base.Deserialize( reader );
			int version = reader.ReadInt();
		}
	}
}
