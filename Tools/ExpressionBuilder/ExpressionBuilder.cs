using System;
using System.Linq.Expressions;

namespace Tools
{
    public class ExpressionBuilder<T>
    {
        private Expression<Func<T, bool>> expression = null;
        private readonly ParameterExpression param;

        public ExpressionBuilder()
        {
            param = Expression.Parameter(typeof(T), "p1");
        }
        public void And(Expression<Func<T, bool>> otherExp)
        {
            if (expression == null)
            {
                expression = otherExp;
                return;
            }
            else if (otherExp == null)
                return;

            expression = Expression.Lambda<Func<T, bool>>(Expression.AndAlso(expression.Body, otherExp.Body), param);
        }

        public void Or(Expression<Func<T, bool>> otherExp)
        {
            if (expression == null)
            {
                expression = otherExp;
                return;
            }
            else if (otherExp == null)
                return;
            expression = Expression.Lambda<Func<T, bool>>(Expression.Or(expression.Body, otherExp.Body), param);
        }

        public Expression<Func<T, bool>> GetExpression()
        {
            ReplaceParamVisitor visitor = new ReplaceParamVisitor(param);
            expression = Expression.Lambda<Func<T, bool>>(visitor.Visit(expression.Body), param);

            return expression;
        }
    }
}
