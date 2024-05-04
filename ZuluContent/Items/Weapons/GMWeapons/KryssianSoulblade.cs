using System;
using ModernUO.Serialization;
using Scripts.Zulu.Utilities;
using Server.Targeting;
using ZuluContent.Zulu.Items;

namespace Server.Items
{
    [SerializationGenerator(0, false)]
    [FlipableAttribute(0x0F5E, 0x0F5F)]
    public partial class KryssianSoulblade : BaseSword, IGMItem
    {
        public override int DefaultStrengthReq => 90;

        public override int DefaultMinDamage => 25;

        public override int DefaultMaxDamage => 46;

        public override int DefaultSpeed => 65;

        public override int InitMinHits => 200;

        public override int InitMaxHits => 200;

        public override bool AllowEquippedCast(Mobile from) => true;

        public override string DefaultName => "Kryssian Soulblade";
        
        public override WeaponAnimation DefaultAnimation => WeaponAnimation.Pierce1H;

        [Constructible]
        public KryssianSoulblade() : base(0x0F5E)
        {
            Layer = Layer.TwoHanded;
            Weight = 5.0;
            Hue = 1162;
            Identified = false;
        }

        public override void OnDoubleClick(Mobile from)
        {
            if (!from.Hidden)
            {
                from.SendFailureMessage("You must be hidden to backstab!");
                return;
            }
            
            from.SendSuccessMessage("Target something to backstab with this weapon.");

            from.Target = new InternalTarget(this);
        }

        private class InternalTarget : Target
        {
            private BaseMeleeWeapon m_Weapon;

            public InternalTarget(BaseMeleeWeapon item) : base(2, false, TargetFlags.None)
            {
                m_Weapon = item;
            }

            protected override void OnTarget(Mobile from, object targeted)
            {
                if (m_Weapon.Deleted)
                    return;
                
                if (!from.Hidden)
                {
                    from.SendFailureMessage("You must be hidden to backstab!");
                    return;
                }

                if (targeted is Mobile mobile)
                {
                    if (mobile.Direction == from.Direction && mobile.InRange(from, 1))
                    {
                        var damage = Math.Min(m_Weapon.ComputeDamage(from, mobile, m_Weapon) * 3, mobile.HitsMax / 2);
                        from.RevealingAction();
                        m_Weapon.PlaySwingAnimation(from);
                        mobile.Damage(damage, from);
                        
                        from.SendSuccessMessage($"You backstab {mobile.Name} for {damage} damage!");
                        mobile.SendFailureMessage($"{from.Name} backstabs you for {damage} damage!");
                    }
                    else
                    {
                        from.SendFailureMessage("You must be behind your target to backstab!");
                    }
                }
                else
                {
                    from.SendFailureMessage("You can only backstab mobiles!");
                }
            }
        }
    }
}