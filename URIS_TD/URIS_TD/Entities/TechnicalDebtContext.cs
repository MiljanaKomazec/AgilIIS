using Microsoft.EntityFrameworkCore;
using URIS_TD.Models;

namespace URIS_TD.Entities
{
    public class TechnicalDebtContext : DbContext
    {
        public TechnicalDebtContext(DbContextOptions<TechnicalDebtContext> options) : base(options)
        {
        }

        public DbSet<TechnicalDebt> Debts { get; set; }
        public DbSet<TypeOfTechnicalDebt> Type {  get; set; }
        
        
    }
}
