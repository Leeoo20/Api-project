using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography.X509Certificates;
using TP4_1_Models_EntityFramework;
using Microsoft.Extensions.Logging;
using System.Runtime.Intrinsics.X86;


namespace TP4_1.Models.EntityFramework
{
    public class FilmRatingDBContext : DbContext
    {
        public static readonly ILoggerFactory MyLoggerFactory = LoggerFactory.Create(builder => builder.AddConsole());

        public FilmRatingDBContext()
        {
        }

        public FilmRatingDBContext(DbContextOptions<FilmRatingDBContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Film> Films { get; set; }

        public virtual DbSet<Notation> Notations { get; set; }

        public virtual DbSet<Utilisateur> Utilisateurs { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {

            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseLoggerFactory(MyLoggerFactory)
                    .EnableSensitiveDataLogging()
                    .UseNpgsql("Server=localhost;port=5432;Database=FilmsDB; uid=postgres;password=postgres;");
            }

        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Notation>(entity =>
            {
                entity.HasKey(e => new { e.UtilisateurId, e.FilmId }).HasName("pk_notation");
              

                entity.HasOne(d => d.FilmNote)
                .WithMany(p => p.NotesFilm)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("fk_note_FilmNote");

                entity.HasOne(d => d.UtilisateurNotant)
                .WithMany(p => p.NotesUtilisateur)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("fk_note_utilisateurNotant");

            });

            modelBuilder.Entity<Utilisateur>(entity =>
            {
                entity.HasKey(e => e.UtilisateurId).HasName("pk_utilisateur");

                entity.HasAlternateKey(u => u.Mail);

            });


            modelBuilder.Entity<Film>(entity =>
            {
                entity.HasKey(e => e.FilmID).HasName("pk_film");
            });


        }





    }
}
