using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text.RegularExpressions;
using DataTables.Mvc;
using LinqKit;
using System.Linq.Dynamic;
using System.Linq.Expressions;

namespace Shared.Html
{
    public static class DataTableMethods
    {
        public static IQueryable<T> ProcessResultSet<T>(IQueryable<T> resultSet, IDataTablesRequest parameters)
        {
            resultSet = Search(resultSet, parameters.Columns, parameters.Search);
            resultSet = parameters.Columns.GetFilteredColumns().Aggregate(resultSet, FilterColumn);
            return parameters.Columns.GetSortedColumns().Aggregate(resultSet, SortColumn);
        }

        private static IQueryable<T> FilterColumn<T>(IQueryable<T> resultSet, Column col)
        {
            var prop = TypeDescriptor.GetProperties(typeof(T)).Find(col.Name, true);
            if (col.Search.IsRegexValue)
            {
                var rx = new Regex(col.Search.Value, RegexOptions.IgnoreCase);
                return resultSet.Where(
                    p =>
                        !string.IsNullOrEmpty(prop.GetValue(p).ToString())
                        && rx.IsMatch(prop.GetValue(p).ToString())
                ).ToArray().AsQueryable();
            }

            return resultSet.Where(
                p =>
                    !string.IsNullOrEmpty(prop.GetValue(p).ToString())
                    && prop
                        .GetValue(p)
                        .ToString()
                        .ToLower()
                        .Contains(col.Search.Value.ToLower())
            ).ToArray().AsQueryable();
        }

        private static IQueryable<T> SortColumn<T>(IQueryable<T> resultSet, Column col)
        {
            var prop = TypeDescriptor.GetProperties(typeof(T)).Find(col.Name, true);
            if (col.SortDirection == Column.OrderDirection.Ascendant)
            {
                return resultSet.OrderBy(p => prop.GetValue(p)).ToArray().AsQueryable();
            }

            return resultSet.OrderByDescending(p => prop.GetValue(p)).ToArray().AsQueryable();
        }

        private static IQueryable<T> Search<T>(IQueryable<T> resultSet, IEnumerable<Column> cols, Search search)
        {
            if (string.IsNullOrEmpty(search?.Value))
            {
                return resultSet;
            }

            var searchCriteria = search.Value.ToLower();
            var columns = typeof (T).GetProperties();
            var expressions = new List<Expression<Func<T, bool>>>();
            var parameter = Expression.Parameter(typeof(T), "x");

            foreach (var col in columns)
            {
                var parameterExpression = Expression.Property(parameter, col.Name);
                if (parameterExpression.Type != typeof(string))
                {
                    continue;
                }

                var value = Expression.Constant(searchCriteria);
                var toLower = Expression.Call(parameterExpression, typeof(string).GetMethod("ToLower", Type.EmptyTypes));
                var type = value.Type;
                var body =
                    Expression.AndAlso(
                        Expression.Not(
                            Expression.Call(typeof(string), "IsNullOrEmpty", null, parameterExpression)
                        ),
                        Expression.Call(toLower, type.GetMethod("Contains", new[] { typeof(string) }), value)
                    );
                
                var lambda = Expression.Lambda<Func<T, bool>>(body, parameter);
                expressions.Add(lambda);
            }

            var predicate = expressions.Aggregate(PredicateBuilder.False<T>(), (current, expression) => current.Or(expression));
            return resultSet.Where(predicate);
        }
    }
}