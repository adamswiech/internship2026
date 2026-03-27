using System;
using db_1;
using System.Diagnostics;
using System.Threading;

internal class Program
{
    static void Main()
    {
        var data = 1000000;
        Stopwatch stopWatch1 = new Stopwatch();
        Stopwatch stopWatch2 = new Stopwatch();

        
        Console.WriteLine("Single thread");
        stopWatch1.Start();
        Insert.Execute(data, 100000);
        stopWatch1.Stop();
        TimeSpan Time1 = stopWatch1.Elapsed;
        Console.WriteLine("Single: " + Time1);



        Console.WriteLine("Multithread");
        stopWatch2.Start();

        Thread t1 = new Thread(() => Insert.Execute(data/2, 100000));
        Thread t2 = new Thread(() => Insert.Execute(data/2, 100000));

        t1.Start();
        t2.Start();

        t1.Join();
        t2.Join();

        stopWatch2.Stop();
        TimeSpan Time2 = stopWatch2.Elapsed;
        Console.WriteLine("Multi: " + Time2);


        Console.ReadKey();
    }
}