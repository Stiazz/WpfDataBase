using System.Data.Entity;

namespace WpfDataBase.Models
{
    public class MobileContext : DbContext
    {
        public delegate void SaveHandler(string text);
        public event SaveHandler Notify;

        public MobileContext() : base("DefaultConnection")
        {

        }

        public DbSet<Phone> Phones { get; set; }

        public override int SaveChanges()
        {
            Notify?.Invoke("");
            return base.SaveChanges();
            
        }
    }
}