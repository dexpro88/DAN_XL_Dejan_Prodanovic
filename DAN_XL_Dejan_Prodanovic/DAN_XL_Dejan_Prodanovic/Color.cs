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
        public static void WriteColorsToFile(List<string>colors)
        {
            if (File.Exists("../../Paleta.txt"))
            {
                System.IO.File.WriteAllText(@"../../Paleta.txt", string.Empty);
            }

            StreamWriter sw = File.AppendText("../../Paleta.txt");

            foreach (string color in colors)
            {
                sw.WriteLine(color);
            }
                 

            sw.Close();

             
        }
    }
}
