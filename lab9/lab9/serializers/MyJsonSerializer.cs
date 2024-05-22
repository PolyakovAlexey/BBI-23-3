using System.Text.Json;

namespace lab9.serializers;

public class MyJsonSerializer : MySerializer
{
    public override T Read<T>(string filename)
    {
        using (FileStream fs = new FileStream(filename, FileMode.OpenOrCreate))
        {
            return JsonSerializer.Deserialize<T>(fs);
        }
    }

    public override void Write<T>(T t, string filename)
    {
        using (FileStream fs = new FileStream(filename, FileMode.OpenOrCreate))
        {
            JsonSerializer.Serialize(fs, t);
        }
    }
}