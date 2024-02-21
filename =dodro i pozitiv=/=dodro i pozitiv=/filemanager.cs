using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Text.Json;

public static class FileManager
{
    public static void SerializeToFile<T>(string filePath, T data)
    {
        var options = new JsonSerializerOptions { WriteIndented = true };
        string jsonString = JsonSerializer.Serialize(data, options);
        File.WriteAllText(filePath, jsonString);
    }

    public static T DeserializeFromFile<T>(string filePath)
    {
        if (!File.Exists(filePath)) return default;
        string jsonString = File.ReadAllText(filePath);
        return JsonSerializer.Deserialize<T>(jsonString);
    }
}