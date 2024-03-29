using System;
using System.Collections.Generic;
using System.Text;
using Server.Mobiles;
using Server.Network;
using Server.Spells;

namespace Server.Items
{
    public class Teleporter : Item
	{
		private bool m_Active, m_Creatures, m_CombatCheck, m_CriminalCheck;
		private Point3D m_PointDest;
		private Map m_MapDest;
		private bool m_SourceEffect;
		private bool m_DestEffect;
		private int m_SoundID;
		private TimeSpan m_Delay;

		[CommandProperty(AccessLevel.GameMaster)]
		public bool SourceEffect
		{
			get { return m_SourceEffect; }
			set { m_SourceEffect = value; }
		}

		[CommandProperty(AccessLevel.GameMaster)]
		public bool DestEffect
		{
			get { return m_DestEffect; }
			set { m_DestEffect = value; }
		}

		[CommandProperty(AccessLevel.GameMaster)]
		public int SoundID
		{
			get { return m_SoundID; }
			set { m_SoundID = value; }
		}

		[CommandProperty(AccessLevel.GameMaster)]
		public TimeSpan Delay
		{
			get { return m_Delay; }
			set { m_Delay = value; }
		}

		[CommandProperty(AccessLevel.GameMaster)]
		public bool Active
		{
			get { return m_Active; }
			set { m_Active = value; }
		}

		[CommandProperty(AccessLevel.GameMaster)]
		public Point3D PointDest
		{
			get { return m_PointDest; }
			set { m_PointDest = value; }
		}

		[CommandProperty(AccessLevel.GameMaster)]
		public Map MapDest
		{
			get { return m_MapDest; }
			set { m_MapDest = value; }
		}

		[CommandProperty(AccessLevel.GameMaster)]
		public bool Creatures
		{
			get { return m_Creatures; }
			set { m_Creatures = value; }
		}

		[CommandProperty(AccessLevel.GameMaster)]
		public bool CombatCheck
		{
			get { return m_CombatCheck; }
			set { m_CombatCheck = value; }
		}

		[CommandProperty(AccessLevel.GameMaster)]
		public bool CriminalCheck
		{
			get { return m_CriminalCheck; }
			set { m_CriminalCheck = value; }
		}

		public override int LabelNumber { get { return 1026095; } } // teleporter


		[Constructible]
public Teleporter()
			: this(new Point3D(0, 0, 0), null, false)
		{
		}


		[Constructible]
public Teleporter(Point3D pointDest, Map mapDest)
			: this(pointDest, mapDest, false)
		{
		}


		[Constructible]
public Teleporter(Point3D pointDest, Map mapDest, bool creatures)
			: base(0x1BC3)
		{
			Movable = false;
			Visible = false;

			m_Active = true;
			m_PointDest = pointDest;
			m_MapDest = mapDest;
			m_Creatures = creatures;

			m_CombatCheck = false;
			m_CriminalCheck = false;
		}

		public override void OnSingleClick(Mobile from)
		{
			base.OnSingleClick(from);

			if (m_Active)
			{
				if (m_MapDest != null && m_PointDest != Point3D.Zero)
					LabelTo(from, "{0} [{1}]", m_PointDest, m_MapDest);
				else if (m_MapDest != null)
					LabelTo(from, "[{0}]", m_MapDest);
				else if (m_PointDest != Point3D.Zero)
					LabelTo(from, m_PointDest.ToString());
			}
			else
			{
				LabelTo(from, "(inactive)");
			}
		}

		public virtual bool CanTeleport(Mobile m)
		{
			if (!m_Creatures && !m.Player)
			{
				return false;
			}
			else if (m_CriminalCheck && m.Criminal)
			{
				m.SendLocalizedMessage(1005561, "", 0x22); // Thou'rt a criminal and cannot escape so easily.
				return false;
			}
			else if (m_CombatCheck && SpellHelper.CheckCombat(m))
			{
				m.SendLocalizedMessage(1005564, "", 0x22); // Wouldst thou flee during the heat of battle??
				return false;
			}

			return true;
		}

		public virtual void StartTeleport(Mobile m)
		{
			if (m_Delay == TimeSpan.Zero)
				DoTeleport(m);
			else
				Timer.StartTimer(m_Delay, () => DoTeleport(m));
		}

