using static System.Console;

public interface IHotDrink
{
    void Consume();
}

internal class Tea : IHotDrink
{
    public void Consume()
    {
        WriteLine("This tea is nice but I'd prefer it with milk.");
    }
}

internal class Coffee : IHotDrink
{
    public void Consume()
    {
        WriteLine("This coffee is sensational!");
    }
}

public interface IHotDrinkFactory
{
    IHotDrink Prepare(int amount);
}

internal class TeaFactory : IHotDrinkFactory
{
    public IHotDrink Prepare(int amount)
    {
        WriteLine($"Put in a tea bag, boil water, pour {amount} ml, add lemon, enjoy!");
        return new Tea();
    }
}

internal class CoffeeFactory : IHotDrinkFactory
{
    public IHotDrink Prepare(int amount)
    {
        WriteLine($"Grind some beans, pour water, pour {amount} ml, add cream and sugar, enjoy");
        return new Coffee();
    }
}

public class HotDringMachine
{
    public enum AvailableDrink
    {
        Coffee, Tea
    }

    private Dictionary<AvailableDrink, IHotDrinkFactory> factories = new Dictionary<AvailableDrink, IHotDrinkFactory>();

    public HotDringMachine()
    {
        foreach (AvailableDrink drink in Enum.GetValues(typeof(AvailableDrink)))
        {
            Console.WriteLine("===");
            Console.WriteLine($"typeof(AvailableDrink): {typeof(AvailableDrink)}");
            Console.WriteLine($"Enum.GetName(typeof(AvailableDrink), drink): {Enum.GetName(typeof(AvailableDrink), drink)}");

            Console.WriteLine(Type.GetType(Enum.GetName(typeof(AvailableDrink), drink)));
            Console.WriteLine("===");

            var factory = (IHotDrinkFactory)Activator.CreateInstance(Type.GetType(Enum.GetName(typeof(AvailableDrink), drink) + "Factory"));
            factories.Add(drink, factory);
        }
    }

    public IHotDrink MakeDrink(AvailableDrink drink, int amount)
    {
        return factories[drink].Prepare(amount);
    }

}

class Program
{
    static void Main(string[] args)
    {
        var machine = new HotDringMachine();
        var drink = machine.MakeDrink(HotDringMachine.AvailableDrink.Tea, 100);
    }
}