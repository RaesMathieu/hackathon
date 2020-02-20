using System;
using System.Linq;
using System.Linq.Expressions;

namespace ThermoBet.MVC.Helper
{
    public static class ExpressionFilter
    {
        public static Expression<Func<TSource, object>> GetExpression<TSource>( this IQueryable<TSource>source, string propertyName)
        {
            var param = Expression.Parameter(typeof(TSource), "x");
            Expression conversion = Expression.Convert(Expression.Property
                (param, propertyName), typeof(object));   //important to use the Expression.Convert
            return Expression.Lambda<Func<TSource, object>>(conversion, param);
        }
    }
}