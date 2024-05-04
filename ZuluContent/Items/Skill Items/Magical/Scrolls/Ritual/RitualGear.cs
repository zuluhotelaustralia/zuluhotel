using ModernUO.Serialization;
using Scripts.Zulu.Utilities;
using Server.Mobiles;
using ZuluContent.Zulu.Items;

namespace Server.Items
{
    [Flipable]
    [SerializationGenerator(0, false)]
    public partial class RitualRobe : BaseOuterTorso
    {
        [SerializableField(0)] private Mobile _owner;

        public override int InitMinHits => 70;

        public override int InitMaxHits => 70;

        public override string DefaultName => "Ritual Robe";

        [Constructible]
        public RitualRobe() : base(0x1F03, 1304)
        {
            Weight = 3.0;
        }

        public static bool CheckRitualEquip(Mobile from, BaseEquippableItem itemToEquip)
        {
            for (var i = 1; i < 25; i++)
            {
                if (i is 9 or 11 or 15 or 16 or 21)
                {
                    continue;
                }

                if (from.FindItemOnLayer((Layer)i) != null)
                {
                    from.SendFailureMessage("You can't equip this while clothed like that.");
                    return false;
                }
            }

            if (itemToEquip is RitualRobe robe && (robe.Owner == null || robe.Owner != from))
            {
                from.SendFailureMessage("That item isn't consecrated.");
                return false;
            }

            if (itemToEquip is YoungOakStaff staff && (staff.Owner == null || staff.Owner != from))
            {
                from.SendFailureMessage("That item isn't consecrated.");
                return false;
            }

            return true;
        }

        public override bool OnEquip(Mobile from)
        {
            var canEquip = CheckRitualEquip(from, this);

            if (canEquip)
                ItemID = 0x204e;

            return canEquip;
        }

        public override void OnRemoved(IEntity parent)
        {
            ItemID = 0x1F03;
        }
    }

    [FlipableAttribute(0x13F8, 0x13F9)]
    [SerializationGenerator(0, false)]
    public partial class YoungOakStaff : BaseStaff
    {
        [SerializableField(0)] private Mobile _owner;

        public override int DefaultStrengthReq => 10;
        public override int DefaultMinDamage => 1;
        public override int DefaultMaxDamage => 1;
        public override int DefaultSpeed => 10;
        public override int InitMinHits => 50;
        public override int InitMaxHits => 50;

        [Constructible]
        public YoungOakStaff() : base(0x13F8)
        {
            Weight = 3.0;
            Layer = Layer.TwoHanded;
        }

        public override bool OnEquip(Mobile from)
        {
            return RitualRobe.CheckRitualEquip(from, this);
        }
    }
}