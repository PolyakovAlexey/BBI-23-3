using System.Xml.Serialization;

namespace lab9.serializers;

public class MyBinarySerializer : MySerializer
{
    public override T Read<T>(string filename) where T : class
    {
        XmlSerializer xmlSerializer = new XmlSerializer(typeof(T));
        using (FileStream fs = new FileStream(filename, FileMode.OpenOrCreate))
        {
            return xmlSerializer.Deserialize(fs) as T;// обобщенный тип данных является классом
        }
    }

    public override void Write<T>(T t, string filename)
    {
        XmlSerializer xmlSerializer = new XmlSerializer(typeof(T));
        using (FileStream fs = new FileStream(filename, FileMode.OpenOrCreate))
        {
            xmlSerializer.Serialize(fs, t);
        }
    }
}