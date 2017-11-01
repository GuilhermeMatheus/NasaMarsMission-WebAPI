using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;

namespace Nasa.Mission.Mars.Entity.Utils
{
    public static class ExpressionUtils
    {
        public static PropertyInfo GetPropertyName<TModel, TProperty>(this Expression<Func<TModel, TProperty>> propertyLambda)
        {
            var expressionBody = propertyLambda.Body.NodeType == ExpressionType.Convert
                ? ((UnaryExpression)propertyLambda.Body).Operand //Conversão. I.E. ((object)_.Prop)
                : propertyLambda.Body; //Expressão imediata. I.E. (_.Prop)

            var member =
                expressionBody as MemberExpression ??
                throw new ArgumentException(
                    $"Expression '{propertyLambda}' refers to a method, not a property.");

            var propInfo = 
                member.Member as PropertyInfo ??
                throw new ArgumentException(
                    $"Expression '{propertyLambda}' refers to a field, not a property.");

            return propInfo;
        }
    }
}
