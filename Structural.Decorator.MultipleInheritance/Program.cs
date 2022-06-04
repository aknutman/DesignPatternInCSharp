namespace Structural.Decorator.MultipleInheritance
{
    public interface IBird
    {
        public void Fly();
    }

    public class Bird : IBird
    {
        public int Weight { get; set; }
        public void Fly()
        {
            Console.WriteLine($"Soaring in the sky with wieght {Weight}");
        }
    }

    public interface ILizard
    {
        public void Crawl();
    }

    public class Lizard : ILizard
    {
        public int Weight { get; set; }
        public void Crawl()
        {
            Console.WriteLine($"Crawling in the dirt with weight {Weight}");
        }
    }

    public class Dragon : IBird, ILizard
    {
        private Bird bird = new Bird();
        private Lizard lizard = new Lizard();
        private int weight;

        public void Crawl()
        {
            lizard.Crawl();
        }

        public void Fly()
        {
            bird.Fly();
        }

        public int Weight
        {
            get
            {
                return weight;
            }
            set
            {
                weight = value;
                bird.Weight = value;
                lizard.Weight = value;
            }
        }
    }

    static class Program
    {
        static void Main(string[] args)
        {
            Dragon dragon = new Dragon();
            dragon.Weight = 123;
            dragon.Crawl();
            dragon.Fly();
        }
    }
}