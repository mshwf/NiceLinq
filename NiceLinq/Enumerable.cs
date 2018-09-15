/*Created by Mohamed A. Elshawaf - 2016
m_shawaf@outlook.com*/
using NiceLinq.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace NiceLinq
{
    /// <summary>
    /// IEnumerable implementation of NiceLinq's methods
    /// </summary>
    public static partial class Enumerable
    {
        /// <summary>
        /// Filters a sequence of objects based on a given subset of matching values.
        /// </summary>
        /// <typeparam name="TSource">The source type.</typeparam>
        /// <typeparam name="TMember">The source member that is used in filtering.</typeparam>
        /// <param name="source">The source that is to be filtered.</param>
        /// <param name="identifier">Function to determine how filtering will take place.</param>
        /// <param name="values">Set of values to include their related objects.</param>
        /// <returns>Filtered set</returns>
        public static IEnumerable<TSource> In<TSource, TMember>(this IEnumerable<TSource> source,
            Func<TSource, TMember> identifier, params TMember[] values) =>
         source.Where(m => values.Contains(identifier(m)));

        /// <summary>
        /// Filters a sequence of objects based on a given subset of non-matching values.
        /// </summary>
        /// <typeparam name="TSource">The source type.</typeparam>
        /// <typeparam name="TMember">The source member that is used in filtering.</typeparam>
        /// <param name="source">The source that is to be filtered.</param>
        /// <param name="identifier">Function to determine how filtering will take place.</param>
        /// <param name="values">Set of values to exclude their related objects.</param>
        /// <returns></returns>
        public static IEnumerable<TSource> NotIn<TSource, TMember>(this IEnumerable<TSource> source,
            Func<TSource, TMember> identifier, params TMember[] values) =>
        source.Where(m => !values.Contains(identifier(m)));


        /// <summary>
        /// Filters a sequence of objects based on a given subset of matching values.
        /// </summary>
        /// <typeparam name="TSource">The source type.</typeparam>
        /// <typeparam name="TMember">The source member that is used in filtering.</typeparam>
        /// <param name="source">The source that is to be filtered.</param>
        /// <param name="identifier">Function to determine how filtering will take place.</param>
        /// <param name="values">List of values to include their related objects.</param>
        /// <returns>Filtered set</returns>
        public static IEnumerable<TSource> In<TSource, TMember>(this IEnumerable<TSource> source,
            Func<TSource, TMember> identifier, IEnumerable<TMember> values) =>
         source.Where(m => values.Contains(identifier(m)));

        /// <summary>
        /// Filters a sequence of objects based on a given subset of non-matching values.
        /// </summary>
        /// <typeparam name="TSource">The source type.</typeparam>
        /// <typeparam name="TMember">The source member that is used in filtering.</typeparam>
        /// <param name="source">The source that is to be filtered.</param>
        /// <param name="identifier">Function to determine how filtering will take place.</param>
        /// <param name="values">List of values to exclude their related objects.</param>
        /// <returns></returns>
        public static IEnumerable<TSource> NotIn<TSource, TMember>(this IEnumerable<TSource> source,
            Func<TSource, TMember> identifier, IEnumerable<TMember> values) =>
        source.Where(m => !values.Contains(identifier(m)));



        public static IEnumerable<T> SelectExcept<T, TKey>(this IEnumerable<T> sequence,
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
        /// <summary>
        /// Order by using the property name.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="notOrderedList">The sequence to be re-ordered</param>
        /// <param name="propertyName">The property name to order by</param>
        /// <returns>The re-ordered list</returns>
        public static IEnumerable<T> OrderBy<T>(this IEnumerable<T> notOrderedList, string propertyName)
        {
            var lambda = Helper.GetLambda<T>(propertyName);
            var orderedList = notOrderedList.OrderBy(lambda);
            return orderedList;
        }

        public static IEnumerable<T> InterlockWith<T>(this IEnumerable<T> seq1, IEnumerable<T> seq2)
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
