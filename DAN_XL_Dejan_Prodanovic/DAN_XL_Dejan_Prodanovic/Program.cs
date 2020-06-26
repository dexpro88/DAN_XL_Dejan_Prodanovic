using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace DAN_XL_Dejan_Prodanovic
{
    class Program
    {
        static void Main(string[] args)
        {
            //list of colors that will be written to file Paleta.txt
            List<string> colors = new List<string>() { "crvena","zelena","plava","zuta","crna",
             "bela","narancasta","ljubicasta","siva","braon","svetlo plava","svetlo crvena"};

            //colors are writen to file
            Color.WriteColorsToFile(colors);
            EndProgramNotification endProgramNotification = new EndProgramNotification();

            Printing printing = new Printing();
            printing.StartSendingRequests();

            //we add method to an event that will be called when the programs end
            printing.ProgramEnded += endProgramNotification.OnProgramEnded;

            //we start thread that will be checking if all the computers printed 
            //at least one document
            Thread endProgramTread = new Thread(printing.CheckEndOfProgram);
            endProgramTread.Start();


            Console.ReadLine();
        }
    }
}
