/*Created by Mohamed A. Elshawaf - 2018
m_shawaf@outlook.com*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

namespace NiceLinq
{/// <summary>
/// IQueryable implementation of NiceLinq's methods
/// </summary>
    public static partial class Queryable
    {
        public static IQueryable<TSource> In<TSource, TMember>(this IQueryable<TSource> source,
   Expression<Func<TSource, TMember>> identifier, params TMember[] values)
        {
            var parameter = Expression.Parameter(typeof(TSource), "m");
            var inExpression = GetExpression(parameter, identifier, values);
            var theExpression = Expression.Lambda<Func<TSource, bool>>(inExpression, parameter);
            return source.Where(theExpression);
        }

        public static IQueryable<TSource> In<TSource, TMember>(this IQueryable<TSource> source,
   Expression<Func<TSource, TMember>> identifier, IEnumerable<TMember> values)
        {
            var parameter = Expression.Parameter(typeof(TSource), "m");
            var inExpression = GetExpression(parameter, identifier, values);
            var theExpression = Expression.Lambda<Func<TSource, bool>>(inExpression, parameter);
            return source.Where(theExpression);
        }

        public static IQueryable<TSource> NotIn<TSource, TMember>(this IQueryable<TSource> source,
  Expression<Func<TSource, TMember>> identifier, params TMember[] values)
        {
            var parameter = Expression.Parameter(typeof(TSource), "m");
            var inExpression = GetExpression(parameter, identifier, values);
            var theExpression = Expression.Lambda<Func<TSource, bool>>(Expression.Not(inExpression), parameter);
            return source.Where(theExpression);
        }

        public static IQueryable<TSource> NotIn<TSource, TMember>(this IQueryable<TSource> source,
   Expression<Func<TSource, TMember>> identifier, IEnumerable<TMember> values)
        {
            var parameter = Expression.Parameter(typeof(TSource), "m");
            var inExpression = GetExpression(parameter, identifier, values);
            var theExpression = Expression.Lambda<Func<TSource, bool>>(Expression.Not(inExpression), parameter);
            return source.Where(theExpression);
        }

        static Expression GetExpression<TSource, TMember>(ParameterExpression parameter, Expression<Func<TSource, TMember>> identifier, IEnumerable<TMember> values)
        {
            var memberName = (identifier.Body as MemberExpression).Member.Name;
            var member = Expression.Property(parameter, memberName);
            var valuesConstant = Expression.Constant(values.ToList());
            MethodInfo method = typeof(List<TMember>).GetMethod("Contains");
            Expression call = Expression.Call(valuesConstant, method, member);
            return call;
        }
    }
}
