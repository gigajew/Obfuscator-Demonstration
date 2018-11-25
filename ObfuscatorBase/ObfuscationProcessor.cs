using dnlib.DotNet;
using ObfuscatorBase.Interfaces;
using ObfuscatorBase.Processors;
using System;

namespace ObfuscatorBase
{
    public class ObfuscationProcessor
    {
        public AssemblyDef LoadedAssembly
        {
            get { return m_LoadedAssembly; }
            protected set { m_LoadedAssembly = value; }
        }

        public bool Loaded
        {
            get { return m_Loaded; }
            protected set { m_Loaded = value; }
        }

        public bool Rename
        {
            get { return m_Rename; }
            set { m_Rename = value; }
        }

        public void Load(string filename)
        {
            AssemblyDef assembly = AssemblyDef.Load(filename);
            LoadedAssembly = assembly;
            Loaded = true;
        }

        public void Save(string filename)
        {
            if (!Loaded)
                return;

            LoadedAssembly.Write(filename);
        }

        public void Unload()
        {
            if (!Loaded)
                return;

            LoadedAssembly = null;
            GC.Collect(0, GCCollectionMode.Forced);

            Loaded = false;
        }

        public void Process()
        {
            if (!Loaded)
                return;

            IProcessor renameProcessor = new RenameProcessor();
            renameProcessor.Process(LoadedAssembly);

            IProcessor metaStripProcessor = new MetaStripProcessor();
            metaStripProcessor.Process(LoadedAssembly);
        }

        private bool m_Rename;
        private bool m_Loaded;
        private AssemblyDef m_LoadedAssembly;
    }
}
