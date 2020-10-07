using System;

namespace ZuluContent.Zulu.Engines.Magic.Enums.Attributes
{
    [AttributeUsage(AttributeTargets.Method | AttributeTargets.Property)]
    public class EnchantActionPriority : Attribute
    {
        public EnchantActionPriority(int priority) => Priority = priority;

        public int Priority { get; set; }
    }
}