
using UJ_AccountManagement.Domain.Interfaces;
using UJ_AccountManagement.Infrastructure.DBContext;
using UJ_AccountManagement.Infrastructure.Repositories;
using UJ_AccountManagement.Services;

namespace UJ_AccountManagement
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var MyAllowedOrigins = "_myAllowSpecificOrigins";

            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            builder.Services.AddScoped<DbConnectionFactory>();
            builder.Services.AddScoped<ITransactionRepository, TransactionRepository>();
            builder.Services.AddScoped<IAccountRepository, AccountRepository>();
            builder.Services.AddScoped<IPhoneNumberNormalizer, PhoneNumberNormalizer>();

            builder.Services.AddCors(options =>
            {
                options.AddPolicy("AllowAllOrigins",
                                  builder =>
                                  builder.AllowAnyOrigin()
                   .AllowAnyMethod()
                   .AllowAnyHeader());
            });

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }


            app.UseHttpsRedirection();
            app.UseCors("AllowAllOrigins");
            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
