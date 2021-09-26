using System;
using System.Collections.Generic;
using Server.Commands.Generic;
using Server.Gumps;
using Server.Items;
using Server.Misc;
using Server.Mobiles;
using Server.Network;
using Server.Targeting;

namespace Server.Guilds
{
    #region Ranks

    [Flags]
    public enum RankFlags
    {
        None = 0x00000000,
        CanInvitePlayer = 0x00000001,
        AccessGuildItems = 0x00000002,
        RemoveLowestRank = 0x00000004,
        RemovePlayers = 0x00000008,
        CanPromoteDemote = 0x00000010,
        ControlWarStatus = 0x00000020,
        AllianceControl = 0x00000040,
        CanSetGuildTitle = 0x00000080,
        CanVote = 0x00000100,

        All = Member | CanInvitePlayer | RemovePlayers | CanPromoteDemote | ControlWarStatus | AllianceControl |
              CanSetGuildTitle,
        Member = RemoveLowestRank | AccessGuildItems | CanVote
    }

    public class RankDefinition
    {
        public static RankDefinition[] Ranks =
        {
            new(1062963, 0, RankFlags.None), //Ronin
            new(1062962, 1, RankFlags.Member), //Member
            new(1062961, 2,
                RankFlags.Member | RankFlags.RemovePlayers | RankFlags.CanInvitePlayer | RankFlags.CanSetGuildTitle |
                RankFlags.CanPromoteDemote), //Emmissary
            new(1062960, 3, RankFlags.Member | RankFlags.ControlWarStatus), //Warlord
            new(1062959, 4, RankFlags.All) //Leader
        };

        public static RankDefinition Leader => Ranks[4];

        public static RankDefinition Member => Ranks[1];

        public static RankDefinition Lowest => Ranks[0];

        public TextDefinition Name { get; }

        public int Rank { get; }

        public RankFlags Flags { get; private set; }

        public RankDefinition(TextDefinition name, int rank, RankFlags flags)
        {
            Name = name;
            Rank = rank;
            Flags = flags;
        }

        public bool GetFlag(RankFlags flag)
        {
            return (Flags & flag) != 0;
        }

        public void SetFlag(RankFlags flag, bool value)
        {
            if (value)
                Flags |= flag;
            else
                Flags &= ~flag;
        }
    }

    #endregion

    public class Guild : BaseGuild
    {
        public static void Configure()
        {
            EventSink.GuildGumpRequest += EventSink_GuildGumpRequest;

            CommandSystem.Register("GuildProps", AccessLevel.Counselor, GuildProps_OnCommand);
        }

        #region GuildProps

        [Usage("GuildProps")]
        [Description(
            "Opens a menu where you can view and edit guild properties of a targeted player or guild stone.  If the new Guild system is active, also brings up the guild gump.")]
        private static void GuildProps_OnCommand(CommandEventArgs e)
        {
            var arg = e.ArgString.Trim();
            var from = e.Mobile;

            if (arg.Length == 0)
            {
                e.Mobile.Target = new GuildPropsTarget();
            }
            else
            {
                var g = uint.TryParse(arg, out var id)
                    ? World.FindGuild((Serial)id) as Guild
                    : FindByAbbrev(arg) as Guild ?? FindByName(arg) as Guild;

                if (g != null) from.SendGump(new PropertiesGump(from, g));
            }
        }

        private class GuildPropsTarget : Target
        {
            public GuildPropsTarget() : base(-1, true, TargetFlags.None)
            {
            }

            protected override void OnTarget(Mobile from, object o)
            {
                if (!BaseCommand.IsAccessible(from, o))
                {
                    from.SendMessage("That is not accessible.");
                    return;
                }

                Guild g = null;

                if (o is Guildstone)
                {
                    var stone = o as Guildstone;
                    if (stone.Guild == null || stone.Guild.Disbanded)
                    {
                        from.SendMessage("The guild associated with that Guildstone no longer exists");
                        return;
                    }

                    g = stone.Guild;
                }
                else if (o is Mobile)
                {
                    g = ((Mobile)o).Guild as Guild;
                }

                if (g != null)
                    from.SendGump(new PropertiesGump(from, g));
                else
                    from.SendMessage("That is not in a guild!");
            }
        }

        #endregion

        #region EventSinks

        public static void EventSink_GuildGumpRequest(Mobile mobile)
        {
        }

        #endregion

        public static readonly int RegistrationFee = 25000;
        public static readonly int AbbrevLimit = 4;
        public static readonly int NameLimit = 40;
        public static readonly int MajorityPercentage = 66;
        public static readonly TimeSpan InactiveTime = TimeSpan.FromDays(30);

        #region Var declarations

        private Mobile m_Leader;

        private string m_Name;
        private string m_Abbreviation;

        private GuildType m_Type;

        #endregion

