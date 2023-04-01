using ContosoPizza.Data;
using ContosoPizza.Services;
// Additional using declarations

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

/* Add the PizzaContext 
- ���������� PizzaContext � ������ ������������ ����������� ASP.NET Core.
- �����, �� PizzaContext ��������������������� ������������ ���� ����� SQLite.
- ������� ����� ���������� SQLite, ���� ����� �� ��������� ���� ContosoPizza.db.
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