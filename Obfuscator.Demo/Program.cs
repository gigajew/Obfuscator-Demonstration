using ObfuscatorBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Obfuscator.Demo
{
    public static class Program
    {
        public static int Main(string[] args)
        {
            ObfuscationProcessor processor = new ObfuscationProcessor();
            processor.Load(args[0]);
            processor.Process();
            processor.Save("obfuscated.exe");
            processor.Unload();

            return 0;
        }
    }
}
