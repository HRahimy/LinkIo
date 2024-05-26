using System.Reflection;
using LinkIo.Application.Common.Interfaces;
using LinkIo.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace LinkIo.Infrastructure.Data;

public class ApplicationDbContext : DbContext, IApplicationDbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

    public DbSet<TodoList> TodoLists => Set<TodoList>();

    public DbSet<TodoItem> TodoItems => Set<TodoItem>();

    public DbSet<Link> Links => Set<Link>();

    public DbSet<LinkReferrer> LinkReferrers => Set<LinkReferrer>();

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }
}
