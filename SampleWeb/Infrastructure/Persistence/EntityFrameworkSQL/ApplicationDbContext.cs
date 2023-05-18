using Microsoft.EntityFrameworkCore;

namespace SampleWeb.Infrastructure.Persistence.EntityFrameworkSQL;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {
    }
}