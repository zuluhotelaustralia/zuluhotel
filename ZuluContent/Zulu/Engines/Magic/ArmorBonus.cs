namespace ZuluContent.Zulu.Engines.Magic
{
    public enum ArmorBonus
    {
        None = 0,
        Iron,
        Steel,
        MeteoricSteel,
        Obsidian,
        Onyx,
        Adamantium,
    }
    
    public static class ArmorBonusExtensions
    {
        public static int GetHue(this ArmorBonus bonus)
        {
            // Using a switch in-case we use different colours later
            return bonus switch
            {
                _ => 1109
            };
        }
    }
}