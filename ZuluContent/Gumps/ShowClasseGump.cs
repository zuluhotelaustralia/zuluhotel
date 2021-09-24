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
            
            AddBackground(0, 0, 460, 430, 2620);
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

            var fontSize = 35;
            var textHeight = fontSize;
            var htmlMessage = $"<CENTER>{message}</CENTER>";
            
            AddBackground();

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
                    htmlMessage += $"<CENTER>In-classe skills are {totalInClassSkills}, Out-of-classe skills are {totalOutClassSkills}.</CENTER>";
                    textHeight += fontSize;
                    htmlMessage += $"<CENTER>You need {levelReqPercent}% of all skills in-classe, and have {currentPercent}% in-classe.</CENTER>";
                    textHeight += fontSize;
                }
                
                var nextLevelReqPercent = (int) (ZuluClass.GetClassLevelPercent(from.ZuluClass.Level + 1) * 100.0);
                var nextLevelInClassSkills = ZuluClass.MinSkills[from.ZuluClass.Level + 1];
                var nextLevelProgress = (int) (totalInClassSkills / nextLevelInClassSkills * 100.0);
                
                htmlMessage += $"<CENTER>Your requirements for level {from.ZuluClass.Level + 1} {classType.FriendlyName()} are:</CENTER>";
                textHeight += fontSize;
                htmlMessage += $"<CENTER>In-classe skills: {(int) nextLevelInClassSkills}.</CENTER>";
                textHeight += fontSize;
                htmlMessage += $"<CENTER>In-classe skills must account for {nextLevelReqPercent}% of total skills.</CENTER>";
                textHeight += fontSize;

                AddHtml(10, 50, 440, textHeight, $"<BASEFONT COLOR=#FFFFFF>{htmlMessage}</BASEFONT>", false,
                    false);
                
                AddLabel(185, 80 + textHeight, 590, "Progress");
                AddLabel(250, 80 + textHeight, 55, $"{nextLevelProgress}%");
                AddImage(175, 110 + textHeight, 0x806, 1155);
                AddImageTiled(175, 109 + textHeight, (int) (110.0 * nextLevelProgress / 100.0), 12, 0x809);
            }
            else
            {
                AddHtml(10, 50, 440, textHeight, $"<BASEFONT COLOR=#FFFFFF>{htmlMessage}</BASEFONT>", false,
                    false);
            }

            // Okay
            AddButton(200, 380, 249, 248, 0, GumpButtonType.Reply, 1);
        }
    }
}