using System;
using Pluralize.NET;
using Scripts.Zulu.Utilities;
using Server.Misc;

namespace Server.Engines.Harvest
{
    public class HarvestResource
    {
        private static readonly IPluralize pluralizer = new Pluralizer();
        
        public Type[] Types { get; set; }
        public double ReqSkill { get; set; }
        public TextDefinition SuccessMessage { get; }
        public string Suffix { get; }

        public void SendSuccessTo(Mobile m, int amount)
        {
            switch (SuccessMessage)
            {
                case { IsNumber: true }:
                    m.SendSuccessMessage(SuccessMessage);
                    break;
                case { IsString: true }:
                {
                    var message = $"{SuccessMessage} {(!string.IsNullOrEmpty(Suffix) ? pluralizer.Format(Suffix, amount) : "")}";
                    m.SendSuccessMessage($"You put {amount} {message.Trim()} in your backpack.");
                    break;
                }
            }
        }

        public HarvestResource(double reqSkill, TextDefinition message, string suffix, params Type[] types)
        {
            ReqSkill = reqSkill;
            Types = types;
            SuccessMessage = message;
            Suffix = suffix;
        }
    }
}