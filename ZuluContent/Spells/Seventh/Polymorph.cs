using System;
using System.Threading.Tasks;
using Server.Gumps;
using Server.Items;
using ZuluContent.Zulu.Engines.Magic.Enchantments.Buffs;

namespace Server.Spells.Seventh
{
    public class PolymorphSpell : MagerySpell, IAsyncSpell
    {
        public static readonly PolymorphCategory[] Categories =
        {
            new(1015235, // Animals
                PolymorphEntry.Chicken,
                PolymorphEntry.Dog,
                PolymorphEntry.Wolf,
                PolymorphEntry.Panther,
                PolymorphEntry.Gorilla,
                PolymorphEntry.BlackBear,
                PolymorphEntry.GrizzlyBear,
                PolymorphEntry.PolarBear,
                PolymorphEntry.HumanMale
            ),

            new(1015245, // Monsters
                PolymorphEntry.Slime,
                PolymorphEntry.Orc,
                PolymorphEntry.LizardMan,
                PolymorphEntry.Gargoyle,
                PolymorphEntry.Ogre,
                PolymorphEntry.Troll,
                PolymorphEntry.Ettin,
                PolymorphEntry.Daemon,
                PolymorphEntry.HumanFemale
            )
        };
        
        public PolymorphSpell(Mobile caster, Item spellItem) : base(caster, spellItem) { }

        public override bool CanCast()
        {
            if (!base.CanCast())
                return false;

            if (Caster.Mounted)
            {
                Caster.SendLocalizedMessage(1042561); //Please dismount first.
                return false;
            }

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

            var gump = new PolymorphGump(Caster, Categories);
            var bodyId = await gump;

            if (bodyId <= 0)
                return;

            var body = (Body) bodyId;

            if (!body.IsHuman)
            {
                var mt = Caster.Mount;

                if (mt != null)
                    mt.Rider = null;
            }

            var hueMod = bodyId == 400 || bodyId == 401 ? Race.DefaultRace.RandomSkinHue() : 0;
            
            Caster.TryAddBuff(new Polymorph
            {
                Duration = SpellHelper.GetDuration(Caster, Caster),
                BodyMods = (bodyId, hueMod),
                Value = SpellHelper.GetModAmount(Caster, Caster, StatType.All)
            });
        }
    }
}