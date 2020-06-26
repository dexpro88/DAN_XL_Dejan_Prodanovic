using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace DAN_XL_Dejan_Prodanovic
{
    class Printing
    {
        string[] printingFormats = new string[2];
        string[] orientations = { "portrait", "landscape" };
        bool[] allComputersPrinted = new bool[10];
        object lock1 = new object();
        object lock2 = new object();
        List<string> colors;

        Random rnd = new Random();

        Thread[] threads = new Thread[10];
        bool endOfProgam = false;

        public delegate void ProgramEndedHandler(object source, EventArgs args);
        public event ProgramEndedHandler ProgramEnded;

        public Printing(List<string> colors)
        {
            
            this.colors = colors;
        }

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
            string printingFormatOfRequest;
            while (!endOfProgam)
            {
                Console.WriteLine();

                int randomNumber = rnd.Next(0, 2);
                int randomColor1 = rnd.Next(0,colors.Count);
                int randomColor2 = rnd.Next(0, colors.Count);
                int randomOrientation = rnd.Next(0, 2);
                printingFormatOfRequest = printingFormats[randomNumber];

                if (randomNumber == 0)
                {
                    A3Printer(colors[randomColor1],orientations[randomOrientation], threadIndex);
                    Thread.Sleep(100);
                }
                else
                {
                    A4Printer(colors[randomColor2], orientations[randomOrientation], threadIndex);
                    Thread.Sleep(100);
                }
            }

        }
        public void A3Printer(string color,string orientation, int threadIndex)
        {
            lock (lock1)
            {
                Console.WriteLine("{0} je poslao zahtev za štampanje dokumenta A3 formata. " +
                    "Boja: {1}. Orijentacija: {2}\n", Thread.CurrentThread.Name, color, orientation);
                //Console.WriteLine("Stampac1 je dobio zahtev od {0}", Thread.CurrentThread.Name);
               
               
                allComputersPrinted[threadIndex] = true;

                Thread.Sleep(1000);
                Console.WriteLine("Korisnik {0} može da preizme dokument A3 formata\n", Thread.CurrentThread.Name);
            }
        }

        public void A4Printer(string color,string orientation, int threadIndex)
        {
            lock (lock2)
            {
                //Console.WriteLine("Stampac2 je dobio zahtev od {0}", Thread.CurrentThread.Name);
                Console.WriteLine("{0} je poslao zahtev za štampanje dokumenta A4 formata. " +
                   "Boja: {1}. Orijentacija: {2}\n", Thread.CurrentThread.Name,color, orientation);
                
                
                allComputersPrinted[threadIndex] = true;
                Thread.Sleep(1000);
                Console.WriteLine("Korisnik {0} može da preizme dokument A4 formata\n", Thread.CurrentThread.Name);
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
            OnProgramEnded();
        }

        protected virtual void OnProgramEnded()
        {
            if (ProgramEnded != null)
                ProgramEnded(this, new EventArgs());
        }
    }
}
