using System;
using System.Threading.Tasks;
using Server.Items;
using Server.Mobiles;
using Server.Network;
using Server.Spells.Seventh;
using ZuluContent.Zulu.Engines.Magic.Enchantments.Buffs;

namespace Server.Spells.Fifth
{
    public class IncognitoSpell : MagerySpell, IAsyncSpell
    {
        public IncognitoSpell(Mobile caster, Item spellItem = null) : base(caster, spellItem) { }
        
        public async Task CastAsync()
        {
            if (Caster.BodyMod == 183 || Caster.BodyMod == 184)
            {
                Caster.SendLocalizedMessage(1042402); // You cannot use incognito while wearing body paint
                return;
            }
            if (DisguiseTimers.IsDisguised(Caster))
            {
                Caster.SendLocalizedMessage(1061631); // You can't do that while disguised.
                return;
            }
            if (!Caster.CanBeginAction(typeof(PolymorphSpell)) || Caster.IsBodyMod)
            {
                DoFizzle();
                return;
            }
            
            if (!Caster.CanBuff(Caster, false, BuffIcon.Incognito))
            {
                Caster.SendLocalizedMessage(1079022); // You're already incognitoed!
                return;
            }

            if (!(Caster is PlayerMobile player))
                return;
            
            DisguiseTimers.StopTimer(Caster);

            var duration = TimeSpan.FromSeconds((Caster.Skills[SkillName.Magery].Value / 10) * 60);
            var name = Caster.Female ? NameList.RandomName("female") : NameList.RandomName("male");
            Caster.TryAddBuff(new Incognito
            {
                Description = $"Disguised as \"{name}\"",
                Duration = duration,
                Values = (
                    0, 
                    player.Race.RandomSkinHue(), 
                    player.Race.RandomHair(player.Female),
                    player.Race.RandomHairHue(),
                    player.Race.RandomFacialHair(player.Female),
                    player.Race.RandomHairHue(),
                    name
                )
            });

            BaseArmor.ValidateMobile(Caster);
            BaseClothing.ValidateMobile(Caster);
            
            Caster.PrivateOverheadMessage(MessageType.Regular, 0x3B2, true, $"Your new name is {name}", Caster.NetState);
            
        }
    }
}