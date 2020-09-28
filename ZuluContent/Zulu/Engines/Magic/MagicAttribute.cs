using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.InteropServices;
using Server;
using ZuluContent.Zulu.Engines.Magic;

namespace Scripts.Engines.Magic
{
    public class MagicAttribute<T> : IMagicMod<T> where T : unmanaged
    {
        public MagicProp Prop { get; }
        public MagicInfo Info => MagicInfo.MagicInfoMap.GetValueOrDefault(Prop);
        public string EnchantName
        {
            get
            {
                if (Info == null)
                    return string.Empty;
                
                return Target switch
                {
                    Enum @enum => Info.GetName(@enum, Cursed),
                    int i => Info.GetName(i, Cursed),
                    _ => Info.GetName(0, Cursed)
                };
            }
        }

        public bool Cursed { get; set; }
        public EnchantNameType Place { get; }
        public T Target { get; set; }
        
        public MagicAttribute(MagicProp prop, T type)
        {
            Prop = prop;
            Place = EnchantNameType.Prefix;
            Target = type;
        }

        public void Remove()
        {
        }

        public void AddTo(Mobile mobile)
        {
        }

        public MagicAttribute<TAttr> IntToEnumAttr<TAttr>() where TAttr : unmanaged
        {
            if (Target is int i && IsEnum<TAttr>())
                return new MagicAttribute<TAttr>(Prop, (TAttr) Enum.ToObject(typeof(TAttr), i));

            return null;
        }

        private static bool IsEnum<TInput>() => typeof(TInput).IsSubclassOf(typeof(Enum));

        public static explicit operator T(MagicAttribute<T> attr) => attr.Target;

    }
}