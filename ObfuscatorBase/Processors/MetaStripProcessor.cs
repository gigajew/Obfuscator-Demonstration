using dnlib.DotNet;
using ObfuscatorBase.Interfaces;
using ObfuscatorBase.Utils;

namespace ObfuscatorBase.Processors
{
    internal class MetaStripProcessor : IProcessor
    {
        public void Process(AssemblyDef assembly)
        {
            if (Analyzer.CanObfuscate(assembly))
            {
                for (int i = 0; i < assembly.CustomAttributes.Count; i++)
                {
                    assembly.CustomAttributes.Remove(assembly.CustomAttributes[i]);
                }
            }

            if (!Analyzer.CanObfuscateMembers(assembly))
                return;

            foreach (ModuleDef module in assembly.Modules)
            {
                Process(module);
            }
        }

        private void Process(ModuleDef module)
        {
            if (Analyzer.CanObfuscate(module))
            {
                for (int i = 0; i < module.CustomAttributes.Count; i++)
                {
                    module.CustomAttributes.Remove(module.CustomAttributes[i]);
                }
            }

            module.Mvid = null;
            module.Name = null;
        }
    }
}
