using Microsoft.EntityFrameworkCore;
using ProyectoTP.BusinessLayer.Interfaces;
using ProyectoTP.BusinessLayer.Service;
using ProyectoTP.DataLayer.Data;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddControllers();
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("ProyectoTPConnection"))
);
builder.Services.AddScoped<IClientService, ClientService>();

builder.Services.AddControllersWithViews();

var app = builder.Build();

/*using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
    context.Database.Migrate();
}*/

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
}
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapRazorPages();
app.MapControllers();

app.Run();
