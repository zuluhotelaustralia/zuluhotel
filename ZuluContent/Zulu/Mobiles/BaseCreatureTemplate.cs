using System;
using Server.Utilities;

// ReSharper disable MemberCanBePrivate.Global
// ReSharper disable UnusedMember.Global

namespace Server.Mobiles
{
    public class BaseCreatureTemplate : BaseCreature
    {
        public string TemplateName { get; private set; }
        public CreatureProperties Properties => TemplateName != null ? ZhConfig.Creatures.Entries[TemplateName] : null;

        [Constructible]
        public BaseCreatureTemplate(string templateName) : base(ZhConfig.Creatures.Entries[templateName])
        {
            TemplateName = templateName;
        }

        public BaseCreatureTemplate(Serial serial) : base(serial)
        {
        }

        public override void Serialize(IGenericWriter writer)
        {
            base.Serialize(writer);
            writer.Write(0); // version
            writer.Write(TemplateName);
        }

        public override void Deserialize(IGenericReader reader)
        {
            base.Deserialize(reader);
            var version = reader.ReadInt();
            
            switch (version)
            {
                case 0:
                {
                    TemplateName = reader.ReadString();
                    if (ZhConfig.Creatures.Entries.TryGetValue(TemplateName, out var props))
                    {
                        props.ApplyTo(this);
                    }
                    else
                    {
                        Console.WriteLine($"Could not find creature template matching {TemplateName}, deleting Mobile {Serial}");
                        Timer.DelayCall(TimeSpan.Zero, Delete);
                    }
                    break;
                }
            }
        }
    }
}