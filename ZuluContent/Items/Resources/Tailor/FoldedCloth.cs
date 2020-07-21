using Server.Network;

namespace Server.Items
{
    public class FoldedCloth : Item, IScissorable, IDyable
	{
		public override double DefaultWeight
		{
			get { return 0.1; }
		}


		[Constructible]
public FoldedCloth() : this( 1 )
		{
		}


		[Constructible]
public FoldedCloth( int amount ) : base( 0x1761 )
		{
			Stackable = true;
			Amount = amount;
		}

		[Constructible]
public FoldedCloth( Serial serial ) : base( serial )
		{
		}

		public bool Dye( Mobile from, DyeTub sender )
		{
			if ( Deleted )
				return false;

			Hue = sender.DyedHue;

			return true;
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

		public bool Scissor( Mobile from, Scissors scissors )
		{
			if ( Deleted || !from.CanSee( this ) ) return false;

			base.ScissorHelper( from, new Bandage(), 1 );

			return true;
		}
	}
}
