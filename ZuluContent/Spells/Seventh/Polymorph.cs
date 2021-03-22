using System;
using System.Threading.Tasks;
using Scripts.Zulu.Engines.Classes;
using Server.Gumps;
using Server.Items;
using ZuluContent.Zulu.Engines.Magic.Enchantments.Buffs;
using static Server.Gumps.PolymorphEntry;

namespace Server.Spells.Seventh
{
    public class PolymorphSpell : MagerySpell, IAsyncSpell
    {
        public static readonly PolymorphEntry[][] Groups =
        {
            new [] {
                Bird,
                Slime,
                Eagle,
                Mongbat,
                Headless,
                Gorilla,
                Ratman,
                GiantSpider,
                GiantSpider,
                GiantSpider
            },
            new [] {
                Scorpion,
                Orc,
                Zombie,
                Orc,
                LizardMan,
                Ghoul,
                GiantSerpent,
                Harpy,
                Harpy,
                Harpy,
            },
            new []
            {
                Ettin,
                Corpser,
                Gazer,
                EarthElemental,
                WaterElemental,
                FireElemental,
                AirElemental,
                Ent,
                Ent,
                Ent
            },
            new []
            {
                Ogre,
                Gargoyle,
                Liche,
                SeaSerpent,
                Daemon,
                Dragon,
                DaemonWithSword,
                Wisp,
                Dragon,
                Dragon
            },
            new []
            {
                SeaSerpent,
                Wisp,
                Liche,
                SeaSerpent,
                Daemon,
                Dragon,
                Daemon,
                Wisp,
                Dragon,
                Dragon
            }
            
        };
        
        public PolymorphSpell(Mobile caster, Item spellItem) : base(caster, spellItem) { }

        public override bool CanCast()
        {
            if (!base.CanCast())
                return false;
            
            if (DisguiseTimers.IsDisguised(Caster))
            {
                Caster.SendLocalizedMessage(502167); // You cannot polymorph while disguised.
                return false;
            }

            if (Caster.BodyMod == 183 || Caster.BodyMod == 184)
            {
                Caster.SendLocalizedMessage(1042512); // You cannot polymorph while wearing body paint
                return false;
            }

            return true;
        }
        
        public async Task CastAsync()
        {
            if (!Caster.CanBuff(Caster, icons: BuffIcon.Polymorph))
                return;

            var magery = Caster.Skills[SkillName.Magery].Value;

            var group = Utility.RandomMinMax(0, 1);
            var critter = Utility.RandomMinMax(0, 9);
            
            switch (magery)
            {
                case < 80:
                    group += 1;
                    break;
                case < 90:
                    group += 2;
                    break;
                case >= 90:
                    group = 3;
                    break;
            }

            if (Caster.GetClassBonus(SkillName.Magery) > 1.0)
            {
                group += 1;
                critter += 2;
            }

            group = Math.Clamp(group, 0, Groups.Length - 1);
            critter = Math.Clamp(critter, 0, Groups[group].Length - 1);
            
            
            // TODO: Use this for ebook Shape Shift?
            // var bodyId = await new NewPolymorphGump(Caster);
            var bodyId = Groups[group][critter].BodyId;
            
            if (bodyId <= 0)
                return;

            var hueMod = bodyId == SeaSerpent.BodyId ? 233 : 0;
            
            Caster.TryAddBuff(new Polymorph
            {
                Duration = SpellHelper.GetDuration(Caster, Caster),
                BodyMods = (bodyId, hueMod),
                Value = SpellHelper.GetModAmount(Caster, Caster, StatType.All)
            });
        }
    }
}