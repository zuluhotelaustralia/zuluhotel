using Server.Targeting;

namespace Server.Items
{
    public class OilCloth : Item, IScissorable, IDyable
    {
        public override int LabelNumber => 1041498; // oil cloth

        public override double DefaultWeight => 1.0;


        [Constructible]
        public OilCloth() : base(0x175D)
        {
            Hue = 2001;
        }

        public bool Dye(Mobile from, DyeTub sender)
        {
            if (Deleted)
                return false;

            Hue = sender.DyedHue;

            return true;
        }

        public bool Scissor(Mobile from, Scissors scissors)
        {
            if (Deleted || !from.CanSee(this))
                return false;

            base.ScissorHelper(from, new Bandage(), 1);

            return true;
        }

        public override void OnDoubleClick(Mobile from)
        {
            if (IsChildOf(from.Backpack))
            {
                from.BeginTarget(-1, false, TargetFlags.None, OnTarget);
                from.SendLocalizedMessage(1005424); // Select the weapon or armor you wish to use the cloth on.
            }
            else
            {
                from.SendLocalizedMessage(1042001); // That must be in your pack for you to use it.
            }
        }

        public void OnTarget(Mobile from, object obj)
        {
            // TODO: Need details on how oil cloths should get consumed here

            if (!IsChildOf(from.Backpack))
            {
                from.SendLocalizedMessage(1042001); // That must be in your pack for you to use it.
            }
            else switch (obj)
            {
                case Item item when item.RootParent != from:
                    from.SendLocalizedMessage(1005425); // You may only wipe down items you are holding or carrying.
                    break;
                case BaseWeapon weapon when weapon.Poison == null || weapon.PoisonCharges <= 0:
                    // Hmm... this does not need to be cleaned.
                    from.LocalOverheadMessage(Network.MessageType.Regular, 0x3B2, 1005422);
                    break;
                case BaseWeapon weapon:
                {
                    if (weapon.PoisonCharges < 2)
                        weapon.PoisonCharges = 0;
                    else
                        weapon.PoisonCharges -= 2;

                    from.SendLocalizedMessage(weapon.PoisonCharges > 0 ? 1005423 : 1010497);
                    break;
                }
                default:
                    from.SendLocalizedMessage(1005426); // The cloth will not work on that.
                    break;
            }
        }

        [Constructible]
        public OilCloth(Serial serial) : base(serial)
        {
        }

        public override void Serialize(IGenericWriter writer)
        {
            base.Serialize(writer);

            writer.Write((int) 0);
        }

        public override void Deserialize(IGenericReader reader)
        {
            base.Deserialize(reader);

            int version = reader.ReadInt();
        }
    }
}