using Microsoft.EntityFrameworkCore;

namespace ApiODATACore.Models
{
    public partial class TestContext : DbContext
    {
        public TestContext(DbContextOptions<TestContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Comments> Comments { get; set; }
        public virtual DbSet<People> People { get; set; }
        

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Comments>(entity =>
            {
                entity.HasOne(d => d.IdPersonNavigation)
                    .WithMany(p => p.Comments)
                    .HasForeignKey(d => d.IdPerson);
            });
        }
    }
}
