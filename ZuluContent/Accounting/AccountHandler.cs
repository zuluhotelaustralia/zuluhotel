using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using Server.Accounting;
using Server.Engines.Help;
using Server.Logging;
using Server.Network;
using Server.Regions;
using Scripts.Zulu.Packets;

namespace Server.Misc
{
    public static class AccountHandler
    {
        private static readonly ILogger logger = LogFactory.GetLogger(typeof(AccountHandler));

        private static int MaxAccountsPerIP;
        private static bool AutoAccountCreation;
        private static readonly bool RestrictDeletion = !TestCenter.Enabled;
        private static readonly TimeSpan DeleteDelay = TimeSpan.FromDays(7.0);
        private static bool PasswordCommandEnabled;

        public static readonly CityInfo[] StartingCities =
        {
            new("Britain", "The Wayfarer's Inn", 1075074, 1602, 1591, 20)
        };

        private static Dictionary<IPAddress, int> m_IPTable;

        private static readonly char[] m_ForbiddenChars =
        {
            '<', '>', ':', '"', '/', '\\', '|', '?', '*'
        };

        public static AccessLevel LockdownLevel { get; set; }

        public static Dictionary<IPAddress, int> IPTable
        {
            get
            {
                if (m_IPTable == null)
                {
                    m_IPTable = new Dictionary<IPAddress, int>();

                    foreach (Account a in Accounts.GetAccounts())
                    {
                        if (a.LoginIPs.Length > 0)
                        {
                            var ip = a.LoginIPs[0];
                            m_IPTable[ip] = (m_IPTable.TryGetValue(ip, out var value) ? value : 0) + 1;
                        }
                    }
                }

                return m_IPTable;
            }
        }

        public static void Configure()
        {
            MaxAccountsPerIP = ServerConfiguration.GetOrUpdateSetting("accountHandler.maxAccountsPerIP", 1);
            AutoAccountCreation = ServerConfiguration.GetOrUpdateSetting("accountHandler.enableAutoAccountCreation", true);
            PasswordCommandEnabled = ServerConfiguration.GetOrUpdateSetting(
                "accountHandler.enablePlayerPasswordCommand",
                false
            );
        }

        public static void Initialize()
        {
            EventSink.DeleteRequest += EventSink_DeleteRequest;
            EventSink.AccountLogin += EventSink_AccountLogin;
            EventSink.GameLogin += EventSink_GameLogin;

            if (PasswordCommandEnabled)
            {
                CommandSystem.Register("Password", AccessLevel.Player, Password_OnCommand);
            }
            
        }

        [Usage("Password <newPassword> <repeatPassword>"), Description(
             "Changes the password of the commanding players account. Requires the same C-class IP address as the account's creator."
         )]
        public static void Password_OnCommand(CommandEventArgs e)
        {
            var from = e.Mobile;

            if (!(from.Account is Account acct))
            {
                return;
            }

            var accessList = acct.LoginIPs;

            if (accessList.Length == 0)
            {
                return;
            }

            var ns = from.NetState;

            if (ns == null)
            {
                return;
            }

            if (e.Length == 0)
            {
                from.SendMessage("You must specify the new password.");
                return;
            }

            if (e.Length == 1)
            {
                from.SendMessage("To prevent potential typing mistakes, you must type the password twice. Use the format:");
                from.SendMessage("Password \"(newPassword)\" \"(repeated)\"");
                return;
            }

            var pass = e.GetString(0);
            var pass2 = e.GetString(1);

            if (pass != pass2)
            {
                from.SendMessage("The passwords do not match.");
                return;
            }

            var isSafe = true;

            for (var i = 0; isSafe && i < pass.Length; ++i)
            {
                isSafe = pass[i] >= 0x20 && pass[i] < 0x7F;
            }

            if (!isSafe)
            {
                from.SendMessage("That is not a valid password.");
                return;
            }

            try
            {
                var ipAddress = ns.Address;

                if (Utility.IPMatchClassC(accessList[0], ipAddress))
                {
                    acct.SetPassword(pass);
                    from.SendMessage("The password to your account has changed.");
                }
                else
                {
                    var entry = PageQueue.GetEntry(from);

                    if (entry != null)
                    {
                        if (entry.Message.StartsWithOrdinal("[Automated: Change Password]"))
                        {
                            from.SendMessage("You already have a password change request in the help system queue.");
                        }
                        else
                        {
                            from.SendMessage("Your IP address does not match that which created this account.");
                        }
                    }
                    else if (PageQueue.CheckAllowedToPage(from))
                    {
                        from.SendMessage(
                            "Your IP address does not match that which created this account.  A page has been entered into the help system on your behalf."
                        );

                        /* The next available Counselor/Game Master will respond as soon as possible.
                         * Please check your Journal for messages every few minutes.
                         */
                        from.SendLocalizedMessage(501234, "", 0x35);

                        PageQueue.Enqueue(
                            new PageEntry(
                                from,
                                $"[Automated: Change Password]<br>Desired password: {pass}<br>Current IP address: {ipAddress}<br>Account IP address: {accessList[0]}",
                                PageType.Account
                            )
                        );
                    }
                }
            }
            catch
            {
                // ignored
            }
        }

