using Test2.Models;

namespace Test2.Infrastructure;

using Microsoft.EntityFrameworkCore;

public partial class BookContext : DbContext
{
    private readonly string? _connectionString;
    
    public BookContext() { }

    public BookContext(IConfiguration configuration, DbContextOptions<BookContext> options)
        : base(options)
    {
        _connectionString = configuration.GetConnectionString("DefaultConnection") ??
                            throw new ArgumentNullException(nameof(configuration), "Connection string is not set");
    }

    public virtual DbSet<Book> Books { get; set; }

    public virtual DbSet<PublishingHouse> PublishingHouses { get; set; }

    public virtual DbSet<BookAuthor> BookAuthors { get; set; }

    public virtual DbSet<Genre> Genres { get; set; }
    
    public virtual DbSet<BookGenre> BookGenres { get; set; }
    
    public virtual DbSet<Author> Authors { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer(_connectionString);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Book>(entity =>
        {
            entity.HasKey(e => e.IdBook);
            entity.HasOne(e => e.PublishingHouse)
                .WithMany(ph => ph.Books)
                .HasForeignKey(e => e.IdPublishingHouse)
                .OnDelete(DeleteBehavior.Cascade);

            entity.ToTable("Book");
        });

        modelBuilder.Entity<BookAuthor>(entity =>
        {
            entity.HasKey(e => new { e.IdBook, e.IdAuthor });

            entity.HasOne(ba => ba.Book)
                .WithMany(b => b.BookAuthors)
                .HasForeignKey(ba => ba.IdBook);

            entity.HasOne(ba => ba.Author)
                .WithMany(a => a.BookAuthors)
                .HasForeignKey(ba => ba.IdAuthor);

            entity.ToTable("BookAuthor");
        });

        modelBuilder.Entity<BookGenre>(entity =>
        {
            entity.HasKey(bg => new { bg.IdBook, bg.IdGenre });

            entity.HasOne(bg => bg.Book)
                .WithMany(b => b.BookGenres)
                .HasForeignKey(bg => bg.IdBook);

            entity.HasOne(bg => bg.Genre)
                .WithMany(g => g.BookGenres)
                .HasForeignKey(bg => bg.IdGenre);

            entity.ToTable("BookGenre");
        });

        modelBuilder.Entity<PublishingHouse>(entity =>
        {
            entity.HasKey(e => e.IdPublishingHouse);
            entity.Property(e => e.Name).HasMaxLength(120);
            entity.Property(e => e.Country).HasMaxLength(120);
            entity.Property(e => e.City).HasMaxLength(120);
            entity.ToTable("PublishingHouse");
        });

        modelBuilder.Entity<Author>(entity =>
        {
            entity.HasKey(e => e.IdAuthor);
            entity.Property(e => e.FirstName).HasMaxLength(120);
            entity.Property(e => e.LastName).HasMaxLength(120);
            entity.ToTable("Author");
        });

        modelBuilder.Entity<Genre>(entity =>
        {
            entity.HasKey(e => e.IdGenre);
            entity.Property(e => e.Name).HasMaxLength(120);
            entity.ToTable("Genre");
        });
        
        modelBuilder.Entity<PublishingHouse>().HasData(
            new PublishingHouse { IdPublishingHouse = 1, Name = "Sanya Press", Country = "UK", City = "London" }
        );
        modelBuilder.Entity<Genre>().HasData(
            new Genre { IdGenre = 1, Name = "My fav genre" }
        );
        modelBuilder.Entity<Author>().HasData(
            new Author { IdAuthor = 1, FirstName = "Sanya", LastName = "Milko" }
        );
        modelBuilder.Entity<Book>().HasData(
            new Book { IdBook = 1, Name = "My book", ReleaseDate = new DateTime(2020, 5, 1), IdPublishingHouse = 1 }
        );
        modelBuilder.Entity<BookAuthor>().HasData(
            new BookAuthor { IdBook = 1, IdAuthor = 1 }
        );
        modelBuilder.Entity<BookGenre>().HasData(
            new BookGenre { IdBook = 1, IdGenre = 1 }
        );
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}