// Copyright (c) ZJUKongroo. All Rights Reserved.

using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.OpenApi;
using Microsoft.OpenApi;

namespace Examer.Helpers;

public sealed class BearerSecuritySchemeTransformer(IAuthenticationSchemeProvider authenticationSchemeProvider) : IOpenApiDocumentTransformer
{
    public async Task TransformAsync(OpenApiDocument document, OpenApiDocumentTransformerContext context, CancellationToken cancellationToken)
    {
        var authenticateSchemes = await authenticationSchemeProvider.GetAllSchemesAsync();

        if (authenticateSchemes.Any(authScheme => authScheme.Name == "Bearer"))
        {
            var securitySchemes = new Dictionary<string, IOpenApiSecurityScheme>
            {
                ["Bearer"] = new OpenApiSecurityScheme()
                {
                    Type = SecuritySchemeType.Http,
                    Scheme = "bearer",
                    In = ParameterLocation.Header,
                    BearerFormat = "Json Web Token"
                }
            };

            document.Components ??= new OpenApiComponents();
            document.Components.SecuritySchemes = securitySchemes;

            // TODO: Login do not need authentication
            foreach (var operation in document.Paths.Values.SelectMany(path => path.Operations!))
            {
                operation.Value.Security ??= [];
                operation.Value.Security.Add(
                    new OpenApiSecurityRequirement
                    {
                        [new OpenApiSecuritySchemeReference("Bearer", document)] = []
                    }
                );
            }
        }
    }
}
