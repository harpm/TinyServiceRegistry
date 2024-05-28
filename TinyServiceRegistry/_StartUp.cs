using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using MySql.EntityFrameworkCore.Extensions;
using Sardanapal.Identity.Authorization.Data;
using Sardanapal.Identity.Dto;
using Sardanapal.Identity.Services.Services;
using Sardanapal.Identity.Services.Services.AccountService;
using Sardanapal.Identity.Services.Services.UserManager;
using Sardanapal.Identity.Share;
using Sardanapal.Identity.ViewModel.Models.Account;
using System.Text;
using TinyServiceRegistry.Domain.Data;
using TinyServiceRegistry.Domain.Entities;
using TinyServiceRegistry.Service.PanelService;
using TinyServiceRegistry.Share.Static;
using static Org.BouncyCastle.Math.EC.ECCurve;

namespace TinyServiceRegistry;

public static class _StartUp
{
    public static IServiceCollection ConfigureTSR(this IServiceCollection services, IConfiguration configs)
    {
        configs.CachConfigs();
        services.AddScoped<IIdentityHolder, IdentityHolder>();
        services.AddSqlServer<TSRUnitOfWork>(CachedConfigs.DbConnectionString
            , opt =>
        {
            opt.UseQuerySplittingBehavior(QuerySplittingBehavior.SplitQuery);
            opt.MigrationsAssembly("TinyServiceRegistry");
        });
        services.AddHttpContextAccessor();
        services.AddScoped<ITokenService, TSRTokenService>();
        services.AddScoped<IUserManagerService<long, TSRUser, TSRRole>, TSRUserManager>(opt =>
        {
            var serviceProv = opt.CreateScope().ServiceProvider;
            return new TSRUserManager(serviceProv.GetRequiredService<TSRUnitOfWork>(), serviceProv.GetRequiredService<ITokenService>(), 0);
        });
        services.AddScoped<IAccountServiceBase<long, LoginVM, LoginDto, RegisterVM>, TSRAccountService>(opt =>
        {
            var serviceProv = opt.CreateScope().ServiceProvider;
            return new TSRAccountService(serviceProv.GetRequiredService<TSRUserManager>(), 0);
        });

        return services;
    }

    public static void CachConfigs(this IConfiguration configs)
    {
        CachedConfigs.DbConnectionString = configs.GetConnectionString("SqlDb");

        CachedConfigs.RedisConnectionString = configs.GetConnectionString("Redis");
        var TokenProvider = configs.GetSection("TokenProvider");
        string secretKeyStr = TokenProvider.GetValue<string>("SecretKey");
        var SymmetricKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKeyStr));
        CachedConfigs.TokenParameters = new TokenValidationParameters()
        {
            ValidIssuer = TokenProvider.GetSection("Issuer").Value,
            ValidAudience = TokenProvider.GetSection("Audience").Value,
            IssuerSigningKey = SymmetricKey
        };
        CachedConfigs.ExpirationTime = Convert.ToInt32(TokenProvider.GetSection("TokenExpireTime").Value);
        CachedConfigs.OTPLength = Convert.ToInt32(TokenProvider.GetSection("OtpLength").Value);
    }
}
