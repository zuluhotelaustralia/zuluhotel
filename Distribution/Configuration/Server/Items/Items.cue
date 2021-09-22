
package Items

#WeaponAnimationEnum: {
    Slash1H: "Slash1H"
    Pierce1H: "Pierce1H"
    Bash1H: "Bash1H"
    Bash2H: "Bash2H"
    Slash2H: "Slash2H"
    Pierce2H: "Pierce2H"
    ShootBow: "ShootBow"
    ShootXBow: "ShootXBow"
    Wrestle: "Wrestle"
}
#WeaponAnimation: or(
    [
        
            for _, v in #WeaponAnimationEnum {
            v
        }])