namespace Auth.Module;

using System.Text;

using Auth.Module.Configuration;
using Auth.Module.Context;
using Auth.Module.Features;
using Auth.Module.Model;
using Auth.Module.Services;

using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;

public static class AuthModuleExtension
{
  public static IServiceCollection AddAuthModule(this IServiceCollection services, IConfiguration configuration)
  {
    _ = services.AddControllers();
    _ = services.AddEndpointsApiExplorer();
    _ = services.AddSwaggerGen(options =>
    {
      options.EnableAnnotations();
      options.AddSecurityDefinition("bearerAuth", new OpenApiSecurityScheme
      {
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        Scheme = "bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = "JWT Authorization header using the Bearer scheme."
      });
      options.AddSecurityRequirement(new OpenApiSecurityRequirement
      {
          {
              new OpenApiSecurityScheme
              {
                  Reference = new OpenApiReference
                  {
                      Type = ReferenceType.SecurityScheme,
                      Id = "bearerAuth"
                  }
              },
              Array.Empty<string>()
          }
      });
    });

    _ = services.AddTransient<IRoleService, AuthService>();
    _ = services.AddTransient<IUserService, AuthService>();
    _ = services.AddTransient<IAuthMail, AuthMail>();

    JwtSettings? jwtSettings = configuration.GetSection("JwtSettings").Get<JwtSettings>()
      ?? throw new ArgumentException("No jwtsettings");
    _ = services.Configure<JwtSettings>(opt => configuration.GetSection("JwtSettings").Bind(opt));

    string connectionString = configuration.GetConnectionString("AuthDb")
      ?? throw new ArgumentException("No connectionstring");

    ServerVersion serverVersion = ServerVersion.AutoDetect(connectionString);

    _ = services.AddDbContext<AuthDbContext>(options => options.UseMySql(connectionString, serverVersion));

    _ = services.AddIdentity<AuthUser, AuthRole>(options =>
    {
      options.Password.RequireDigit = jwtSettings.RequireDigit;
      options.Password.RequiredLength = jwtSettings.RequiredLength;
      options.Password.RequireNonAlphanumeric = jwtSettings.RequireNonAlphanumeric;
      options.Password.RequireUppercase = jwtSettings.RequireUppercase;
      options.Password.RequireLowercase = jwtSettings.RequireLowercase;
    })
      .AddEntityFrameworkStores<AuthDbContext>()
      .AddDefaultTokenProviders();

    _ = services.AddAuthorization()
    .AddAuthentication(options =>
    {
      options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
      options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
      options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    })
    //https://github.com/aspnet-contrib/AspNet.Security.OAuth.Providers/tree/dev/src
    .AddJwtBearer(options =>
    {
      options.RequireHttpsMetadata = false;
      options.TokenValidationParameters = new TokenValidationParameters
      {
        ValidIssuer = jwtSettings.Issuer,
        ValidAudience = jwtSettings.Issuer,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings.Secret)),
        ClockSkew = TimeSpan.Zero
      };
    });

    _ = services.AddAuthorizationBuilder()

        //https://www.yogihosting.com/aspnet-core-identity-policies/
        //https://learn.microsoft.com/en-us/aspnet/core/security/authorization/claims?view=aspnetcore-8.0

        .AddPolicy("SuperAdminPolicy", policy => policy
            .RequireRole("SuperAdmin")
            .RequireAuthenticatedUser())
        .AddPolicy("AdminPolicy", policy => policy
            .RequireRole("Admin")
            .RequireAuthenticatedUser())
        .AddPolicy("UserPolicy", policy => policy
            .RequireRole("User")
            .RequireAuthenticatedUser());

    return services;
  }

  public static WebApplication UseApiAuth(this WebApplication app)
  {
    _ = app.UseAuthentication();
    _ = app.UseAuthorization();

    return app;
  }

  public static WebApplication UpdateIdentityDb(this WebApplication host)
  {
    using (IServiceScope scope = host.Services.CreateScope())
    {
      scope.ServiceProvider.GetRequiredService<AuthDbContext>().EnsureDbExists();
    }

    return host;
  }

  public static IServiceCollection ChangeMailProvider<T>(this IServiceCollection services) where T : class, IAuthMail
  {
    ServiceDescriptor descriptor = new(typeof(IAuthMail), typeof(T), ServiceLifetime.Transient);
    _ = services.Replace(descriptor);

    return services;
  }
}