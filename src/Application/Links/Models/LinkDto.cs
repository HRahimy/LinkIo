using LinkIo.Domain.Entities;

namespace LinkIo.Application.Links.Models;
public class LinkDto
{
    public int Id { get; init; }
    public required string OriginalUrl { get; init; }
    public required string ShortUrlCode { get; init; }
    public required int ClickCount { get; init; }

    private class Mapping : Profile
    {
        public Mapping()
        {
            CreateMap<Link, LinkDto>()
                .ForMember(d => d.ClickCount, opt => opt.MapFrom(s => s.Referrers.Count));
        }
    }
}
