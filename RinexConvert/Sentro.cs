using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RinexConvert
{
    class Sentro : IGNSSDC
    {
        private string workingDirec;
        private string jokta;
        private string fileNameToDecompres;
        private string desiredNam;
        private string newWorkingDirect;

        public void DeleteFile()
        {
            throw new NotImplementedException();
        }

        public void Execute()
        {
            throw new NotImplementedException();
        }

        public string LineWriter(string workingDirect, string joktan, string fileNameToDecompress, string year)
        {
            workingDirec = workingDirect; jokta = joktan; fileNameToDecompres = fileNameToDecompress; desiredNam = year;
            newWorkingDirect = workingDirect;
            newWorkingDirect += "joktan.bat";

            FileStream stream = null;
            stream = new FileStream(newWorkingDirect, FileMode.CreateNew);
            using (StreamWriter writer = new StreamWriter(stream, Encoding.ASCII))
            {
                writer.WriteLine("teqc -sep sbf {0}", fileNameToDecompress);
            }
            return "From Sentro";
        }

        public void teqcFileResolver()
        {
            throw new NotImplementedException();
        }
    }
}
