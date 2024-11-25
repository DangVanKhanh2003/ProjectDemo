using Microsoft.EntityFrameworkCore;
namespace ProjectDemo.Entites
{
    public class MyDBContext: DbContext
    {
        public DbSet<Products> Products { get; set; }

        public DbSet<Categories> Categories { get; set; }
        public MyDBContext(DbContextOptions<MyDBContext> options) : base(options)
        {
        }

        public MyDBContext() { }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseSqlServer("Server=INT-KhanhDV\\SQLEXPRESS;Database=ProjectDemo;Integrated Security=True;Trust Server Certificate=True");

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Categories>(entity =>
            {
                entity.ToTable("Category");
                entity.HasKey(e => e.CategoryId);

            });

            modelBuilder.Entity<Products>(entity =>
            {
                entity.ToTable("Products");
                entity.HasKey(p => p.ProductId);
                entity.Property(e => e.ProductId)
                      .UseIdentityColumn(1, 1);
                entity.Property(p => p.Price)
                      .HasColumnType("money");
                entity.HasOne(p => p.Categories)
                      .WithMany(c => c.Products)
                      .HasForeignKey(p => p.CategoryId)
                      .OnDelete(DeleteBehavior.Cascade); // Optional: Define delete behavior
                ;
            });
        }
    }
}
