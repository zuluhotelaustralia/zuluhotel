using Server;

namespace Scripts.Zulu.Engines.Classes
{
    public interface IShilCheckSkill
    {
        public bool CheckSkill(SkillName skill, int difficulty, int points);
    }
}