namespace ZuluContent.Zulu.Items
{
    public interface IArmorRating
    {
        public int BaseArmorRating { get; set; }
        public double BaseArmorRatingScaled { get; }
        public double ArmorRating { get; }
        public double ArmorRatingScaled { get; }
    }
}