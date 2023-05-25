using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace WeFin.Services
{
    internal class JWTLogin
    {
    }
}

/*
public class AccountController : ApiControllerBase
{
    private readonly IMemoryCache _cache;
    private readonly IOptionsMonitor<JwtOption> _jwtOpt;

    private readonly IPasswordCryptor _passwordCryptor;

    private readonly MyDbContext _efContext;

    public AccountController(ILogger<AccountController> logger,
        IMemoryCache cache,
        IOptionsMonitor<JwtOption> jwtOpt,
        IPasswordCryptor passwordCryptor,
        MyDbContext efContext) : base(logger)
    {
        _cache = cache;
        _jwtOpt = jwtOpt;
        _passwordCryptor = passwordCryptor;
        _efContext = efContext;
    }
    [HttpPost]
    public async Task<IActionResult> Login([FromForm] LoginRqtDto rqtDto)
    {
        var cryptedPwd = _passwordCryptor.Encrypt(rqtDto.Password, default);
        string adminIdCacheKey = CacheKeyHelper.GetAdminIdCacheKey(rqtDto.Account);
        if (!_cache.TryGetValue(adminIdCacheKey, out int adminId))
        {
            adminId = await _efContext.Admins
               .Where(a => a.Account == rqtDto.Account && a.Password == cryptedPwd)
               .Select(a => a.AdminId)
               .FirstOrDefaultAsync();
            if (adminId < 1)
            {
                return Unauthorized();
            }
            _cache.Set(adminIdCacheKey, adminId, TimeSpan.FromDays(1));
        }
        else
        {
            bool checkPwd = await _efContext.Admins.AnyAsync(a => a.AdminId == adminId && a.Password == cryptedPwd);
            if (!checkPwd)
            {
                return Unauthorized();
            }
        }
        var claims = new Claim[]
        {
                 new(ClaimTypes.NameIdentifier, adminId.ToString()),
                 new(ClaimTypes.Name, rqtDto.Account),
                 new(ClaimTypes.Role, "admin")
        };
        var jwtSetting = _jwtOpt.CurrentValue;
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSetting.Key));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
        var expiry = DateTime.Now.AddHours(jwtSetting.ExpiryInHours);
        var token = new JwtSecurityToken(jwtSetting.Issuer, jwtSetting.Audience, claims, expires: expiry, signingCredentials: creds);
        var tokenText = new JwtSecurityTokenHandler().WriteToken(token);
        return Ok(tokenText);
    }
}
*/