using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAN_XL_Dejan_Prodanovic
{
    class Color
    {
        public static void WriteClorsToFile(List<string>colors)
        {
            if (File.Exists("../../Paleta.txt"))
            {
                System.IO.File.WriteAllText(@"../../Paleta.txt", string.Empty);
            }

            StreamWriter sw = File.AppendText("../../Paleta.txt");

            foreach (var color in colors)
            {
                sw.WriteLine(colors);
            }
                 

            sw.Close();

             
        }
    }
}
