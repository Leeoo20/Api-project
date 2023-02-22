using Microsoft.EntityFrameworkCore;
using TP4_1.Models.DataManager;
using TP4_1.Models.EntityFramework;
using TP4_1.Models.Repository;
using TP4_1_Models_EntityFramework;

namespace TP4_1
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

        

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            builder.Services.AddDbContext<FilmRatingDBContext>(options
            => options.UseNpgsql(builder.Configuration.GetConnectionString("FilmDbContext")));

            builder.Services.AddScoped<IDataRepository<Utilisateur>, UtilisateurManager>();
            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}