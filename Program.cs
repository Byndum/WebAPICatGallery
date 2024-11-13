
using MongoDB.Driver;
using Swashbuckle.AspNetCore.SwaggerUI;
using System.Collections;
using WebAPICatGallery.Repositories;

namespace WebAPICatGallery
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            //builder.WebHost.ConfigureKestrel(options => options.ListenLocalhost(8080));


            // Add services to the container.
            builder.Services.AddAuthorization();

            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            builder.Services.Configure<CatDatabase>(
                builder.Configuration.GetSection("DatabaseCatSetting"));

            builder.Services.AddSingleton<ICatRepository, CatRepository>();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            app.UseSwagger();
            app.UseSwaggerUI(c => {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
                c.RoutePrefix = ""; // or "swagger" to serve Swagger UI at the /swagger endpoint
            });
            
            /*
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI(c => {
                    c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
                    c.RoutePrefix = ""; // or "swagger" to serve Swagger UI at the /swagger endpoint
                });
            }
            */

            //app.UseHttpsRedirection();

            //app.UseAuthorization();

            app.MapGet("/", () => "Hello World!");

            //IList<Cat> cats = new List<Cat>();
            //cats.Add(new Cat {Id="0",Name="garfield", Breed="Yellow" });

            //Webservice endpoints
            app.MapGet("/cat/", async (string id, ICatRepository catRepo) =>
            {
                //return cats.Where(c => c.Id == id).First();
                return await catRepo.Get(id);

            });

            app.MapPost("/cat/", async (Cat cat, ICatRepository catRepo) => 
            {
                //cats.Add(cat);
                await catRepo.Add(cat);
            });

            app.Run();
        }
    }
}
