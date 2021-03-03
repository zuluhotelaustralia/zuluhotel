using System;
using Server.Items;

namespace Server.Spells.First
{
    public class CreateFoodSpell : MagerySpell
    {
        private static readonly FoodInfo[] Food =
        {
            new FoodInfo(typeof(Grapes), "a grape bunch"),
            new FoodInfo(typeof(Ham), "a ham"),
            new FoodInfo(typeof(CheeseWedge), "a wedge of cheese"),
            new FoodInfo(typeof(Muffins), "muffins"),
            new FoodInfo(typeof(FishSteak), "a fish steak"),
            new FoodInfo(typeof(Ribs), "cut of ribs"),
            new FoodInfo(typeof(CookedBird), "a cooked bird"),
            new FoodInfo(typeof(Sausage), "sausage"),
            new FoodInfo(typeof(Apple), "an apple"),
            new FoodInfo(typeof(Peach), "a peach")
        };

        public CreateFoodSpell(Mobile caster, Item spellItem) : base(caster, spellItem)
        {
        }


        public override void OnCast()
        {
            if (CheckSequence())
            {
                var foodInfo = Food[Utility.Random(Food.Length)];
                var food = foodInfo.Create();

                if (food != null)
                {
                    Caster.AddToBackpack(food);

                    // You magically create food in your backpack:
                    Caster.SendLocalizedMessage(1042695, true, " " + foodInfo.Name);

                    Caster.FixedParticles(0, 10, 5, 2003, EffectLayer.RightHand);
                    Caster.PlaySound(0x1E2);
                }
            }

            FinishSequence();
        }
    }

    public class FoodInfo
    {
        public FoodInfo(Type type, string name)
        {
            Type = type;
            Name = name;
        }

        public Type Type { get; set; }

        public string Name { get; set; }

        public Item Create()
        {
            Item item;

            try
            {
                item = (Item) Activator.CreateInstance(Type);
            }
            catch
            {
                item = null;
            }

            return item;
        }
    }
}