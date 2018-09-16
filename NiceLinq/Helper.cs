using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;

namespace Mshwf.NiceLinq
{
    internal static class Helper
    {
        internal static Expression GetExpression<TSource, TMember>(ParameterExpression parameter, Expression<Func<TSource, TMember>> identifier, IEnumerable<TMember> values)
        {
            var memberName = (identifier.Body as MemberExpression).Member.Name;
            var member = Expression.Property(parameter, memberName);
            var valuesConstant = Expression.Constant(values.ToList());
            MethodInfo method = typeof(List<TMember>).GetMethod("Contains");
            Expression call = Expression.Call(valuesConstant, method, member);
            return call;
        }

        internal static T Selector<T>(T obj, IEnumerable<PropertyInfo> properties)
        {
            var instance = Activator.CreateInstance<T>();
            foreach (var property in properties)
                property.SetValue(instance, property.GetValue(obj, null), null);

            return instance;
        }


        internal static Func<T, object> GetLambda<T>(string propertyName) =>
         CreateExpression<T>(propertyName).Compile();


        internal static Expression<Func<T, object>> GetExpression<T>(string propertyName) =>
            CreateExpression<T>(propertyName);

        private static Expression<Func<T, object>> CreateExpression<T>(string propertyName)
        {
            var param = Expression.Parameter(typeof(T), "p");
            var member = Expression.Property(param, propertyName);
            Expression conversion = Expression.Convert(member, typeof(object)); //explicit casting is a must
            var lambda = Expression.Lambda<Func<T, object>>(conversion, param);
            return lambda;
        }
    }
}
