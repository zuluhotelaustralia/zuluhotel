using System;
using static Server.Configurations.MessageHueConfiguration;

namespace Server.Engines.Harvest
{
    public class HarvestResource
    {
        private Type[] m_Types;
        private double m_ReqSkill;
        private object m_SuccessMessage;

        public Type[] Types
        {
            get { return m_Types; }
            set { m_Types = value; }
        }

        public double ReqSkill
        {
            get { return m_ReqSkill; }
            set { m_ReqSkill = value; }
        }

        public object SuccessMessage
        {
            get { return m_SuccessMessage; }
        }

        public void SendSuccessTo(Mobile m, int amount)
        {
            if (m_SuccessMessage is int)
                m.SendLocalizedMessage((int) m_SuccessMessage);
            else if (m_SuccessMessage is string)
                m.SendMessage(MessageSuccessHue, $"You put {amount} {(string) m_SuccessMessage} ore in your backpack.");
        }

        public HarvestResource(double reqSkill, object message, params Type[] types)
        {
            m_ReqSkill = reqSkill;
            m_Types = types;
            m_SuccessMessage = message;
        }
    }
}