using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace DAN_XL_Dejan_Prodanovic
{
    class Program
    {
        static void Main(string[] args)
        {
            List<string> colors = new List<string>() { "crvena","zelena","plava","zuta","crna",
             "bela","narancasta","ljubicasta","siva","braon","svetlo plava","svetlo crvena"};

            Color.WriteColorsToFile(colors);
            EndProgramNotification endProgramNotification = new EndProgramNotification();

            Printing printing = new Printing(colors);
            printing.StartSendingRequests();

            printing.ProgramEnded += endProgramNotification.OnProgramEnded;

            Thread endProgramTread = new Thread(printing.CheckEndOfProgram);
            endProgramTread.Start();


            Console.ReadLine();
        }
    }
}
