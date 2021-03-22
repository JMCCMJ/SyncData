using Microsoft.EntityFrameworkCore;

namespace SyncDataAPI
{
    public class FormsContext : DbContext
    {
        public DbSet<Form> Forms { get; set; }
        public DbSet<Field> Fields { get; set; }
        public DbSet<LogData> LogDatas { get; set; }
        public DbSet<SubApplication> SubApplications { get; set; }

        public FormsContext(): base()
        {
            //Database.EnsureCreated();

        }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            options.UseSqlite("Data Source=forms.db");
        }
            
    }

}
