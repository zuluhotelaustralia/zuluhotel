using System.Buffers;
using Server.Collections;
using static Scripts.Zulu.Utilities.ZuluUtil;

namespace Server.Gumps
{
    public class GumpJsonEntry : GumpEntry
    {
        public static readonly byte[] LayoutName = Gump.StringToBuffer("json");
        public object Data { get; set; }

        public GumpJsonEntry(Mobile player, object data)
        {
            Data = data;
        }

        public override string Compile(OrderedHashSet<string> strings) =>
            $"{{ json {strings.GetOrAdd(SerializeAsBase64Json(Data) ?? "")} }}";

        public override void AppendTo(ref SpanWriter writer, OrderedHashSet<string> strings, ref int entries, ref int switches)
        {
            writer.Write((ushort)0x7B20); // "{ "
            writer.Write(LayoutName);
            writer.WriteAscii(strings.GetOrAdd(SerializeAsBase64Json(Data) ?? "").ToString());
            writer.Write((ushort)0x207D); // " }"

            entries++;
        }
    }
}
