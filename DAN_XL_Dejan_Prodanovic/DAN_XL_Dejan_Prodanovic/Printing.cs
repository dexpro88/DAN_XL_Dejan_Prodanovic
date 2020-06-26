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
    }
}
