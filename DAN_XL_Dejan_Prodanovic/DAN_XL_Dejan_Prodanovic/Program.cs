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
            Printing printing = new Printing();
            printing.StartSendingRequests();
            Thread endProgramTread = new Thread(printing.CheckEndOfProgram);
            endProgramTread.Start();


            Console.ReadLine();
        }
    }
}
