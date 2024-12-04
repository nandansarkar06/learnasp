using Microsoft.EntityFrameworkCore;
using NzWalk.Models;

namespace NzWalk.Data;

public class NzWalksDbContext: DbContext
{

    public NzWalksDbContext(DbContextOptions<NzWalksDbContext> options) : base(options)
    {
        
    }

    public DbSet<Difficulty> Difficulties { get; set; }
    public DbSet<Region> Regions { get; set; }
    public DbSet<Walk> Walks { get; set; }
    
}