		public virtual void DoTeleport(Mobile m)
		{
			Map map = m_MapDest;

			if (map == null || map == Map.Internal)
				map = m.Map;

			Point3D p = m_PointDest;

			if (p == Point3D.Zero)
				p = m.Location;

			BaseCreature.TeleportPets(m, p, map);

			bool sendEffect = !m.Hidden || m.AccessLevel == AccessLevel.Player;

			if (m_SourceEffect && sendEffect)
				Effects.SendLocationEffect(m.Location, m.Map, 0x3728, 10, 10);

			m.MoveToWorld(p, map);

			if (m_DestEffect && sendEffect)
				Effects.SendLocationEffect(m.Location, m.Map, 0x3728, 10, 10);

			if (m_SoundID > 0 && sendEffect)
				Effects.PlaySound(m.Location, m.Map, m_SoundID);
		}

		public override bool OnMoveOver(Mobile mobile)
		{
			if (m_Active && CanTeleport(mobile))
			{
				StartTeleport(mobile);
				return false;
			}

			return true;
		}

		[Constructible]
public Teleporter(Serial serial)
			: base(serial)
		{
		}

		public override void Serialize(IGenericWriter writer)
		{
			base.Serialize(writer);

			writer.Write((int)4); // version

			writer.Write((bool)m_CriminalCheck);
			writer.Write((bool)m_CombatCheck);

			writer.Write((bool)m_SourceEffect);
			writer.Write((bool)m_DestEffect);
			writer.Write((TimeSpan)m_Delay);
			writer.WriteEncodedInt((int)m_SoundID);

			writer.Write(m_Creatures);

			writer.Write(m_Active);
			writer.Write(m_PointDest);
			writer.Write(m_MapDest);
		}

		public override void Deserialize(IGenericReader reader)
		{
			base.Deserialize(reader);

			int version = reader.ReadInt();

			switch (version)
			{
				case 4:
					{
						m_CriminalCheck = reader.ReadBool();
						goto case 3;
					}
				case 3:
					{
						m_CombatCheck = reader.ReadBool();
						goto case 2;
					}
				case 2:
					{
						m_SourceEffect = reader.ReadBool();
						m_DestEffect = reader.ReadBool();
						m_Delay = reader.ReadTimeSpan();
						m_SoundID = reader.ReadEncodedInt();

						goto case 1;
					}
				case 1:
					{
						m_Creatures = reader.ReadBool();

						goto case 0;
					}
				case 0:
					{
						m_Active = reader.ReadBool();
						m_PointDest = reader.ReadPoint3D();
						m_MapDest = reader.ReadMap();

						break;
					}
			}
		}
	}

	public class SkillTeleporter : Teleporter
	{
		private SkillName m_Skill;
		private double m_Required;
		private string m_MessageString;
		private int m_MessageNumber;

		[CommandProperty(AccessLevel.GameMaster)]
		public SkillName Skill
		{
			get { return m_Skill; }
			set { m_Skill = value; }
		}

		[CommandProperty(AccessLevel.GameMaster)]
		public double Required
		{
			get { return m_Required; }
			set { m_Required = value; }
		}

		[CommandProperty(AccessLevel.GameMaster)]
		public string MessageString
		{
			get { return m_MessageString; }
			set { m_MessageString = value; }
		}

		[CommandProperty(AccessLevel.GameMaster)]
		public int MessageNumber
		{
			get { return m_MessageNumber; }
			set { m_MessageNumber = value; }
		}

		private void EndMessageLock(object state)
		{
			((Mobile)state).EndAction(this);
		}

		public override bool CanTeleport(Mobile m)
		{
			if (!base.CanTeleport(m))
				return false;

			Skill sk = m.Skills[m_Skill];

			if (sk == null || sk.Base < m_Required)
			{
				if (m.BeginAction(this))
				{
					if (m_MessageString != null)
						m.NetState.SendMessage(Serial, ItemID, MessageType.Regular, 0x3B2, 3, false, "ENU", null, m_MessageString);
					else if (m_MessageNumber != 0)
						m.NetState.SendMessageLocalized(Serial, ItemID, MessageType.Regular, 0x3B2, 3, m_MessageNumber, null);

					Timer.StartTimer(TimeSpan.FromSeconds(5.0), () => EndMessageLock(m));
				}

				return false;
			}

			return true;
		}


		public SkillTeleporter()
		{
		}

