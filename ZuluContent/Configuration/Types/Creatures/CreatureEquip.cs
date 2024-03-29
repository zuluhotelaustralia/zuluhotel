using System.ComponentModel;
using Server;
using Server.Engines.Magic.HitScripts;
using Server.Items;
using Server.Mobiles;
using Type = System.Type;

namespace ZuluContent.Configuration.Types.Creatures
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

        [DefaultValue(false)] public bool Lootable { get; set; } = false;
    }

    public record CreatureAttack
    {
        public PropValue Speed { get; set; }
        public PropValue Damage { get; set; } = 1;
        public SkillName Skill { get; set; }
        public WeaponAnimation? Animation { get; set; }
        public int? HitSound { get; set; }
        public int? MissSound { get; set; }
        public int? MaxRange { get; set; }
        public WeaponAbility Ability { get; set; }
        public double? AbilityChance { get; set; }
        public bool? HasBreath { get; set; }
        public bool? HasWebs { get; set; }
        public Poison HitPoison { get; set; }
        public double? HitPoisonChance { get; set; } 
        public int? ProjectileEffectId { get; set; }
    }
}