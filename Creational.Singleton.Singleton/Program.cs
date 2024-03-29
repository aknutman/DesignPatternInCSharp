﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using MoreLinq;
using Autofac;
//using NUnit.Framework;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DeisgnPatternInCSharp.Creational.Singleton.Singleton
{
    public interface IDatabase
    {
        int GetPopulation(string name);
    }

    public class SingletonDatabase : IDatabase
    {
        private Dictionary<string, int> capitals;
        private static int instanceCount; // 0
        public static int Count => instanceCount;

        private SingletonDatabase()
        {
            instanceCount++;

            Console.WriteLine("Initializing database");

            capitals = File.ReadAllLines(
                Path.Combine(
                    new FileInfo(typeof(IDatabase).Assembly.Location).DirectoryName, "capitals.txt"
                    )
                )
                .Batch(2)
                .ToDictionary(
                    list => list.ElementAt(0).Trim(),
                    list => int.Parse(list.ElementAt(1))
                );
        }

        public int GetPopulation(string name)
        {
            return capitals[name];
        }

        private static Lazy<SingletonDatabase> instance = new Lazy<SingletonDatabase>(() => new SingletonDatabase());

        public static SingletonDatabase Instance => instance.Value;
    }

    //[TestClass]
    //public class SingletonTests
    //{
    //    [TestMethod]
    //    public void IsSingletonTest()
    //    {
    //        var db = SingletonDatabase.Instance;
    //        var db2 = SingletonDatabase.Instance;
    //        Assert.AreEqual(db, db2);
    //        //Assert.That(db, Is.SameAs(db2));
    //        //Assert.That(SingletonDatabase.Count, Is.EqualTo(1));
    //    }
    //}

    static class Program
    {
        static void Main(string[] args)
        {
            var db = SingletonDatabase.Instance;
            var city = "﻿Tokyo";
            Console.WriteLine($"{city} has population {db.GetPopulation(city)}");

            Console.ReadLine();
        }
    }
}
