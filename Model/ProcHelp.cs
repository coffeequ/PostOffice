using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PostOffice.Model
{
    class ProcHelp
    {
        private readonly string path;

        public ProcHelp(string path)
        {
            this.path = path;
        }

        public void CallHelp()
        {
            var proc = new System.Diagnostics.Process();
            proc.StartInfo.FileName = path;
            proc.StartInfo.UseShellExecute = true;
            proc.Start();
        }
    }
}
