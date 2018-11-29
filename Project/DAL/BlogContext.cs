using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using Project.Models;
    
namespace Project.DAL
{
    public class BlogContext : DbContext
    {
        public BlogContext() : 
            base("BlogContext") { }

        public DbSet<Account> Account { get; set; }
        public DbSet<Article> Article { get; set; }
        public DbSet<ArticleSort> ArticleSort { get; set; }
        public DbSet<BackImage> Back { get; set; }
        public DbSet<FirendlyLink> Link { get; set; }
        public DbSet<LeavaMessage> Msg { get; set; }
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
        }
    }

}
