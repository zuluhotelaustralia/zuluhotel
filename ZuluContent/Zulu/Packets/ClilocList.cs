using System;
using System.IO;
using System.Text;
using System.Collections;
using System.Collections.Generic;
using Server;
using Server.Json;

namespace Scripts.Zulu.Packets
{
    public static class ClilocList
    {

        public static IReadOnlyDictionary<int, string> Entries { get; private set; }

        public static void Initialize()
        {
            Entries = Load("enu");
        }

        private static SortedDictionary<int, string> Load(string language)
        {
            var path = Core.FindDataFile($"Cliloc.{language}");

            using var bin = new BinaryReader(new PeekableStream(new FileStream(path, FileMode.Open, FileAccess.Read, FileShare.Read)));
            var stream = (PeekableStream)bin.BaseStream;
            bin.ReadInt32();
            bin.ReadInt16();

            var entries = new SortedDictionary<int, string>();

            var buf = new byte[1024];
            while (stream.PeekByte() != -1)
            {
                int number = bin.ReadInt32();
                bin.ReadByte();
                int length = bin.ReadInt16();

                if (length > buf.Length)
                    buf = new byte[(length + 1023) & ~1023];

                bin.Read(buf, 0, length);
                entries.Add(number, Encoding.UTF8.GetString(buf, 0, length));
            }
            
            Console.WriteLine($"Cliloc loaded: {path}");

            return entries;
        }

        public static string Translate(string baseCliloc, string arg = "", bool capitalize = false)
        {
            while (arg.Length != 0 && arg[0] == '\t')
                arg = arg.Remove(0, 1);

            List<string> arguments = new List<string>();

            for (int i = 0; i < arg.Length; i++)
            {
                if (arg[i] == '\t')
                {
                    arguments.Add(arg.Substring(0, i));
                    arg = arg.Substring(i + 1);
                    i = 0;
                }
            }

            bool has_arguments = arguments.Count != 0;

            arguments.Add(arg);

            int index = 0;
            while (true)
            {
                int pos = baseCliloc.IndexOf('~');

                if (pos == -1)
                    break;

                int pos2 = baseCliloc.IndexOf('~', pos + 1);

                if (pos2 == -1)
                    break;

                string a = index >= arguments.Count ? string.Empty : arguments[index];

                if (a.Length > 1)
                {
                    if (a[0] == '#')
                    {
                        if (int.TryParse(a.Substring(1), out int id1))
                            arguments[index] = Entries.ContainsKey(id1) ? Entries[id1] : string.Empty;
                        else
                            arguments[index] = a;
                    }
                    else if (has_arguments && int.TryParse(a, out int clil))
                    {
                        if (Entries.TryGetValue(clil, out string value) && !string.IsNullOrEmpty(value))
                            arguments[index] = value;
                    }
                }

                baseCliloc = baseCliloc.Remove(pos, pos2 - pos + 1).Insert(pos, index >= arguments.Count ? string.Empty : arguments[index]);
                index++;
            }

            return baseCliloc;
        }

        private class PeekableStream : Stream
        {
            bool hasPeek;
            Stream input;
            byte[] peeked;

            public PeekableStream(Stream input)
            {
                this.input = input;
                this.peeked = new byte[1];
            }

            public override bool CanRead
            {
                get { return input.CanRead; }
            }

            public override bool CanSeek
            {
                get { return input.CanSeek; }
            }

            public override bool CanWrite
            {
                get { return false; }
            }

            public override void Flush()
            {
                throw new NotSupportedException();
            }

            public override long Length
            {
                get { return input.Length; }
            }

            public int PeekByte()
            {
                if (!hasPeek)
                    hasPeek = Read(peeked, 0, 1) == 1;
                return hasPeek ? peeked[0] : -1;
            }

            public override int ReadByte()
            {
                if (hasPeek)
                {
                    hasPeek = false;
                    return peeked[0];
                }
                return base.ReadByte();
            }

            public override long Position
            {
                get
                {
                    if (hasPeek)
                        return input.Position - 1;
                    return input.Position;
                }
                set
                {
                    if (value != Position)
                    {
                        hasPeek = false;
                        input.Position = value;
                    }
                }
            }

            public override int Read(byte[] buffer, int offset, int count)
            {
                int read = 0;
                if (hasPeek && count > 0)
                {
                    hasPeek = false;
                    buffer[offset] = peeked[0];
                    offset++;
                    count--;
                    read++;
                }
                read += input.Read(buffer, offset, count);
                return read;
            }

            public override long Seek(long offset, SeekOrigin origin)
            {
                long val;
                if (hasPeek && origin == SeekOrigin.Current)
                    val = input.Seek(offset - 1, origin);
                else
                    val = input.Seek(offset, origin);
                hasPeek = false;
                return val;
            }

            public override void SetLength(long value)
            {
                throw new NotSupportedException();
            }

            public override void Write(byte[] buffer, int offset, int count)
            {
                throw new NotSupportedException();
            }
        }
    }


}
