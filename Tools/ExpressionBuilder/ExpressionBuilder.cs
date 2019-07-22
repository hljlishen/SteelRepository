using System;
using System.Linq.Expressions;

namespace Tools
{
    public class ExpressionBuilder<T>
    {
        private Expression expression = null;
        private readonly ParameterExpression param;

        public ExpressionBuilder()
        {
            param = Expression.Parameter(typeof(T), "p1");
        }
        public void And(Expression<Func<T, bool>> otherExp)
        {
            if (!IsValid(otherExp)) return;

            expression = Expression.AndAlso(expression, otherExp.Body);
        }

        public void Or(Expression<Func<T, bool>> otherExp)
        {
            if (!IsValid(otherExp)) return;

            expression = Expression.Or(expression, otherExp.Body);
        }

        public Expression<Func<T, bool>> GetExpression()
        {
            if (expression == null) return null;

            ReplaceParamVisitor visitor = new ReplaceParamVisitor(param);
            var ret = Expression.Lambda<Func<T, bool>>(visitor.Visit(expression), param);
            expression = null;

            return ret;
        }

        private bool IsValid(Expression<Func<T, bool>> otherExp)
        {
            if (expression == null)
            {
                expression = otherExp.Body;
                return false;
            }
            else if (otherExp == null)
                throw new Exception("表达式不能为null");

            return true;
        }
    }
}
