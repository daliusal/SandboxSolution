using Microsoft.EntityFrameworkCore;
using GameStoreAPI.Models;
using Microsoft.AspNetCore.OData;
using Microsoft.OData.ModelBuilder;
using Microsoft.OData.Edm;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

static IEdmModel GetEdmModel()
{
    ODataConventionModelBuilder builder = new();
    builder.EntitySet<Game>("Game");
    return builder.GetEdmModel();
}

builder.Services.AddControllers().AddOData(options => {
    options.AddRouteComponents("odata", GetEdmModel()).Select().Filter().OrderBy().SetMaxTop(null).Count();
}).AddNewtonsoftJson();

/*builder.Services.AddControllers().AddOData(options => {
    options.Select().Filter().OrderBy().SetMaxTop(null).Count();
}).AddNewtonsoftJson();*/

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

builder.Services.AddScoped<IGameRepo, GameRepo>();
builder.Services.AddScoped<IPublisherRepo, PublisherRepo>();
builder.Services.AddDbContext<GameStoreDBContext>(options => 
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors(policy =>
{
    policy.WithOrigins("https://localhost:7172");
    policy.WithMethods("*");
    policy.WithHeaders("*");
});
app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
