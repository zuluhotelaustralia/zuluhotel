using System;
using Server.Engines.Magic.HitScripts;
using Server.Items;

namespace Server.Mobiles.Monsters
{
    public record CreatureEquip
    {
        public Type ItemType { get; set; }
        public string Name { get; set; }
        public PropValue Hue { get; set; }
        public PropValue ArmorRating { get; set; }
    }

    public record CreatureAttack
    {
        public PropValue Speed { get; set; }
        public PropValue Damage { get; set; }
        public SkillName? Skill { get; set; }
        public WeaponAnimation? Animation { get; set; }
        public int? HitSound { get; set; }
        public int? MissSound { get; set; }
        public int? MaxRange { get; set; }
        public WeaponAbility Ability { get; set; }
        public double? AbilityChance { get; set; }
        public bool? HasBreath { get; set; }
        public bool? HasWebs { get; set; }
    }
}