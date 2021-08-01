using System;
using System.Threading.Tasks;
using Scripts.Zulu.Engines.Classes;
using Scripts.Zulu.Utilities;
using Server.Items;
using Server.Misc;
using Server.Targeting;
using ZuluContent.Zulu.Items;
using ZuluContent.Zulu.Skills;

namespace Server.SkillHandlers
{
    public class Poisoning : BaseSkillHandler
    {
        public override SkillName Skill => SkillName.Poisoning;

        public override async Task<TimeSpan> OnUse(Mobile from)
        {
            from.SendSuccessMessage(502137); // Select the poison you wish to use

            var target1 = new AsyncTarget<object>(from,
                new TargetOptions {Range = 1});
            from.Target = target1;

            var (targeted1, _) = await target1;

            if (!(targeted1 is BasePoisonPotion poisonPotion))
            {
                from.SendFailureMessage(502139); // That is not a poison potion.
                return Delay;
            }

            from.SendSuccessMessage(502142); // To what do you wish to apply the poison?

            var target2 = new AsyncTarget<object>(from,
                new TargetOptions {Range = 1});
            from.Target = target2;

            var (targeted2, _) = await target2;

            if (poisonPotion.Deleted)
                return Delay;

            if (targeted2 is Food food)
                PoisonFood(@from, poisonPotion, food);
            else if (targeted2 is BaseWeapon
                {Layer: Layer.OneHanded, Type: WeaponType.Slashing or WeaponType.Piercing} weapon)
                PoisonWeapon(@from, poisonPotion, weapon);
            else // Target can't be poisoned
                @from.SendFailureMessage(
                    502145); // You cannot poison that! You can only poison bladed or piercing weapons, food or drink.

            return Delay;
        }

        private void PoisonFood(Mobile from, BasePoisonPotion poisonPotion, Food food)
        {
            var power = (double) poisonPotion.Poison.Level;
            var difficulty = power * 20;

            if (difficulty > 120.0)
                difficulty = 120.0;

            Titles.AwardKarma(from, -50, true);

            poisonPotion.Consume();
            from.AddToBackpack(new Bottle());

            if (from.ShilCheckSkill(Skill, (int) difficulty, (int) (difficulty * 20)))
            {
                power *= from.GetClassModifier(Skill);

                var poison = Poison.GetPoison(Math.Min((int) power, Poison.Poisons.Count - 1));

                from.PlaySound(0x247);
                from.SendSuccessMessage(1010517); // You apply the poison
                food.Poison = poison;
            }
            else
            {
                var poisoningSkill = from.Skills[Skill].Value;
                poisoningSkill *= from.GetClassModifier(Skill);

                if (Utility.Random(100) > poisoningSkill)
                {
                    from.SendFailureMessage(
                        502148); // You make a grave mistake while applying the poison.
                    from.ApplyPoison(from, poisonPotion.Poison);
                }
                else
                {
                    @from.SendFailureMessage(1010518); // You fail to apply a sufficient dose of poison
                }
            }
        }

        private void PoisonWeapon(Mobile from, BasePoisonPotion poisonPotion, BaseWeapon weapon)
        {
            var power = (double) poisonPotion.Poison.Level;
            var difficulty = power * 25;

            if (weapon is IGMItem)
                difficulty += 20.0;

            if (difficulty > 160.0)
                difficulty = 160.0;

            var poisoningSkill = from.Skills[Skill].Value;

            power = poisoningSkill switch
            {
                < 50.0 => power - 2.0,
                < 75 => power - 1.0,
                > 160 => power + 2.0,
                > 135 => power + 1.0,
                _ => power
            };
            power = Math.Max(power, 1.0);

            Titles.AwardKarma(from, -50, true);

            poisonPotion.Consume();
            from.AddToBackpack(new Bottle());

            if (from.ShilCheckSkill(Skill, (int) difficulty, (int) (difficulty * 20)))
            {
                if (from.ClassContainsSkill(Skill))
                    power += 1;

                var poison = Poison.GetPoison(Math.Min((int) power, Poison.Poisons.Count - 1));

                from.PlaySound(0x247);
                from.SendSuccessMessage(1010517); // You apply the poison

                weapon.Poison = poison;
                weapon.PoisonCharges = (int) (power * 3);
            }
            else
            {
                poisoningSkill -= (int) (difficulty / 2);
                poisoningSkill *= from.GetClassModifier(Skill);

                if (Utility.Random(100) > poisoningSkill)
                {
                    from.SendFailureMessage(
                        502148); // You make a grave mistake while applying the poison.
                    from.ApplyPoison(from, poisonPotion.Poison);
                }
                else
                {
                    @from.SendFailureMessage(1010518); // You fail to apply a sufficient dose of poison
                }
            }
        }
    }
}