		public SkillTeleporter(Serial serial)
			: base(serial)
		{
		}

		public override void Serialize(IGenericWriter writer)
		{
			base.Serialize(writer);

			writer.Write((int)0); // version

			writer.Write((int)m_Skill);
			writer.Write((double)m_Required);
			writer.Write((string)m_MessageString);
			writer.Write((int)m_MessageNumber);
		}

		public override void Deserialize(IGenericReader reader)
		{
			base.Deserialize(reader);

			int version = reader.ReadInt();

			switch (version)
			{
				case 0:
					{
						m_Skill = (SkillName)reader.ReadInt();
						m_Required = reader.ReadDouble();
						m_MessageString = reader.ReadString();
						m_MessageNumber = reader.ReadInt();

						break;
					}
			}
		}
	}

	public class KeywordTeleporter : Teleporter
	{
		private string m_Substring;
		private int m_Keyword;
		private int m_Range;

		[CommandProperty(AccessLevel.GameMaster)]
		public string Substring
		{
			get { return m_Substring; }
			set { m_Substring = value; }
		}

		[CommandProperty(AccessLevel.GameMaster)]
		public int Keyword
		{
			get { return m_Keyword; }
			set { m_Keyword = value; }
		}

		[CommandProperty(AccessLevel.GameMaster)]
		public int Range
		{
			get { return m_Range; }
			set { m_Range = value; }
		}

		public override bool HandlesOnSpeech { get { return true; } }

		public override void OnSpeech(SpeechEventArgs e)
		{
			if (!e.Handled && Active)
			{
				Mobile m = e.Mobile;

				if (!m.InRange(GetWorldLocation(), m_Range))
					return;

				bool isMatch = false;

				if (m_Keyword >= 0 && e.HasKeyword(m_Keyword))
					isMatch = true;
				else if (m_Substring != null && e.Speech.ToLower().IndexOf(m_Substring.ToLower()) >= 0)
					isMatch = true;

				if (!isMatch || !CanTeleport(m))
					return;

				e.Handled = true;
				StartTeleport(m);
			}
		}

		public override void DoTeleport(Mobile m)
		{
			if (!m.InRange(GetWorldLocation(), m_Range) || m.Map != Map)
				return;

			base.DoTeleport(m);
		}

		public override bool OnMoveOver(Mobile mobile)
		{
			return true;
		}


		public KeywordTeleporter()
		{
			m_Keyword = -1;
			m_Substring = null;
		}

		public KeywordTeleporter(Serial serial)
			: base(serial)
		{
		}

		public override void Serialize(IGenericWriter writer)
		{
			base.Serialize(writer);

			writer.Write((int)0); // version

			writer.Write(m_Substring);
			writer.Write(m_Keyword);
			writer.Write(m_Range);
		}

		public override void Deserialize(IGenericReader reader)
		{
			base.Deserialize(reader);

			int version = reader.ReadInt();

			switch (version)
			{
				case 0:
					{
						m_Substring = reader.ReadString();
						m_Keyword = reader.ReadInt();
						m_Range = reader.ReadInt();

						break;
					}
			}
		}
	}

	public class WaitTeleporter : KeywordTeleporter
	{
		private static Dictionary<Mobile, TeleportingInfo> m_Table;

		public static void Initialize()
		{
			m_Table = new Dictionary<Mobile, TeleportingInfo>();

			EventSink.Logout += EventSink_Logout;
		}

		public static void EventSink_Logout(Mobile from)
		{
			TeleportingInfo info;

			if (from == null || !m_Table.TryGetValue(from, out info))
				return;

			info.TimerToken.Cancel();
			m_Table.Remove(from);
		}

		private int m_StartNumber;
		private string m_StartMessage;
		private int m_ProgressNumber;
		private string m_ProgressMessage;
		private bool m_ShowTimeRemaining;

		[CommandProperty(AccessLevel.GameMaster)]
		public int StartNumber
		{
			get { return m_StartNumber; }
			set { m_StartNumber = value; }
		}

		[CommandProperty(AccessLevel.GameMaster)]
		public string StartMessage
		{
			get { return m_StartMessage; }
			set { m_StartMessage = value; }
		}

		[CommandProperty(AccessLevel.GameMaster)]
		public int ProgressNumber
		{
			get { return m_ProgressNumber; }
			set { m_ProgressNumber = value; }
		}

