using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RinexConvert
{
    interface IGNSSDC
    {
        string LineWriter(string workingDirect, string joktan, string fileNameToDecompress, string year);

        void teqcFileResolver();

        void DeleteFile();
        void Execute();
    }
}