        public Guild(Mobile leader, string name, string abbreviation)
        {
            #region Ctor mumbo-jumbo

            m_Leader = leader;

            Members = new List<Mobile>();
            Allies = new List<Guild>();
            Enemies = new List<Guild>();
            WarDeclarations = new List<Guild>();
            WarInvitations = new List<Guild>();
            AllyDeclarations = new List<Guild>();
            AllyInvitations = new List<Guild>();
            Candidates = new List<Mobile>();
            Accepted = new List<Mobile>();

            LastFealty = DateTime.Now;

            m_Name = name;
            m_Abbreviation = abbreviation;

            TypeLastChange = DateTime.MinValue;

            AddMember(m_Leader);

            if (m_Leader is PlayerMobile)
                ((PlayerMobile)m_Leader).GuildRank = RankDefinition.Leader;

            #endregion
        }

        public Guild(Serial serial) : base(serial) //serialization ctor
        {
        }

        public void InvalidateMemberProperties()
        {
            InvalidateMemberProperties(false);
        }

        public void InvalidateMemberProperties(bool onlyOPL)
        {
            if (Members != null)
                for (var i = 0; i < Members.Count; i++)
                {
                    var m = Members[i];

                    if (!onlyOPL)
                        m.Delta(MobileDelta.Noto);
                }
        }

        public void InvalidateMemberNotoriety()
        {
            if (Members != null)
                for (var i = 0; i < Members.Count; i++)
                    Members[i].Delta(MobileDelta.Noto);
        }

        [CommandProperty(AccessLevel.GameMaster)]
        public Mobile Leader
        {
            get
            {
                if (m_Leader == null || m_Leader.Deleted || m_Leader.Guild != this)
                    CalculateGuildmaster();

                return m_Leader;
            }
            set
            {
                if (value != null)
                    AddMember(value); //Also removes from old guild.

                if (m_Leader is PlayerMobile mobile && mobile.Guild == this)
                    mobile.GuildRank = RankDefinition.Member;

                m_Leader = value;

                if (m_Leader is PlayerMobile)
                    ((PlayerMobile)m_Leader).GuildRank = RankDefinition.Leader;
            }
        }


        public override bool Disbanded => m_Leader == null || m_Leader.Deleted;

        public override void OnDelete(Mobile mob)
        {
            RemoveMember(mob);
        }

        public void Disband()
        {
            m_Leader = null;

            foreach (var m in Members)
            {
                m.SendLocalizedMessage(502131); // Your guild has disbanded.

                if (m is PlayerMobile)
                    ((PlayerMobile)m).GuildRank = RankDefinition.Lowest;

                m.Guild = null;
            }

            Members.Clear();

            for (var i = Allies.Count - 1; i >= 0; --i)
                if (i < Allies.Count)
                    RemoveAlly(Allies[i]);

            for (var i = Enemies.Count - 1; i >= 0; --i)
                if (i < Enemies.Count)
                    RemoveEnemy(Enemies[i]);

            Guildstone?.Delete();

            Guildstone = null;
        }

        #region Is<something>(...)

        public bool IsMember(Mobile m)
        {
            return Members.Contains(m);
        }

        public bool IsAlly(Guild g)
        {
            return Allies.Contains(g);
        }

        public bool IsEnemy(Guild g)
        {
            if (Type != GuildType.Regular && g.Type != GuildType.Regular && Type != g.Type)
                return true;

            return IsWar(g);
        }

        public bool IsWar(Guild g)
        {
            if (g == null)
                return false;

            return Enemies.Contains(g);
        }

        #endregion

        #region Serialization
        
        public override void BeforeSerialize()
        {
        }

        public override void Serialize(IGenericWriter writer)
        {
            if (LastFealty + TimeSpan.FromDays(1.0) < DateTime.Now)
                CalculateGuildmaster();

            writer.Write(5); //version

            writer.Write(AllyDeclarations);
            writer.Write(AllyInvitations);

            writer.Write(TypeLastChange);

            writer.Write((int)m_Type);

            writer.Write(LastFealty);

            writer.Write(m_Leader);
            writer.Write(m_Name);
            writer.Write(m_Abbreviation);

            writer.Write(Allies);
            writer.Write(Enemies);
            writer.Write(WarDeclarations);
            writer.Write(WarInvitations);

            writer.Write(Members);
            writer.Write(Candidates);
            writer.Write(Accepted);

            writer.Write(Guildstone);
            writer.Write(Teleporter);

            writer.Write(Charter);
            writer.Write(Website);
        }

        public override void Delete()
        {
            World.RemoveGuild(this);
        }

