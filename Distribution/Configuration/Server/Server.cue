
package Server

#SkillNameEnum: {
    Alchemy: "Alchemy"
    Anatomy: "Anatomy"
    AnimalLore: "AnimalLore"
    ItemID: "ItemID"
    ArmsLore: "ArmsLore"
    Parry: "Parry"
    Begging: "Begging"
    Blacksmith: "Blacksmith"
    Fletching: "Fletching"
    Peacemaking: "Peacemaking"
    Camping: "Camping"
    Carpentry: "Carpentry"
    Cartography: "Cartography"
    Cooking: "Cooking"
    DetectHidden: "DetectHidden"
    Discordance: "Discordance"
    EvalInt: "EvalInt"
    Healing: "Healing"
    Fishing: "Fishing"
    Forensics: "Forensics"
    Herding: "Herding"
    Hiding: "Hiding"
    Provocation: "Provocation"
    Inscribe: "Inscribe"
    Lockpicking: "Lockpicking"
    Magery: "Magery"
    MagicResist: "MagicResist"
    Tactics: "Tactics"
    Snooping: "Snooping"
    Musicianship: "Musicianship"
    Poisoning: "Poisoning"
    Archery: "Archery"
    SpiritSpeak: "SpiritSpeak"
    Stealing: "Stealing"
    Tailoring: "Tailoring"
    AnimalTaming: "AnimalTaming"
    TasteID: "TasteID"
    Tinkering: "Tinkering"
    Tracking: "Tracking"
    Veterinary: "Veterinary"
    Swords: "Swords"
    Macing: "Macing"
    Fencing: "Fencing"
    Wrestling: "Wrestling"
    Lumberjacking: "Lumberjacking"
    Mining: "Mining"
    Meditation: "Meditation"
    Stealth: "Stealth"
    RemoveTrap: "RemoveTrap"
    Necromancy: "Necromancy"
    Focus: "Focus"
    Chivalry: "Chivalry"
    Bushido: "Bushido"
    Ninjitsu: "Ninjitsu"
    Spellweaving: "Spellweaving"
    Mysticism: "Mysticism"
    Imbuing: "Imbuing"
    Throwing: "Throwing"
}
#SkillName: or(
    [
        
            for _, v in #SkillNameEnum {
            v
        }])
#Poison: "Lesser" | "Regular" | "Greater" | "Deadly" | "Lethal" | null
#Body: int & >= 0
#ExpansionEnum: {
    None: "None"
    T2A: "T2A"
    UOR: "UOR"
    UOTD: "UOTD"
    LBR: "LBR"
    AOS: "AOS"
    SE: "SE"
    ML: "ML"
    SA: "SA"
    HS: "HS"
    TOL: "TOL"
    EJ: "EJ"
}
#Expansion: or(
    [
        
            for _, v in #ExpansionEnum {
            v
        }])
#Race: {
    @dotnet({FullName:Server.Race})
    RequiredExpansion: (#Expansion | null)
    MaleBody: (int32 | null)
    MaleGhostBody: (int32 | null)
    FemaleBody: (int32 | null)
    FemaleGhostBody: (int32 | null)
    RaceID: (int32 | null)
    RaceIndex: (int32 | null)
    Name: string
    PluralName: string
}