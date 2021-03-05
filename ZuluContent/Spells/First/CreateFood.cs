using System;
using System.Threading.Tasks;
using Server.Items;
#pragma warning disable 1998

namespace Server.Spells.First
{
    public class CreateFoodSpell : MagerySpell, IAsyncSpell
    {
        private static readonly (string description, Func<Item> creator)[] Food =
        {
            ("a grape bunch", () => new Grapes()),
            ("a ham", () => new Ham()),
            ("a wedge of cheese", () => new CheeseWedge()),
            ("muffins", () => new Muffins()),
            ("a fish steak", () => new FishSteak()),
            ("cut of ribs", () => new Ribs()),
            ("a cooked bird", () => new CookedBird()),
            ("sausage", () => new Sausage()),
            ("an apple", () => new Apple()),
            ("a peach", () => new Peach())
        };

        public CreateFoodSpell(Mobile caster, Item spellItem) : base(caster, spellItem) { }
        
        public async Task CastAsync()
        {
            var (description, creator) = Food.RandomElement();
            var food = creator();
            
            Caster.AddToBackpack(food);
            // You magically create food in your backpack:
            Caster.SendLocalizedMessage(1042695, true, " " + description);
            Caster.FixedParticles(0, 10, 5, 2003, EffectLayer.RightHand);
            Caster.PlaySound(0x1E2);
        }
    }
}