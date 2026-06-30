using Dsw2026Ej15.Api.Configurations;
using Dsw2026Ej15.Api.Middlewares;
using Dsw2026Ej15.Data;
using Dsw2026Ej15.Data.Extensions;
using Dsw2026Ej15.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;


namespace Dsw2026Ej15.Api
{
    public class Program
    {
        public static void Main(string[] args)
        {

            var builder = WebApplication.CreateBuilder(args);

            var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

            // Add services to the container.

            builder.Services.AddDbContext<Dsw2026Ej15DbContext>(options => 
            {
                options.UseSqlServer(connectionString); 
            });

            builder.Services.AddControllers();

            builder.Services.AddApliacationPersistence();
            //Agrego singleton
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            builder.Services.AddScoped<IPersistence, PersitenceEf>();

            builder.Services.AddHealthChecks();

            var app = builder.Build();

            app.UseMiddleware<ExceptionMiddleware>();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            { 
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseAuthorization();
            app.MapControllers();

            app.MapHealthChecks("/health-check");

            using var scope = app.Services.CreateScope();
            var service = scope.ServiceProvider;
            var context = service.GetRequiredService<Dsw2026Ej15DbContext>();
            context.SeedworkSpeciality(@"specialities.json");

            app.Run();
        }
    }
}
