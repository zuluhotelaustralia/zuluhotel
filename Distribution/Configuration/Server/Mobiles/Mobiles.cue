
package Mobiles
import (
    System "github.com/zuluhotelaustralia/zuluhotel/System"
    Server "github.com/zuluhotelaustralia/zuluhotel/Server"
    Items "github.com/zuluhotelaustralia/zuluhotel/Server/Items"
    HitScripts "github.com/zuluhotelaustralia/zuluhotel/Server/Engines/Magic/HitScripts"
    Classes "github.com/zuluhotelaustralia/zuluhotel/Scripts/Zulu/Engines/Classes"
    Misc "github.com/zuluhotelaustralia/zuluhotel/Server/Misc"
)

#CreatureTypeEnum: {
    None: "None"
    Human: "Human"
    Undead: "Undead"
    Elemental: "Elemental"
    Dragonkin: "Dragonkin"
    Animal: "Animal"
    Daemon: "Daemon"
    Beholder: "Beholder"
    Animated: "Animated"
    Slime: "Slime"
    Terathan: "Terathan"
    Plant: "Plant"
    Orc: "Orc"
    Troll: "Troll"
    Gargoyle: "Gargoyle"
    Ophidian: "Ophidian"
    Ratkin: "Ratkin"
    Giantkin: "Giantkin"
}
#CreatureType: or(
    [
        
            for _, v in #CreatureTypeEnum {
            v
        }])
#CreatureTypeArray: [
    ...#CreatureType]
#OppositionGroup: {
    @dotnet({FullName:Server.Mobiles.OppositionGroup})
    Type: (#CreatureType | null)
    Friendlies: (#CreatureTypeArray | null)
    Enemies: (#CreatureTypeArray | null)
}
#PropValue: (number | {
    Min: number
    Max: number
})
#CreatureEquip: {
    @dotnet({FullName:Server.Mobiles.CreatureEquip})
    ItemType: System.#Type
    Name: *"" | string
    Hue: (*0 | #PropValue | null)
    ArmorRating: (*0 | #PropValue | null)
}
#CreatureAttack: {
    @dotnet({FullName:Server.Mobiles.CreatureAttack})
    Speed: (#PropValue | null)
    Damage: (#PropValue | null)
    Skill: (Server.#SkillName | null)
    Animation: (Items.#WeaponAnimation | null)
    HitSound: (int32 | null)
    MissSound: (int32 | null)
    MaxRange: (int32 | null)
    Ability: (HitScripts.#WeaponAbility | null)
    AbilityChance: (number | null)
    HasBreath: (bool | null)
    HasWebs: (bool | null)
    HitPoison: (Server.#Poison | null)
    HitPoisonChance: (number | null)
}
#AITypeEnum: {
    AI_Use_Default: "AI_Use_Default"
    AI_Melee: "AI_Melee"
    AI_Animal: "AI_Animal"
    AI_Archer: "AI_Archer"
    AI_Healer: "AI_Healer"
    AI_Vendor: "AI_Vendor"
    AI_Mage: "AI_Mage"
    AI_Berserk: "AI_Berserk"
    AI_Predator: "AI_Predator"
    AI_Thief: "AI_Thief"
    AI_Familiar: "AI_Familiar"
}
#AIType: or(
    [
        
            for _, v in #AITypeEnum {
            v
        }])
#FightModeEnum: {
    None: "None"
    Aggressor: "Aggressor"
    Strongest: "Strongest"
    Weakest: "Weakest"
    Closest: "Closest"
    Evil: "Evil"
}
#FightMode: or(
    [
        
            for _, v in #FightModeEnum {
            v
        }])
#HideTypeEnum: {
    Regular: "Regular"
    Rat: "Rat"
    Wolf: "Wolf"
    Bear: "Bear"
    Serpent: "Serpent"
    Lizard: "Lizard"
    Troll: "Troll"
    Ostard: "Ostard"
    Necromancer: "Necromancer"
    Lava: "Lava"
    Liche: "Liche"
    IceCrystal: "IceCrystal"
    Dragon: "Dragon"
    Wyrm: "Wyrm"
    Balron: "Balron"
    GoldenDragon: "GoldenDragon"
}
#HideType: or(
    [
        
            for _, v in #HideTypeEnum {
            v
        }])
#CreatureProperties: {
    @dotnet({FullName:Server.Mobiles.CreatureProperties})
    Name: string
    Kind: string
    CorpseNameOverride: string
    BaseType: string
    Str: (#PropValue | null)
    Int: (#PropValue | null)
    Dex: (#PropValue | null)
    ActiveSpeed: (#PropValue | null)
    PassiveSpeed: (#PropValue | null)
    AiType: (#AIType | null)
    AlwaysAttackable: (bool | null)
    AlwaysMurderer: (bool | null)
    AutoDispel: (bool | null)
    BardImmune: (bool | null)
    BaseSoundID: (int32 | null)
    Body: (Server.#Body | null)
    CanFly: (bool | null)
    CanRummageCorpses: (bool | null)
    CanSwim: (bool | null)
    ClassLevel: (int32 | null)
    ClassType: (Classes.#ZuluClassType | null)
    ClickTitle: (bool | null)
    ControlSlots: (int32 | null)
    CreatureType: (#CreatureType | null)
    VirtualArmor: (#PropValue | null)
    DeleteCorpseOnDeath: (bool | null)
    Fame: (#PropValue | null)
    Female: (bool | null)
    FightMode: (#FightMode | null)
    FightRange: (#PropValue | null)
    HideType: (#HideType | null)
    Hides: (int32 | null)
    HitsMax: (#PropValue | null)
    Hue: (#PropValue | null)
    InitialInnocent: (bool | null)
    Karma: (#PropValue | null)
    LootItemChance: (int32 | null)
    LootItemLevel: (int32 | null)
    LootTable: string
    ManaMaxSeed: (#PropValue | null)
    Meat: (#PropValue | null)
    MinTameSkill: (#PropValue | null)
    OppositionGroup: (#OppositionGroup | null)
    PerceptionRange: (#PropValue | null)
    ProvokeSkillOverride: (int32 | null)
    Race: (Server.#Race | null)
    ReacquireDelay: (System.#TimeSpan | null)
    RiseCreatureDelay: (System.#TimeSpan | null)
    RiseCreatureType: System.#Type
    SaySpellMantra: (bool | null)
    SpeechType: (Misc.#InhumanSpeech | null)
    StamMaxSeed: (#PropValue | null)
    Tamable: (bool | null)
    TargetAcquireExhaustion: (bool | null)
    Team: (int32 | null)
    Title: string
    TreasureMapLevel: (#PropValue | null)
    PreferredSpells: [...string]
    Resistances: { ... }
    Skills: { ... }
    Attack: (#CreatureAttack | null)
    Equipment: [
        ...#CreatureEquip]
}