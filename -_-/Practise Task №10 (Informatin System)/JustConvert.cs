using Newtonsoft.Json;
using System.Text.Json.Serialization;

namespace Practise_Task__10__Informatin_System_
{
    static class JustConvert
    {
        public static string desktop, MainFolder, fileName;
        public static void Serialization(List<User> list, string mainfolder, string filename)
        {
            desktop = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            MainFolder = mainfolder;
            fileName = filename;

            string json = JsonConvert.SerializeObject(list);
            if (Directory.Exists(desktop + MainFolder))
            {
                File.WriteAllText(desktop + MainFolder + fileName, json);
            }
            else
            {
                Directory.CreateDirectory(desktop + MainFolder);
                File.WriteAllText(desktop + MainFolder + fileName, json);
            }
        }
        public static List<User> Deserealization()
        {
            string json = File.ReadAllText(desktop + MainFolder + fileName);
            List<User> list = JsonConvert.DeserializeObject<List<User>>(json);
            return list;
        }
    }
}
