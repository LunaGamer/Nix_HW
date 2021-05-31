using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Nix_hw5_Philosophers
{
    class Program
    {
        public static void Eat1(int i, Mutex fork1, Mutex fork2) //Базовая реализация задачи обедающих философов, как на картинке задания
        {
            Random rnd = new Random();
            var sleep = rnd.Next(1000, 10000);
            Console.WriteLine($"Philosopher {i} is thinking");
            Thread.Sleep(sleep);
            fork2.WaitOne();
            Console.WriteLine($"Philosopher {i} took left fork - fork {(i < 5 ? i + 1 : 1)}");
            Console.WriteLine($"Philosopher {i} is waiting");
            Thread.Sleep(1000);
            fork1.WaitOne();
            Console.WriteLine($"Philosopher {i} took right fork - fork {i}");
            Console.WriteLine($"Philosopher {i} is eating");
            Thread.Sleep(5000);
            Console.WriteLine($"Philosopher {i} finished eating");
            fork1.ReleaseMutex();
            Console.WriteLine($"Philosopher {i} put down right fork - fork {i}");
            fork2.ReleaseMutex();
            Console.WriteLine($"Philosopher {i} put down left fork - fork {(i < 5 ? i + 1 : 1)}");
        }

        public static void Eat2(int i, Mutex fork1, Mutex fork2) //1-ый способ - одновременный захват всех нужных ресурсов до выполнения других операций
        {
            Random rnd = new Random();                          //В данном случае объект захватывает оба ресурса до совершения каких-либо действий над ними.
            var sleep = rnd.Next(1000, 10000);                  //Такой способ может приводить к излишнему простаиванию ресурсов,
            Console.WriteLine($"Philosopher {i} is thinking");  //когда в данный момент нужен только 1 ресурс, а захвачены уже оба,
            Thread.Sleep(sleep);                                //но он вполне нормально справляется с задачей ухода от возникновения локов. Однако такой способ 
            fork2.WaitOne();                                    //не всегда возможно реализовать, может возникнуть ситуация когда для получения 
            Console.WriteLine($"Philosopher {i} took right " +  //ресурса 2 нужно выполнить какие-то действия над ресурсом 1, либо сам процесс захвата 1го
                $"fork - fork {i}");                            //ресурса занимает много времени, тогда 2ой ресурс может быть захвачен другим процессом
            fork1.WaitOne();                                    //и все равно возникнет лок
            Console.WriteLine($"Philosopher {i} took left " +
                $"fork - fork {(i < 5 ? i + 1 : 1)}");
            Console.WriteLine($"Philosopher {i} is eating");
            Thread.Sleep(5000); 
            fork1.ReleaseMutex();
            Console.WriteLine($"Philosopher {i} put down right fork - fork {i}");
            fork2.ReleaseMutex();
            Console.WriteLine($"Philosopher {i} put down left fork - fork {(i < 5 ? i + 1 : 1)}");
        }

        public static void Eat3(int i, Mutex fork1, Mutex fork2) //2-ой способ - возвращение 1го ресурса при неудавшейся попытке захватить 2ой
        {
            Random rnd = new Random();
            int sleep;
            bool free;
            do
            {
                sleep = rnd.Next(1000, 10000);
                Console.WriteLine($"Philosopher {i} is thinking");
                Thread.Sleep(sleep);
                fork2.WaitOne();
                Console.WriteLine($"Philosopher {i} took left fork - fork {(i < 5 ? i + 1 : 1)}");
                Console.WriteLine($"Philosopher {i} is waiting");
                Thread.Sleep(1000);
                free = fork1.WaitOne(3000); //на протяжении какого-то времени пытаемся захватить 2ой ресурс
                if (free) //Если ресурс удалось захватить, то работа продолжается как в базовой реализации
                {
                    Console.WriteLine($"Philosopher {i} took right fork - fork {i}");
                    Console.WriteLine($"Philosopher {i} is eating");
                    Thread.Sleep(5000);
                    Console.WriteLine($"Philosopher {i} finished eating");
                    fork1.ReleaseMutex();
                    Console.WriteLine($"Philosopher {i} put down right fork - fork {i}");
                    fork2.ReleaseMutex();
                    Console.WriteLine($"Philosopher {i} put down left fork - fork {(i < 5 ? i + 1 : 1)}");
                }
                else //Если мы не дождались захвата 2го ресурса, то возвращаем занятый ранее 1ый ресурс
                {
                    Console.WriteLine($"Philosopher {i} couldnt take both forks at the same time, returning to thinking");
                    fork2.ReleaseMutex();
                    Console.WriteLine($"Philosopher {i} put down left fork - fork {(i < 5 ? i + 1 : 1)}");
                }
            } while (!free);
            //Из-за постоянного возврата ресурсов такой способ может занимать много времени, также может возникнуть ситуация,
            //когда перед захватом 2го ресурса нам нужно как-то подготовить и изменить 1ый ресурс, тогда перед его возвращением необходимо
            //будет привести 1ый ресурс в изначальное состояние, что опять будет отнимать какое-то время
        }

        public static void Waiter (Thread [] ps) //3-ий способ - введение арбитража потоков, в данном случае официанта 
        {
            bool[] k = new bool[5];

            do
            {
                for (int i = 0; i < 5; i++)
                {
                    if (!k[i] && !ps[(i > 0 ? i - 1 : 4)].IsAlive && !ps[(i < 4 ? i + 1 : 0)].IsAlive) 
                    {
                        k[i] = true;
                        ps[i].Start();
                    }
                }
            } while (!k[0] || !k[1] || !k[2] || !k[3] || !k[4]);
            foreach (Thread p in ps)
            {
                p.Join();
            }
            //официант выдает разрешение на ужин, и, соответственно, вилки только тем философам
            //у которых ни один из соседей не ужинает в данный момент
        }

        public static void Task1() //запуск 1го способа
        {
            Mutex fork1 = new Mutex();
            Mutex fork2 = new Mutex();
            Mutex fork3 = new Mutex();
            Mutex fork4 = new Mutex();
            Mutex fork5 = new Mutex();

            Thread p1 = new Thread(() => Eat2(1, fork1, fork2));
            Thread p2 = new Thread(() => Eat2(2, fork2, fork3));
            Thread p3 = new Thread(() => Eat2(3, fork3, fork4));
            Thread p4 = new Thread(() => Eat2(4, fork4, fork5));
            Thread p5 = new Thread(() => Eat2(5, fork5, fork1));
            Thread[] ps = new Thread[5] { p1, p2, p3, p4, p5 };

            foreach (Thread p in ps)
            {
                p.Start();
            }
            foreach (Thread p in ps)
            {
                p.Join();
            }

            Console.WriteLine("Dinner 1 is finished!");
        }

        public static void Task2() //запуск 2го способа
        {
            Mutex fork1 = new Mutex();
            Mutex fork2 = new Mutex();
            Mutex fork3 = new Mutex();
            Mutex fork4 = new Mutex();
            Mutex fork5 = new Mutex();

            Thread p1 = new Thread(() => Eat3(1, fork1, fork2));
            Thread p2 = new Thread(() => Eat3(2, fork2, fork3));
            Thread p3 = new Thread(() => Eat3(3, fork3, fork4));
            Thread p4 = new Thread(() => Eat3(4, fork4, fork5));
            Thread p5 = new Thread(() => Eat3(5, fork5, fork1));
            Thread[] ps = new Thread[5] { p1, p2, p3, p4, p5 };

            foreach (Thread p in ps)
            {
                p.Start();
            }
            foreach (Thread p in ps)
            {
                p.Join();
            }

            Console.WriteLine("Dinner 2 is finished!");
        }

        public static void Task3() //запуск 3го способа
        {
            Mutex fork1 = new Mutex();
            Mutex fork2 = new Mutex();
            Mutex fork3 = new Mutex();
            Mutex fork4 = new Mutex();
            Mutex fork5 = new Mutex();

            Thread p1 = new Thread(() => Eat1(1, fork1, fork2));
            Thread p2 = new Thread(() => Eat1(2, fork2, fork3));
            Thread p3 = new Thread(() => Eat1(3, fork3, fork4));
            Thread p4 = new Thread(() => Eat1(4, fork4, fork5));
            Thread p5 = new Thread(() => Eat1(5, fork5, fork1));
            Thread[] ps = new Thread[5] { p1, p2, p3, p4, p5 };

            Waiter(ps);

            Console.WriteLine("Dinner 3 is finished!");
        }

        static void Main(string[] args)
        {
            Task1(); //запуск всех трех способов
            Task2();
            Task3();
            Console.WriteLine("All Dinners are finished!");
            Console.ReadKey();
        }
    }
}
