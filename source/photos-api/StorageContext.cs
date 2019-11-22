using photos_library.Models;
using Microsoft.EntityFrameworkCore;

namespace photos_api {
	public class StorageContext : DbContext {
		public StorageContext() {
		}
        public StorageContext(DbContextOptions options)
            : base(options)
        {
        }

		public DbSet<Album> Albums { get; set; }

        public DbSet<Photo> Photos { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Album>().HasKey(a => new { a.ID });
			modelBuilder.Entity<Photo>().HasKey(p => new { p.ID });
        }
	}
}
