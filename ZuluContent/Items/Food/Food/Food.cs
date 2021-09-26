using System.Collections.Generic;
using Scripts.Zulu.Utilities;
using Server.ContextMenus;

namespace Server.Items
{
    public abstract class Food : Item
    {
        [CommandProperty(AccessLevel.GameMaster)]
        public Mobile Poisoner { get; set; }

        [CommandProperty(AccessLevel.GameMaster)]
        public Poison Poison { get; set; }

        [CommandProperty(AccessLevel.GameMaster)]
        public int FillFactor { get; set; }

        public Food(int itemID) : this(1, itemID)
        {
        }

        public Food(int amount, int itemID) : base(itemID)
        {
            Stackable = true;
            Amount = amount;
            FillFactor = 1;
        }

        public Food(Serial serial) : base(serial)
        {
        }
        
        public override void GetContextMenuEntries(Mobile from, List<ContextMenuEntry> list)
        {
            base.GetContextMenuEntries(from, list);

            if (from.Alive)
            {
                list.Add(new EatEntry(from, this));
            }
        }

        public override void OnDoubleClick(Mobile from)
        {
            if (!Movable)
                return;

            if (from.InRange(GetWorldLocation(), 1)) Eat(from);
        }

        public virtual bool Eat(Mobile from)
        {
            // Fill the Mobile with FillFactor
            if (CheckHunger(from))
            {
                // Play a random "eat" sound
                from.PlaySound(Utility.Random(0x3A, 3));

                if (from.Body.IsHuman && !from.Mounted)
                    from.Animate(34, 5, 1, true, false, 0);

                if (Poison != null)
                    from.ApplyPoison(Poisoner, Poison);

                Consume();

                return true;
            }

            return false;
        }

        public virtual bool CheckHunger(Mobile from)
        {
            return FillHunger(from, FillFactor);
        }

        public static bool FillHunger(Mobile from, int fillFactor)
        {
            if (from.Hunger >= 20)
            {
                from.SendFailureMessage(500867); // You are simply too full to eat any more!
                return false;
            }

            var iHunger = from.Hunger + fillFactor;

            if (from.Stam < from.StamMax)
                from.Stam += Utility.Random(6, 3) + fillFactor / 5;

            if (iHunger >= 20)
            {
                from.Hunger = 20;
                from.SendSuccessMessage(500872); // You manage to eat the food, but you are stuffed!
            }
            else
            {
                from.Hunger = iHunger;

                if (iHunger < 5)
                    from.SendSuccessMessage(500868); // You eat the food, but are still extremely hungry.
                else if (iHunger < 10)
                    from.SendSuccessMessage(500869); // You eat the food, and begin to feel more satiated.
                else if (iHunger < 15)
                    from.SendSuccessMessage(500870); // After eating the food, you feel much less hungry.
                else
                    from.SendSuccessMessage(500871); // You feel quite full after consuming the food.
            }

            return true;
        }

        public override void Serialize(IGenericWriter writer)
        {
            base.Serialize(writer);

            writer.Write(4); // version

            writer.Write(Poisoner);

            Poison.Serialize(Poison, writer);
            writer.Write(FillFactor);
        }

        public override void Deserialize(IGenericReader reader)
        {
            base.Deserialize(reader);

            var version = reader.ReadInt();

            switch (version)
            {
                case 1:
                {
                    switch (reader.ReadInt())
                    {
                        case 0:
                            Poison = null;
                            break;
                        case 1:
                            Poison = Poison.Lesser;
                            break;
                        case 2:
                            Poison = Poison.Regular;
                            break;
                        case 3:
                            Poison = Poison.Greater;
                            break;
                        case 4:
                            Poison = Poison.Deadly;
                            break;
                    }

                    break;
                }
                case 2:
                {
                    Poison = Poison.Deserialize(reader);
                    break;
                }
                case 3:
                {
                    Poison = Poison.Deserialize(reader);
                    FillFactor = reader.ReadInt();
                    break;
                }
                case 4:
                {
                    Poisoner = reader.ReadEntity<Mobile>();
                    goto case 3;
                }
            }
        }
    }


#if false
	public class Pizza : Food
	{

		[Constructible]
public Pizza() : base( 0x1040 )
		{
			Stackable = false;
			this.Weight = 1.0;
			this.FillFactor = 6;
		}

		[Constructible]
public Pizza( Serial serial ) : base( serial )
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
#endif
}