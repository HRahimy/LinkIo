using System.Security.Claims;

namespace LinkIo.Application.Common.Interfaces;

public interface IUser
{
    string? Id { get; }
    ClaimsPrincipal? ClaimsPrincipal { get; }
}
