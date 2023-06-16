using CleanArchitecture.Application.Interfaces.Services;
using CleanArchitecture.Infraestructure.Security.Services;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;

namespace CleanArchitecture.Infraestructure.Security.Extensions
{
    public static class SecurityServicesExtension
    {
        public static void ConfigureSecurityServices(this IServiceCollection services)
        {
            services.AddScoped<ITokenService, TokenService>();
            services.AddScoped<IPasswordService, PasswordService>();
            services.AddScoped<IECDsaEncryptionService, ECDsaEncryptionService>();
            
            services.AddAuthentication()
                .AddJwtBearer("JwtECDsa", options =>
                {
                    var ecdsaEncryptionService = services.BuildServiceProvider().GetRequiredService<IECDsaEncryptionService>();
                    var storedJwk = ecdsaEncryptionService.GetPublicKey();

                    options.IncludeErrorDetails = true;

                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidIssuer = "https://localhost:7108",
                        ValidAudience = "ProjetoM.WebApi",
                        RequireSignedTokens = true,
                        RequireExpirationTime = true,
                        ValidateLifetime = true,
                        ValidateAudience = true,
                        ValidateIssuer = true,
                        IssuerSigningKey = storedJwk
                    };
                });
        }
    }
}
