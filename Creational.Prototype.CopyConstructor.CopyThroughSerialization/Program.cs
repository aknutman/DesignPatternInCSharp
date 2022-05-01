using System.Runtime.Serialization.Formatters.Binary;
using System.Xml.Serialization;

public static class ExtensionMethods
{
    public static T DeepCopy<T>(this T Self)
    {
        var stream = new MemoryStream();
        var formatter = new BinaryFormatter();
        formatter.Serialize(stream, Self);
        stream.Seek(0, SeekOrigin.Begin);
        object copy = formatter.Deserialize(stream);
        stream.Close();
        return (T)copy;
    }

    public static T DeepCopyXml<T>(this T Self)
    {
        using (var ms = new MemoryStream())
        {
            var s = new XmlSerializer(typeof(T));
            s.Serialize(ms, Self);
            ms.Position = 0;
            return (T)s.Deserialize(ms);
        }
    }
}

//[Serializable]
public class Person
{
    public string[] Names;
    public Address Address;

    public Person()
    {

    }

    public Person(string[] names, Address address)
    {
        names = names ?? throw new ArgumentNullException(nameof(names));
        address = address ?? throw new ArgumentNullException(nameof(address));

        this.Names = names;
        this.Address = address;
    }

    public Person(Person other)
    {
        Names = other.Names;
        Address = new Address(other.Address);
    }

    public override string ToString()
    {
        return $"{nameof(Names)}: {string.Join(" ", Names)}, {nameof(Address)}: {Address}";
    }
}

//[Serializable]
public class Address
{
    public string StreetName;
    public int HouseNumber;

    public Address()
    {

    }

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
}

static class Program
{
    static void Main(string[] args)
    {
        var john = new Person(new[] { "John", "Smith" }, new Address("London Road", 123));

        var jane = john.DeepCopyXml();
        jane.Names[0] = "Jane";
        jane.Address.HouseNumber = 111;

        Console.WriteLine(john);
        Console.WriteLine(jane);

    }
}