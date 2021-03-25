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
        
        private record ShapeshiftData
        {
            public int Difficulty { get; init; }
            public int Points { get; init; }
        }

        private Dictionary<int, ShapeshiftData> BodyData { get; set; } =
            new()
            {
                {PolymorphEntry.Bird.BodyId, new ShapeshiftData(){ Difficulty = 10, Points = 100}},
                {PolymorphEntry.Rabbit.BodyId, new ShapeshiftData(){ Difficulty = 15, Points = 150}},
                {PolymorphEntry.Eagle.BodyId, new ShapeshiftData(){ Difficulty = 20, Points = 200}},
                {PolymorphEntry.Cat.BodyId, new ShapeshiftData(){ Difficulty = 25, Points = 250}},
                {PolymorphEntry.Dog.BodyId, new ShapeshiftData(){ Difficulty = 30, Points = 300}},
                {PolymorphEntry.Wolf.BodyId, new ShapeshiftData(){ Difficulty = 40, Points = 400}},
                {PolymorphEntry.Deer.BodyId, new ShapeshiftData(){ Difficulty = 45, Points = 450}},
                {PolymorphEntry.Panther.BodyId, new ShapeshiftData(){ Difficulty = 50, Points = 500}},
                {PolymorphEntry.BlackBear.BodyId, new ShapeshiftData(){ Difficulty = 50, Points = 500}},
                {PolymorphEntry.GrizzlyBear.BodyId, new ShapeshiftData(){ Difficulty = 60, Points = 600}},
                {PolymorphEntry.PolarBear.BodyId, new ShapeshiftData(){ Difficulty = 65, Points = 650}},
                {PolymorphEntry.GiantSerpent.BodyId, new ShapeshiftData(){ Difficulty = 70, Points = 700}},
                {PolymorphEntry.EarthElemental.BodyId, new ShapeshiftData(){ Difficulty = 80, Points = 800}},
                {PolymorphEntry.FireElemental.BodyId, new ShapeshiftData(){ Difficulty = 90, Points = 900}},
                {PolymorphEntry.WaterElemental.BodyId, new ShapeshiftData(){ Difficulty = 95, Points = 950}},
                {PolymorphEntry.AirElemental.BodyId, new ShapeshiftData(){ Difficulty = 97, Points = 970}},
                {PolymorphEntry.Dragon.BodyId, new ShapeshiftData(){ Difficulty = 100, Points = 1000}},
                {PolymorphEntry.Reaper.BodyId, new ShapeshiftData(){ Difficulty = 110, Points = 1100}},
                {PolymorphEntry.Wisp.BodyId, new ShapeshiftData(){ Difficulty = 120, Points = 1200}},
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
            
            Caster.TryAddBuff(new Polymorph
            {
                Duration = SpellHelper.GetDuration(Caster, Caster),
                BodyMods = (bodyId, hueMod),
                Value = (int) (SpellHelper.GetModAmount(Caster, Caster, StatType.All) * 0.75)
            });
        }
    }
}