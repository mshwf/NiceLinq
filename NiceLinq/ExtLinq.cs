/*Created by Mohamed A. Elshawaf - 2016
m_shawaf@outlook.com*/
using System;
using System.Collections.Generic;
using System.Linq;
namespace NiceLinq
{
    /// <summary>
    /// Extended LINQ methods will live here.
    /// </summary>
    public static class ExtLinq
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

    }
}
