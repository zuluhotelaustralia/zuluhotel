using System;
using System.Buffers;
using System.Linq;
using System.Text;
using Server.Misc;
using Server.Network;

namespace Server.Spells
{
    public interface IBuff
    {
        public const int BlankCliloc = 1114057; // ~1_val~
        public static int TitleFontSize = 4;
        public static int DescriptionFontSize = 4;
        public static int DetailsFontSize = 3;

        public BuffIcon Icon { get; init; }
        public string Title { get; init; }
        public string Description { get; init; }
        public string[] Details { get; init; }
        public bool ExpireOnDeath { get; init; }
        public bool Dispellable { get; init; }
        public TimeSpan Duration { get; init; }
        public DateTime Start { get; init; }
        
        public void OnBuffAdded(Mobile parent);
        public void OnBuffRemoved(Mobile parent);

        public static string[] GetDefaultDetails(IBuff buff) => new []
        {
            buff.ExpireOnDeath ? "Expires on death" : "Retained on death",
            buff.Dispellable ? "Dispellable" : "Undispellable",
        };
        
        #region Packets

        private string TemplatePacketArgs()
        {
            var details = GetDefaultDetails(this).Concat(Details ?? Array.Empty<string>());
            // Easier to build the string this way as the tooltips seem to be whitespace sensitive
            var sb = new StringBuilder();
            sb.Append("<body>");
            sb.Append($"<basefont SIZE={TitleFontSize}>{Title}</basefont><br>");
            sb.Append("<div align=left>");
            sb.Append($"<basefont SIZE={DescriptionFontSize}>{Description}</basefont><br><br>");
            sb.Append($"<basefont SIZE={DetailsFontSize}>{string.Join("<br>", details)}</basefont><br>");
            sb.Append("</div></body>");

            return sb.ToString();
        }
        
        public void SendAddBuffPacket(Mobile mobile) => SendAddBuffPacket(
            mobile.NetState,
            mobile.Serial,
            Icon,
            0,
            BlankCliloc,
            TemplatePacketArgs(),
            Start != DateTime.MinValue ? Start + Duration - DateTime.UtcNow : TimeSpan.Zero
        );

        private static void SendAddBuffPacket(
            NetState ns,
            Serial mob,
            BuffIcon iconId,
            int titleCliloc,
            int secondaryCliloc,
            TextDefinition args,
            TimeSpan ts
        )
        {
            if (ns == null)
                return;

            var hasArgs = args != null;
            var length = hasArgs ? args.ToString().Length * 2 + 52 : 46;
            var writer = new SpanWriter(stackalloc byte[length]);
            writer.Write((byte) 0xDF); // Packet ID
            writer.Write((ushort) length);
            writer.Write(mob);
            writer.Write((short) iconId);
            writer.Write((short) 0x1); // command (0 = remove, 1 = add, 2 = data)
            writer.Write(0);

            writer.Write((short) iconId);
            writer.Write((short) 0x1); // command (0 = remove, 1 = add, 2 = data)
            writer.Write(0);
            writer.Write((short) (ts <= TimeSpan.Zero ? 0 : ts.TotalSeconds));
            writer.Clear(3);
            writer.Write(titleCliloc);
            writer.Write(secondaryCliloc);

            if (hasArgs)
            {
                writer.Write(0);
                writer.Write((short) 0x1);
                writer.Write((ushort) 0);
                writer.WriteLE('\t');
                writer.WriteLittleUniNull(args);
                writer.Write((short) 0x1);
                writer.Write((ushort) 0);
            }
            else
            {
                writer.Clear(10);
            }

            ns.Send(writer.Span);
        }

        public void SendRemoveBuffPacket(Mobile mobile) =>
            SendRemoveBuffPacket(mobile.NetState, mobile.Serial, Icon);

        private static void SendRemoveBuffPacket(NetState ns, Serial mob, BuffIcon iconId)
        {
            if (ns == null)
                return;

            var writer = new SpanWriter(stackalloc byte[15]);
            writer.Write((byte) 0xDF); // Packet ID
            writer.Write((ushort) 15);
            writer.Write(mob);
            writer.Write((short) iconId);
            writer.Write((short) 0x0); // command (0 = remove, 1 = add, 2 = data)
            writer.Write(0);

            ns.Send(writer.Span);
        }
        #endregion

    }
}