        public override void Deserialize(IGenericReader reader)
        {
            var version = reader.ReadInt();

            switch (version)
            {
                case 5:
                case 4:
                {
                    AllyDeclarations = reader.ReadEntityList<Guild>();
                    AllyInvitations = reader.ReadEntityList<Guild>();

                    goto case 3;
                }
                case 3:
                {
                    TypeLastChange = reader.ReadDateTime();

                    goto case 2;
                }
                case 2:
                {
                    m_Type = (GuildType)reader.ReadInt();

                    goto case 1;
                }
                case 1:
                {
                    LastFealty = reader.ReadDateTime();

                    goto case 0;
                }
                case 0:
                {
                    m_Leader = reader.ReadEntity<Mobile>();

                    if (m_Leader is PlayerMobile)
                        ((PlayerMobile)m_Leader).GuildRank = RankDefinition.Leader;

                    m_Name = reader.ReadString();
                    m_Abbreviation = reader.ReadString();

                    Allies = reader.ReadEntityList<Guild>();
                    Enemies = reader.ReadEntityList<Guild>();
                    WarDeclarations = reader.ReadEntityList<Guild>();
                    WarInvitations = reader.ReadEntityList<Guild>();

                    Members = reader.ReadEntityList<Mobile>();
                    ;
                    Candidates = reader.ReadEntityList<Mobile>();
                    ;
                    Accepted = reader.ReadEntityList<Mobile>();
                    ;

                    Guildstone = reader.ReadEntity<Item>();
                    Teleporter = reader.ReadEntity<Item>();

                    Charter = reader.ReadString();
                    Website = reader.ReadString();

                    break;
                }
            }

            if (AllyDeclarations == null)
                AllyDeclarations = new List<Guild>();

            if (AllyInvitations == null)
                AllyInvitations = new List<Guild>();

            Timer.DelayCall(TimeSpan.Zero, VerifyGuild_Callback);
        }

        private void VerifyGuild_Callback()
        {
            if (Guildstone == null || Members.Count == 0)
                Disband();
        }

        #endregion

        #region Add/Remove Member/Old Ally/Old Enemy

        public void AddMember(Mobile m)
        {
            if (!Members.Contains(m))
            {
                if (m.Guild != null && m.Guild != this)
                    ((Guild)m.Guild).RemoveMember(m);

                Members.Add(m);
                m.Guild = this;
                m.GuildFealty = m_Leader;

                if (m is PlayerMobile)
                    ((PlayerMobile)m).GuildRank = RankDefinition.Lowest;
            }
        }

        public void RemoveMember(Mobile m)
        {
            RemoveMember(m, 1018028); // You have been dismissed from your guild.
        }

        public void RemoveMember(Mobile m, int message)
        {
            if (Members.Contains(m))
            {
                Members.Remove(m);

                var guild = m.Guild as Guild;

                m.Guild = null;

                if (m is PlayerMobile)
                    ((PlayerMobile)m).GuildRank = RankDefinition.Lowest;

                if (message > 0)
                    m.SendLocalizedMessage(message);

                if (m == m_Leader)
                {
                    CalculateGuildmaster();

                    if (m_Leader == null)
                        Disband();
                }

                if (Members.Count == 0)
                    Disband();

                m.Delta(MobileDelta.Noto);
            }
        }

        public void AddAlly(Guild g)
        {
            if (!Allies.Contains(g))
            {
                Allies.Add(g);

                g.AddAlly(this);
            }
        }

        public void RemoveAlly(Guild g)
        {
            if (Allies.Contains(g))
            {
                Allies.Remove(g);

                g.RemoveAlly(this);
            }
        }

        public void AddEnemy(Guild g)
        {
            if (!Enemies.Contains(g))
            {
                Enemies.Add(g);

                g.AddEnemy(this);
            }
        }

        public void RemoveEnemy(Guild g)
        {
            if (Enemies != null && Enemies.Contains(g))
            {
                Enemies.Remove(g);

                g.RemoveEnemy(this);
            }
        }

        #endregion

        #region Guild[Text]Message(...)

        public void GuildMessage(int num, bool append, string format, params object[] args)
        {
            GuildMessage(num, append, string.Format(format, args));
        }

        public void GuildMessage(int number)
        {
            for (var i = 0; i < Members.Count; ++i)
                Members[i].SendLocalizedMessage(number);
        }

        public void GuildMessage(int number, string args)
        {
            GuildMessage(number, args, 0x3B2);
        }

        public void GuildMessage(int number, string args, int hue)
        {
            for (var i = 0; i < Members.Count; ++i)
                Members[i].SendLocalizedMessage(number, args, hue);
        }

        public void GuildMessage(int number, bool append, string affix)
        {
            GuildMessage(number, append, affix, "", 0x3B2);
        }

        public void GuildMessage(int number, bool append, string affix, string args)
        {
            GuildMessage(number, append, affix, args, 0x3B2);
        }

        public void GuildMessage(int number, bool append, string affix, string args, int hue)
        {
            for (var i = 0; i < Members.Count; ++i)
                Members[i].SendLocalizedMessage(number, append, affix, args, hue);
        }

