namespace Server.Commands.Generic
{
    public class ScreenCommandImplementor : BaseCommandImplementor
    {
        public ScreenCommandImplementor()
        {
            Accessors = new[] {"Screen"};
            SupportRequirement = CommandSupport.Area;
            SupportsConditionals = true;
            AccessLevel = AccessLevel.GameMaster;
            Usage = "Screen <command> [condition]";
            Description =
                "Invokes the command on all appropriate objects in your screen. Optional condition arguments can further restrict the set of objects.";
        }

        public override void Process(Mobile from, BaseCommand command, string[] args)
        {
            var impl = RangeCommandImplementor.Instance;

            impl?.Process(18, from, command, args);
        }
    }
}