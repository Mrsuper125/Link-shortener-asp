using Link_shortener_asp.Models;
using Microsoft.EntityFrameworkCore;

namespace Link_shortener_asp.Data;

public class LinkShortenerContext : DbContext
{
    public LinkShortenerContext(DbContextOptions<LinkShortenerContext> options) : base(options)
    {
        Database.EnsureCreated();
    }
    
    public DbSet<Link> Links { get; set; }
}