using Scripts.Zulu.Engines.Classes;
using Scripts.Zulu.Utilities;
using Server.Engines.Craft;
using Server.Mobiles;

namespace Server.Items
{
    public class Sand : Item
    {
        public override string DefaultName => "grain of sand";

        public override void Serialize(IGenericWriter writer)
        {
            base.Serialize(writer);

            writer.Write((int) 0); // version
        }

        public override void Deserialize(IGenericReader reader)
        {
            base.Deserialize(reader);

            int version = reader.ReadInt();
        }

        [Constructible]
        public Sand() : this(1)
        {
        }
        
        [Constructible]
        public Sand(int amount) : base(0xEED)
        {
            Stackable = true;
            Amount = amount;
            Hue = 0x83B;
        }

        public Sand(Serial serial) : base(serial)
        {
        }

        public override void OnDoubleClick(Mobile from)
        {
            if (!Movable)
                return;

            if (RootParent is BaseCreature)
            {
                from.SendLocalizedMessage(500447); // That is not accessible
                return;
            }

            if (!from.InRange(GetWorldLocation(), 2))
            {
                from.SendLocalizedMessage(1060178); // You are too far away to perform that action!
                return;
            }

            if (!DefBlacksmithy.CheckForge(from, 1))
            {
                from.SendFailureMessage("You must be near a forge to smelt sand!");
                return;
            }

            if (!from.ShilCheckSkill(SkillName.Blacksmith, points: 0))
            {
                Consume(1);
                from.SendFailureMessage("You waste some sand.");
                return;
            }

            var glass = new RawGlass(Amount);
            from.PlaySound(0x2B);
            from.AddToBackpack(glass);
            from.SendSuccessMessage($"You create {Amount} raw glass and place it in your pack.");
            Delete();
        }
    }
}