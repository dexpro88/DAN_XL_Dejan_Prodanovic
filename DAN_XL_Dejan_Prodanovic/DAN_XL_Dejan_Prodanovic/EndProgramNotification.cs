using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAN_XL_Dejan_Prodanovic
{
    class EndProgramNotification
    {
        public void OnProgramEnded(object source, EventArgs e)
        {
            Console.WriteLine("Program je zavrsio sa radom.\nSvi racunari su istampali bar jedan dokument");
        }
    }
}
