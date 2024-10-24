using UI.ClientServices;
using UI.Components;
using UI.Components.Pages;
//using Bussiness;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

////Inyeccion de servicion por capas 
//builder.Services.ConfigureBussinessLayer();

builder.Services.AddTransient<ProductosServices>();
builder.Services.AddTransient<ProductosVendidosServices>();
builder.Services.AddTransient<UsuariosServices>(); 
builder.Services.AddTransient<VentasServices>();

var apiUrl = builder.Configuration["ApiUrl"];

builder.Services.AddHttpClient<ProductosServices>(
    client => client.BaseAddress = new Uri($"{apiUrl}/api/Productos/")
    );
builder.Services.AddHttpClient<ProductosVendidosServices>(
    client => client.BaseAddress = new Uri($"{apiUrl}/api/ProductosVendidos/")
    );
builder.Services.AddHttpClient<UsuariosServices>(
    client => client.BaseAddress = new Uri($"{apiUrl}/api/Usuarios/")
    );
builder.Services.AddHttpClient<VentasServices>(
    client => client.BaseAddress = new Uri($"{apiUrl}/api/Ventas/")
    );

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();
app.UseAntiforgery();

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();
