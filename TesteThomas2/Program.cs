using Microsoft.EntityFrameworkCore;
using TesteThomas2.Data;
using TesteThomas2.Service.ClienteService;
using TesteThomas2.Service.EstoqueService;
using TesteThomas2.Services.ClienteService.Interfaces;
using TesteThomas2.Services.EstoqueService.Interfaces;
using TesteThomas2.Services.VendasService;
using TesteThomas2.Services.VendasService.Interfaces;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();


// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// EF Core
builder.Services.AddDbContext<LojaDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("developer"),
    b => b.MigrationsAssembly(typeof(LojaDbContext).Assembly.FullName)));

// Injecao de Dependencia
builder.Services.AddScoped<IProdutoService, ProdutoService>();
builder.Services.AddScoped<IVendasService, VendasService>();
builder.Services.AddScoped<IClientesService, ClientesService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment()) {
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment()) {
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
