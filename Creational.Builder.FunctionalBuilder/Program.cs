
public class Person
{
    public string Name, Position;
}

public abstract class FunctionalBuilder<TSubject, TSelf>
    where TSelf : FunctionalBuilder<TSubject, TSelf>
    where TSubject : new()
{
    private readonly List<Func<Person, Person>> actions = new List<Func<Person, Person>>();

    public TSelf Called(string name)
        => Do(p => p.Name = name);

    public TSelf Do(Action<Person> action)
        => AddAction(action);

    private TSelf AddAction(Action<Person> action)
    {
        actions.Add(p => { action(p); return p; });

        return (TSelf)this;
    }

    public Person Build() => actions.Aggregate(new Person(), (p, f) => f(p));
}

public sealed class PersonBuilder : FunctionalBuilder<Person, PersonBuilder>
{
    public PersonBuilder Called(string name) 
        => Do(p => p.Name = name);
}

//public sealed class PersonBuilder
//{
//    private readonly List<Func<Person, Person>> actions = new List<Func<Person, Person>>();

//    public PersonBuilder Called(string name)
//        => Do(p => p.Name = name);

//    public PersonBuilder Do(Action<Person> action)
//        => AddAction(action);

//    private PersonBuilder AddAction(Action<Person> action)
//    {
//        actions.Add(p => { action(p); return p; });

//        return this;
//    }

//    public Person Build() => actions.Aggregate(new Person(), (p, f) => f(p));
//}

public static class PersonBuilderExtensions
{
    //public static PersonBuilder WorksAs(this PersonBuilder builder, string position)
    //    => builder.Do(p => p.Position = position);
    public static PersonBuilder WorksAs(this PersonBuilder builder, string position)
    {
        builder.Do(p => p.Position = position);

        return builder;
    }
}

static class Program
{
    public static void Main(string[] args)
    {
        var person = new PersonBuilder()
                .Called("Sarah")
                .WorksAs("Developer")
                .Build();

        Console.WriteLine($"This person name is {person.Name} and work as {person.Position}.");
    }
}




//public class Person
//{
//    public string Name, Position;
//}

//public sealed class PersonBuilder
//{
//    public readonly List<Action<Person>> Actions = new List<Action<Person>>();

//    public PersonBuilder Called(string name)
//    {
//        Actions.Add(p => { p.Name = name; });
//        return this;
//    }

//    public Person Build()
//    {
//        var p = new Person();
//        Actions.ForEach(a => a(p));
//        return p;
//    }
//}

//public static class PersonBuilderExtensions
//{
//    public static PersonBuilder WorksAsA
//      (this PersonBuilder builder, string position)
//    {
//        builder.Actions.Add(p =>
//        {
//            p.Position = position;
//        });
//        return builder;
//    }
//}

//public class FunctionalBuilder
//{
//    public static void Main(string[] args)
//    {
//        var pb = new PersonBuilder();
//        var person = pb.Called("Dmitri").WorksAsA("Programmer").Build();

//    }
//}