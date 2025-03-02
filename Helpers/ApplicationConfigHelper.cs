using Examer.Database;
using Examer.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.EntityFrameworkCore;

namespace Examer.Helpers;

public static class ApplicationConfigHelper
{
    public static IServiceCollection RegisterServices(this IServiceCollection services, ConfigurationManager configuration)
    {
        JwtConfig jwtConfig = new();
        configuration.Bind("JwtConfig", jwtConfig);
        JwtHelper jwtHelper = new()
        {
            JwtConfig = jwtConfig
        };

        services.AddScoped<IAuthenticationRepository, AuthenticationRepository>();
        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<IExamRepository, ExamRepository>();
        services.AddScoped<IProblemRepository, ProblemRepository>();
        services.AddScoped<ICommitRepository, CommitRepository>();
        services.AddScoped<IMarkingRepository, MarkingRepository>();
        services.AddScoped<IFileRepository, FileRepository>();
        services.AddSingleton(jwtHelper);

        services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
        services.AddControllers().AddXmlDataContractSerializerFormatters();
        services.AddRouting(options =>
        {
            options.LowercaseUrls = true;
            options.LowercaseQueryStrings = true;
        });
        services.Configure<FormOptions>(options =>
        {
            options.MultipartBodyLengthLimit = 1073741824;
            options.MemoryBufferThreshold = 1073741824;
        });
        services.AddDbContext<ExamerDbContext>(options => 
            options.UseSqlite(configuration.GetConnectionString("Examer"))
        );
        services.AddEndpointsApiExplorer();
        services.AddOpenApi(options => 
        {
            options.AddDocumentTransformer<BearerSecuritySchemeTransformer>();
        });
        
        services.AddAuthentication(options =>
        {
            options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        })
        .AddJwtBearer(options => {
            options.TokenValidationParameters = new()
            {
                ValidIssuer = jwtHelper.JwtConfig.Issuer,
                ValidAudience = jwtHelper.JwtConfig.Audience,
                IssuerSigningKey = jwtHelper.JwtConfig.SymmetricSecurityKey,
                ValidateLifetime = true,
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateIssuerSigningKey = true,
                RequireExpirationTime = true
            };
        });

        return services;
    }
}
