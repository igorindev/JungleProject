using System.IO;
using System.Xml.Serialization;

public interface ILoad<T>
{
    T LoadData(string path);
}

public class Load<T> : ILoad<T> where T : class
{
    public T LoadData(string path)
    {
        if (File.Exists(path))
        {
            StreamReader fileReader = new StreamReader(path);
            XmlSerializer obj = new XmlSerializer(typeof(T));
            T save = obj.Deserialize(fileReader.BaseStream) as T;

            fileReader.Close();

            return save;
        }

        return null;
    }
}
