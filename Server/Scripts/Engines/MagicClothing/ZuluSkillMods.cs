using System;
using System.Collections;
using System.Collections.Generic;
using Server;
using Server.Items;

namespace Server
{
    public class ZuluSkillMods
    {
        private Item _parent;
        private SkillMod m_Mod;

        [CommandProperty(AccessLevel.Developer)]
        public SkillMod Mod
        {
            get { return m_Mod; }
        }

        public ZuluSkillMods(Item parent)
        {
            _parent = parent;
        }

        public ZuluSkillMods(Item parent, ZuluSkillMods other)
        {
            _parent = parent;
            m_Mod = other.Mod;
        }

        public void Serialize(GenericWriter writer)
        {
            writer.Write((int)0); //version

            bool blankmod;
            if (m_Mod == null)
            {
                blankmod = true;
            }
            else
            {
                blankmod = false;
            }

            writer.Write(blankmod);

            if (!blankmod)
            {
                writer.Write((int)(m_Mod.Skill));  //it's a skillname
                writer.Write(m_Mod.Value);
            }
        }

        public void Deserialize(GenericReader reader)
        {
            int version = reader.ReadInt();

            bool blank = reader.ReadBool();

            if (!blank)
            {
                SkillName sn = (SkillName)(reader.ReadInt());
                double bonus = reader.ReadDouble();

                m_Mod = new DefaultSkillMod(sn, true, bonus);
            }
        }

        public void SetMod(SkillName sn, double bonus)
        {
            if (m_Mod != null)
            {
                Console.WriteLine("WARNING:  Skillmod overwritten! {0}", _parent.ToString());
            }
            m_Mod = new DefaultSkillMod(sn, true, bonus);
            m_Mod.ObeyCap = false;
        }

        public void AddTo(Mobile m)
        {
            if (m_Mod != null)
            {
                m.AddSkillMod(m_Mod);
            }
        }

        public void Remove(Mobile m)
        {
            if (m_Mod == null)
                return;

            m.RemoveSkillMod(m_Mod);
        }
    }
}
