using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using Scripts.Zulu.Engines.Classes;
using Scripts.Zulu.Utilities;
using Server;
using Server.Network;
using Server.Items;
using Server.Gumps;
using Server.Mobiles;
using Server.Spells;
using Server.Targeting;
using ZuluContent.Zulu.Engines.Magic.Enchantments.Buffs;

namespace Scripts.Zulu.Spells.Earth
{
    public class ShapeshiftSpell : EarthSpell, IAsyncSpell
    {
        public override TimeSpan CastDelayBase => TimeSpan.FromSeconds(0);
    
        public override double RequiredSkill => 60.0;
    
        public override int RequiredMana => 5;

        private static readonly Dictionary<int, (int Difficulty, int Points)> BodyData =
            new()
            {
                {PolymorphEntry.Bird.BodyId, (10, 100)},
                {PolymorphEntry.Rabbit.BodyId, (15, 150)},
                {PolymorphEntry.Eagle.BodyId, (20, 200)},
                {PolymorphEntry.Cat.BodyId, (25, 250)},
                {PolymorphEntry.Dog.BodyId, (30, 300)},
                {PolymorphEntry.Wolf.BodyId, (40, 400)},
                {PolymorphEntry.Deer.BodyId, (45, 450)},
                {PolymorphEntry.Panther.BodyId, (50, 500)},
                {PolymorphEntry.BlackBear.BodyId, (50, 500)},
                {PolymorphEntry.GrizzlyBear.BodyId, (60, 600)},
                {PolymorphEntry.PolarBear.BodyId, (65, 650)},
                {PolymorphEntry.GiantSerpent.BodyId, (70, 700)},
                {PolymorphEntry.EarthElemental.BodyId, (80, 800)},
                {PolymorphEntry.FireElemental.BodyId, (90, 900)},
                {PolymorphEntry.WaterElemental.BodyId, (95, 950)},
                {PolymorphEntry.AirElemental.BodyId, (97, 970)},
                {PolymorphEntry.Dragon.BodyId, (100, 1000)},
                {PolymorphEntry.Reaper.BodyId, (110, 1100)},
                {PolymorphEntry.Wisp.BodyId, (120, 1200)},
            };

        public ShapeshiftSpell(Mobile caster, Item spellItem) : base(caster, spellItem)
        {
        }
        
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
            {
                return;
            }
            
            if (!Caster.CanBuff(Caster, icons: BuffIcon.AnimalForm))
            {
                (Caster as IBuffable)?.BuffManager.RemoveBuff<Polymorph>();
                Caster.FixedParticles(0x373A, 10, 10, 5007, EffectLayer.Waist);
                Caster.PlaySound(0x209);
                Caster.SendSuccessMessage("You resume your true form.");
                return;
            }

            var bodyId = await new NewPolymorphGump(Caster);

            if (bodyId <= 0)
                return;

            if (!Caster.ShilCheckSkill(SkillName.Meditation, BodyData[bodyId].Difficulty, BodyData[bodyId].Points))
            {
                Caster.SendFailureMessage("You fail to transform yourself.");
                DoFizzle();
                return;
            }

            var hueMod = 0;
            
            Caster.FixedParticles(0x373A, 10, 10, 5007, EffectLayer.Waist);
            Caster.PlaySound(0x209);

            var statMod = (int) (SpellHelper.GetModAmount(Caster, Caster, StatType.All) * 0.75);
            
            Caster.TryAddBuff(new Polymorph
            {
                Title = "Shapeshift",
                Icon = BuffIcon.AnimalForm,
                Duration = SpellHelper.GetDuration(Caster, Caster),
                BodyMods = (body: bodyId, bodyHue: hueMod),
                StatMods = (StrMod: statMod, DexMod: statMod, IntMod: statMod)
            });
        }
    }
}