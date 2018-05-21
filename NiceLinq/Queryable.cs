/*Created by Mohamed A. Elshawaf - 2018
m_shawaf@outlook.com*/
using NiceLinq.Helpers;
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
            var inExpression = Helper.GetExpression(parameter, identifier, values);
            var theExpression = Expression.Lambda<Func<TSource, bool>>(inExpression, parameter);
            return source.Where(theExpression);
        }

        public static IQueryable<TSource> In<TSource, TMember>(this IQueryable<TSource> source,
   Expression<Func<TSource, TMember>> identifier, IEnumerable<TMember> values)
        {
            var parameter = Expression.Parameter(typeof(TSource), "m");
            var inExpression = Helper.GetExpression(parameter, identifier, values);
            var theExpression = Expression.Lambda<Func<TSource, bool>>(inExpression, parameter);
            return source.Where(theExpression);
        }

        public static IQueryable<TSource> NotIn<TSource, TMember>(this IQueryable<TSource> source,
  Expression<Func<TSource, TMember>> identifier, params TMember[] values)
        {
            var parameter = Expression.Parameter(typeof(TSource), "m");
            var inExpression = Helper.GetExpression(parameter, identifier, values);
            var theExpression = Expression.Lambda<Func<TSource, bool>>(Expression.Not(inExpression), parameter);
            return source.Where(theExpression);
        }

        public static IQueryable<TSource> NotIn<TSource, TMember>(this IQueryable<TSource> source,
   Expression<Func<TSource, TMember>> identifier, IEnumerable<TMember> values)
        {
            var parameter = Expression.Parameter(typeof(TSource), "m");
            var inExpression = Helper.GetExpression(parameter, identifier, values);
            var theExpression = Expression.Lambda<Func<TSource, bool>>(Expression.Not(inExpression), parameter);
            return source.Where(theExpression);
        }

        public static IQueryable<T> SelectExcept<T, TKey>(this IQueryable<T> sequence,
        Expression<Func<T, TKey>> excluder)
        {
            List<string> excludedProperties = new List<string>();
            if (excluder.Body is MemberExpression memberExpression)
            {
                excludedProperties.Add(memberExpression.Member.Name);
            }
            else if (excluder.Body is NewExpression anonymousExpression)
            {
                excludedProperties.AddRange(anonymousExpression.Members.Select(m => m.Name));
            }
            var includedProperties = typeof(T).GetProperties()
                .Where(p => !excludedProperties.Contains(p.Name));

            return sequence.Select(x => Helper.Selector(x, includedProperties));
        }

        public static IQueryable<T> OrderBy<T>(this IQueryable<T> notOrderedList, string propertyName)
        {
            var lambda = Helper.GetExpression<T>(propertyName);
            var orderedList = notOrderedList.OrderBy(lambda);
            return orderedList;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="seq1"></param>
        /// <param name="seq2"></param>
        /// <returns></returns>
        public static IQueryable<T> InterlockWith<T>(this IQueryable<T> seq1, IQueryable<T> seq2)
        {
            Tuple<T[], int>[] metaSequences = new Tuple<T[], int>[2];
            metaSequences[0] = Tuple.New(seq1.ToArray(), seq1.Count());
            metaSequences[1] = Tuple.New(seq2.ToArray(), seq2.Count());
            var orderedMetas = metaSequences.OrderBy(x => x.Second).ToArray();

            for (int i = 0; i < orderedMetas[0].Second; i++)
            {
                yield return metaSequences[0].First[i];
                yield return metaSequences[1].First[i];
            }

            var remainingItems = orderedMetas[1].First.Skip(orderedMetas[0].Second);
            foreach (var item in remainingItems)
            {
                yield return item;
            }
        }
    }
}
