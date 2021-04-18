namespace Scripts.Zulu.Engines.Races
{
    public static class SpecNameExtensions
    {
        public static string FriendlyName(this ZuluRaceType spec)
        {
            return spec switch
            {
                ZuluRaceType.Human => "Human",
                ZuluRaceType.Elf => "Elf",
                ZuluRaceType.DarkElf => "Dark-Elf",
                ZuluRaceType.Goblin => "Goblin",
                ZuluRaceType.Barbarian => "Barbarian",
                ZuluRaceType.Dwarf => "Dwarf",
                _ => "None"
            };
        }
    }
}