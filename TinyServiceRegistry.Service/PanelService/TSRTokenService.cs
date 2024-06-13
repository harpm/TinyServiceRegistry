using Microsoft.IdentityModel.Tokens;
using Sardanapal.Identity.Services.Services;
using Sardanapal.Identity.Share.Static;
using Sardanapal.ViewModel.Response;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace TinyServiceRegistry.Service.PanelService;

public class TSRTokenService : TokenService
{
    public TSRTokenService()
    {
        
    }

    public IResponse<string> GenerateToken<TUserKey>(TUserKey uid, byte roleId)
    {
        TUserKey uid2 = uid;
        IResponse<string> result = new Response<string>("TokenService", OperationType.Function);
        return result.Fill(() =>
        {
            SigningCredentials signingCredentials = new SigningCredentials(StaticConfigs.TokenParameters.IssuerSigningKey, "HS256");
            Claim[] claims = new Claim[2]
            {
                    new Claim(ClaimTypes.NameIdentifier, uid2.ToString()),
                    new Claim(ClaimTypes.Role, roleId.ToString())
            };
            string validIssuer = StaticConfigs.TokenParameters.ValidIssuer;
            string validAudience = StaticConfigs.TokenParameters.ValidAudience;
            DateTime? expires = DateTime.UtcNow.AddMinutes(StaticConfigs.ExpirationTime);
            SigningCredentials signingCredentials2 = signingCredentials;
            JwtSecurityToken token = new JwtSecurityToken(validIssuer, validAudience, claims, null, expires, signingCredentials2);
            result.Set(StatusCode.Succeeded, new JwtSecurityTokenHandler().WriteToken(token));
        });
    }
    public virtual IResponse<string> GenerateToken<TUserKey>(TUserKey uid, params byte[] roleIds)
    {
        byte[] roleIds2 = roleIds;
        TUserKey uid2 = uid;
        IResponse<string> result = new Response<string>(ServiceName, OperationType.Function);
        return result.Fill(delegate
        {
            Claim[] array = new Claim[roleIds2.Length];
            for (int i = 0; i < roleIds2.Length; i++)
            {
                array[i] = new Claim(ClaimTypes.Role, roleIds2[0].ToString());
            }

            List<Claim> list = new List<Claim>
                {
                    new Claim(ClaimTypes.Role, uid2.ToString())
                };
            list.AddRange(array);
            SigningCredentials signingCredentials = new SigningCredentials(StaticConfigs.TokenParameters.IssuerSigningKey, "HS256");
            string validIssuer = StaticConfigs.TokenParameters.ValidIssuer;
            string validAudience = StaticConfigs.TokenParameters.ValidAudience;
            DateTime? expires = DateTime.UtcNow.AddMinutes(StaticConfigs.ExpirationTime);
            SigningCredentials signingCredentials2 = signingCredentials;
            JwtSecurityToken token = new JwtSecurityToken(validIssuer, validAudience, list, null, expires, signingCredentials2);
            result.Set(StatusCode.Succeeded, new JwtSecurityTokenHandler().WriteToken(token));
        });
    }

    public IResponse<bool> ValidateToken(string token, out ClaimsPrincipal claims)
    {
        IResponse<bool> response = new Response<bool>("TokenService", OperationType.Function);
        claims = new ClaimsPrincipal();
        try
        {
            JwtSecurityTokenHandler jwtSecurityTokenHandler = new JwtSecurityTokenHandler();
            claims = jwtSecurityTokenHandler.ValidateToken(token, StaticConfigs.TokenParameters, out var _);
            response.Set(StatusCode.Succeeded, data: true);
            return response;
        }
        catch (Exception exception)
        {
            response.Set(StatusCode.Exception, exception);
            return response;
        }
    }

    public IResponse<bool> ValidateTokenRole(string token, byte roleId)
    {
        string token2 = token;
        IResponse<bool> result = new Response("TokenService", OperationType.Function);
        return result.Fill(() =>
        {
            SecurityToken validatedToken;
            ClaimsPrincipal claimsPrincipal = new JwtSecurityTokenHandler().ValidateToken(token2, StaticConfigs.TokenParameters, out validatedToken);
            result.Set(StatusCode.Succeeded, claimsPrincipal.HasClaim((Claim c) => c.Type == "http://schemas.microsoft.com/ws/2008/06/identity/claims/role" && c.Value == roleId.ToString()));
        });

    }

    public IResponse<bool> ValidateTokenRoles(string token, byte[] roleIds)
    {
        string token2 = token;
        byte[] roleIds2 = roleIds;
        IResponse<bool> result = new Response("TokenService", OperationType.Function);
        return result.Fill(() =>
        {
            SecurityToken validatedToken;
            ClaimsPrincipal claimsPrincipal = new JwtSecurityTokenHandler().ValidateToken(token2, StaticConfigs.TokenParameters, out validatedToken);
            result.Set(StatusCode.Succeeded, claimsPrincipal.HasClaim((Claim c) => c.Type == "http://schemas.microsoft.com/ws/2008/06/identity/claims/role" && roleIds2.Select((byte r) => r.ToString()).Contains(c.Value)));
        });
    }
}