		[CommandProperty(AccessLevel.GameMaster)]
		public string ProgressMessage
		{
			get { return m_ProgressMessage; }
			set { m_ProgressMessage = value; }
		}

		[CommandProperty(AccessLevel.GameMaster)]
		public bool ShowTimeRemaining
		{
			get { return m_ShowTimeRemaining; }
			set { m_ShowTimeRemaining = value; }
		}


		public WaitTeleporter()
		{
		}

		public static string FormatTime(TimeSpan ts)
		{
			if (ts.TotalHours >= 1)
			{
				int h = (int)Math.Round(ts.TotalHours);
				return $"{h} hour{(h == 1 ? "" : "s")}";
			}
			else if (ts.TotalMinutes >= 1)
			{
				int m = (int)Math.Round(ts.TotalMinutes);
				return $"{m} minute{(m == 1 ? "" : "s")}";
			}

			int s = Math.Max((int)Math.Round(ts.TotalSeconds), 0);
			return $"{s} second{(s == 1 ? "" : "s")}";
		}

		private void EndLock(Mobile m)
		{
			m.EndAction(this);
		}

		public override void StartTeleport(Mobile m)
		{
			TeleportingInfo info;

			if (m_Table.TryGetValue(m, out info))
			{
				if (info.Teleporter == this)
				{
					if (m.BeginAction(this))
					{
						if (m_ProgressMessage != null)
							m.SendMessage(m_ProgressMessage);
						else if (m_ProgressNumber != 0)
							m.SendLocalizedMessage(m_ProgressNumber);

						if (m_ShowTimeRemaining)
							m.SendMessage("Time remaining: {0}", FormatTime(m_Table[m].TimerToken.Next - Core.Now));

						Timer.StartTimer(TimeSpan.FromSeconds(5), () => EndLock(m));
					}

					return;
				}
				else
				{
					info.TimerToken.Cancel();
				}
			}

			if (m_StartMessage != null)
				m.SendMessage(m_StartMessage);
			else if (m_StartNumber != 0)
				m.SendLocalizedMessage(m_StartNumber);

			if (Delay == TimeSpan.Zero)
				DoTeleport(m);
            else
            {
                Timer.StartTimer(Delay, () => DoTeleport(m), out var timerToken);
                m_Table[m] = new TeleportingInfo(this, timerToken);
            }
        }

		public override void DoTeleport(Mobile m)
		{
			m_Table.Remove(m);

			base.DoTeleport(m);
		}

		public WaitTeleporter(Serial serial)
			: base(serial)
		{
		}

		public override void Serialize(IGenericWriter writer)
		{
			base.Serialize(writer);

			writer.Write((int)0); // version

			writer.Write(m_StartNumber);
			writer.Write(m_StartMessage);
			writer.Write(m_ProgressNumber);
			writer.Write(m_ProgressMessage);
			writer.Write(m_ShowTimeRemaining);
		}

		public override void Deserialize(IGenericReader reader)
		{
			base.Deserialize(reader);

			int version = reader.ReadInt();

			m_StartNumber = reader.ReadInt();
			m_StartMessage = reader.ReadString();
			m_ProgressNumber = reader.ReadInt();
			m_ProgressMessage = reader.ReadString();
			m_ShowTimeRemaining = reader.ReadBool();
		}

		private class TeleportingInfo
		{
            public WaitTeleporter Teleporter { get; }

            public TimerExecutionToken TimerToken { get; }

			public TeleportingInfo(WaitTeleporter tele, TimerExecutionToken token)
			{
                Teleporter  = tele;
                TimerToken  = token;
			}
		}
	}

	public class TimeoutTeleporter : Teleporter
	{
		private TimeSpan m_TimeoutDelay;
		private Dictionary<Mobile, TimerExecutionToken> m_Teleporting;

		[CommandProperty(AccessLevel.GameMaster)]
		public TimeSpan TimeoutDelay
		{
			get { return m_TimeoutDelay; }
			set { m_TimeoutDelay = value; }
		}


		public TimeoutTeleporter()
			: this(new Point3D(0, 0, 0), null, false)
		{
		}


		public TimeoutTeleporter(Point3D pointDest, Map mapDest)
			: this(pointDest, mapDest, false)
		{
		}


