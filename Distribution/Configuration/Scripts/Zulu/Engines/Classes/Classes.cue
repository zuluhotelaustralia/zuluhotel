
package Classes

#ZuluClassTypeEnum: {
    None: "None"
    Bard: "Bard"
    Crafter: "Crafter"
    Mage: "Mage"
    Ranger: "Ranger"
    Thief: "Thief"
    Warrior: "Warrior"
    PowerPlayer: "PowerPlayer"
}
#ZuluClassType: or(
    [
        
            for _, v in #ZuluClassTypeEnum {
            v
        }])