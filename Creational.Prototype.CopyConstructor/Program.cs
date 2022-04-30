public interface IPrototype<T>
{
    T DeepCopy();
}

public class Person : IPrototype<Person>
{
    public string[] Names;
    public Address Address;
    public Person(string[] names, Address address)
    {
        names = names ?? throw new ArgumentNullException(nameof(names));
        address = address ?? throw new ArgumentNullException(nameof(address));

        this.Names = names;
        this.Address = address;
    }

    // Copy Constructor
    public Person(Person other)
    {
        Names = other.Names;
        Address = new Address(other.Address);
    }

    public override string ToString()
    {
        return $"{nameof(Names)}: {string.Join(" ", Names)}, {nameof(Address)}: {Address}";
    }

    // Or, implementing an interface prototype of object T. A bit tedious
    public Person DeepCopy()
    {
        return new Person(Names, Address.DeepCopy());
    }
}

public class Address : IPrototype<Address>
{
    public string StreetName;
    public int HouseNumber;

    public Address(string streetName, int houseNumber)
    {
        streetName = streetName ?? throw new ArgumentNullException(nameof(streetName));

        if (streetName == null)
        {
            throw new ArgumentNullException(paramName: nameof(streetName));
        }
        StreetName = streetName;
        HouseNumber = houseNumber;

    }

    public Address(Address other)
    {
        other = other ?? throw new ArgumentNullException(nameof(other));
        StreetName = other.StreetName;
        HouseNumber = other.HouseNumber;

    }

    public override string ToString()
    {
        return $"{nameof(StreetName)}: {StreetName}, {nameof(HouseNumber)}: {HouseNumber}";
    }

    public Address DeepCopy()
    {
        return new Address(StreetName, HouseNumber);
    }
}

static class Program
{
    static void Main(string[] args)
    {
        var john = new Person(new[] { "John", "Smith" }, new Address("London Road", 123));
        
        var jane = john.DeepCopy();
        jane.Address.HouseNumber = 111;

        Console.WriteLine(john);
        Console.WriteLine(jane);
        
    }
}