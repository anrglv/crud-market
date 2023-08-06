using Microsoft.EntityFrameworkCore;

namespace CRUDMarket
{
    public class MarketContext : DbContext
    {
        public MarketContext(DbContextOptions<MarketContext> options) : base(options)
        {
        }

        public DbSet<SevenEleven> Market { get; set; }
    }
}