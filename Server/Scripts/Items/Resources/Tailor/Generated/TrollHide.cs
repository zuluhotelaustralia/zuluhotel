//Generated file.  Do not modify by hand.
namespace Server.Items{
	[FlipableAttribute( 0x1079, 0x1078 )]
	public class TrollHide : BaseHides, IScissorable
	{
		[Constructable]
		public TrollHide() : this( 1 )
		{
		}

		[Constructable]
		public TrollHide( int amount ) : base( CraftResource.TrollLeather, amount )
		{
			this.Hue = 0x54a;
		}

		public TrollHide( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );

			writer.Write( (int) 0 ); // version
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );

			int version = reader.ReadInt();
		}

		public bool Scissor( Mobile from, Scissors scissors )
		{
			if ( Deleted || !from.CanSee( this ) ) return false;

			if ( Core.AOS && !IsChildOf ( from.Backpack ) )
			{
				from.SendLocalizedMessage ( 502437 ); // Items you wish to cut must be in your backpack
				return false;
			}
			base.ScissorHelper( from, new TrollLeather(), 1 );

			return true;
		}
	}
}