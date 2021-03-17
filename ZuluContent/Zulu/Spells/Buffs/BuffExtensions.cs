using System;
using System.Linq;
using Scripts.Zulu.Packets;
using ZuluContent.Zulu.Engines.Magic.Enchantments.Buffs;

namespace Server.Spells
{
    public static class BuffExtensions
    {
        public static bool CanBuff(this Mobile caster, Mobile target, bool notify = true, params BuffIcon[] icons)
        {
            if (target.HasOneOfBuffs(icons))
            {
                if (caster != null && notify)
                    caster.SendLocalizedMessage(caster == target
                            ? 502173 // You are already under a similar effect.
                            : 1156094 // Your target is already under the effect of this ability.
                    );

                return false;
            }

            return true;
        }

        public static bool HasOneOfBuffs(this Mobile target, params BuffIcon[] buffIcons)
        {
            return (target as IBuffable)?.BuffManager.Values.Any(b => buffIcons.Contains(b.Icon)) == true;
        }
        
        public static bool HasBuff(this Mobile target, BuffIcon icon)
        {
            return (target as IBuffable)?.BuffManager.HasBuff(icon) == true;
        }

        public static bool TryAddBuff(this Mobile target, IBuff buff)
        {
            return (target as IBuffable)?.BuffManager.TryAddBuff(buff) == true;
        }
    }
}