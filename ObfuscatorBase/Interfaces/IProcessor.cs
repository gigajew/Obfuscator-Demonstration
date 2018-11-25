using dnlib.DotNet;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ObfuscatorBase.Interfaces
{
    internal interface IProcessor
    {
        void Process(AssemblyDef assembly);
    }
}
