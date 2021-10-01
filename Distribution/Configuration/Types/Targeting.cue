package Types

#TargetFlags: "None" | "Harmful" | "Beneficial"

#TargetOptions: {
    Range:int32
    AllowGround:bool
    AllowMultis:bool
    AllowNonLocal:bool
    CheckLos:bool
    Flags: #TargetFlags
}