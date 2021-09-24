using System;
using System.ComponentModel;
using Scripts.Cue;

namespace Server.Mobiles
{
    [CueExpression(@"(number | {
    Min: number
    Max: number
})")]
    public record PropValue
    {
        public double Min { get; set; }
        public double? Max { get; set; }

        public PropValue(double min, double? max = null)
        {
            Min = min;
            Max = max;
        }

        public static PropValue Between(double min, double max)
        {
            return new(min, max);
        }

        public static PropValue Dice(string d)
        {
            DiceRoll dice = d;
            var value = new PropValue(dice.Count + dice.Bonus, dice.Count * dice.Sides + dice.Bonus);

            return value;
        }

        [EditorBrowsable(EditorBrowsableState.Never)]
        public void Deconstruct(out double min, out double? max)
        {
            min = Min;
            max = Max;
        }

        public static implicit operator int(PropValue d)
        {
            return Convert.ToInt32(d.Next());
        }

        public static implicit operator double(PropValue d)
        {
            return d.Next();
        }

        public static implicit operator PropValue(double d)
        {
            return new(d);
        }
        
        public static implicit operator PropValue((double min, double max ) value)
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