		public TimeoutTeleporter(Point3D pointDest, Map mapDest, bool creatures)
			: base(pointDest, mapDest, creatures)
		{
			m_Teleporting = new Dictionary<Mobile, TimerExecutionToken>();
		}

		public void StartTimer(Mobile m)
		{
			StartTimer(m, m_TimeoutDelay);
		}

		private void StartTimer(Mobile m, TimeSpan delay)
		{
            StopTimer(m);
            Timer.StartTimer(delay, () => StartTeleport(m), out var timerToken);
            m_Teleporting[m] = timerToken;
		}

		public void StopTimer(Mobile m)
		{
            if (m_Teleporting.Remove(m, out var t))
            {
                t.Cancel();
            }
		}

		public override void DoTeleport(Mobile m)
		{
			m_Teleporting.Remove(m);

			base.DoTeleport(m);
		}

		public override bool OnMoveOver(Mobile mobile)
		{
			if (Active)
			{
				if (!CanTeleport(mobile))
					return false;

				StartTimer(mobile);
			}

			return true;
		}

		public TimeoutTeleporter(Serial serial)
			: base(serial)
		{
		}

		public override void Serialize(IGenericWriter writer)
		{
			base.Serialize(writer);

			writer.Write((int)0); // version

			writer.Write(m_TimeoutDelay);
			writer.Write(m_Teleporting.Count);

			foreach (var kvp in m_Teleporting)
			{
				writer.Write(kvp.Key);
				writer.Write(kvp.Value.Next);
			}
		}

		public override void Deserialize(IGenericReader reader)
		{
			base.Deserialize(reader);

			int version = reader.ReadInt();

			m_TimeoutDelay = reader.ReadTimeSpan();
			m_Teleporting = new Dictionary<Mobile, TimerExecutionToken>();

			int count = reader.ReadInt();

			for (int i = 0; i < count; ++i)
			{
				Mobile m = reader.ReadEntity<Mobile>();
				DateTime end = reader.ReadDateTime();

				StartTimer(m, end - Core.Now);
			}
		}
	}

	public class TimeoutGoal : Item
	{
		private TimeoutTeleporter m_Teleporter;

		[CommandProperty(AccessLevel.GameMaster)]
		public TimeoutTeleporter Teleporter
		{
			get { return m_Teleporter; }
			set { m_Teleporter = value; }
		}


		public TimeoutGoal()
			: base(0x1822)
		{
			Movable = false;
			Visible = false;

			Hue = 1154;
		}

		public override bool OnMoveOver(Mobile mobile)
		{
			if (m_Teleporter != null)
				m_Teleporter.StopTimer(mobile);

			return true;
		}

		public override string DefaultName
		{
			get { return "timeout teleporter goal"; }
		}

		public TimeoutGoal(Serial serial)
			: base(serial)
		{
		}

		public override void Serialize(IGenericWriter writer)
		{
			base.Serialize(writer);

			writer.Write((int)0); // version

			writer.Write(m_Teleporter);
		}

		public override void Deserialize(IGenericReader reader)
		{
			base.Deserialize(reader);

			int version = reader.ReadInt();

			m_Teleporter = reader.ReadEntity<TimeoutTeleporter>();
		}
	}

	public class ConditionTeleporter : Teleporter
	{
		[Flags]
		protected enum ConditionFlag
		{
			None = 0x000,
			DenyMounted = 0x001,
			DenyFollowers = 0x002,
			DenyPackContents = 0x004,
			DenyHolding = 0x008,
			DenyEquipment = 0x010,
			DenyTransformed = 0x020,
			StaffOnly = 0x040,
			DenyPackEthereals = 0x080,
			DeadOnly = 0x100
		}

		private ConditionFlag m_Flags;

		[CommandProperty(AccessLevel.GameMaster)]
		public bool DenyMounted
		{
			get { return GetFlag(ConditionFlag.DenyMounted); }
			set { SetFlag(ConditionFlag.DenyMounted, value); }
		}

		[CommandProperty(AccessLevel.GameMaster)]
		public bool DenyFollowers
		{
			get { return GetFlag(ConditionFlag.DenyFollowers); }
			set { SetFlag(ConditionFlag.DenyFollowers, value); }
		}

