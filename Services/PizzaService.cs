using ContosoPizza.Models;
using ContosoPizza.Data;
using Microsoft.EntityFrameworkCore;

namespace ContosoPizza.Services;

public class PizzaService
{
    private readonly PizzaContext context;

    public PizzaService(PizzaContext context)
    {
        this.context = context;
    }

    public IEnumerable<Pizza> GetAll()
    {
        // ����� AsNoTracking ���������� ������ EF Core �������� ���������� ���.
        return context.Pizzas.AsNoTracking().ToList();
    }

    public Pizza? GetById(int id)
    {
        /*
        -����� Include ���������� ������ ������-�����, ��� �������, �� ��������� ���������� Toppings � Sauce ������
        ���� ������� � ��������� (������� ������������). ��� ����� EF Core ������� �������� null ��� ��� ������������.
        
        -����� SingleOrDefault ������� ���, ��� ������� ������-������.
        1. ���� ���� ��������� ������, null �����������.
        2. ���� ����� ������ ���������, ����������� �������.
        3. ������-����� ����� ������, ���������� ���� Id ������� id ���������.
        */
        return context.Pizzas
            .Include(p => p.Toppings)
            .Include(p => p.Sauce)
            .AsNoTracking()
            .SingleOrDefault(p => p.Id == id);
    }

    public Pizza? Create(Pizza newPizza)
    {
        context.Pizzas.Add(newPizza);
        context.SaveChanges(); // ����� SaveChanges ������ EF Core �������� ���� �ᒺ��� � ��� �����.

        return newPizza;
    }

    public void AddTopping(int pizzaId, int toppingId)
    {
        Pizza? pizzaToUpdate = context.Pizzas.Find(pizzaId);
        Topping? toppingToAdd = context.Toppings.Find(toppingId);

        if (pizzaToUpdate is null || toppingToAdd is null)
        {
            throw new InvalidOperationException("Pizza or topping does not exist");
        }

        pizzaToUpdate.Toppings ??= new List<Topping>();

        pizzaToUpdate.Toppings.Add(toppingToAdd);

        context.SaveChanges();
    }

    public void UpdateSauce(int pizzaId, int sauceId)
    {
        Pizza? pizzaToUpdate = context.Pizzas.Find(pizzaId);
        Sauce? sauceToUpdate = context.Sauces.Find(sauceId);

        if (pizzaToUpdate is null || sauceToUpdate is null)
        {
            throw new InvalidOperationException("Pizza or sauce does not exist");
        }

        pizzaToUpdate.Sauce = sauceToUpdate;

        context.SaveChanges();
    }

    public void DeleteById(int id)
    {
        Pizza? pizzaToDelete = context.Pizzas.Find(id);

        if (pizzaToDelete is not null)
        {
            context.Pizzas.Remove(pizzaToDelete);
            context.SaveChanges();
        }
    }
}