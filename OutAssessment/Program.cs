using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assesment
{
    class Program
    {
        static void Main(string[] args)
        {
            string exepath = System.Reflection.Assembly.GetExecutingAssembly().Location;
            string path = System.IO.Path.GetDirectoryName(exepath);

            string inputFilename = path + "\\OutNames.csv";


            CreateDataSet ds = new CreateDataSet(inputFilename);
            ds.WriteNameList();
            ds.WriteAddressList();
        }
    }
}
