using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ObfuscatorBase.Utils
{
    internal static class Randomization
    {
        private const string m_Alpha = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ";
        private const string m_Numeric = "0123456789";
        private const string m_AlphaNumeric = m_Alpha + m_Numeric;

        static Randomization()
        {
            InitializeAlgorithm();
        }

        private static void InitializeAlgorithm()
        {
            m_Random = new Random(GetRandomSeed());
        }

        private static int GetRandomSeed()
        {
            int seed = Guid.NewGuid().GetHashCode();
            seed ^= Environment.TickCount;
            return seed;
        }

        public static string GetRandomAlphaNumericString(int minLength, int maxLength)
        {
            StringBuilder builder = new StringBuilder(GetRandomInt(minLength, maxLength));
            for (int i = 0; i < builder.Capacity; i++)
            {
                builder.Append(m_AlphaNumeric[GetRandomInt(0, m_AlphaNumeric.Length)]);
            }
            string result = builder.ToString();
            builder = null;
            return result;
        }

        public static string GetRandomAlphaString(int minLength, int maxLength)
        {
            StringBuilder builder = new StringBuilder(GetRandomInt(minLength, maxLength));
            for (int i = 0; i < builder.Capacity; i++)
            {
                builder.Append(m_Alpha[GetRandomInt(0, m_Alpha.Length)]);
            }
            string result = builder.ToString();
            builder = null;
            return result;
        }

        private static int GetRandomInt(int minLength, int maxLength)
        {
            return m_Random.Next(minLength, maxLength);
        }

        private static Random m_Random;
    }
}