        private static void EventSink_DeleteRequest(NetState state, int index)
        {
            if (!(state.Account is Account acct))
            {
                state.Disconnect("Attempted to delete a character but the account could not be found.");
                return;
            }

            DeleteResultType res;

            if (index < 0 || index >= acct.Length)
            {
                res = DeleteResultType.BadRequest;
            }
            else
            {
                var m = acct[index];

                if (m == null)
                {
                    res = DeleteResultType.CharNotExist;
                }
                else if (m.NetState != null)
                {
                    res = DeleteResultType.CharBeingPlayed;
                }
                else if (RestrictDeletion && Core.Now < m.Created + DeleteDelay)
                {
                    res = DeleteResultType.CharTooYoung;
                }
                else if (m.AccessLevel == AccessLevel.Player &&
                         Region.Find(m.LogoutLocation, m.LogoutMap).IsPartOf<JailRegion>()
                ) // Don't need to check current location, if netstate is null, they're logged out
                {
                    res = DeleteResultType.BadRequest;
                }
                else
                {
                    state.LogInfo("Deleting character {0} (0x{1:X})", index, m.Serial.Value);

                    acct.Comments.Add(new AccountComment("System", $"Character #{index + 1} {m} deleted by {state}"));

                    m.Delete();
                    state.SendCharacterListUpdate(acct);
                    return;
                }
            }

            state.SendCharacterDeleteResult(res);
            state.SendCharacterListUpdate(acct);

        }

        public static bool CanCreate(IPAddress ip) =>
            !IPTable.TryGetValue(ip, out var result) || result < MaxAccountsPerIP;

        private static bool IsForbiddenChar(char c)
        {
            for (var i = 0; i < m_ForbiddenChars.Length; ++i)
            {
                if (c == m_ForbiddenChars[i])
                {
                    return true;
                }
            }

            return false;
        }

        private static Account CreateAccount(NetState state, string un, string pw)
        {
            if (un.Length == 0 || pw.Length == 0)
            {
                return null;
            }

            var isSafe = !(un.StartsWithOrdinal(" ") ||
                           un.EndsWithOrdinal(" ") ||
                           un.EndsWithOrdinal("."));

            for (var i = 0; isSafe && i < un.Length; ++i)
            {
                isSafe = un[i] >= 0x20 && un[i] < 0x7F && !IsForbiddenChar(un[i]);
            }

            for (var i = 0; isSafe && i < pw.Length; ++i)
            {
                isSafe = pw[i] >= 0x20 && pw[i] < 0x7F;
            }

            if (!isSafe)
            {
                return null;
            }

            if (!CanCreate(state.Address))
            {
                logger.Information(
                    "Login: {0}: Account '{1}' not created, ip already has {2} account{3}.",
                    state,
                    un,
                    MaxAccountsPerIP,
                    MaxAccountsPerIP == 1 ? "" : "s"
                );
                return null;
            }

            logger.Information("Login: {0}: Creating new account '{1}'", state, un);

            var a = new Account(un, pw);

            return a;
        }

