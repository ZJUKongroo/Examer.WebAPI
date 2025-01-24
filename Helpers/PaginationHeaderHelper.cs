using Examer.Models;
using Examer.DtoParameters;
using Examer.Enums;
using System.Text.Json;
using System.Text.Encodings.Web;
using Microsoft.AspNetCore.Mvc;

namespace Examer.Helpers;

public static class PaginationHeaderHelper
{
    public static void AppendPaginationHeader<T, U>(this IHeaderDictionary headers, PagedList<T> pagedList, U parameter, IUrlHelper urlHelper, string route) where T : IModelBase where U : IDtoParameterBase
    {
        var previousPageLink = pagedList.HasPrevious ? CreateResourceUri(parameter, ResourceUriType.PreviousPage, urlHelper, route) : null;
        var nextPageLink = pagedList.HasNext ? CreateResourceUri(parameter, ResourceUriType.NextPage, urlHelper, route) : null;

        var paginationMetadata = new
        {
            totalCount = pagedList.TotalCount,
            pageSize = pagedList.PageSize,
            currentPage = pagedList.CurrentPage,
            totalPages = pagedList.TotalPages,
            previousPageLink,
            nextPageLink
        };

        headers.Append("X-Pagination", JsonSerializer.Serialize(paginationMetadata, new JsonSerializerOptions
        {
            Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping
        }));
    }

    private static string CreateResourceUri<T>(T parameter, ResourceUriType type, IUrlHelper urlHelper, string route) where T : IDtoParameterBase
    {
        switch (type)
        {
            case ResourceUriType.PreviousPage:
                return urlHelper.Link(route, new
                {
                    pageNumber = parameter.PageNumber - 1,
                    pageSize = parameter.PageSize,
                })!;
            case ResourceUriType.NextPage:
                return urlHelper.Link(route, new
                {
                    pageNumber = parameter.PageNumber + 1,
                    pageSize = parameter.PageSize,
                })!;
            default:
                return urlHelper.Link(route, new{
                    pageNumber = parameter.PageNumber,
                    pageSize = parameter.PageSize
                })!;
        }
    }
}
