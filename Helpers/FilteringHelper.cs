using Examer.DtoParameters;
using Examer.Enums;
using Examer.Models;
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

            queryExpression = queryExpression.TestMemberType<T, U, int>(parameter, property);
            queryExpression = queryExpression.TestMemberType<T, U, string>(parameter, property);
            queryExpression = queryExpression.TestMemberType<T, U, Guid>(parameter, property);
            queryExpression = queryExpression.TestMemberType<T, U, Gender>(parameter, property);
            queryExpression = queryExpression.TestMemberType<T, U, EthnicGroup>(parameter, property);
            queryExpression = queryExpression.TestMemberType<T, U, PoliticalStatus>(parameter, property);
            queryExpression = queryExpression.TestMemberType<T, U, FileType>(parameter, property);
        }

        return queryExpression;
    }

    private static IQueryable<T> TestMemberType<T, U, V>(this IQueryable<T> queryExpression, U parameter, PropertyInfo property) where T : IModelBase where U : IDtoParameterBase
    {
        if (property.PropertyType != typeof(V))
            return queryExpression;

        var value = (V)property.GetValue(parameter)!;
        if (IsNullValue(value))
            return queryExpression;

        Expression<Func<V>> valueLambda = () => value;
        var valueExpression = valueLambda.Body;

        var param = Expression.Parameter(typeof(T), "param");

        try
        {
            var lambdaBody = MakeLambdaBody<T, V>(param, valueExpression, property.Name); // property.Name needs recursiving
            var whereLambda = Expression.Lambda<Func<T, bool>>(lambdaBody, param);
            return queryExpression = queryExpression.Where(whereLambda);
        }
        catch (InvalidOperationException) // no such fields
        {
            return queryExpression;
        }
    }

    private static bool IsNullValue<T>(T value) => typeof(T).Name switch
    {
        "Int32" => value!.Equals(0),
        "String" => value == null,
        "Guid" => value!.Equals(Guid.Empty),
        "Gender" => value!.Equals(Gender.Null),
        "EthnicGroup" => value!.Equals(EthnicGroup.Null),
        "PoliticalStatus" => value!.Equals(PoliticalStatus.Null),
        "FileType" => value!.Equals(FileType.Null),
        _ => true // DELETE
    };

    private static Expression MakeLambdaBody<T, U>(ParameterExpression param, Expression valueExpression, string memberName) where T : IModelBase => typeof(U).Name switch
    {
        "Int32" => MakeLambdaBodyEqual<T>(param, valueExpression, memberName),
        "String" => MakeLambdaBodyContains<T>(param, valueExpression, memberName),
        "Guid" => MakeLambdaBodyEqual<T>(param, valueExpression, memberName),
        "Gender" => MakeLambdaBodyEqual<T>(param, valueExpression, memberName),
        "EthnicGroup" => MakeLambdaBodyEqual<T>(param, valueExpression, memberName),
        "PoliticalStatus" => MakeLambdaBodyEqual<T>(param, valueExpression, memberName),
        "FileType" => MakeLambdaBodyEqual<T>(param, valueExpression, memberName),
        _ => MakeLambdaBodyEqual<T>(param, valueExpression, memberName) // DELETE
    };

    private static BinaryExpression MakeLambdaBodyEqual<T>(ParameterExpression param, Expression valueExpression, string memberName) where T : IModelBase
    {
            return Expression.Equal(
                Expression.MakeMemberAccess(
                    param,
                    typeof(T).GetMember(memberName).Single()
                ),
                valueExpression
            );
    }

    private static MethodCallExpression MakeLambdaBodyContains<T>(ParameterExpression param, Expression valueExpression, string memberName) where T : IModelBase
    {
        return Expression.Call(
            Expression.MakeMemberAccess(
                param,
                typeof(T).GetMember(memberName).Single()
            ),
            typeof(string).GetMethod("Contains", [typeof(string)])!,
            valueExpression
        );
    }
}
