using System;
using Server;
using Server.Network;

namespace Scripts.Zulu.Packets
{
  public class Packets
  {
    public static void Initialize()
    {
      CommandSystem.Register("PacketOverride", AccessLevel.GameMaster, PacketOverride);
      CommandSystem.Register("PacketOverrideSend", AccessLevel.GameMaster, PacketOverrideSend);
    }

    [Usage("PacketOverrideSend")]
    [Description("Sends a test packet.")]
    private static void PacketOverrideSend(CommandEventArgs e)
    {
      SendAsciiMessage(e.Mobile.NetState.ToString(), "A string");
    }

    [Usage("PacketOverride")]
    [Description("Overrides the packet.")]
    private static void PacketOverride(CommandEventArgs e)
    {
      OverridePacketHandler(ref Packets.SendAsciiMessage, baseHandler => (ns, s) =>
      {
        s = $"PacketOverride {s}";
        baseHandler(ns, s);
      });
    }

    //Packets.cs

    public static void _SendAsciiMessage(string x, string y)
    {
      Console.WriteLine($"{x} {y}");
    }


    public static Action<string, string> SendAsciiMessage = _SendAsciiMessage;
    public static void OverridePacketHandler<T>(ref T handler, Func<T, T> wrapFunc) where T : Delegate
    {
      handler = wrapFunc(handler);
    }

    static Action<string, string> OverrideSendAsciiMessage(Action<string, string> baseHandler)
    {
      void Handler(string s1, string s2)
      {
        s2 = $"Override {s2}";
        baseHandler(s1, s2);
      }

      return Handler;
    }

    //UOContent

    public static void Configure()
    {

      OverridePacketHandler(ref Packets.SendAsciiMessage, OverrideSendAsciiMessage);
    }
  }
}
