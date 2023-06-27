using System.Linq.Expressions;

namespace ScheduledMeets.Persistence.Helpers;

internal sealed class ConvertRemover<T> : ExpressionVisitor
{
    protected override Expression VisitUnary(UnaryExpression node) => node is
    {
        NodeType: ExpressionType.Convert,
        Type.IsInterface: true,
        Method: null,
        Operand: Expression operand
    } && operand.Type == typeof(T)
    ? Visit(operand)
    : base.VisitUnary(node);
}
