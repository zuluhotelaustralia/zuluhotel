namespace Server.Mobiles
{
    /// <summary>
    /// Summary description for MobileAI.
    /// </summary>
    ///
    public enum FightMode
    {
        None, // Never focus on others
        Aggressor, // Only attack aggressors
        Strongest, // Attack the strongest
        Weakest, // Attack the weakest
        Closest, // Attack the closest
        Evil // Only attack aggressor -or- negative karma
    }
}