using System.IO;
using System.Xml.Serialization;

public interface ISave<T>
{
    void SaveData(T data, string path);
}

public class Save<T> : ISave<T> where T : class
{
    public void SaveData(T data, string path)
    {
        StreamWriter fileWriter = new StreamWriter(path);
        XmlSerializer obj = new XmlSerializer(typeof(T));

        obj.Serialize(fileWriter.BaseStream, data);
        fileWriter.Close();
    }
}