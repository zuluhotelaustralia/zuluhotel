using Server.Targeting;

namespace Server.Spells
{
    public class AsyncSpellTarget : AsyncTarget
    {
        public readonly IAsyncSpell Spell;
        
        public AsyncSpellTarget(IAsyncSpell spell, Mobile mobile, TargetOptions opts) : base(mobile, opts)
        {
            Spell = spell;
        }
    }
    
    public class AsyncSpellTarget<T> : AsyncSpellTarget
    {
        public AsyncSpellTarget(IAsyncSpell spell, Mobile mobile, TargetOptions opts) : base(spell, mobile, opts)
        {
        }
        public override AsyncSpellTarget<T> GetAwaiter() => this;
        public new TargetResponse<T> GetResult() => Response.Cast<T>();
    }
    
}