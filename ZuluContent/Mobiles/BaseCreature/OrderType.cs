namespace Server.Mobiles
{
    public enum OrderType
    {
        None, //When no order, let's roam
        Come, //"(All/Name) come"  Summons all or one pet to your location.
        Drop, //"(Name) drop"  Drops its loot to the ground (if it carries any).
        Follow, //"(Name) follow"  Follows targeted being.

        //"(All/Name) follow me"  Makes all or one pet follow you.
        Friend, //"(Name) friend"  Allows targeted player to confirm resurrection.
        Unfriend, // Remove a friend
        Guard, //"(Name) guard"  Makes the specified pet guard you. Pets can only guard their owner.

        //"(All/Name) guard me"  Makes all or one pet guard you.
        Attack, //"(All/Name) kill",

        //"(All/Name) attack"  All or the specified pet(s) currently under your control attack the target.
        Patrol, //"(Name) patrol"  Roves between two or more guarded targets.
        Release, //"(Name) release"  Releases pet back into the wild (removes "tame" status).
        Stay, //"(All/Name) stay" All or the specified pet(s) will stop and stay in current spot.
        Stop, //"(All/Name) stop Cancels any current orders to attack, guard or follow.
        Transfer, //"(Name) transfer" Transfers complete ownership to targeted player.
        Loot,
    }
}