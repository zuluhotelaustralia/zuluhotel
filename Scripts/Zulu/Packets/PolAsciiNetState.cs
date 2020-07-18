using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using Server;
using Server.Items;
using Server.Network;


namespace RunZH.Scripts.Zulu.Packets
{
    /**
     * This NetState implementation is used to emulate the POL "Look and Feel" which comes from using ASCII
     * messages for displaying single click item names instead of Unicode.
     *
     * Usage: alter `Server/Network/MessagePump.cs` and replace `new NetState` with `new PolAsciiNetState`
     */
    public class PolAsciiNetState : NetState
    {
        public PolAsciiNetState(Socket socket, MessagePump messagePump) : base(socket, messagePump)
        {
        }
        
        public override void Send(Packet p)
        {
            // Only rewrite ASCII for English clients
            if (Mobile?.Language == "ENU") 
            {
                switch (p)
                {
                    case UnicodeMessage _:
                        RewriteUnicodeMessage(ref p);
                        break;
                    case DisplayEquipmentInfo _:
                        RewriteEquipmentInfo(ref p);
                        break;
                    case MessageLocalized _:
                        RewriteMessageLocalized(ref p);
                        break;
                    case MessageLocalizedAffix _:
                        RewriteMessageLocalized(ref p);
                        break;
                }
            }
            
            base.Send(p);
        }
        
        private static PacketReader GetReader(Packet p)
        {
            var bytes = p.UnderlyingStream.ToArray();
            var reader = new PacketReader(bytes, bytes.Length, false);

            return reader;
        }
        

        private static void RewriteUnicodeMessage(ref Packet p)
        {
            var reader = GetReader(p);

            var serial = reader.ReadInt32();
            var graphic = reader.ReadInt16();
            var type = (MessageType)reader.ReadByte();
            var hue = reader.ReadInt16();
            var font = reader.ReadInt16();
            var lang = reader.ReadString(4);
            var name = reader.ReadString(30);
            var text = reader.ReadUnicodeString();

            p = new AsciiMessage(serial, graphic, type, hue, font, name, text);
        }
        
        private static void RewriteEquipmentInfo(ref Packet p)
        {
            var reader = GetReader(p);
            
            var sub = reader.ReadInt16();
            var serial = reader.ReadInt32();
            var label = reader.ReadInt32();

            var item = World.FindItem(serial);

            if (!ClilocList.Entries.TryGetValue(label, out var text) || item == null)
                return;

            var res = item switch
            {
                BaseClothing clothing => clothing.Resource,
                BaseWeapon weapon => weapon.Resource,
                BaseArmor armor => armor.Resource,
                BaseJewel jewel => jewel.Resource,
                _ => CraftResource.None
            };

            if (res != CraftResource.None)
                text = $"{CraftResources.GetName(res)} {text}";

            p = new AsciiMessage(serial, item.ItemID, MessageType.Label, 0x3B2, 3, string.Empty, text);
        }

        private static void RewriteMessageLocalized(ref Packet p)
        {
            var reader = GetReader(p);
            
            var serial = reader.ReadInt32();
            var graphic = reader.ReadInt16();
            var type = (MessageType)reader.ReadByte();
            var hue = reader.ReadInt16();
            var font = reader.ReadInt16();
            var number = reader.ReadInt32();
            var flags = p is MessageLocalizedAffix ? (AffixType) reader.ReadByte() : AffixType.System;
            var name = reader.ReadString(30);
            var affix = p is MessageLocalizedAffix ? reader.ReadString() : string.Empty;
            var args = p is MessageLocalizedAffix ? reader.ReadUnicodeString() : reader.ReadUnicodeStringLE();

            if (!ClilocList.Entries.TryGetValue(number, out var cliloc))
                return;

            var text = ClilocList.Translate(cliloc, args);
            
            if (p is MessageLocalizedAffix)
            {
                text = flags switch
                {
                    AffixType.Append => $"{text}{affix}",
                    AffixType.Prepend => $"{affix}{text}",
                    _ => $"{affix}{text}"
                };
                
                if ((flags & AffixType.System) != 0)
                    type = MessageType.System;
            }

            p = new AsciiMessage(serial, graphic, type, hue, font, name, text);
        }

    }
}