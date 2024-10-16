using FilmsAPI.Data;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace FilmsAPI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            //Initiation of the connection with the database, remembering that our connection string is allocated in AppSettings.json
            var connectionString = builder.Configuration.GetConnectionString("FilmConnection");

            builder.Services.AddDbContext<FilmContext>(opts => 
                opts.UseMySql(connectionString, 
                ServerVersion.AutoDetect(connectionString)));

            builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

            // Add services to the container.

            builder.Services.AddControllers().AddNewtonsoftJson();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo 
                    { Title = "FilmAPI", Version = "v1" });
                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                c.IncludeXmlComments(xmlPath);
            });

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
