namespace Server
{
    public class DiceRoll
    {
        public int Count { get; set; }
        public int Sides { get; set; }
        public int Bonus { get; set; }
        
        public DiceRoll(int count, int sides, int bonus)
        {
            Count = count;
            Sides = sides;
            Bonus = bonus;
        }

        public DiceRoll(string str)
        {
            var start = 0;
            var index = str.IndexOf('d', start);

            if (index < start)
                return;

            Count = Utility.ToInt32(str.Substring(start, index - start));


            start = index + 1;
            index = str.IndexOf('+', start);
            var negative = index < start;

            if (negative)
                index = str.IndexOf('-', start);

            if (index < start)
                index = str.Length;

            Sides = Utility.ToInt32(str.Substring(start, index - start));

            if (index == str.Length)
                return;

            start = index + 1;
            index = str.Length;

            Bonus = Utility.ToInt32(str.Substring(start, index - start));

            if (negative)
                Bonus *= -1;
        }
        
        public static implicit operator DiceRoll(string s) => new(s);
        
        public int Roll()
        {
            var v = Bonus;

            for (var i = 0; i < Count; ++i)
                v += Utility.Random(1, Sides);

            return v;
        }
    }
}