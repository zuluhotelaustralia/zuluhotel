using System;
using Scripts.Zulu.Engines.Classes;
using Scripts.Zulu.Utilities;
using Server.Items;
using Server.Mobiles;
using Server.Network;
using Server.Spells;

namespace Server.Gumps
{
    public class TrainSkillsGump : Gump
    {
        private readonly SkillTrainingDeed m_Deed;
        private readonly SkillName m_Teaching;
        private readonly int m_PointsToLearn;
        private readonly BaseCreature m_Teacher;
        
        private void AddBackground()
        {
            AddPage(0);
            
            AddBackground(0, 0, 300, 300, 2620);
        }

        public TrainSkillsGump(Mobile from, BaseCreature teacher, SkillTrainingDeed deed, SkillName teaching, int pointsToLearn) : base(250, 200)
        {
            m_Teacher = teacher;
            m_Deed = deed;
            m_Teaching = teaching;
            m_PointsToLearn = pointsToLearn;
            
            AddBackground();
            AddImage(60, -10, 2446);
            AddLabel(120, -8, 50, "Train Skill");
            
            AddLabel(120, 30, 199, teaching.ToString());
            
            AddLabel(20, 70, 60, "Current Skill Value");
            AddLabel(200, 70, 60, from.Skills[teaching].Value.ToString());
            
            AddLabel(20, 100, 189, "Maximum Skill Value");
            AddLabel(200, 100, 189, (pointsToLearn / 10).ToString());
            
            AddImageTiled(5, 150, 290, 5, 5121);

            AddItem(20, 190, 0x0EEF);
            AddLabel(70, 192, 50, "Gold credit remaining");
            AddLabel(220, 192, 155, deed.Credits.ToString());
            
            AddButton(35, 245, 2103, 2104, 1);
            AddLabel(70, 242, 50, "Amount to use");
            AddImage(213, 240, 2444);
            AddTextEntry(220, 242, 40, 30, 155, 0, Math.Min(pointsToLearn, deed.Credits).ToString(), 3);
        }

        public override void OnResponse(NetState state, RelayInfo info)
        {
            if (info.ButtonID == 1 && state.Mobile is PlayerMobile playerMobile)
            {
                Int32.TryParse(info.GetTextEntry(0).Text.Trim(), out var creditsNumber);
                
                if (creditsNumber == 0)
                {
                    playerMobile.SendFailureMessage("That is not a valid amount.");
                    playerMobile.SendGump(new TrainSkillsGump(playerMobile, m_Teacher, m_Deed, m_Teaching,
                        m_PointsToLearn));
                    return;
                }
                
                if (creditsNumber > m_PointsToLearn)
                {
                    playerMobile.SendFailureMessage("You cannot train your skill that high.");
                    playerMobile.SendGump(new TrainSkillsGump(playerMobile, m_Teacher, m_Deed, m_Teaching,
                        m_PointsToLearn));
                    return;
                }
                
                if (creditsNumber > m_Deed.Credits)
                {
                    playerMobile.SendFailureMessage("You do not have enough credits for that amount.");
                    playerMobile.SendGump(new TrainSkillsGump(playerMobile, m_Teacher, m_Deed, m_Teaching,
                        m_PointsToLearn));
                    return;
                }

                if (m_Teacher.Teach(m_Teaching, playerMobile, creditsNumber, true))
                {
                    m_Deed.Credits -= creditsNumber;
                    if (m_Deed.Credits == 0)
                        m_Deed.Delete();
                }
            }
        }
    }
}