using System.IO;
using System.Xml.Serialization;
using UnityModManagerNet;

namespace AdofaiMod2
{
    public class Setting : UnityModManager.ModSettings
    {
        public string set1 = "set1";
        public bool set2 = false;
        public int set3 = 0;
        
        public override void Save(UnityModManager.ModEntry modEntry)
        {
            var filePath = GetPath(modEntry);
            try
            {
                using (var writer = new StreamWriter(filePath))
                {
                    var serializer = new XmlSerializer(GetType());
                    serializer.Serialize(writer, this);
                }
            }
            catch
            {
                // ignored
            }
        }
        
        public override string GetPath(UnityModManager .ModEntry modEntry)
        {
            return Path.Combine(modEntry.Path, GetType().Name + ".xml");
        }
    }
}