using System;
using System.Text;

namespace Flyweight
{
    public class FormattedText
    {
        private readonly string plainText;
        private bool[] capitalize;
        public FormattedText(string plainText)
        {
            this.plainText = plainText;
            capitalize = new bool[plainText.Length];
        }

        public void Capitalize(int start, int end)
        {
            for (int i = start; i <= end; i++)
                capitalize[i] = true;
        }

        public override string ToString()
        {
            var sb = new StringBuilder();
            for (var i = 0; i < plainText.Length; i++)
            {
                var c = plainText[i];
                sb.Append(capitalize[i] ? char.ToUpper(c) : c);
            }
            return sb.ToString();
        }
    }
    public class Programm
    {
        static void Main(string[] args)
        {
            var ft = new FormattedText("This is a brave new world");
            ft.Capitalize(10, 15);
            Console.WriteLine(ft);
        }
    }
}