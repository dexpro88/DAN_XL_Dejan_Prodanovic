using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace DAN_XL_Dejan_Prodanovic
{
    class Printing
    {
        bool[] allComputersPrinted = new bool[10];
        object lock1 = new object();
        object lock2 = new object();

        Random rnd = new Random();

        Thread[] threads = new Thread[10];
        bool endOfProgam = false;

        public void StartSendingRequests()
        {
            for (int i = 0; i < 10; i++)
            {


                string threadName = "racunar" + (i + 1);
                int temp = i;
                Thread t = new Thread(() => SendRequest(threadName, temp));

                t.Name = threadName;
                t.Start();
                threads[i] = t;
            }
        }

        public void SendRequest(string threadName, int threadIndex)
        {
            while (!endOfProgam)
            {
                Console.WriteLine();

                int broj = rnd.Next(1, 3);

                if (broj == 1)
                {
                    //Console.WriteLine(threadName + "je poslao zahtev stampacu1");
                    Printer1("nesto za stampac1 blablabla", threadIndex);
                    Thread.Sleep(100);
                    
                }
                else
                {
                    //Console.WriteLine(threadName + "je poslao zahtev stampacu2");
                    Printer2("nesto za stampac2 bezveze", threadIndex);
                    Thread.Sleep(100);
                    

                }
            }

        }
        public void Printer1(string nesto, int threadIndex)
        {
            lock (lock1)
            {
                Console.WriteLine("Stampac1 je dobio zahtev od {0}", Thread.CurrentThread.Name);
                Console.WriteLine(nesto);
                Console.WriteLine("\n");
                allComputersPrinted[threadIndex] = true;

                Thread.Sleep(1000);
            }
        }

        public void Printer2(string nesto, int threadIndex)
        {
            lock (lock2)
            {
                Console.WriteLine("Stampac2 je dobio zahtev od {0}", Thread.CurrentThread.Name);
                Console.WriteLine(nesto);
                Console.WriteLine("\n");
                allComputersPrinted[threadIndex] = true;
                Thread.Sleep(1000);
            }
        }

        public void CheckEndOfProgram()
        {
            while (true)
            {
                bool end = true;
                foreach (var computerPrinted in allComputersPrinted)
                {
                    if (!computerPrinted)
                    {
                        end = false;
                        break;
                    }
                }
                if (end)
                {
                    endOfProgam = true;
                    break;
                }
                
                Thread.Sleep(100);
            }
            foreach (var thread in threads)
            {
                thread.Join();
            }
            Console.WriteLine("Program je zavrsio sa radom");
        }
    }
}
