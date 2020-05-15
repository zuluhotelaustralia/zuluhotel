using System;
using System.IO;
using Server;
using Server.Accounting;

namespace Server.Commands
{
    public class PlaySFX
    {
        public static void Initialize()
        {
            CommandSystem.Register("PlaySFX", AccessLevel.Developer, new CommandEventHandler(PlaySFX_OnCommand));
        }

        [Usage("PlaySFX <sfx number>")]
        [Description("Plays a sound effect.")]
        private static void PlaySFX_OnCommand(CommandEventArgs e)
        {
            if (e.Length >= 1)
            {
                e.Mobile.PlaySound(e.GetInt32(0));
            }
        }
    }

    public class PlayParticles
    {
        public static void Initialize()
        {
            CommandSystem.Register("PlayParticles", AccessLevel.Developer, new CommandEventHandler(PlayParticles_OnCommand));
        }

        [Usage("PlayParticles <id>")]
        [Description("Plays a sound effect.")]
        private static void PlayParticles_OnCommand(CommandEventArgs e)
        {

            // 	public void FixedParticles( int itemID, int speed, int duration, int effect, EffectLayer layer )
            //  e.g.  m.FixedParticles( 0x3709, 10, 30, 5052, EffectLayer.LeftFoot ); //flamestrike effect
            if (e.Length >= 1)
            {
                e.Mobile.FixedParticles(e.GetInt32(0), 10, 30, 5052, EffectLayer.LeftFoot);
            }
        }
    }

    public class PlayEffect
    {
        public static void Initialize()
        {
            CommandSystem.Register("PlayEffect", AccessLevel.Developer, new CommandEventHandler(PlayEffect_OnCommand));
        }

        [Usage("PlayEffect <sfx number>")]
        [Description("Plays an effect.")]
        private static void PlayEffect_OnCommand(CommandEventArgs e)
        {
            if (e.Length >= 1)
            {
                e.Mobile.FixedEffect(e.GetInt32(0), 6, 1);
            }
        }
    }
}
