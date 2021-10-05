using Server.Items;

namespace Server.ContextMenus
{
    public class IdentifyEntry : ContextMenuEntry
    {
        private readonly Item m_Item;

        public IdentifyEntry(Mobile from, Item item) : base(-1997906, 2)
        {
            m_Item = item;
        }

        public override void OnClick()
        {
            if (!Owner.From.CheckAlive())
            {
                return;
            }

            Owner.From.TargetLocked = true;

            if (Owner.From.UseSkill(SkillName.ItemID))
            {
                Owner.From.Target.Invoke(Owner.From, m_Item);
            }

            Owner.From.TargetLocked = false;
        }
    }
}
