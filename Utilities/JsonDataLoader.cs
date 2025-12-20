using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;

public static class JsonDataLoader
{
    public static List<T> LoadData<T>(string filePath)
    {
        var json = File.ReadAllText(filePath);
        return JsonConvert.DeserializeObject<List<T>>(json);
    }
}