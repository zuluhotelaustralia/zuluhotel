using System;
using Server.Network;
using ZuluContent.Configuration.Types.Rituals;

namespace Server.Gumps
{
    public class RitualWordsGump : Gump
    {
        private RitualSettings[] m_RitualData;
        private string m_RitualType;
        
        private void AddBackground()
        {
            AddPage(0);
            
            AddBackground(0, 0, 500, 430, 2620);
        }

        public RitualWordsGump(Mobile from, string ritualType) : base(250, 200)
        {
            AddBackground();

            m_RitualType = ritualType;
            m_RitualData = ZhConfig.Rituals.RitualsSettings[ritualType];

            for (var i = 0; i < m_RitualData.Length; i++)
            {
                AddHtml(10, 50 + (i * 30), 440, 35, $"<BASEFONT COLOR=#FFFFFF><CENTER>{m_RitualData[i].Speech}</CENTER></BASEFONT>", false,
                    false);
                AddButton(450, 50 + (i * 30), 4014, 4015, 2 + i);
            }
            
            // Okay
            AddButton(200, 380, 249, 248, 0, GumpButtonType.Reply, 1);
        }
        
        public override void OnResponse( NetState state, RelayInfo info )
		{
			var from = state.Mobile;

            var buttonId = info.ButtonID - 2;

			if(buttonId >= 0)
			{
                from.DoSpeech(m_RitualData[buttonId].Speech, Array.Empty<int>(), MessageType.Regular, Utility.ClipDyedHue(55));
                from.SendGump(new RitualWordsGump(from, m_RitualType));
            }
		}
    }
}