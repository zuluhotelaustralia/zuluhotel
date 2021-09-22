using System.ComponentModel;
using Server.Engines.Magic.HitScripts;
using Server.Items;
using Server.Spells;
using Type = System.Type;

namespace Server.Mobiles
{
    public record CreatureEquip
    {
        public Type ItemType { get; set; }
        
        [DefaultValue("")]
        public string Name { get; set; } = string.Empty; 
        [DefaultValue(0)]
        public PropValue Hue { get; set; } = 0;
        
        [DefaultValue(0)]
        public PropValue ArmorRating { get; set; } = 0;
    }

    public record CreatureAttack
    {
        public PropValue Speed { get; set; }
        public PropValue Damage { get; set; } = 1;
        public SkillName Skill { get; set; } = SkillName.Wrestling;
        public WeaponAnimation? Animation { get; set; } = WeaponAnimation.Wrestle;
        public int? HitSound { get; set; }
        public int? MissSound { get; set; }
        public int? MaxRange { get; set; }
        public WeaponAbility Ability { get; set; }
        public double? AbilityChance { get; set; }
        public bool? HasBreath { get; set; }
        public bool? HasWebs { get; set; }
        public Poison HitPoison { get; set; }
        public double? HitPoisonChance { get; set; } 
    }

    // [Register.CueExpression(@"""First"" | ""Second""")]
    public enum TestEnum
    {
        First,
        Second
    }

    public class TestCueType
    {
        public TestEnum Value { get; set; }
        public SpellEntry Spell { get; set; }
    }

    public abstract class TestCueTypeAbstract
    {
        public void OnHit(int attacker, ref int defender) {}
    }
    
    public record TestCueTypeRecord
    {
        public int Damage = 100;
        public TestCueTypeAbstract CueTypeAbstract;
    }
}