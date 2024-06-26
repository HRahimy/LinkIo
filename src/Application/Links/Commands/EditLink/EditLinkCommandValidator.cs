﻿using System.Text.RegularExpressions;
using LinkIo.Application.Common.Interfaces;

namespace LinkIo.Application.Links.Commands.EditLink;
public class EditLinkCommandValidator : AbstractValidator<EditLinkCommand>
{
    private readonly IApplicationDbContext _context;
    private readonly IUser _user;
    public EditLinkCommandValidator(IApplicationDbContext context, IUser user)
    {
        _context = context;
        _user = user;

        RuleFor(v => v.Id)
            .NotNull()
            .MustAsync(ExistAndBelongsToUser).WithMessage("Link with given Id does not exist or doesnt belong to user");

        RuleFor(v => v.ShortUrlCode)
            .NotNull()
            .NotEmpty()
            .Length(15).WithMessage("ShortUrlCode must be exactly 15 characters")
            .Must(BeAlphanumeric).WithMessage("ShortUrlCode can only contain letters and numbers")
            .MustAsync(BeUniqueCode).WithMessage("ShortUrlCode must be unique");
    }

    public static bool BeAlphanumeric(string input)
    {
        string pattern = @"^[a-zA-Z0-9]+$";

        return Regex.IsMatch(input, pattern);
    }

    public async Task<bool> ExistAndBelongsToUser(int id, CancellationToken cancellationToken)
    {
        return await _context.Links.AnyAsync(l => l.Id == id && l.CreatedBy == _user.Id);
    }

    public async Task<bool> BeUniqueCode(string code, CancellationToken cancellationToken)
    {
        return await _context.Links.AllAsync(e => e.ShortUrlCode != code, cancellationToken);
    }
}
