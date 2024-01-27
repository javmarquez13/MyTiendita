using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using MyTiendita.Data.Repositories;
using MyTiendita.Data.RepositoryImplementation;
using MyTiendita.Persistence.Database;
using MyTiendita.Services.BLL;
using System.Text.Json.Serialization;

try
{
    var builder = WebApplication.CreateBuilder(args);

    // Add services to the container.

    builder.Services.AddControllers()
        .AddJsonOptions(options => options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);
    // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
    builder.Services.AddEndpointsApiExplorer();


    builder.Services.AddSwaggerGen(c =>
    {
        c.SwaggerDoc("v1", new OpenApiInfo
        {
            Title = "MyTiendita API",
            Version = "0.0.0.1",
        });
    });

    builder.Services.AddDbContext<ApplicationDbContext>(options =>
    {
        options.UseSqlServer(builder.Configuration.GetConnectionString("MyTienditaConn"));
    });



    //Dependency Injections
    builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
    builder.Services.AddScoped<IProductRepository, ProductRepository>();
    builder.Services.AddScoped<IStockRepository, StockRepository>();
    builder.Services.AddScoped<ProductBLL>();


    var app = builder.Build();

    using (var scope = app.Services.CreateScope())
    {
        var dc = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
        dc.Database.Migrate();
    }

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
catch (Exception ex)
{
    throw;
}
finally
{

}

