using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Sardanapal.Http.Service.Services;
using Sardanapal.Identity.Authorization.Data;
using Sardanapal.Identity.Services.Services;
using Sardanapal.Identity.Services.Services.UserManager;
using Sardanapal.Identity.Share.Static;
using Sardanapal.InterfacePanel.Service;
using TinyServiceRegistry.Domain.Data;
using TinyServiceRegistry.Domain.Entities;
using TinyServiceRegistry.Service;
using TinyServiceRegistry.Service.PanelService;

namespace TinyServiceRegistry;

public static class _StartUp
{
    public static IServiceCollection ConfigureTSR(this IServiceCollection services, IConfiguration configs)
    {
        configs.CachConfigs();
        services.AddScoped<IIdentityHolder, IdentityHolder>();
        services.AddSqlServer<TSRUnitOfWork>(StaticConfigs.DbConnectionString
            , opt =>
        {
            opt.UseQuerySplittingBehavior(QuerySplittingBehavior.SplitQuery);
            opt.MigrationsAssembly("TinyServiceRegistry");
        });
        services.AddHttpContextAccessor();
        services.AddScoped<ITokenService, TSRTokenService>();
        services.AddScoped<IUserManagerService<long, TSRUser, TSRRole>, TSRAdminUserManager>();
        services.AddScoped<IRequestService, RequestService>();

        services.InjectTSRServices();

        return services;
    }

    public static void CachConfigs(this IConfiguration configs)
    {
        StaticConfigs.DbConnectionString = configs.GetConnectionString("SqlDb");

        StaticConfigs.RedisConnectionString = configs.GetConnectionString("Redis");
        var TokenProvider = configs.GetSection("TokenProvider");
        string secretKeyStr = TokenProvider.GetValue<string>("SecretKey");
        var SymmetricKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKeyStr));
        StaticConfigs.TokenParameters = new TokenValidationParameters()
        {
            ValidIssuer = TokenProvider.GetSection("Issuer").Value,
            ValidAudience = TokenProvider.GetSection("Audience").Value,
            IssuerSigningKey = SymmetricKey
        };
        StaticConfigs.ExpirationTime = Convert.ToInt32(TokenProvider.GetSection("TokenExpireTime").Value);
        StaticConfigs.OTPLength = Convert.ToInt32(TokenProvider.GetSection("OtpLength").Value);
    }
}