        public static void EventSink_AccountLogin(AccountLoginEventArgs e)
        {
            if (!IPLimiter.SocketBlock && !IPLimiter.Verify(e.State.Address))
            {
                e.Accepted = false;
                e.RejectReason = ALRReason.InUse;

                logger.Information("Login: {0}: Past IP limit threshold", e.State);

                using var op = new StreamWriter("ipLimits.log", true);
                op.WriteLine("{0}\tPast IP limit threshold\t{1}", e.State, Core.Now);

                return;
            }

            var un = e.Username;
            var pw = e.Password;

            e.Accepted = false;

            if (!(Accounts.GetAccount(un) is Account acct))
            {
                // To prevent someone from making an account of just '' or a bunch of meaningless spaces
                if (AutoAccountCreation && un.Trim().Length > 0)
                {
                    e.State.Account = acct = CreateAccount(e.State, un, pw);
                    e.Accepted = acct?.CheckAccess(e.State) ?? false;

                    if (!e.Accepted)
                    {
                        e.RejectReason = ALRReason.BadComm;
                    }
                }
                else
                {
                    logger.Information("Login: {0}: Invalid username '{1}'", e.State, un);
                    e.RejectReason = ALRReason.Invalid;
                }
            }
            else if (!acct.HasAccess(e.State))
            {
                logger.Information("Login: {0}: Access denied for '{1}'", e.State, un);
                e.RejectReason = LockdownLevel > AccessLevel.Player ? ALRReason.BadComm : ALRReason.BadPass;
            }
            else if (!acct.CheckPassword(pw) && !TokenAuthHandler.ConsumeLoginToken(acct, pw))
            {
                logger.Information("Login: {0}: Invalid password for '{1}'", e.State, un);
                e.RejectReason = ALRReason.BadPass;
            }
            else if (acct.Banned)
            {
                logger.Information("Login: {0}: Banned account '{1}'", e.State, un);
                e.RejectReason = ALRReason.Blocked;
            }
            else
            {
                logger.Information("Login: {0}: Valid credentials for '{1}'", e.State, un);
                e.State.Account = acct;
                e.Accepted = true;

                acct.LogAccess(e.State);
            }

            if (!e.Accepted)
            {
                AccountAttackLimiter.RegisterInvalidAccess(e.State);
            }
        }

        public static void EventSink_GameLogin(GameLoginEventArgs e)
        {
            if (!IPLimiter.SocketBlock && !IPLimiter.Verify(e.State.Address))
            {
                e.Accepted = false;

                logger.Warning("Login: {0}: Past IP limit threshold", e.State);

                using var op = new StreamWriter("ipLimits.log", true);
                op.WriteLine("{0}\tPast IP limit threshold\t{1}", e.State, Core.Now);

                return;
            }

            var un = e.Username;
            var pw = e.Password;

            if (Accounts.GetAccount(un) is not Account acct)
            {
                e.Accepted = false;
            }
            else if (!acct.HasAccess(e.State))
            {
                logger.Information("Login: {0}: Access denied for '{1}'", e.State, un);
                e.Accepted = false;
            }
            else if (!acct.CheckPassword(pw) && !TokenAuthHandler.ConsumeLoginToken(acct, pw))
            {
                logger.Information("Login: {0}: Invalid password for '{1}'", e.State, un);
                e.Accepted = false;
            }
            else if (acct.Banned)
            {
                logger.Information("Login: {0}: Banned account '{1}'", e.State, un);
                e.Accepted = false;
            }
            else
            {
                acct.LogAccess(e.State);

                logger.Information("Login: {0}: Account '{1}' at character list", e.State, un);
                e.State.Account = acct;
                e.Accepted = true;
                e.CityInfo = StartingCities;
            }

            if (!e.Accepted)
            {
                AccountAttackLimiter.RegisterInvalidAccess(e.State);
            }
            
            e.State.PacketEncoder = OutgoingPacketInterceptor.Intercept;
        }

        public static bool CheckAccount(Mobile mobCheck, Mobile accCheck)
        {
            if (accCheck?.Account is Account a)
            {
                for (var i = 0; i < a.Length; ++i)
                {
                    if (a[i] == mobCheck)
                    {
                        return true;
                    }
                }
            }

            return false;
        }
    }
}
