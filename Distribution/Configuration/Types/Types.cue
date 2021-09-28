package Types

// TODO: Disabled due to perf issues with field comprehension in current version of CUE
//#NpcEntry: {
// Kind:   "NpcEntry"
// for key, val in #CreatureProperties {
//  "\(key)": *#NpcDefaults[key] | val
// }
// Attack: {
//  for key, val in #CreatureAttack {
//   "\(key)": *#NpcDefaults.Attack[key] | val
//  }
// }
//}

// TODO: Replace with above field comprehension when perf issues solved
#NpcEntry: {
	Kind:                    "NpcEntry"
	Name:                    string
	CorpseNameOverride:      *#NpcDefaults.CorpseNameOverride | #CreatureProperties.CorpseNameOverride
	BaseType:                *#NpcDefaults.BaseType | #CreatureProperties.BaseType
	Str:                     *#NpcDefaults.Str | #CreatureProperties.Str
	Int:                     *#NpcDefaults.Int | #CreatureProperties.Int
	Dex:                     *#NpcDefaults.Dex | #CreatureProperties.Dex
	ActiveSpeed:             *#NpcDefaults.ActiveSpeed | #CreatureProperties.ActiveSpeed
	PassiveSpeed:            *#NpcDefaults.PassiveSpeed | #CreatureProperties.PassiveSpeed
	AiType:                  *#NpcDefaults.AiType | #CreatureProperties.AiType
	AlwaysAttackable:        *#NpcDefaults.AlwaysAttackable | #CreatureProperties.AlwaysAttackable
	AlwaysMurderer:          *#NpcDefaults.AlwaysMurderer | #CreatureProperties.AlwaysMurderer
	InitialInnocent:         *#NpcDefaults.InitialInnocent | #CreatureProperties.InitialInnocent
	AutoDispel:              *#NpcDefaults.AutoDispel | #CreatureProperties.AutoDispel
	BardImmune:              *#NpcDefaults.BardImmune | #CreatureProperties.BardImmune
	CanFly:                  *#NpcDefaults.CanFly | #CreatureProperties.CanFly
	CanSwim:                 *#NpcDefaults.CanSwim | #CreatureProperties.CanSwim
	ClassLevel:              *#NpcDefaults.ClassLevel | #CreatureProperties.ClassLevel
	ClassType:               *#NpcDefaults.ClassType | #CreatureProperties.ClassType
	ClickTitle:              *#NpcDefaults.ClickTitle | #CreatureProperties.ClickTitle
	ControlSlots:            *#NpcDefaults.ControlSlots | #CreatureProperties.ControlSlots
	VirtualArmor:            *#NpcDefaults.VirtualArmor | #CreatureProperties.VirtualArmor
	DeleteCorpseOnDeath:     *#NpcDefaults.DeleteCorpseOnDeath | #CreatureProperties.DeleteCorpseOnDeath
	Fame:                    *#NpcDefaults.Fame | #CreatureProperties.Fame
	Karma:                   *#NpcDefaults.Karma | #CreatureProperties.Karma
	HideType:                *#NpcDefaults.HideType | #CreatureProperties.HideType
	Hides:                   *#NpcDefaults.Hides | #CreatureProperties.Hides
	BaseSoundID:             *#NpcDefaults.BaseSoundID | #CreatureProperties.BaseSoundID
	Body:                    *#NpcDefaults.Body | #CreatureProperties.Body
	CanRummageCorpses:       *#NpcDefaults.CanRummageCorpses | #CreatureProperties.CanRummageCorpses
	CreatureType:            *#NpcDefaults.CreatureType | #CreatureProperties.CreatureType
	Female:                  *#NpcDefaults.Female | #CreatureProperties.Female
	FightMode:               *#NpcDefaults.FightMode | #CreatureProperties.FightMode
	FightRange:              *#NpcDefaults.FightRange | #CreatureProperties.FightRange
	HitsMaxSeed:             *#NpcDefaults.HitsMaxSeed | #CreatureProperties.HitsMaxSeed
	Hue:                     *#NpcDefaults.Hue | #CreatureProperties.Hue
	LootItemChance:          *#NpcDefaults.LootItemChance | #CreatureProperties.LootItemChance
	LootItemLevel:           *#NpcDefaults.LootItemLevel | #CreatureProperties.LootItemLevel
	LootTable:               *#NpcDefaults.LootTable | #CreatureProperties.LootTable
	ManaMaxSeed:             *#NpcDefaults.ManaMaxSeed | #CreatureProperties.ManaMaxSeed
	Meat:                    *#NpcDefaults.Meat | #CreatureProperties.Meat
	PerceptionRange:         *#NpcDefaults.PerceptionRange | #CreatureProperties.PerceptionRange
	MinTameSkill:            *#NpcDefaults.MinTameSkill | #CreatureProperties.MinTameSkill
	OppositionGroup:         *#NpcDefaults.OppositionGroup | #CreatureProperties.OppositionGroup
	Race:                    *#NpcDefaults.Race | #CreatureProperties.Race
	ReacquireDelay:          *#NpcDefaults.ReacquireDelay | #CreatureProperties.ReacquireDelay
	RiseCreatureDelay:       *#NpcDefaults.RiseCreatureDelay | #CreatureProperties.RiseCreatureDelay
	RiseCreatureTemplate:    *#NpcDefaults.RiseCreatureTemplate | #CreatureProperties.RiseCreatureTemplate
	SaySpellMantra:          *#NpcDefaults.SaySpellMantra | #CreatureProperties.SaySpellMantra
	SpeechType:              *#NpcDefaults.SpeechType | #CreatureProperties.SpeechType
	TargetAcquireExhaustion: *#NpcDefaults.TargetAcquireExhaustion | #CreatureProperties.TargetAcquireExhaustion
	Team:                    *#NpcDefaults.Team | #CreatureProperties.Team
	Title:                   *#NpcDefaults.Title | #CreatureProperties.Title
	TreasureMapLevel:        *#NpcDefaults.TreasureMapLevel | #CreatureProperties.TreasureMapLevel
	ProvokeSkillOverride:    *#NpcDefaults.ProvokeSkillOverride | #CreatureProperties.ProvokeSkillOverride
	StamMaxSeed:             *#NpcDefaults.StamMaxSeed | #CreatureProperties.StamMaxSeed
	Tamable:                 *#NpcDefaults.Tamable | #CreatureProperties.Tamable
	Skills:                  *#NpcDefaults.Skills | #CreatureProperties.Skills
	Resistances:             *#NpcDefaults.Resistances | #CreatureProperties.Resistances
	Equipment:               *#NpcDefaults.Equipment | #CreatureProperties.Equipment
	PreferredSpells:         *#NpcDefaults.PreferredSpells | #CreatureProperties.PreferredSpells
	OppositionGroup:         *#NpcDefaults.OppositionGroup | #CreatureProperties.OppositionGroup

	Attack: {
		Damage:             *#NpcDefaults.Attack.Damage | #CreatureAttack.Damage
		Animation:          *#NpcDefaults.Attack.Animation | #CreatureAttack.Animation
		Speed:              *#NpcDefaults.Attack.Speed | #CreatureAttack.Speed
		Ability:            *#NpcDefaults.Attack.Ability | #CreatureAttack.Ability
		AbilityChance:      *#NpcDefaults.Attack.AbilityChance | #CreatureAttack.AbilityChance
		HasBreath:          *#NpcDefaults.Attack.HasBreath | #CreatureAttack.HasBreath
		HasWebs:            *#NpcDefaults.Attack.HasWebs | #CreatureAttack.HasWebs
		HitSound:           *#NpcDefaults.Attack.HitSound | #CreatureAttack.HitSound
		MissSound:          *#NpcDefaults.Attack.MissSound | #CreatureAttack.MissSound
		MaxRange:           *#NpcDefaults.Attack.MaxRange | #CreatureAttack.MaxRange
		Skill:              *#NpcDefaults.Attack.Skill | #CreatureAttack.Skill
		HitPoison:          *#NpcDefaults.Attack.HitPoison | #CreatureAttack.HitPoison
		HitPoisonChance:    *#NpcDefaults.Attack.HitPoisonChance | #CreatureAttack.HitPoisonChance
		ProjectileEffectId: *#NpcDefaults.Attack.ProjectileEffectId | #CreatureAttack.ProjectileEffectId
	}
}
