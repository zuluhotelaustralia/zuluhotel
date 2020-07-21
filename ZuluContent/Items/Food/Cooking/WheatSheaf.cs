using Server.Targeting;

namespace Server.Items
{
    public class WheatSheaf : Item
    {

        [Constructible]
public WheatSheaf() : this( 1 )
        {
        }


        [Constructible]
public WheatSheaf( int amount ) : base( 7869 )
        {
            Weight = 1.0;
            Stackable = true;
            Amount = amount;
        }

        public override void OnDoubleClick( Mobile from )
        {
            if ( !Movable )
                return;

            from.BeginTarget( 4, false, TargetFlags.None, OnTarget );
        }

        public virtual void OnTarget( Mobile from, object obj )
        {
            if ( obj is AddonComponent )
                obj = (obj as AddonComponent).Addon;

            IFlourMill mill = obj as IFlourMill;

            if ( mill != null )
            {
                int needs = mill.MaxFlour - mill.CurFlour;

                if ( needs > Amount )
                    needs = Amount;

                mill.CurFlour += needs;
                Consume( needs );
            }
        }

        [Constructible]
public WheatSheaf( Serial serial ) : base( serial )
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
