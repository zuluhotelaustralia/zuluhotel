using System;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using Server.Items;
using Server.Network;

namespace Server.Gumps;

public class JsHammer : BaseBashing
{
    [Constructible]
    public JsHammer() : base(0x13B3)
    {
        Hue = 1180;
        Layer = Layer.OneHanded;
    }


    [Constructible]
    public JsHammer(Serial serial) : base(serial)
    {
    }

    public override void OnSingleClick(Mobile from)
    {
        from.NetState.SendMessage(Serial, ItemID, MessageType.Label, 0, 3, true, null, "", "JS Gump Test");
    }

    public override void OnDoubleClick(Mobile from)
    {
        SendGump(from);
    }

    record JsGumpResponse(string Text, bool? Something, string LongString);

    private async void SendGump(Mobile from)
    {
        var response = await new JsGump<dynamic, JsGumpResponse>(
            "TestGump", 
            from, 
            new
            {
                serial = Serial.Value,
                testData = "Hello",
            }
        );

        Console.WriteLine($"Response from JsGump: {response}");
    }

    public override void Serialize(IGenericWriter writer)
    {
        base.Serialize(writer);
        writer.Write((int)0); // version
    }

    public override void Deserialize(IGenericReader reader)
    {
        base.Deserialize(reader);
        int version = reader.ReadInt();
    }
}