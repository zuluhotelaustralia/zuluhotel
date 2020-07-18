namespace RunZH.Scripts.Zulu.Engines.Classes
{
    public static class SpecNameExtensions
    {
        public static string FriendlyName(this SpecName spec)
        {
            return spec switch
            {
                SpecName.Bard => "Bard",
                SpecName.Crafter => "Crafter",
                SpecName.Mage => "Mage",
                SpecName.Powerplayer => "Power Player",
                SpecName.Ranger => "Ranger",
                SpecName.Warrior => "Warrior",
                SpecName.Thief => "Thief",
                _ => "None"
            };
        }
    }
}