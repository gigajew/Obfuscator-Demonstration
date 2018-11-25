using dnlib.DotNet;
using ObfuscatorBase.Interfaces;
using ObfuscatorBase.Utils;

namespace ObfuscatorBase.Processors
{
    internal class RenameProcessor : IProcessor
    {
        public void Process(AssemblyDef assembly)
        {
            //if (Analyzer.CanObfuscate(assembly))
                //assembly.Name = Randomization.GetRandomAlphaString(1024, 2048);

            if (!Analyzer.CanObfuscateMembers(assembly))
                return;

            foreach (ModuleDef module in assembly.Modules)
            {
                Process(module);
            }
        }

        public void Process(ModuleDef module)
        {
            if (Analyzer.CanObfuscate(module))
                module.Name = Randomization.GetRandomAlphaString(1024, 2048);

            if (!Analyzer.CanObfuscateMembers(module))
                return;

            foreach (TypeDef type in module.Types)
            {
                Process(type);
            }
        }

        private void Process(TypeDef type)
        {
            if (Analyzer.CanObfuscate(type))
            {
                if (type.IsRuntimeSpecialName || type.IsGlobalModuleType)
                    return;

                type.Name = Randomization.GetRandomAlphaString(1024, 2048);
                type.Namespace = Randomization.GetRandomAlphaString(1024, 2048);
            }

            if (!Analyzer.CanObfuscateMembers(type))
                return;
            
            foreach (PropertyDef property in type.Properties)
            {
                Process(property);
            }

            foreach (FieldDef field in type.Fields)
            {
                Process(field);
            }

            foreach (EventDef @event in type.Events)
            {
                Process(@event);
            }

            foreach (MethodDef method in type.Methods)
            {
                Process(method);
            }
        }

        private void Process(PropertyDef property)
        {
            if (Analyzer.CanObfuscate(property))
            {
                if (property.IsRuntimeSpecialName)
                    return;

                property.Name = Randomization.GetRandomAlphaString(1024, 2048);
            }

            if (!Analyzer.CanObfuscateMembers(property))
                return;
        }

        private static void Process(FieldDef field)
        {
            if (Analyzer.CanObfuscate(field))
            {
                if (field.IsRuntimeSpecialName)
                    return;

                if (field.IsLiteral && field.DeclaringType.IsEnum)
                    return;

                field.Name = Randomization.GetRandomAlphaString(1024, 2048);
            }

            if (!Analyzer.CanObfuscateMembers(field))
                return;
        }

        private void Process(EventDef @event)
        {
            if (Analyzer.CanObfuscate(@event))
            {
                if (@event.IsRuntimeSpecialName)
                    return;

                @event.Name = Randomization.GetRandomAlphaString(1024, 2048);
            }
        }

        private void Process(MethodDef method)
        {
            if (Analyzer.CanObfuscate(method))
            {
                if (method.IsRuntime || method.IsRuntimeSpecialName ||
    method.IsConstructor || method.IsStaticConstructor ||
    method.DeclaringType.IsForwarder)
                    return;

                method.Name = Randomization.GetRandomAlphaString(1024, 2048);
            }

            if (!Analyzer.CanObfuscateMembers(method))
                return;

            foreach (ParamDef parameter in method.ParamDefs)
            {
                Process(parameter);
            }
        }

        private void Process(ParamDef parameter)
        {
            if (Analyzer.CanObfuscate(parameter))
            {
                parameter.Name = Randomization.GetRandomAlphaString(1024, 2048);
            }
        }
    }
}
