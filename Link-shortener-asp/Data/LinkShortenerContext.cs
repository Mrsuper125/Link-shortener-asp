using Link_shortener_asp.Models;
using Microsoft.EntityFrameworkCore;

namespace Link_shortener_asp.Data;

public class LinkShortenerContext : DbContext
{
    public LinkShortenerContext(DbContextOptions<LinkShortenerContext> options) : base(options)
    {
        Database.EnsureCreated();
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseNpgsql("Host=localhost;Username=postgres;Database=link_shortener;Password=root");
        base.OnConfiguring(optionsBuilder);
    }

    public DbSet<Link> Links { get; set; }
}