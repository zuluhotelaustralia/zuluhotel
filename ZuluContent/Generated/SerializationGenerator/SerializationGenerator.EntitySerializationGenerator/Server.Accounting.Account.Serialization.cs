#pragma warning disable

namespace Server.Accounting
{
    public partial class Account
    {
        private const int _version = 3;

        public string Username
        {
            get => _username;
            set
            {
                if (value != _username)
                {
                    _username = value;
                    ((ISerializable)this).MarkDirty();
                }
            }
        }

        public Server.Accounting.Security.PasswordProtectionAlgorithm PasswordAlgorithm
        {
            get => _passwordAlgorithm;
            set
            {
                if (value != _passwordAlgorithm)
                {
                    _passwordAlgorithm = value;
                    ((ISerializable)this).MarkDirty();
                }
            }
        }

        public string Password
        {
            get => _password;
            set
            {
                if (value != _password)
                {
                    _password = value;
                    ((ISerializable)this).MarkDirty();
                }
            }
        }

        public Server.AccessLevel AccessLevel
        {
            get => _accessLevel;
            set
            {
                if (value != _accessLevel)
                {
                    _accessLevel = value;
                    ((ISerializable)this).MarkDirty();
                }
            }
        }

        public int Flags
        {
            get => _flags;
            set
            {
                if (value != _flags)
                {
                    _flags = value;
                    ((ISerializable)this).MarkDirty();
                }
            }
        }

        public System.DateTime Created
        {
            get => _created;
            private set
            {
                if (value != _created)
                {
                    _created = value;
                    ((ISerializable)this).MarkDirty();
                }
            }
        }

        public System.DateTime LastLogin
        {
            get => _lastLogin;
            set
            {
                if (value != _lastLogin)
                {
                    _lastLogin = value;
                    ((ISerializable)this).MarkDirty();
                }
            }
        }

        [CommandProperty(AccessLevel.Administrator)]
        public int TotalGold
        {
            get => _totalGold;
            private set
            {
                if (value != _totalGold)
                {
                    _totalGold = value;
                    ((ISerializable)this).MarkDirty();
                }
            }
        }

        [CommandProperty(AccessLevel.Administrator)]
        public int TotalPlat
        {
            get => _totalPlat;
            private set
            {
                if (value != _totalPlat)
                {
                    _totalPlat = value;
                    ((ISerializable)this).MarkDirty();
                }
            }
        }

        private Server.Mobile[] Mobiles
        {
            get => _mobiles;
            set
            {
                if (value != _mobiles)
                {
                    _mobiles = value;
                    ((ISerializable)this).MarkDirty();
                }
            }
        }

        public System.Net.IPAddress[] LoginIPs
        {
            get => _loginIPs;
            set
            {
                if (value != _loginIPs)
                {
                    _loginIPs = value;
                    ((ISerializable)this).MarkDirty();
                }
            }
        }

        public string[] IpRestrictions
        {
            get => _ipRestrictions;
            set
            {
                if (value != _ipRestrictions)
                {
                    _ipRestrictions = value;
                    ((ISerializable)this).MarkDirty();
                }
            }
        }

        [CommandProperty(AccessLevel.Administrator)]
        public string Email
        {
            get => _email;
            set
            {
                if (value != _email)
                {
                    _email = value;
                    ((ISerializable)this).MarkDirty();
                }
            }
        }

        long ISerializable.SavePosition { get; set; } = -1;
        BufferWriter ISerializable.SaveBuffer { get; set; }
        public Account(Serial serial)
        {
            Serial = serial;
            SetTypeRef(typeof(Account));
        }

