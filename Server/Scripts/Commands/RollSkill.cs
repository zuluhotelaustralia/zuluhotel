using System;
using System.Reflection;
using Server.Targeting;
using Server;

namespace Server.Commands
{
    public class RollSkill
    {
        public static void Initialize()
        {
            CommandSystem.Register( "RollSkill", AccessLevel.Developer, new CommandEventHandler( RollSkill_OnCommand ) );
            CommandSystem.Register( "TrainSkill", AccessLevel.Developer, new CommandEventHandler( TrainSkill_OnCommand ) );
        }

        [Usage( "RollSkill [skill] [attempts]" )]
            [Description( "Attempts a gain a specific skill" )]
            private static void RollSkill_OnCommand( CommandEventArgs e )
        {
            SkillName skill;

            if ( ! System.Enum.TryParse(e.GetString(0), true, out skill) ) {
                e.Mobile.SendMessage( "Unknown Skill Name {0}", e.GetString(0) );
            }

            int attempts = 1;
            if ( e.Length >= 2 ) {
                attempts = e.GetInt32(1);
            }

            e.Mobile.SendMessage( "Rolling {0} {1} times.",
                                  Enum.GetName(typeof(SkillName), skill),
                                  attempts);

            // We just use 0.5 (50%) as the "chance" value as that doesn't
            // appear to be a factor in the gain chance calculation.
                    
            for ( int i = 0 ; i < attempts ; i++ ) {
                Server.Misc.SkillCheck.CheckSkill(e.Mobile,
                                                  e.Mobile.Skills[skill],
                                                  null /* amObj? wtf is it for? */,
                                                  0.5 /* chance */);
            }
        }

        [Usage( "TrainSkill [skill] [target]" )]
            [Description( "Gains a specific skill until we hit the target value or we've tried 100k times" )]
            private static void TrainSkill_OnCommand( CommandEventArgs e )
        {
            SkillName skill;

            if ( e.Length < 2 ) {
                e.Mobile.SendMessage("Usage TrainSkill [skill] [target]");
            }

            if ( ! System.Enum.TryParse(e.GetString(0), true, out skill) ) {
                e.Mobile.SendMessage( "Unknown Skill Name {0}", e.GetString(0) );
            }
            
            int target = e.GetInt32(1);
            int attempts = 0;
            
            // We just use 0.5 (50%) as the "chance" value as that doesn't
            // appear to be a factor in the gain chance calculation.
                    
            for ( int i = 0 ; i < 100000 ; i++ ) {
                attempts++;
                Server.Misc.SkillCheck.CheckSkill(e.Mobile,
                                                  e.Mobile.Skills[skill],
                                                  null /* amObj? wtf is it for? */,
                                                  0.5 /* chance */);
                if ( e.Mobile.Skills[skill].BaseFixedPoint >= target ) {
                    e.Mobile.SendMessage("Achieved after {0} rolls.", attempts);
                    return;
                }
            }

            e.Mobile.SendMessage("Aborting after {0} rolls, only got to {1}", attempts, e.Mobile.Skills[skill].Base);
        }
    }
}
