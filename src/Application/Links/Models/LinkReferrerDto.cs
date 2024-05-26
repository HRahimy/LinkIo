using LinkIo.Domain.Entities;

namespace LinkIo.Application.Links.Models;
public class LinkReferrerDto
{
    public string Url { get; init; } = string.Empty;
    public int Count { get; init; }

    private class Mapping : Profile
    {
        public Mapping()
        {
            CreateMap<LinkReferrer, LinkReferrerDto>()
                .ForMember(x => x.Count, opt => opt.Ignore());
        }
    }
}
