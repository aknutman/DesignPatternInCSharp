﻿using System;
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

    public class BetterFormattedText
    {
        private string plainText;
        private List<TextRange> formatting = new List<TextRange>();

        public BetterFormattedText(string plainText)
        {
            this.plainText = plainText;
        }

        public TextRange GetRange(int start, int end)
        {
            var range = new TextRange { Start = start, End = end };
            formatting.Add(range);
            return range;
        }

        public override string ToString()
        {
            var sb = new StringBuilder();
            for (var i = 0; i < plainText.Length; i++)
            {
                var c = plainText[i];
                foreach (var range in formatting)
                    if (range.Covers(i) && range.Capitalize)
                        c = char.ToUpper(c);

                sb.Append(c);
            }

            return sb.ToString();
        }

        public class TextRange
        {
            public int Start, End;
            public bool Capitalize, Bold, Italic;

            public bool Covers(int position)
            {
                return position >= Start && position <= End;
            }
        }
    }
    public class Programm
    {
        static void Main(string[] args)
        {
            string theString = "This is a brave new world";

            var ft = new FormattedText(theString);
            ft.Capitalize(10, 15);
            Console.WriteLine(ft);

            var bft = new BetterFormattedText(theString);
            bft.GetRange(10, 15).Capitalize = true;
            Console.WriteLine(bft);
        }
    }
}