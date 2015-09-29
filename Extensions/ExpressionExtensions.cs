using System.Linq.Expressions;

namespace YHCSheng.Extensions {
    public static class ExpressionExtensions {
        public static Expression AndAlso(this Expression left, Expression right) {
            return Expression.AndAlso(left, right);
        }
        //public static Expression Call(this Expression instance, string methodName, params Expression[] arguments) {
        //    return Expression.Call(instance, instance.GetType().GetMethod(methodName), arguments);
        //}
        public static Expression Property(this Expression expression, string propertyName) {
            return Expression.Property(expression, propertyName);
        }
        public static Expression GreaterThan(this Expression left, Expression right) {
            return Expression.GreaterThan(left, right);
        }
        public static Expression<TDelegate> ToLambda<TDelegate>(this Expression body, params  ParameterExpression[] parameters) {
            return Expression.Lambda<TDelegate>(body, parameters);
        }
    }
}