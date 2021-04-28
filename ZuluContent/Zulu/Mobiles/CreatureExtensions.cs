#nullable enable
using System;
using Server.Mobiles;
using Server.Utilities;

namespace Server
{
    public static class Creatures
    {
        public static BaseCreatureTemplate? Create(string template)
        {
            if (!ZhConfig.Creatures.Entries.TryGetValue(template, out var properties))
            {
                Utility.PushColor(ConsoleColor.DarkYellow);
                Console.WriteLine($"Failed {nameof(BaseCreatureTemplate)} creation, template {template} does not exist:");
                Console.WriteLine(Environment.StackTrace);
                Utility.PopColor();
                return null;
            }
            
            BaseCreatureTemplate creature;
            if (properties.BaseType != typeof(BaseCreatureTemplate))
            {
                if (!properties.BaseType.IsAssignableTo(typeof(BaseCreatureTemplate)))
                {
                    Utility.PushColor(ConsoleColor.DarkYellow);
                    Console.WriteLine($"BaseType {properties.BaseType} is not assignable to {typeof(BaseCreature)}");
                    Console.WriteLine(Environment.StackTrace);
                    Utility.PopColor();
                    return null;
                }

                try
                {
                    creature = properties.BaseType.CreateInstance<BaseCreatureTemplate>(template);
                }
                catch (Exception e)
                {
                    Utility.PushColor(ConsoleColor.Red);

                    Console.WriteLine($"Exception raised during {nameof(BaseCreatureTemplate)}.{nameof(Create)}");
                    Console.WriteLine(e);
                    Utility.PopColor();
                    return null;
                }
            }
            else
            {
                creature = new BaseCreatureTemplate(template);
            }

            return creature;
        }

        public static bool Exists(string template) => ZhConfig.Creatures.Entries.ContainsKey(template);
    }
}