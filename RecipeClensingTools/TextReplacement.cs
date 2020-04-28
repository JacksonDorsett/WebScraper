using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecipeClensingTools
{
    class TextReplacement
    {
        List<int> LinesContainingString;
        string[] lines;
        FileStream stream;

        public TextReplacement(string filePath, string oldStr)
        {
            stream = new FileStream(filePath, FileMode.Open);
            StreamReader sr = new StreamReader(stream);
            lines = sr.ReadToEnd().Split('\n');
            sr.Close();
        }

        private void FindAllLines(string search)
        {
            foreach (string line in lines)
            {
                if (line.Contains(search))
                {

                }
            }
        }
    }
}
