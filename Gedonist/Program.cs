using Gedonist.DataAccess;
using Gedonist.Core.Interfaces;
using Gedonist.DataAccess.Repositories;
using Gedonist.DataAccess.SeedData;
using Microsoft.EntityFrameworkCore;
using Swashbuckle.AspNetCore;

//TODO ������������, ���� ���������� �����, (����� ������� �����������)
//TODO �����������, ���� ���������� ����� (����� Redis)
//TODO JWT �����������, ���� ���������� ����� (� ���� ������� ������������)
//TODO ��������� ������������, (����������������)

var builder = WebApplication.CreateBuilder(args);

var configuration = builder.Configuration;
IServiceCollection services = builder.Services;

services.AddMvc(c => c.EnableEndpointRouting = false);
services.AddSwaggerGen( options =>
{
    var basePath = AppContext.BaseDirectory;
    var xmlPath = Path.Combine(basePath, "Gedonist.xml");
    options.IncludeXmlComments(xmlPath);
});
services.AddScoped<IProductsService, ProductService>();
services.AddScoped<ICategoriesService, CategoryService>();
services.AddScoped<IProductCategoryBindsService, ProductCategoryBindService>();

services.AddDbContext<AppDataContext>(options =>
    options.UseLazyLoadingProxies()
           .UseNpgsql(configuration.GetConnectionString("DataConnection"))
);

var app = builder.Build();

//using (var scope = app.Services.CreateScope())
//{
//    var dbContext = new AppDataContext(scope.ServiceProvider.GetRequiredService<DbContextOptions<AppDataContext>>());
//    SeedData.SeedDatabase(dbContext);
//}


app.UseSwagger();
app.UseSwaggerUI(options =>
    options.SwaggerEndpoint("/swagger/v1/swagger.json","v1")
);
app.UseMvc(
    routes =>
    {
        routes.MapRoute(
            name: "Default",
            template: "/{controller=Products}/{action=GetAll}/{id?}");
    });


app.Run();
