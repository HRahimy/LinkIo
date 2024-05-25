using System.Security.Claims;

using LinkIo.Application.Common.Interfaces;

namespace LinkIo.Web.Services;

public class CurrentUser : IUser
{
    private readonly IHttpContextAccessor _httpContextAccessor;

    public CurrentUser(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
    }

    public string? Id => _httpContextAccessor.HttpContext?.User?.FindFirstValue(ClaimTypes.NameIdentifier);
    public ClaimsPrincipal? ClaimsPrincipal => _httpContextAccessor.HttpContext?.User;
}
