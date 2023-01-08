using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace customer
{
    public class FileOperation
    {
        // Store the path of the textfile in your system
        private  string file = @"";

        public FileOperation()
        {
            this.file = @"C:\Users\lenovo\source\repos\cargageWPF\customer\usr.dat";
        }

        public string ReadFile() {
            // To read the entire file at once
            if (File.Exists(file))
            {
                // Read all the content in one string
                // and display the string
                return File.ReadAllText(file);
            }
            else return null;
        }

        public void WriteFile(string text) {
            // To read a text file line by line
            if (File.Exists(file))
            {
                // Store each line in array of strings
                File.WriteAllText(file, text);
            }
            else
            {
                throw new FileNotFoundException();
            }
        }
        
    }
}