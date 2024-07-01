using TecSystem.Service;
using TecSystem.Service.Interface;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddSingleton<IListaService, ListaService>();
builder.Services.AddSingleton<ITarefaService, TarefaService>();
builder.Services.AddControllersWithViews();

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
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
