using System.Linq.Expressions;

namespace SteelRepository.Controllers.ExpressionBuilder
{
    internal class ReplaceParamVisitor : ExpressionVisitor
    {
        public ReplaceParamVisitor(ParameterExpression param)
        {
            this.param = param;
        }

        public ParameterExpression param { get; private set; }

        protected override Expression VisitParameter(ParameterExpression node)
        {
            return param;
        }

    }
}
