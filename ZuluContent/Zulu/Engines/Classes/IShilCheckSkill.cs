using Server;

namespace Scripts.Zulu.Engines.Classes
{
    public interface IShilCheckSkill
    {
        public bool CheckSkill(SkillName skill, int difficulty = -1, int points = 0);
    }
}