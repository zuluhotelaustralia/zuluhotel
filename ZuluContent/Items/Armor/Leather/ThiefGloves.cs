using ModernUO.Serialization;
using Scripts.Zulu.Utilities;
using Server.Targeting;

namespace Server.Items
{
    public enum ThiefGlovesHue
    {
        Black,
        Blue,
        Red
    }

    [SerializationGenerator(0, false)]
    [Flipable]
    public partial class ThiefGloves : BaseArmor
    {
        public override int InitMinHits => 70;

        public override int InitMaxHits => 70;

        public override int ArmorBase => 0;

        public override ArmorMaterialType MaterialType => ArmorMaterialType.Leather;

        [Constructible]
        public ThiefGloves() : this((ThiefGlovesHue)Utility.Random(2))
        {
        }

        [Constructible]
        public ThiefGloves(ThiefGlovesHue hue) : base(0x13C6)
        {
            Weight = 1.0;

            Hue = hue switch
            {
                ThiefGlovesHue.Black => 0x485,
                ThiefGlovesHue.Blue => 0x492,
                ThiefGlovesHue.Red => 0x494
            };
        }

        public override void OnSingleClick(Mobile from)
        {
            LabelTo(from, "Thief Gloves");
        }

        public override void OnDoubleClick(Mobile from)
        {
            if (from.HandArmor != this)
            {
                from.SendFailureMessage("You have to equip that to snoop!");
                return;
            }

            from.SendSuccessMessage("Who would you like to snoop?");

            from.Target = new SnoopingTarget();
        }

        private class SnoopingTarget : Target
        {
            public SnoopingTarget() : base(1, false, TargetFlags.None)
            {
            }

            protected override void OnTarget(Mobile from, object targeted)
            {
                if (targeted is not Mobile mobile || mobile == from)
                {
                    from.SendFailureMessage("You cannot snoop that!");
                    return;
                }

                mobile.Backpack.OnSnoop(from);
            }
        }
    }
}