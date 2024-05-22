using ProtoBuf;

namespace lab9.serializers;

public class MyXmlSerializer : MySerializer
{
    public override T Read<T>(string filename)
    {
        using (FileStream fs = new FileStream(filename, FileMode.OpenOrCreate))
        {
            return Serializer.Deserialize<T>(fs);
        }
    }

    public override void Write<T>(T t, string filename)
    {
        using (FileStream fs = new FileStream(filename, FileMode.OpenOrCreate))
        {
            Serializer.Serialize(fs, t);
        }
    }
}