using System;
using static Server.Configurations.MessageHueConfiguration;

namespace Server.Engines.Harvest
{
    public class HarvestResource
    {
        public Type[] Types { get; set; }

        public double ReqSkill { get; set; }

        public object SuccessMessage { get; }

        public void SendSuccessTo(Mobile m, int amount)
        {
            if (SuccessMessage is int)
                m.SendLocalizedMessage((int) SuccessMessage);
            else if (SuccessMessage is string)
                m.SendMessage(MessageSuccessHue, $"You put {amount} {(string) SuccessMessage} ore in your backpack.");
        }

        public HarvestResource(double reqSkill, object message, params Type[] types)
        {
            ReqSkill = reqSkill;
            Types = types;
            SuccessMessage = message;
        }
    }
}