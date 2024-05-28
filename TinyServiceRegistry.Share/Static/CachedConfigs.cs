
using Microsoft.IdentityModel.Tokens;

namespace TinyServiceRegistry.Share.Static;

public static class CachedConfigs
{
    public static string DbConnectionString;
    public static string? RedisConnectionString { get; set; }
    public static TokenValidationParameters TokenParameters { get; set; }
    public static int ExpirationTime { get; set; }
    public static int? OTPLength { get; set; }
}
