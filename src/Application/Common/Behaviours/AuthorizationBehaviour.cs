using System.Reflection;
using LinkIo.Application.Common.Exceptions;
using LinkIo.Application.Common.Interfaces;
using LinkIo.Application.Common.Security;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace LinkIo.Application.Common.Behaviours;

public class AuthorizationBehaviour<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse> where TRequest : notnull
{
    private readonly IUser _user;
    private readonly AuthorizationOptions _authorizationOptions;

    public AuthorizationBehaviour(
        IUser user,
        IOptions<AuthorizationOptions> authorizationOptions
        )
    {
        _user = user;
        _authorizationOptions = authorizationOptions.Value;
    }

    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        var authorizeAttributes = request.GetType().GetCustomAttributes<Security.AuthorizeAttribute>();


        if (authorizeAttributes.Any())
        {
            // Must be authenticated user
            if (_user.Id == null)
            {
                throw new UnauthorizedAccessException();
            }

            // Role-based authorization
            //var authorizeAttributesWithRoles = authorizeAttributes.Where(a => !string.IsNullOrWhiteSpace(a.Roles));

            //if (authorizeAttributesWithRoles.Any())
            //{
            //    var authorized = false;

            //    foreach (var roles in authorizeAttributesWithRoles.Select(a => a.Roles.Split(',')))
            //    {
            //        foreach (var role in roles)
            //        {
            //            var isInRole = await _identityService.IsInRoleAsync(_user.Id, role.Trim());
            //            if (isInRole)
            //            {
            //                authorized = true;
            //                break;
            //            }
            //        }
            //    }

            //    // Must be a member of at least one role in roles
            //    if (!authorized)
            //    {
            //        throw new ForbiddenAccessException();
            //    }
            //}

            // Policy-based authorization
            //var authorizeAttributesWithPolicies = authorizeAttributes.Where(a => !string.IsNullOrWhiteSpace(a.Policy));
            //if (authorizeAttributesWithPolicies.Any())
            //{
            //    foreach (var policy in authorizeAttributesWithPolicies.Select(a => a.Policy))
            //    {
            //        var authorized = await _identityService.AuthorizeAsync(_user.Id, policy);

            //        if (!authorized)
            //        {
            //            throw new ForbiddenAccessException();
            //        }
            //    }
            //}

            //Scope-based authorization
            var authorizeAttributesWithScopes = authorizeAttributes.Where(a => !string.IsNullOrWhiteSpace(a.PermissionScope));
            if (authorizeAttributesWithScopes.Any())
            {
                foreach (var scope in authorizeAttributesWithScopes.Select(a => a.PermissionScope))
                {
                    var authorized = false;

                    var scopeRequirements = _authorizationOptions.GetPolicy(scope)?
                        .Requirements
                        .Where(e => e.GetType() == typeof(HasPermissionScopeRequirement))
                        .Select(x => x as HasPermissionScopeRequirement)
                        .ToList() ?? [];

                    if (scopeRequirements.IsNullOrEmpty()) authorized = true;

                    foreach (var requirement in scopeRequirements)
                    {
                        if (requirement == null)
                        {
                            authorized = true;
                            continue;
                        }

                        if (_user.ClaimsPrincipal == null
                            || !_user.ClaimsPrincipal!.HasClaim(c => c.Type == "scope" && c.Issuer == requirement.Issuer)
                            )
                        {
                            throw new ForbiddenAccessException();
                        }

                        //var scopes = _user.ClaimsPrincipal.FindFirst(c => c.Type == "scope" && c.Issuer == requirement.Issuer)?.Value.Split(' ');

                        var scopes = _user.ClaimsPrincipal
                            .FindAll(c => c.Type == "permissions" && c.Issuer == requirement.Issuer)?
                            .Select(c => c.Value.Split(' ')[0]).ToArray();

                        if (scopes == null)
                        {
                            authorized = false;
                            break;
                        }

                        if (scopes.Any(s => s == requirement.Scope))
                        {
                            authorized = true;
                            continue;
                        }


                    }

                    if (!authorized)
                    {
                        throw new ForbiddenAccessException();
                    }
                }
            }
        }

        // User is authorized / authorization not required
        return await next();
    }
}
