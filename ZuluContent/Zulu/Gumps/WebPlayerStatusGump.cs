using Server.Engines.Magic;
using Server.Mobiles;
using Server.Spells;

namespace Server.Gumps;

public class WebPlayerStatusGump : WebGump
{
    public WebPlayerStatusGump() : base(nameof(WebPlayerStatusGump))
    {
    }

    private record WebPlayerStatus(string Name, int NameHue, bool Female, int Hits, int HitsMax, bool IsPoisoned, bool IsYellowHits, int Str, int Dex, int Int, int Stam, int StamMax, int Mana, int ManaMax,
        int TotalGold, int PhysicalResistance, double ArmorRating, int TotalWeight, int MaxWeight, string Race,
        int StatCap, int Followers, int FollowersMax, int FireResistance, int ColdResistance, int PoisonResistance,
        int EnergyResistance, int Luck, int TithingPoints, int Hunger, int HealingBonus, int MagicImmunity,
        int MagicReflection, int PhysicalResist, PoisonLevel PoisonImmunity, int FireResist, int WaterResist,
        int AirResist, int EarthResist, int NecroResist, int ShortTermMurders, int Kills, int MinDamage, int MaxDamage);

    public static async void Update(Mobile target)
    {
        if (target is not PlayerMobile player)
            return;

        var gump = player.FindGump<WebPlayerStatusGump>() as WebPlayerStatusGump ?? new WebPlayerStatusGump();

        int minDamage = 0, maxDamage = 0;
        player.Weapon?.GetStatusDamage(player, out minDamage, out maxDamage);
        var payload = new WebPlayerStatus(player.Name, player.NameHue, player.Female, player.Hits, player.HitsMax, player.Poisoned, player.YellowHealthbar, player.Str, player.Dex, player.Int, player.Stam,
            player.StamMax, player.Mana, player.ManaMax, player.TotalGold, player.PhysicalResistance,
            player.ArmorRating, player.TotalWeight, player.MaxWeight, player.Race.Name, player.StatCap, player.Followers,
            player.FollowersMax, player.FireResistance, player.ColdResistance, player.PoisonResistance,
            player.EnergyResistance, player.Luck, player.TithingPoints, player.Hunger, player.HealingBonus,
            player.MagicImmunity, player.MagicReflection, player.PhysicalResist, player.PoisonImmunity,
            player.FireResist, player.WaterResist, player.AirResist, player.EarthResist, player.NecroResist,
            player.ShortTermMurders, player.Kills, minDamage, maxDamage);

        gump.Send(player.NetState, payload);
    }
}