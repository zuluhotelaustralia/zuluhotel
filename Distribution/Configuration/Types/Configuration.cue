package Types

#CraftResource: {
    ItemType: #Type
    Name: (#TextDefinition | null)
    Amount: (int32 | null)
    Message: (#TextDefinition | null)
}
#CraftEntry: {
    ItemType: #Type
    Name: (#TextDefinition | null)
    GroupName: (#TextDefinition | null)
    Skill: (number | null)
    SecondarySkill: (#SkillName | null)
    Skill2: (number | null)
    Resources: [...#CraftResource]
    UseAllRes: (bool | null)
    NeedHeat: (bool | null)
    NeedOven: (bool | null)
    NeedMill: (bool | null)
}