using System;

namespace Scripts.Cue
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct | AttributeTargets.Field | AttributeTargets.Property)]
    public class CueExpressionAttribute : Attribute
    {
        public string Expr { get; }

        public CueExpressionAttribute(string expr)
        {
            Expr = expr;
        }
    }
}