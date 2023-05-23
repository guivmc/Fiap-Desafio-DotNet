using FiapTestAPI.Database;
using FiapTestAPI.Database.Providers;
using FiapTestAPI.Database.Providers.Interfaces;
using FiapTestAPI.Database.Repositories;
using FiapTestAPI.Database.Repositories.Interfaces;
using Microsoft.Extensions.Configuration;


namespace FiapTestAPI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            ConfigurationManager configuration = builder.Configuration;

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            builder.Services.AddSingleton(new DatabaseConfig { ConnectionConfig = configuration["DatabaseName"]! });

            builder.Services.AddSingleton<IDatabaseBootStrap, DatabaseBootStrap>();
            builder.Services.AddSingleton<IAlunoProvider, AlunoProvider>();
            builder.Services.AddSingleton<IAlunoRepository, AlunoRepository>();
            builder.Services.AddSingleton<ITurmaProvider, TurmaProvider>();
            builder.Services.AddSingleton<ITurmaRepository, TurmaRepository>();
            builder.Services.AddSingleton<IRelacaoProvider, RelacaoProvider>();
            builder.Services.AddSingleton<IRelacaoRepository, RelacaoRepository>();

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

            app.Services.GetService<IDatabaseBootStrap>()!.SetUp();

            app.Run();
        }
    }
}