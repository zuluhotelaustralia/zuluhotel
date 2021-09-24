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
    
    [Flipable]
    public class ThiefGloves : BaseArmor
    {
        public override int InitMinHits => 70;

        public override int InitMaxHits => 70;

        public override int ArmorBase => 0;

        public override ArmorMaterialType MaterialType => ArmorMaterialType.Leather;

        public override string DefaultName => "Thief Gloves";
        
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

        [Constructible]
        public ThiefGloves(Serial serial) : base(serial)
        {
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