using System;
using Scripts.Zulu.Utilities;

namespace Server.Engines.Harvest
{
    public class HarvestResource
    {
        public Type[] Types { get; set; }

        public double ReqSkill { get; set; }

        public object SuccessMessage { get; }

        public void SendSuccessTo(Mobile m, int amount)
        {
            switch (SuccessMessage)
            {
                case int message:
                    m.SendLocalizedMessage(message);
                    break;
                case string message:
                    m.SendSuccessMessage($"You put {amount} {message} in your backpack.");
                    break;
            }
        }

        public HarvestResource(double reqSkill, object message, params Type[] types)
        {
            ReqSkill = reqSkill;
            Types = types;
            SuccessMessage = message;
        }
    }
}