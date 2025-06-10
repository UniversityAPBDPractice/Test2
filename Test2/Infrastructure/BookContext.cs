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
        modelBuilder.Entity<PublishingHouse>(entity =>
        {
            entity.HasKey(e => e.IdPublishingHouse).HasName("PublishingHouse_pk");
            entity.HasMany(e => e.Books);
            entity.ToTable("PublishingHouse");

            entity.Property(e => e.Name).HasMaxLength(120);
            entity.Property(e => e.Country).HasMaxLength(120);
            entity.Property(e => e.City).HasMaxLength(120);
        });

        modelBuilder.Entity<Book>(entity =>
        {
            entity.HasKey(e => new { e.IdBook, e.IdPublishingHouse }).HasName("Book_pk");
            entity.HasMany(e => e.BookAuthors);
            entity.HasMany(e => e.BookGenres);
            entity.ToTable("Book");

            entity.Property(e => e.ReleaseDate).HasColumnType("datetime");

            /*entity.HasOne(d => d.IdPublishingHouse).WithMany(p => p.ClientTrips)
                .HasForeignKey(d => d.IdPublishingHouse)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("Table_5_Client");*/
        });

        modelBuilder.Entity<BookAuthor>(entity =>
        {
            entity.HasKey(e => e.IdBook).HasName("Country_pk");
            entity.HasKey(e => e.IdAuthor).HasName("Author_pk");

            entity.ToTable("BookAuthor");

            /*entity.HasMany(d => d.IdTrips).WithMany(p => p.IdCountries)
                .UsingEntity<Dictionary<string, object>>(
                    "CountryTrip",
                    r => r.HasOne<Trip>().WithMany()
                        .HasForeignKey("IdTrip")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("Country_Trip_Trip"),
                    l => l.HasOne<Country>().WithMany()
                        .HasForeignKey("IdCountry")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("Country_Trip_Country"),
                    j =>
                    {
                        j.HasKey("IdCountry", "IdTrip").HasName("Country_Trip_pk");
                        j.ToTable("Country_Trip");
                    });*/
        });

        modelBuilder.Entity<Genre>(entity =>
        {
            entity.HasKey(e => e.IdGenre).HasName("Genre_pk");
            entity.HasMany(e => e.BooksGenres);
            entity.Property(e => e.Name).HasMaxLength(120);
            entity.ToTable("Genre");
        });

        modelBuilder.Entity<BookGenre>(entity =>
        {
            entity.HasKey(e => e.IdBook).HasName("BookGenre_pk");
            entity.HasKey(e => e.IdGenre).HasName("BookGenre_pk");
        });

        modelBuilder.Entity<Author>(entity =>
        {
            entity.HasKey(e => e.IdAuthor).HasName("Author_pk");
            entity.HasMany(e => e.BookAuthors);
            entity.Property(e => e.FirstName).HasMaxLength(120);
            entity.Property(e => e.LastName).HasMaxLength(120);
            entity.ToTable("Author");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}