using System.Collections.Generic;
using Server.Network;

namespace Server.Misc
{
  public class Paperdoll
  {
    public static void Initialize()
    {
      EventSink.PaperdollRequest += EventSink_PaperdollRequest;
    }

    public static void EventSink_PaperdollRequest(Mobile beholder, Mobile beheld)
    {
      beholder.NetState.SendDisplayPaperdoll(beheld.Serial, Titles.ComputeTitle(beholder, beheld), beheld.Warmode, beheld.AllowEquipFrom(beholder));
    }
  }
}
