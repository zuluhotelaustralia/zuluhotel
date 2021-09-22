
package Configuration
import (
    System "github.com/zuluhotelaustralia/zuluhotel/System"
    Misc "github.com/zuluhotelaustralia/zuluhotel/Server/Misc"
    Server "github.com/zuluhotelaustralia/zuluhotel/Server"
)

#CraftResource: {
    @dotnet({FullName:Scripts.Configuration.CraftResource})
    ItemType: System.#Type
    Name: (Misc.#TextDefinition | null)
    Amount: (int32 | null)
    Message: (Misc.#TextDefinition | null)
}
#CraftEntry: {
    @dotnet({FullName:Scripts.Configuration.CraftEntry})
    ItemType: System.#Type
    Name: (Misc.#TextDefinition | null)
    GroupName: (Misc.#TextDefinition | null)
    Skill: (number | null)
    SecondarySkill: (Server.#SkillName | null)
    Skill2: (number | null)
    Resources: [
        ...#CraftResource]
    UseAllRes: (bool | null)
    NeedHeat: (bool | null)
    NeedOven: (bool | null)
    NeedMill: (bool | null)
}