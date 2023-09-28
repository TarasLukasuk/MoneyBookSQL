using System.Data.Entity;

namespace MoneyBookSQL
{
    internal class AppContextSQL : DbContext
    {
        public AppContextSQL() : base("DefaultConnection")
        {

        }

        public DbSet<MoneyBook> MoneyBooks { get; set; }
    }
}
