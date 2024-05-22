namespace lab9.serializers;

public abstract class MySerializer // показываем, что наш обобщенный тип данных является классом
{
    public abstract void Write<T>(T t, string filename);

    public abstract T Read<T>(string filename) where T : class;
}