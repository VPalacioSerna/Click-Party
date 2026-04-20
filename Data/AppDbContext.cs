using ClickParty.Models;
using Microsoft.EntityFrameworkCore;

namespace ClickParty.Data;

public class AppDbContext : DbContext
{
  public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
  {
    
  }
  
  public DbSet<Event> events { get; set; }
}