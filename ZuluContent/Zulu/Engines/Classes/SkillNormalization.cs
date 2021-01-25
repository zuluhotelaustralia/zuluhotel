using Server;

namespace Scripts.Zulu.Engines.Classes
{
    /**
     * Removes the 'non-real' bonuses to skills from stats.
     * Initialize gets run at server boot.
     */
    // ReSharper disable once UnusedType.Global
    public static class SkillNormalization
    {
        // ReSharper disable once UnusedMember.Global
        public static void Initialize()
        {
            foreach (var info in SkillInfo.Table)
            {
                info.DexScale = 0;
                info.StrScale = 0;
                info.IntScale = 0;
                info.StatTotal = 0;
            }
        }
    }
}