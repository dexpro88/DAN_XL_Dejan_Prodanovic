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
        //array that stores print formats
        string[] printingFormats = new string[2];
        //array that stores types of orientation
        string[] orientations = { "portrait", "landscape" };
        bool[] allComputersPrinted = new bool[10];
        object lock1 = new object();
        object lock2 = new object();
        List<string> colors;

        Random rnd = new Random();

        List<Thread> threads = new List<Thread>();
        bool endOfProgam = false;

        public delegate void ProgramEndedHandler(object source, EventArgs args);
        //event that will be called when program ends
        public event ProgramEndedHandler ProgramEnded;

        public Printing()
        {
            //we read colors from file Paleta.txt
            this.colors = Color.ReadColorsToFile();
        }

        /// <summary>
        /// method that creates  and starts 10 threads that represent 
        /// every thread gets a name based on its index
        /// threads are stored in treads list
        /// </summary>
        public void StartSendingRequests()
        {
            for (int i = 0; i < 10; i++)
            {
                string threadName = "racunar" + (i + 1);
                int temp = i;
                Thread t = new Thread(() => SendRequest(threadName, temp));

                t.Name = threadName;
                t.Start();
                threads.Add(t);
            }
        }
        /// <summary>
        /// Method that represents sending of request to a printer.
        /// Every thred (computer) that runs this method is calling mehtods A3Printer or A4Printer based on randomly 
        /// generated number. It randomlu chooses color and orientation based on randomly generated number.
        /// </summary>
        /// <param name="threadName"></param>
        /// <param name="threadIndex"></param>
        public void SendRequest(string threadName, int threadIndex)
        {
            string printingFormatOfRequest;
            while (!endOfProgam)
            {
                Console.WriteLine();

                int randomNumber = rnd.Next(0, 2);
                int randomColor1 = rnd.Next(0, colors.Count);
                int randomColor2 = rnd.Next(0, colors.Count);
                int randomOrientation = rnd.Next(0, 2);
                printingFormatOfRequest = printingFormats[randomNumber];

                if (randomNumber == 0)
                {
                    A3Printer(colors[randomColor1], orientations[randomOrientation], threadIndex);
                    Thread.Sleep(100);
                }
                else
                {
                    A4Printer(colors[randomColor2], orientations[randomOrientation], threadIndex);
                    Thread.Sleep(100);
                }
            }

        }

        /// <summary>
        /// method that represents A3 Printer it prints a message about a request and than it sleeps for 1000 ms 
        /// to simulate printing. After that it prints message that the document is ready
        /// </summary>
        /// <param name="color"></param>
        /// <param name="orientation"></param>
        /// <param name="threadIndex"></param>
        public void A3Printer(string color, string orientation, int threadIndex)
        {
            lock (lock1)
            {
                Console.WriteLine("{0} je poslao zahtev za štampanje dokumenta A3 formata. " +
                    "Boja: {1}. Orijentacija: {2}\n", Thread.CurrentThread.Name, color, orientation);
                
                //we set value in an array on true so that method that checks if all computers printed 
                //at least one document will know when to end the program
                allComputersPrinted[threadIndex] = true;

                Thread.Sleep(1000);
                Console.WriteLine("Korisnik {0} može da preizme dokument A3 formata\n", Thread.CurrentThread.Name);
            }
        }

        /// <summary>
        /// method that represents A4 Printer it prints a message about a request and than it sleeps for 1000 ms 
        /// to simulate printing. After that it prints message that the document is ready
        /// </summary>
        /// <param name="color"></param>
        /// <param name="orientation"></param>
        /// <param name="threadIndex"></param>
        public void A4Printer(string color, string orientation, int threadIndex)
        {
            lock (lock2)
            {
                Console.WriteLine("{0} je poslao zahtev za štampanje dokumenta A4 formata. " +
                   "Boja: {1}. Orijentacija: {2}\n", Thread.CurrentThread.Name, color, orientation);

                //we set value in an array on true so that method that checks if all computers printed 
                //at least one document will know when to end the program
                allComputersPrinted[threadIndex] = true;
                Thread.Sleep(1000);
                Console.WriteLine("Korisnik {0} može da preizme dokument A4 formata\n", Thread.CurrentThread.Name);
            }
        }

        /// <summary>
        /// method that checks if all computers printed at least one document on every 100 ms
        /// ic checks if all the values in allComputersPrinted array are true
        /// if they are it sets bool variable endOfProgam on true and it signals computer threads to end
        /// </summary>
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
            //we call an event that prints a message that program has ended
            OnProgramEnded();
        }

        /// <summary>
        /// mehtod that will be called by event
        /// </summary>
        protected virtual void OnProgramEnded()
        {
            if (ProgramEnded != null)
                ProgramEnded(this, new EventArgs());
        }
    }
}