		[CommandProperty(AccessLevel.GameMaster)]
		public bool DenyPackContents
		{
			get { return GetFlag(ConditionFlag.DenyPackContents); }
			set { SetFlag(ConditionFlag.DenyPackContents, value); }
		}

		[CommandProperty(AccessLevel.GameMaster)]
		public bool DenyHolding
		{
			get { return GetFlag(ConditionFlag.DenyHolding); }
			set { SetFlag(ConditionFlag.DenyHolding, value); }
		}

		[CommandProperty(AccessLevel.GameMaster)]
		public bool DenyEquipment
		{
			get { return GetFlag(ConditionFlag.DenyEquipment); }
			set { SetFlag(ConditionFlag.DenyEquipment, value); }
		}

		[CommandProperty(AccessLevel.GameMaster)]
		public bool DenyTransformed
		{
			get { return GetFlag(ConditionFlag.DenyTransformed); }
			set { SetFlag(ConditionFlag.DenyTransformed, value); }
		}

		[CommandProperty(AccessLevel.GameMaster)]
		public bool StaffOnly
		{
			get { return GetFlag(ConditionFlag.StaffOnly); }
			set { SetFlag(ConditionFlag.StaffOnly, value); }
		}

		[CommandProperty(AccessLevel.GameMaster)]
		public bool DenyPackEthereals
		{
			get { return GetFlag(ConditionFlag.DenyPackEthereals); }
			set { SetFlag(ConditionFlag.DenyPackEthereals, value); }
		}

		[CommandProperty(AccessLevel.GameMaster)]
		public bool DeadOnly
		{
			get { return GetFlag(ConditionFlag.DeadOnly); }
			set { SetFlag(ConditionFlag.DeadOnly, value); }
		}

		public override bool CanTeleport(Mobile m)
		{
			if (!base.CanTeleport(m))
				return false;

			if (GetFlag(ConditionFlag.StaffOnly) && m.AccessLevel < AccessLevel.Counselor)
				return false;

			if (GetFlag(ConditionFlag.DenyMounted) && m.Mounted)
			{
				m.SendLocalizedMessage(1077252); // You must dismount before proceeding.
				return false;
			}

			if (GetFlag(ConditionFlag.DenyFollowers) && m.Followers != 0)
			{
				m.SendLocalizedMessage(1077250); // No pets permitted beyond this point.
				return false;
			}

			Container pack = m.Backpack;

			if (pack != null)
			{
				if (GetFlag(ConditionFlag.DenyPackContents) && pack.TotalItems != 0)
				{
					m.SendMessage("You must empty your backpack before proceeding.");
					return false;
				}
			}

			if (GetFlag(ConditionFlag.DenyHolding) && m.Holding != null)
			{
				m.SendMessage("You must let go of what you are holding before proceeding.");
				return false;
			}

			if (GetFlag(ConditionFlag.DenyEquipment))
			{
				foreach (Item item in m.Items)
				{
					switch (item.Layer)
					{
						case Layer.Hair:
						case Layer.FacialHair:
						case Layer.Backpack:
						case Layer.Mount:
						case Layer.Bank:
							{
								continue; // ignore
							}
						default:
							{
								m.SendMessage("You must remove all of your equipment before proceeding.");
								return false;
							}
					}
				}
			}

			if (GetFlag(ConditionFlag.DenyTransformed) && m.IsBodyMod)
			{
				m.SendMessage("You cannot go there in this form.");
				return false;
			}

			if (GetFlag(ConditionFlag.DeadOnly) && m.Alive)
			{
				m.SendLocalizedMessage(1060014); // Only the dead may pass.
				return false;
			}

			return true;
		}


		public ConditionTeleporter()
		{
		}

		public ConditionTeleporter(Serial serial)
			: base(serial)
		{
		}

		public override void Serialize(IGenericWriter writer)
		{
			base.Serialize(writer);

			writer.Write((int)0); // version

			writer.Write((int)m_Flags);
		}

		public override void Deserialize(IGenericReader reader)
		{
			base.Deserialize(reader);

			int version = reader.ReadInt();

			m_Flags = (ConditionFlag)reader.ReadInt();
		}

		protected bool GetFlag(ConditionFlag flag)
		{
			return (m_Flags & flag) != 0;
		}

		protected void SetFlag(ConditionFlag flag, bool value)
		{
			if (value)
				m_Flags |= flag;
			else
				m_Flags &= ~flag;
		}
	}
}
