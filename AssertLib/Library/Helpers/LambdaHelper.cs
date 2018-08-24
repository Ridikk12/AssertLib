using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace AssertLib.Library
{
    public static class LambdaHelper
    {
        public static string GetMemberName(this LambdaExpression expr)
        {
            var lexpr = expr;
            MemberExpression mexpr = null;
            if (lexpr.Body is MemberExpression)
            {
                mexpr = (MemberExpression)lexpr.Body;
            }
            else if (lexpr.Body is UnaryExpression)
            {
                mexpr = (MemberExpression)((UnaryExpression)lexpr.Body).Operand;
            }
            if (mexpr == null)
            {
                return null;
            }
            return mexpr.Member.Name;
        }
    }
}
