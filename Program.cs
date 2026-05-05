using Microsoft.EntityFrameworkCore;
using OSKanban.Data;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlite("Data Source=kanban.db"));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseRouting();

app.UseAuthorization();

app.MapStaticAssets();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=OS}/{action=Index}/{id?}")
    .WithStaticAssets();


using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();

    if (!db.OrdemServicos.Any())
    {
        db.OrdemServicos.AddRange(
            new OSKanban.Models.OrdemServico
            {
                Unidade = "Delfino II",
                Tecnico = "Anderson",
                Status = "Em andamento",
                Data = DateTime.Now,
                Patrimonio = "87485",
                Defeito = "Não liga",
                Observacoes = "Limpeza técnica"
            },
            new OSKanban.Models.OrdemServico
            {
                Unidade = "Maracanã",
                Tecnico = "Marco Túlio",
                Status = "Finalizado",
                Data = DateTime.Now,
                Patrimonio = "87499",
                Defeito = "Sem vídeo",
                Observacoes = "Troca de peças"
            }
        );

        db.SaveChanges();
    }
}

app.Run();
