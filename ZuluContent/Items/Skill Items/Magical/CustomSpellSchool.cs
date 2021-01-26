namespace Server.Spells
{
    public enum CustomSpellSchoolType
    {
        Earth
    }

    [System.AttributeUsage(System.AttributeTargets.Class |
                           System.AttributeTargets.Struct)
    ]
    public class CustomSpellSchool : System.Attribute
    {
        private CustomSpellSchoolType m_School;

        public CustomSpellSchoolType School
        {
            get => m_School;
        }

        public CustomSpellSchool(CustomSpellSchoolType school)
        {
            m_School = school;
        }
    }
}