using LinkIo.Domain.Entities;

namespace LinkIo.Application.Links.Models;
public class LinkDetailsDto
{
    public int Id { get; init; }
    public string? OriginalUrl { get; init; }
    public string? ShortUrlCode { get; init; }
    public required int ClickCount { get; init; }

    public DateTimeOffset Created { get; init; }
    public string? CreatedBy { get; init; }
    public DateTimeOffset LastModified { get; init; }
    public string? LastModifiedBy { get; init; }

    public IEnumerable<LinkReferrerDto>? Referrers { get; set; }

    private class Mapping : Profile
    {
        public Mapping()
        {
            CreateMap<Link, LinkDetailsDto>()
                .ForMember(d => d.ClickCount, opt => opt.MapFrom(s => s.Referrers.Count));
        }
    }
}
