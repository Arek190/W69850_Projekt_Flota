using System.Xml;
using Newtonsoft.Json;
using W69850_Projekt_flota.Models;

namespace W69850_Projekt_flota.Services;

public class UserManager
{
    //private static string filePath = "Data/users.json";
    private static string filePath = Path.Combine(
        Path.GetFullPath(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"..\..\..\Data")),
        "users.json"
    );


    public static List<User> LoadUsers()
    {
        if (!File.Exists(filePath))
            return new List<User>();

        string json = File.ReadAllText(filePath);
        return JsonConvert.DeserializeObject<List<User>>(json) ?? new List<User>();
    }

    public static void SaveUsers(List<User> users)
    {
        string json = Newtonsoft.Json.JsonConvert.SerializeObject(users, Newtonsoft.Json.Formatting.Indented);
        File.WriteAllText(filePath, json);
    }

    public static void AddUser(List<User> users, string firstName, string lastName)
    {
        int id = users.Count + 1;
        users.Add(new User { Id = id, FirstName = firstName, LastName = lastName });
    }

    public static void DisplayUsers(List<User> users)
    {
        foreach (var user in users)
        {
            Console.WriteLine(user);
        }
    }
}