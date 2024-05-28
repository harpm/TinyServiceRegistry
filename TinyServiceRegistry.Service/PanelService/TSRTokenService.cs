using Microsoft.IdentityModel.Tokens;
using Sardanapal.Identity.Services.Services;
using Sardanapal.ViewModel.Response;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using TinyServiceRegistry.Share.Static;

namespace TinyServiceRegistry.Service.PanelService;

public class TSRTokenService : ITokenService
{
    public TSRTokenService()
    {
        
    }

    public IResponse<string> GenerateToken<TUserKey>(TUserKey uid, byte roleId)
    {
        TUserKey uid2 = uid;
        IResponse<string> result = new Response<string>("TokenService", OperationType.Function);
        return result.Fill(delegate
        {
            SigningCredentials signingCredentials = new SigningCredentials(CachedConfigs.TokenParameters.IssuerSigningKey, "HS256");
            Claim[] claims = new Claim[2]
            {
                    new Claim("http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier", uid2.ToString()),
                    new Claim("http://schemas.microsoft.com/ws/2008/06/identity/claims/role", roleId.ToString())
            };
            string validIssuer = CachedConfigs.TokenParameters.ValidIssuer;
            string validAudience = CachedConfigs.TokenParameters.ValidAudience;
            DateTime? expires = DateTime.UtcNow.AddMinutes(CachedConfigs.ExpirationTime);
            SigningCredentials signingCredentials2 = signingCredentials;
            JwtSecurityToken token = new JwtSecurityToken(validIssuer, validAudience, claims, null, expires, signingCredentials2);
            result.Set(StatusCode.Succeeded, new JwtSecurityTokenHandler().WriteToken(token));
            return result;
        });
    }

    public IResponse<bool> ValidateToken(string token, out ClaimsPrincipal claims)
    {
        IResponse<bool> response = new Response<bool>("TokenService", OperationType.Function);
        claims = new ClaimsPrincipal();
        try
        {
            JwtSecurityTokenHandler jwtSecurityTokenHandler = new JwtSecurityTokenHandler();
            claims = jwtSecurityTokenHandler.ValidateToken(token, CachedConfigs.TokenParameters, out var _);
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
        return result.Fill(delegate
        {
            SecurityToken validatedToken;
            ClaimsPrincipal claimsPrincipal = new JwtSecurityTokenHandler().ValidateToken(token2, CachedConfigs.TokenParameters, out validatedToken);
            result.Set(StatusCode.Succeeded, claimsPrincipal.HasClaim((Claim c) => c.Type == "http://schemas.microsoft.com/ws/2008/06/identity/claims/role" && c.Value == roleId.ToString()));
            return result;
        });

    }

    public IResponse<bool> ValidateTokenRoles(string token, byte[] roleIds)
    {
        string token2 = token;
        byte[] roleIds2 = roleIds;
        IResponse<bool> result = new Response("TokenService", OperationType.Function);
        return result.Fill(delegate
        {
            SecurityToken validatedToken;
            ClaimsPrincipal claimsPrincipal = new JwtSecurityTokenHandler().ValidateToken(token2, CachedConfigs.TokenParameters, out validatedToken);
            result.Set(StatusCode.Succeeded, claimsPrincipal.HasClaim((Claim c) => c.Type == "http://schemas.microsoft.com/ws/2008/06/identity/claims/role" && roleIds2.Select((byte r) => r.ToString()).Contains(c.Value)));
            return result;
        });
    }
}
