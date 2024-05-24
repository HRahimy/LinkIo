using System.Data.SqlClient;
using System.Text;
using LinkIo.Application.Common.Interfaces;
using LinkIo.Application.Links.Models;
using LinkIo.Domain.Entities;
using LinkIo.Domain.Events;

namespace LinkIo.Application.Links.Commands.CreateLink;
public record CreateLinkCommand : IRequest<LinkDto>
{
    public required string Url { get; init; }
}

public class CreateLinkCommandHandler : IRequestHandler<CreateLinkCommand, LinkDto>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;

    private const int MaxRetries = 10;
    private const string Characters = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";


    public CreateLinkCommandHandler(IApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<LinkDto> Handle(CreateLinkCommand request, CancellationToken cancellationToken)
    {
        var entity = new Link
        {
            OriginalUrl = request.Url
        };

        entity.AddDomainEvent(new LinkCreatedEvent(entity));

        int retryCount = 0;
        bool isSaved = false;

        while (!isSaved && retryCount < MaxRetries)
        {
            try
            {
                entity.ShortUrlCode = GenerateRandomString(15);
                _context.Links.Add(entity);
                await _context.SaveChangesAsync(cancellationToken);
                isSaved = true;
            }
            catch (DbUpdateException ex) when (IsUniqueConstraintViolation(ex))
            {
                retryCount++;
            }
        }

        return !isSaved
            ? throw new Exception("Failed to generate a unique random string after multiple attempts.")
            : _mapper.Map<LinkDto>(entity);
    }

    private bool IsUniqueConstraintViolation(DbUpdateException ex)
    {
        if (ex.InnerException is SqlException sqlException)
        {
            return sqlException.Number == 2627 || sqlException.Number == 2601;
        }
        return false;
    }

    public static string GenerateRandomString(int length)
    {
        Random random = new Random();
        StringBuilder sb = new StringBuilder(length);

        for (int i = 0; i < length; i++)
        {
            int index = random.Next(Characters.Length);
            sb.Append(Characters[index]);
        }

        return sb.ToString();
    }

}
