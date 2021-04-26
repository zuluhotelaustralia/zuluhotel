using System;
using Server.Utilities;

// ReSharper disable MemberCanBePrivate.Global
// ReSharper disable UnusedMember.Global

namespace Server.Mobiles
{
    public class BaseCreatureTemplate : BaseCreature
    {
        public string TemplateName { get; private set; }
        public CreatureProperties Properties => ZhConfig.Creatures.Entries[TemplateName];

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
        
        public static BaseCreatureTemplate Create(string templateName)
        {
            var properties = ZhConfig.Creatures.Entries[templateName];
            
            BaseCreatureTemplate creature;
            if (properties.BaseType != typeof(BaseCreatureTemplate))
            {
                if (!properties.BaseType.IsSubclassOf(typeof(BaseCreatureTemplate)))
                    return null;

                creature = properties.BaseType.CreateInstance<BaseCreatureTemplate>(templateName);
            }
            else
            {
                creature = new BaseCreatureTemplate(templateName);
            }

            return creature;
        }
    }
}