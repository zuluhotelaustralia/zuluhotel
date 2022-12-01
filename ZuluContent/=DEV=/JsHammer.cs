using System;
using System.Dynamic;
using System.Text.Json.Nodes;
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

    record WebGumpResponse(string Text, int Blah);

    private async void SendGump(Mobile from)
    {
        from.CloseGump<WebTestGump>();

        var gump = new WebTestGump();
        from.NetState.Gumps.Add(gump);

        var firstResponse = await gump.SendAsync<dynamic, JsonNode>(
            from.NetState, 
            new
            {
                serial = Serial.Value, 
                testData = "Hello"
            }
        );

        if (firstResponse is null)
        {
            Console.WriteLine("Gump was closed");
            return;
        }
        
        Console.WriteLine($"First Response from WebGump: {firstResponse}");

        var secondResponse = await gump.SendAsync<dynamic, JsonNode>(
            from.NetState, 
            new
            {
                serial = Serial.Value, 
                testData = "Second Hello", 
                echo = firstResponse["text"]?.GetValue<string>()
            }
        ); 
        
        if (secondResponse is null)
        {
            Console.WriteLine("Gump was closed");
            return;
        }

        Console.WriteLine($"Second Response from WebGump: {secondResponse}");

        gump.Close(from.NetState);
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