using dnlib.DotNet;

namespace ObfuscatorBase.Utils
{
    internal static class Analyzer
    {
        public static bool CanObfuscate(dynamic obj)
        {
            CustomAttribute obfuscationAttribute = GetObfuscationAttribute(obj);

            if (obfuscationAttribute == null)
                return true;

            CANamedArgument exclude = obfuscationAttribute.GetProperty("Exclude");

            if (exclude == null)
                return false;

            if ((bool)exclude.Value == true)
            {
                return false;
            }

            return true;
        }

        public static bool CanObfuscateMembers(dynamic obj)
        {
            CustomAttribute obfuscationAttribute = GetObfuscationAttribute(obj);

            if (obfuscationAttribute == null)
                return true;

            CANamedArgument exclude = obfuscationAttribute.GetProperty("Exclude");
            CANamedArgument includeMembers = obfuscationAttribute.GetProperty("ApplyToMembers");

            if (exclude == null || includeMembers == null)
                return true;

            if ((bool)exclude.Value == true &&
                (bool)includeMembers.Value == true)
            {
                    return false;
            }

            return true;
        }

        private static CustomAttribute GetObfuscationAttribute(dynamic obj)
        {
            CustomAttributeCollection collection = obj.CustomAttributes;
            if (collection == null)
                return null;

            foreach (CustomAttribute attrib in collection)
            {
                if (string.Equals(attrib.TypeFullName, "System.Reflection.ObfuscationAttribute") ||
                    string.Equals(attrib.TypeFullName, "System.Reflection.ObfuscateAssemblyAttribute"))
                {
                    return attrib;
                }
            }

            return null;
        }
    }
}
