using Microsoft.EntityFrameworkCore;
using OSKanban.Models;

namespace OSKanban.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }

        public DbSet<OrdemServico> OrdemServicos { get; set; }
    }
}