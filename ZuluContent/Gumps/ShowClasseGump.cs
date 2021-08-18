using System.Linq;
using Scripts.Zulu.Engines.Classes;
using Scripts.Zulu.Utilities;
using Server.Items;
using Server.Mobiles;
using Server.Network;
using Server.Spells;

namespace Server.Gumps
{
    public class ShowClasseGump : Gump
    {
        private void AddBackground()
        {
            AddPage(0);
            
            AddBackground(0, 0, 460, 350, 83);
        }

        public ShowClasseGump(PlayerMobile from) : base(250, 200)
        {
            from.ZuluClass.ComputeClass();
            
            string message;
            var classType = ZuluClassType.None;

            if (from.ZuluClass.Type == ZuluClassType.None)
            {
                if (from.TargetZuluClass != ZuluClassType.None)
                {
                    classType = from.TargetZuluClass;
                    message = $"You are currently a level 0 {classType.FriendlyName()}.";
                }
                else message = "You aren't a member of any particular class.";
                    
            }
            else
            {
                classType = from.ZuluClass.Type;
                message = $"You are a qualified level {from.ZuluClass.Level} {classType.FriendlyName()}.";
            }
            
            AddBackground();

            AddLabel(40, 50, 590, message);

            var offset = 0;

            if (classType != ZuluClassType.None)
            {
                var allSkillsTotal = 0.0;
                foreach (var skill in from.Skills)
                {
                    allSkillsTotal += skill.Value;
                }
                var classSkills = ZuluClass.ClassSkills[classType];
                var totalInClassSkills = (int) classSkills.Select(s => from.Skills[s].Value).Sum();
                var totalOutClassSkills = (int) allSkillsTotal - totalInClassSkills;
                var levelReqPercent = (int) (ZuluClass.GetClassLevelPercent(from.ZuluClass.Level) * 100.0);
                var currentPercent = (int) (totalInClassSkills / allSkillsTotal * 100);
                
                if (from.ZuluClass.Level > 0)
                {
                    AddLabel(40, 70, 590, $"In-classe skills are {totalInClassSkills}, Out-of-classe skills are {totalOutClassSkills}.");
                    AddLabel(40, 90, 590, $"You need {levelReqPercent}% of all skills in-classe, and have {currentPercent}% in-classe.");
                    offset = 40;
                }
                
                var nextLevelReqPercent = (int) (ZuluClass.GetClassLevelPercent(from.ZuluClass.Level + 1) * 100.0);
                var nextLevelInClassSkills = ZuluClass.MinSkills[from.ZuluClass.Level + 1];
                var nextLevelProgress = (int) (totalInClassSkills / nextLevelInClassSkills * 100.0);
                
                AddLabel(40, 90 + offset, 590, $"Your requirements for level {from.ZuluClass.Level + 1} {classType.FriendlyName()} are:");
                AddLabel(40, 110 + offset, 590, $"In-classe skills: {(int) nextLevelInClassSkills}.");
                AddLabel(40, 130 + offset, 590, $"In-classe skills must account for {nextLevelReqPercent}% of total skills.");
                
                AddLabel(185, 170 + offset, 590, "Progress");
                AddLabel(250, 170 + offset, 55, $"{nextLevelProgress}%");
                AddImage(175, 200 + offset, 0x806, 1155);
                AddImageTiled(175, 200 + offset, (int) (110.0 * nextLevelProgress / 100.0), 12, 0x809);
            }

            // Okay
            AddButton(200, 300, 249, 248, 0, GumpButtonType.Reply, 1);
        }
    }
}