        public void GuildTextMessage(string text)
        {
            GuildTextMessage(0x3B2, text);
        }

        public void GuildTextMessage(string format, params object[] args)
        {
            GuildTextMessage(0x3B2, string.Format(format, args));
        }

        public void GuildTextMessage(int hue, string text)
        {
            for (var i = 0; i < Members.Count; ++i)
                Members[i].SendMessage(hue, text);
        }

        public void GuildTextMessage(int hue, string format, params object[] args)
        {
            GuildTextMessage(hue, string.Format(format, args));
        }

        public void GuildChat(Mobile from, int hue, string text)
        {
            var buffer = stackalloc byte[OutgoingMessagePackets.GetMaxMessageLength(text)].InitializePacket();

            for (var i = 0; i < Members.Count; i++)
            {
                var length = OutgoingMessagePackets.CreateMessage(
                    buffer, from.Serial, from.Body, MessageType.Guild, hue, 3, false, from.Language,
                    from.Name, text
                );

                if (length != buffer.Length) buffer = buffer.Slice(0, length); // Adjust to the actual size

                Members[i].NetState?.Send(buffer);
            }
        }

        public void GuildChat(Mobile from, string text)
        {
            var pm = from as PlayerMobile;

            GuildChat(from, pm == null ? 0x3B2 : pm.GuildMessageHue, text);
        }

        #endregion

        #region Voting

        public bool CanVote(Mobile m)
        {
            return m != null && !m.Deleted && m.Guild == this;
        }

        public bool CanBeVotedFor(Mobile m)
        {
            return m != null && !m.Deleted && m.Guild == this;
        }

        public void CalculateGuildmaster()
        {
            var votes = new Dictionary<Mobile, int>();

            var votingMembers = 0;

            for (var i = 0; Members != null && i < Members.Count; ++i)
            {
                var memb = Members[i];

                if (!CanVote(memb))
                    continue;

                var m = memb.GuildFealty;

                if (!CanBeVotedFor(m))
                {
                    if (m_Leader != null && !m_Leader.Deleted && m_Leader.Guild == this)
                        m = m_Leader;
                    else
                        m = memb;
                }

                if (m == null)
                    continue;

                int v;

                if (!votes.TryGetValue(m, out v))
                    votes[m] = 1;
                else
                    votes[m] = v + 1;

                votingMembers++;
            }

            Mobile winner = null;
            var highVotes = 0;

            foreach (var kvp in votes)
            {
                var m = kvp.Key;
                var val = kvp.Value;

                if (winner == null || val > highVotes)
                {
                    winner = m;
                    highVotes = val;
                }
            }

            if (m_Leader != winner && winner != null)
                GuildMessage(1018015, true, winner.Name); // Guild Message: Guildmaster changed to:

            Leader = winner;
            LastFealty = DateTime.Now;
        }

        #endregion

        #region Getters & Setters

        [CommandProperty(AccessLevel.GameMaster)]
        public Item Guildstone { get; set; }

        [CommandProperty(AccessLevel.GameMaster)]
        public Item Teleporter { get; set; }

        [CommandProperty(AccessLevel.GameMaster)]
        public override string Name
        {
            get => m_Name;
            set
            {
                m_Name = value;

                InvalidateMemberProperties(true);
            }
        }

        [CommandProperty(AccessLevel.GameMaster)]
        public string Website { get; set; }

        [CommandProperty(AccessLevel.GameMaster)]
        public override string Abbreviation
        {
            get => m_Abbreviation;
            set
            {
                m_Abbreviation = value;

                InvalidateMemberProperties(true);
            }
        }

        [CommandProperty(AccessLevel.GameMaster)]
        public string Charter { get; set; }

        [CommandProperty(AccessLevel.GameMaster)]
        public override GuildType Type
        {
            get => GuildType.Regular;
            set
            {
                if (m_Type != value)
                {
                    m_Type = value;
                    TypeLastChange = DateTime.Now;

                    InvalidateMemberProperties();
                }
            }
        }

        [CommandProperty(AccessLevel.GameMaster)]
        public DateTime LastFealty { get; set; }

        [CommandProperty(AccessLevel.GameMaster)]
        public DateTime TypeLastChange { get; private set; }

        public List<Guild> Allies { get; private set; }

        public List<Guild> Enemies { get; private set; }

        public List<Guild> AllyDeclarations { get; private set; }

        public List<Guild> AllyInvitations { get; private set; }

        public List<Guild> WarDeclarations { get; private set; }

        public List<Guild> WarInvitations { get; private set; }

        public List<Mobile> Candidates { get; private set; }

        public List<Mobile> Accepted { get; private set; }

        public List<Mobile> Members { get; private set; }

        #endregion
    }
}