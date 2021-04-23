using System;
using System.ComponentModel;

namespace Server.Mobiles
{
    public record CreatureProp
    {
        public double Min { get; set; }
        public double? Max { get; set; }

        public CreatureProp(double min, double? max = null)
        {
            Min = min;
            Max = max;
        }

        public static CreatureProp Between(double min, double max)
        {
            return new(min, max);
        }

        public static CreatureProp Dice(string d)
        {
            DiceRoll dice = d;
            var value = new CreatureProp(dice.Count + dice.Bonus, dice.Count * dice.Sides + dice.Bonus);

            return value;
        }

        [EditorBrowsable(EditorBrowsableState.Never)]
        public void Deconstruct(out double min, out double? max)
        {
            min = Min;
            max = Max;
        }

        public static implicit operator int(CreatureProp d)
        {
            return Convert.ToInt32(d.Next());
        }

        public static implicit operator double(CreatureProp d)
        {
            return d.Next();
        }

        public static implicit operator CreatureProp(double d)
        {
            return new(d);
        }
        
        public static implicit operator CreatureProp((double min, double max ) value)
        {
            return new(value.min, value.max);
        }

        private double Next()
        {
            if (!Max.HasValue)
                return Min;

            return Min + Utility.RandomDouble() * (Max.GetValueOrDefault(0.0) - Min);
        }
    }
}