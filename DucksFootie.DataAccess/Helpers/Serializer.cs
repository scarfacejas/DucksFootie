using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;

namespace DucksFootie.DataAccess.Helpers
{
    public class Serializer
    {
        public static object GetDeserializedData(string path)
        {
            if (!File.Exists(path))
                return null;

            using (var stream = File.Open(path, FileMode.Open))
            {
                var serializer = new BinaryFormatter();

                return serializer.Deserialize(stream);
            }
        }

        public static void SaveData(string path, object data)
        {
            using (var stream = File.Open(path, FileMode.Create))
            {
                var serializer = new BinaryFormatter();

                serializer.Serialize(stream, data);
            }
        }
    }
}
