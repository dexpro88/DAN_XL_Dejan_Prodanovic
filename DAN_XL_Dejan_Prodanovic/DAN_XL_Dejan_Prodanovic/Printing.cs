using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
                    //Printer1("nesto za stampac1 blablabla", threadIndex);
                    Thread.Sleep(2000);
                    //stampac_1.Set();
                }
                else
                {
                    //Console.WriteLine(threadName + "je poslao zahtev stampacu2");
                    //Printer2("nesto za stampac2 bezveze", threadIndex);
                    Thread.Sleep(2000);
                    //stampac_2.Set();

                }
            }

        }
    }
}
