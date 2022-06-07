using System;
class Program
{
    //asynchronous(đa luồn)
    static void DoSomeThing(int seconds, string msg, ConsoleColor color)
    {
        lock (Console.Out)
        {
            Console.ForegroundColor = color;
            Console.WriteLine($"{msg,10} ...Start");
            Console.ResetColor();
        }
        

        string a = "abc";
        //..
        lock (a)
        {
            //..
        }
        for (int i = 1; i <= seconds; i++)
        {
            lock (Console.Out)
            {
                Console.ForegroundColor = color;
                Console.WriteLine($"{msg,10} {i,2}");
                Console.ResetColor();
            }
            
            Thread.Sleep(1000);
        }
        lock (Console.Out)
        {
            Console.ForegroundColor = color;
            Console.WriteLine($"{msg,10} ...End");
            Console.ResetColor();
        }
        

    }
    static async Task Task2()
    {
        Task t2 = new Task(
            () => {
                DoSomeThing(10, "T2", ConsoleColor.Green);
            }
        );
        t2.Start();
        //await t2;
        //t2.Wait();
        Console.WriteLine("T2 đã hoàn thành!");
        //return t2;
    }
    static async Task Task3()
    {
        Task t3 = new Task(
            (object ob) =>
            {
                string tentacvu = (string)ob;
                DoSomeThing(4, tentacvu, ConsoleColor.Yellow);
            }
            , "T3");
        t3.Start();
        await t3;
        //t3.Wait();
        Console.WriteLine("T3 đã hoàn thành!");
        //return t3;
    }

    static async Task<string> Task4()
    {
        Task<string> t4 = new Task<string>(
            () => {
                DoSomeThing(10, "T4", ConsoleColor.Green);
                return "Return from T4";
            }
            ); //() => { return string; }
        t4.Start();
        var kq = await t4;
        Console.WriteLine("T4 đã hoàn thành!");
        return kq;
    }

    static async Task<string> Task5()
    {
        Task<string> t5 = new Task<string>(
            (object ob) =>
            {
                string t = (string)ob;
                DoSomeThing(4, t, ConsoleColor.Yellow);
                return $"Return from {t}";
            }
            , "T5"); //() => { return string; }
        t5.Start();
        string kq = await t5;
        return kq;
    }

    // async await

    static async Task Main(string[] args)
    {
        // đa luồn
        // task
        Task t2 = Task2();

        Task t3 = Task3();

        //Task<string> t4 = Task4();



        //Task<string> t5 = Task5();

        t2.Start(); //Thread riêng
        //t3.Start(); //Thread riêng
        //t4.Start();
        //t5.Start();
        DoSomeThing(6, "T1", ConsoleColor.Magenta); //Thread riêng

        //DoSomeThing(10, "T2", ConsoleColor.Green);

        //DoSomeThing(4, "T3", ConsoleColor.Yellow);

        //t2.Wait();
        //t3.Wait();
        Task.WaitAll(t2, t3);
        //Task.WaitAll(t4, t5);
        //await t2;
        await t3;
        //var kq4 =  await t4;
        //var kq5 = await t5;
        //var kq4 = t4.Result;
        //var kq5 = t5.Result;
        //Console.WriteLine(kq4);
        //Console.WriteLine(kq5);
        Console.WriteLine("Hello World!");
        Console.ReadKey();
    }

    
}