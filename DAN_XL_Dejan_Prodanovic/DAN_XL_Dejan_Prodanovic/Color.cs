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
        /// <summary>
        /// method that writes list of colors in file Paleta.txt
        /// </summary>
        /// <param name="colors"></param>
        public static void WriteColorsToFile(List<string> colors)
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

        /// <summary>
        /// Method that reads colors from file. It returns list of strings with color names
        /// </summary>
        /// <returns></returns>
        public static List<string> ReadColorsToFile()
        {
            List<string> colors = new List<string>();

            using (StreamReader sr = new StreamReader("../../Paleta.txt"))
            {
                string line;

                while ((line = sr.ReadLine()) != null)
                {
                    colors.Add(line);
                }
            }
            return colors;

        }
    }
}
