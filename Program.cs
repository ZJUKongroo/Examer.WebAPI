using Examer.Helpers;
using Scalar.AspNetCore;

var builder = WebApplication.CreateBuilder(args);

builder.WebHost.ConfigureKestrel(options =>
{
    options.Limits.MaxRequestBodySize = 1073741824;
    options.Limits.MaxRequestBufferSize = 1073741824;
    options.Limits.MaxRequestLineSize = 1048576;
});

builder.Services.RegisterServices(builder.Configuration);

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi("/openapi/Examer.json");
    app.MapScalarApiReference(options =>
    {
        options.WithOpenApiRoutePattern("/openapi/Examer.json");
    });
}

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.UseDefaultFiles();

app.UseStaticFiles();

app.MapControllers();

app.Run();
