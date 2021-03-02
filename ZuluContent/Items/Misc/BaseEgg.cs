using Server.Spells;

namespace Server.Items
{
    public abstract class BaseEgg : Item
    {
        public override double DefaultWeight => 0.02;

        public BaseEgg(Serial serial) : base(serial)
        {

        }

        public BaseEgg(int amount) : base(0x1725)
        {
            Stackable = true;
            Amount = amount;
        }

        public override void OnDoubleClick(Mobile from)
        {
            if (!Movable)
                return;

            if (!IsChildOf(from.Backpack))
            {
                from.SendLocalizedMessage(1042001); // That must be in your pack for you to use it.
                return;
            }

            var p = new Point3D(from);
            if (!SpellHelper.FindValidSpawnLocation(from.Map, ref p, true))
                return;
            
            Consume(1);
            from.SendMessage("The egg begins to move and ");

            SpawnCreatureFromEgg(from, p);
        }

        public abstract void SpawnCreatureFromEgg(Mobile from, Point3D p);
    }
}
