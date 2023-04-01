using ContosoPizza.Data;
using ContosoPizza.Services;
// Additional using declarations

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

/* Add the PizzaContext 
- Реєструється PizzaContext в системі впровадження залежностей ASP.NET Core.
- Вказує, що PizzaContext використовуватиметься постачальник бази даних SQLite.
- Визначає рядок підключення SQLite, який вказує на локальний файл ContosoPizza.db.
*/
builder.Services.AddSqlite<PizzaContext>("Data Source=ContosoPizza.db");

// Add the PromotionsContext
builder.Services.AddSqlite<PromotionsContext>("Data Source=./Promotions/Promotions.db");

builder.Services.AddScoped<PizzaService>();

WebApplication app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

// Add the CreateDbIfNotExists method call
app.CreateDbIfNotExists();

app.Run();