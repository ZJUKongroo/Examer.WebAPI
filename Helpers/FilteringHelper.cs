using Examer.DtoParameters;
using Examer.Enums;
using Examer.Models;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.Linq.Expressions;
using System.Reflection;

namespace Examer.Helpers;

public static class FilteHelper
{
    public static IQueryable<T> Filtering<T, U>(this IQueryable<T> queryExpression, U parameter) where T : IModelBase where U : IDtoParameterBase
    {
        PropertyInfo[] properties = typeof(U).GetProperties(BindingFlags.Public | BindingFlags.Instance);

        foreach (PropertyInfo property in properties)
        {
            if (property.Name == "PageNumber" || property.Name == "PageSize")
                continue;

            if (property.PropertyType == typeof(int))
            {
                var value = (int)property.GetValue(parameter)!;
                if (value == 0)
                    continue;

                Expression<Func<int>> valueLambda = () => value;
                var valueExpression = valueLambda.Body;

                var param = Expression.Parameter(typeof(T), "param");
                var whereLambda = Expression.Lambda<Func<T, bool>>(
                    Expression.Equal(
                        Expression.MakeMemberAccess(
                            param,
                            typeof(T).GetMember(property.Name).Single()
                        ),
                        valueExpression
                    ),
                    param
                );

                queryExpression = queryExpression.Where(whereLambda);
            }
            else if (property.PropertyType == typeof(string))
            {
                var value = (string)property.GetValue(parameter)!;
                if (string.IsNullOrWhiteSpace(value))
                    continue;

                Expression<Func<string>> valueLambda = () => value;
                var valueExpression = valueLambda.Body;

                var param = Expression.Parameter(typeof(T), "param");
                var whereLambda = Expression.Lambda<Func<T, bool>>(
                    Expression.Call(
                        Expression.MakeMemberAccess(
                            param,
                            typeof(T).GetMember(property.Name).Single()
                        ),
                        typeof(string).GetMethod("Contains", [typeof(string)])!,
                        valueExpression
                    ),
                    param
                );

                queryExpression = queryExpression.Where(whereLambda);
            }
            else if (property.PropertyType == typeof(Guid))
            {
                var value = (Guid)property.GetValue(parameter)!;
                if (value == Guid.Empty)
                    continue;

                Expression<Func<Guid>> valueLambda = () => value;
                var valueExpression = valueLambda.Body;

                var param = Expression.Parameter(typeof(T), "param");
                var whereLambda = Expression.Lambda<Func<T, bool>>(
                    Expression.Equal(
                        Expression.MakeMemberAccess(
                            param,
                            typeof(T).GetMember(property.Name).Single()
                        ),
                        valueExpression
                    ),
                    param
                );

                queryExpression = queryExpression.Where(whereLambda);
            }
            else if (property.PropertyType == typeof(Gender))
            {
                var value = (Gender)property.GetValue(parameter)!;
                if (value == Gender.Null)
                    continue;

                Expression<Func<Gender>> valueLambda = () => value;
                var valueExpression = valueLambda.Body;

                var param = Expression.Parameter(typeof(T), "param");
                var whereLambda = Expression.Lambda<Func<T, bool>>(
                    Expression.Equal(
                        Expression.MakeMemberAccess(
                            param,
                            typeof(T).GetMember(property.Name).Single()
                        ),
                        valueExpression
                    ),
                    param
                );

                queryExpression = queryExpression.Where(whereLambda);
            }
            else if (property.PropertyType == typeof(EthnicGroup))
            {
                var value = (EthnicGroup)property.GetValue(parameter)!;
                if (value == EthnicGroup.Null)
                    continue;

                Expression<Func<EthnicGroup>> valueLambda = () => value;
                var valueExpression = valueLambda.Body;

                var param = Expression.Parameter(typeof(T), "param");
                var whereLambda = Expression.Lambda<Func<T, bool>>(
                    Expression.Equal(
                        Expression.MakeMemberAccess(
                            param,
                            typeof(T).GetMember(property.Name).Single()
                        ),
                        valueExpression
                    ),
                    param
                );

                queryExpression = queryExpression.Where(whereLambda);
            }
            else if (property.PropertyType == typeof(PoliticalStatus))
            {
                var value = (PoliticalStatus)property.GetValue(parameter)!;
                if (value == PoliticalStatus.Null)
                    continue;

                Expression<Func<PoliticalStatus>> valueLambda = () => value;
                var valueExpression = valueLambda.Body;

                var param = Expression.Parameter(typeof(T), "param");
                var whereLambda = Expression.Lambda<Func<T, bool>>(
                    Expression.Equal(
                        Expression.MakeMemberAccess(
                            param,
                            typeof(T).GetMember(property.Name).Single()
                        ),
                        valueExpression
                    ),
                    param
                );

                queryExpression = queryExpression.Where(whereLambda);
            }
            else if (property.PropertyType == typeof(DateTime))
            {
                // Waiting for coding
            }
            else
            {
                Console.WriteLine("Unknown type");
            }
        }

        return queryExpression;
    }
}
