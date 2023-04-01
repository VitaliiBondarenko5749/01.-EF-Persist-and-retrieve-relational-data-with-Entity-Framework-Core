using ContosoPizza.Models;
using Microsoft.EntityFrameworkCore;

/*
- Конструктор приймає параметр типу DbContextOptions<PizzaContext>. 
Це дозволяє зовнішньому коду передавати конфігурацію, тож його DbContext можна використовувати між тестовим і робочим кодом 
і навіть використовувати з різними постачальниками.

- Властивості DbSet<T> відповідають таблицям, які будуть створені в базі даних.

- Імена таблиць відповідатимуть DbSet<T>іменам властивостей у PizzaContext класі. За потреби цю поведінку можна змінити.

- Після створення екземпляра PizzaContext відкриються властивості Pizzas, Toppings і Sauces. Зміни, 
які ви вносите в колекції, доступні цим властивостям, будуть поширені в базу даних.
*/

namespace ContosoPizza.Data
{
    public class PizzaContext : DbContext
    {
        public PizzaContext(DbContextOptions<PizzaContext> options) : base(options) { }

        public DbSet<Pizza> Pizzas => Set<Pizza>();
        public DbSet<Topping> Toppings => Set<Topping>();
        public DbSet<Sauce> Sauces => Set<Sauce>();
    }
}