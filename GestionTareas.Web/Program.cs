using GestionTareas.Abstractions;
using GestionTareas.Datos;
using GestionTareas.Negocio;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

var app = builder.Build();


//configuracion de la conexion 
builder.Services.AddSingleton(new AppDbContext(builder.Configuration.GetConnectionString("conexion")));
//dependencias
builder.Services.AddScoped<ITareasRepositorio, TareasRepositorio>();
builder.Services.AddScoped<TareaServicio>();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