        public void Serialize(IGenericWriter writer)
        {
            writer.WriteEncodedInt(_version);

            writer.Write(Username);

            writer.WriteEnum<Server.Accounting.Security.PasswordProtectionAlgorithm>(PasswordAlgorithm);

            writer.Write(Password);

            writer.WriteEnum<Server.AccessLevel>(AccessLevel);

            writer.Write(Flags);

            writer.Write(Created);

            writer.Write(LastLogin);

            writer.Write(TotalGold);

            writer.Write(TotalPlat);

            var mobilesLength = Mobiles?.Length ?? 0;
            writer.Write(mobilesLength);
            for (var mobilesIndex = 0; mobilesIndex < mobilesLength; mobilesIndex++)
            {
                writer.Write(Mobiles![mobilesIndex]);
            }

            var commentsCount = Comments?.Count ?? 0;
            writer.Write(commentsCount);
            if (commentsCount > 0)
            {
                foreach (var commentsEntry in Comments!)
                {
                    commentsEntry.Serialize(writer);
                }
            }

            var tagsCount = Tags?.Count ?? 0;
            writer.Write(tagsCount);
            if (tagsCount > 0)
            {
                foreach (var tagsEntry in Tags!)
                {
                    tagsEntry.Serialize(writer);
                }
            }

            var loginIPsLength = LoginIPs?.Length ?? 0;
            writer.Write(loginIPsLength);
            for (var loginIPsIndex = 0; loginIPsIndex < loginIPsLength; loginIPsIndex++)
            {
                writer.Write(LoginIPs![loginIPsIndex]);
            }

            var ipRestrictionsLength = IpRestrictions?.Length ?? 0;
            writer.Write(ipRestrictionsLength);
            for (var ipRestrictionsIndex = 0; ipRestrictionsIndex < ipRestrictionsLength; ipRestrictionsIndex++)
            {
                writer.Write(IpRestrictions![ipRestrictionsIndex]);
            }

            writer.Write(TotalGameTime);

            writer.Write(Email);
        }

        public void Deserialize(IGenericReader reader)
        {
            var version = reader.ReadEncodedInt();

            if (version < _version)
            {
                Deserialize(reader, version);
                ((Server.ISerializable)this).MarkDirty();
                return;
            }

            Username = reader.ReadString();

            PasswordAlgorithm = reader.ReadEnum<Server.Accounting.Security.PasswordProtectionAlgorithm>();

            Password = reader.ReadString();

            AccessLevel = reader.ReadEnum<Server.AccessLevel>();

            Flags = reader.ReadInt();

            Created = reader.ReadDateTime();

            LastLogin = reader.ReadDateTime();

            TotalGold = reader.ReadInt();

            TotalPlat = reader.ReadInt();

            Mobiles = new Server.Mobile[reader.ReadInt()];
            for (var MobilesIndex = 0; MobilesIndex < Mobiles.Length; MobilesIndex++)
            {
                Mobiles[MobilesIndex] = reader.ReadEntity<Server.Mobile>();
            }

            Server.Accounting.AccountComment commentsEntry;
            var commentsCount = reader.ReadInt();
            Comments = new System.Collections.Generic.List<Server.Accounting.AccountComment>(commentsCount);
            for (var commentsIndex = 0; commentsIndex < commentsCount; commentsIndex++)
            {
                commentsEntry = new Server.Accounting.AccountComment(reader);
                Comments.Add(commentsEntry);
            }

            Server.Accounting.AccountTag tagsEntry;
            var tagsCount = reader.ReadInt();
            Tags = new System.Collections.Generic.List<Server.Accounting.AccountTag>(tagsCount);
            for (var tagsIndex = 0; tagsIndex < tagsCount; tagsIndex++)
            {
                tagsEntry = new Server.Accounting.AccountTag(reader);
                Tags.Add(tagsEntry);
            }

            LoginIPs = new System.Net.IPAddress[reader.ReadInt()];
            for (var LoginIPsIndex = 0; LoginIPsIndex < LoginIPs.Length; LoginIPsIndex++)
            {
                LoginIPs[LoginIPsIndex] = reader.ReadIPAddress();
            }

            IpRestrictions = new string[reader.ReadInt()];
            for (var IpRestrictionsIndex = 0; IpRestrictionsIndex < IpRestrictions.Length; IpRestrictionsIndex++)
            {
                IpRestrictions[IpRestrictionsIndex] = reader.ReadString();
            }

            TotalGameTime = reader.ReadTimeSpan();

            Email = reader.ReadString();

            Timer.DelayCall(AfterDeserialization);
        }
    }
}
