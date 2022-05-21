using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
//using jetbra

namespace Creational.Singleton.PerThreadSingleton
{
    public sealed class PerThreadSingleton
    {
        private static ThreadLocal<PerThreadSingleton> threadInstance = new ThreadLocal<PerThreadSingleton>(
            () => new PerThreadSingleton()
        );

        public int Id;

        private PerThreadSingleton()
        {
            Id = Thread.CurrentThread.ManagedThreadId;
        }

        public static PerThreadSingleton Instance => threadInstance.Value;
    }

    public class Program
    {
        public static void Main(string[] args)
        {
            var t1 = Task.Factory.StartNew(() =>
            {
                Console.WriteLine("t1: "+ PerThreadSingleton.Instance.Id);
            });

            var t2 = Task.Factory.StartNew(() =>
            {
                Console.WriteLine("t2: " + PerThreadSingleton.Instance.Id);
                Console.WriteLine("t2: " + PerThreadSingleton.Instance.Id);
            });

            Task.WaitAll(t1, t2);
        }
    }
}