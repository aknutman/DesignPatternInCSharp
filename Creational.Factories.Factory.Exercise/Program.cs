using System;

namespace Coding.Exercise
{
    public class Person
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }

    public class PersonFactory
    {
        private int id = 0;
        public Person CreatePerson(string name)
        {
            return new Person() { Id = id++, Name = name };
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            var pf = new PersonFactory();

            var p1 = pf.CreatePerson("Chris");

            Console.WriteLine($"{p1.Id}. {p1.Name}");
            
            var p2 = pf.CreatePerson("John");

            Console.WriteLine($"{p2.Id}. {p2.Name}");
        }
    }
}
