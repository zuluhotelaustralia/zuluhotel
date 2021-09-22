using System;
using Server.Accounting;

namespace Server.Misc
{
    public static class AccountPrompt
    {
        private static readonly bool AutoCreateDefaultOwnerAccount;

        private static readonly string DefaultOwnerAcctName; 
        private static readonly string DefaultOwnerAcctPassword; 
        private static readonly string DefaultOwnerPlayerName;

        static AccountPrompt()
        {
            AutoCreateDefaultOwnerAccount = 
                ServerConfiguration.GetOrUpdateSetting("accountPrompt.autoCreateDefaultOwnerAccount", true);
            DefaultOwnerAcctName = 
                ServerConfiguration.GetOrUpdateSetting("accountPrompt.defaultOwnerAcctName", "owner");
            DefaultOwnerAcctPassword =
                ServerConfiguration.GetOrUpdateSetting("accountPrompt.defaultOwnerAcctPassword", "owner");
            DefaultOwnerPlayerName =
                ServerConfiguration.GetOrUpdateSetting("accountPrompt.defaultOwnerPlayerName", "owner");
        }

        public static void Initialize()
        {
            if (Accounts.Count == 0)
            {
                var key = ConsoleKey.D;
                if (!AutoCreateDefaultOwnerAccount)
                {
                    Console.WriteLine("This server has no accounts.");
                    Console.Write("Do you want to create the owner account now? (y/n), " +
                                  "Or create the default owner account? (d)");
                    key = Console.ReadKey(true).Key;
                }

                switch (key)
                {
                    case ConsoleKey.Y:
                    {
                        Console.WriteLine();

                        Console.Write("Username: ");
                        var username = Console.ReadLine();

                        Console.Write("Password: ");
                        var password = Console.ReadLine();

                        var account = new Account(username, password) {AccessLevel = AccessLevel.Owner};

                        Console.WriteLine("Account created.");
                        break;
                    }
                    case ConsoleKey.D:
                    {
                        var account = new Account(DefaultOwnerAcctName, DefaultOwnerAcctPassword) {AccessLevel = AccessLevel.Owner};
                        
                        var args = new CharacterCreatedEventArgs(
                            null,
                            account,
                            DefaultOwnerPlayerName,
                            false,
                            Race.Human.RandomSkinHue(),
                            130,
                            130,
                            130,
                            Utility.RandomList(AccountHandler.StartingCities),
                            new SkillNameValue[]
                            {
                                new(SkillName.Alchemy, 0),
                                new(SkillName.Anatomy, 0),
                                new(SkillName.Archery, 0),
                            },
                            Race.Human.RandomSkinHue(), 
                            Utility.RandomDyedHue(), 
                            Utility.RandomDyedHue(), 
                            Race.Human.RandomHair(false), 
                            Race.Human.RandomFacialHair(false), 
                            Race.Human.RandomHairHue(), 
                            0,
                            Race.Human
                        );
                        CharacterCreation.HandleCharacterCreation(args);
                        
                        Console.WriteLine($"Default owner account created ({DefaultOwnerAcctName} / {DefaultOwnerAcctPassword}).");
                        break;
                    }
                    default:
                        Console.WriteLine();

                        Console.WriteLine("Account not created.");
                        break;
                }
            }
        }
    }
}