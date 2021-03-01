using ZuluContent.Zulu.Engines.Magic.Enchantments;

namespace Server.Items
{
    public abstract class BaseHat : BaseClothing
    {
        [CommandProperty(AccessLevel.GameMaster)]
        public ItemFortificationType Fortified
        {
            get => Enchantments.Get((ItemFortification e) => e.Value);
            set => Enchantments.Set((ItemFortification e) => e.Value = value);
        }

        public override double ArmorScalar => Fortified == ItemFortificationType.Fortified ? 0.15 : 0.5;

        public BaseHat(int itemID, int hue) : base(itemID, Layer.Helm, hue)
        {
        }

        public BaseHat(Serial serial) : base(serial)
        {
        }

        public void Fortify(BaseArmor helm)
        {
            MaxHitPoints = helm.MaxHitPoints;
            HitPoints = helm.HitPoints;
            BaseArmorRating = helm.BaseArmorRating;

            foreach (var (_, value) in helm.Enchantments.Values)
            {
                Enchantments.Set(value);
            }

            Fortified = ItemFortificationType.Fortified;

            helm.Delete();
        }

        public override void Serialize(IGenericWriter writer)
        {
            base.Serialize(writer);

            writer.Write((int) 1); // version
        }

        public override void Deserialize(IGenericReader reader)
        {
            base.Deserialize(reader);

            int version = reader.ReadInt();
        }
    }
}