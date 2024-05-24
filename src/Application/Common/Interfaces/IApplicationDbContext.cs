using LinkIo.Domain.Entities;

namespace LinkIo.Application.Common.Interfaces;

public interface IApplicationDbContext
{
    DbSet<TodoList> TodoLists { get; }

    DbSet<TodoItem> TodoItems { get; }

    DbSet<Link> Links { get; }

    DbSet<LinkReferrer> LinkReferrers { get; }

    Task<int> SaveChangesAsync(CancellationToken cancellationToken);
}
