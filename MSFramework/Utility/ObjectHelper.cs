using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace MedicalSys.Framework
{
    public static class ObjectHelper
    {
        /// <summary>
        /// Creates the deep copy.
        /// </summary>
        /// <param name="source">The source.</param>
        /// <returns>System.Object.</returns>
        public static object CreateDeepCopy(object source)
        {
            MemoryStream memoryStream = new MemoryStream();
            BinaryFormatter formatter = new BinaryFormatter();
            formatter.Serialize(memoryStream, source);
            memoryStream.Position = 0;

            object obj = formatter.Deserialize(memoryStream);
            return obj;
        }

        /// <summary>
        /// Objects to int.
        /// </summary>
        /// <param name="obj">The obj.</param>
        /// <returns>ObjectToInt</returns>
        public static int ObjectToInt(object obj)
        {
            if (obj == null)
            {
                return 0;
            }
            int r = 0;
            int.TryParse(obj.ToString(), out r);
            return r;
        }

        /// <summary>
        /// Objects to uint.
        /// </summary>
        /// <param name="obj">The obj.</param>
        /// <returns>System.UInt32.</returns>
        public static uint ObjectToUint(object obj)
        {
            if (obj == null)
            {
                return 0;
            }
            uint r = 0;
            uint.TryParse(obj.ToString(), out r);
            return r;
        }

    }
}
