using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAN_XL_Dejan_Prodanovic
{
    class EndProgramNotification
    {
        /// <summary>
        /// method that prints message that program is ended
        /// it is called by an event
        /// </summary>
        /// <param name="source"></param>
        /// <param name="e"></param>
        public void OnProgramEnded(object source, EventArgs e)
        {
            Console.WriteLine("Program je zavrsio sa radom.\nSvi racunari su istampali bar jedan dokument\n\n" +
                "Pritisnite Enter da izadjete iz programa");
        }
    }
}
