namespace Scripts.Zulu.Engines.Classes
{
    public static class SpecNameExtensions
    {
        public static string FriendlyName(this ZuluClassType spec)
        {
            return spec switch
            {
                ZuluClassType.Bard => "Bard",
                ZuluClassType.Crafter => "Crafter",
                ZuluClassType.Mage => "Mage",
                ZuluClassType.PowerPlayer => "Power Player",
                ZuluClassType.Ranger => "Ranger",
                ZuluClassType.Warrior => "Warrior",
                ZuluClassType.Thief => "Thief",
                _ => "None"
            };
        }
    }
}