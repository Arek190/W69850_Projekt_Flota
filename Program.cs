namespace W69850_Projekt_flota;
using W69850_Projekt_flota.Models;
using W69850_Projekt_flota.Services;

class Program
{
    static List<Vehicle> vehicles = FleetManager.LoadVehicles();
    static List<User> users = UserManager.LoadUsers();

    static void Main()
    {
        while (true)
        {
            Console.WriteLine("\n🚗 System zarządzania flotą");
            Console.WriteLine("1. Dodaj pojazd");
            Console.WriteLine("2. Dodaj użytkownika");
            Console.WriteLine("3. Wyświetl flotę");
            Console.WriteLine("4. Wyświetl użytkowników");
            Console.WriteLine("5. Wypożycz pojazd");
            Console.WriteLine("6. Zwróć pojazd");
            Console.WriteLine("7. Wyświetl historię wypożyczeń");
            Console.WriteLine("8. Wyświetl stan pojazdów");
            Console.WriteLine("9. Zaktualizuj stan pojazdu");
            Console.WriteLine("10. Wyjście");

            Console.Write("Wybierz opcję: ");
            string option = Console.ReadLine();

            switch (option)
            {
                case "1":
                    Console.Write("Marka: ");
                    string brand = Console.ReadLine();
                    Console.Write("Model: ");
                    string model = Console.ReadLine();
                    Console.Write("Rok: ");
                    int year = int.Parse(Console.ReadLine());
                    FleetManager.AddVehicle(vehicles, brand, model, year);
                    break;
                case "2":
                    Console.Write("Imię: ");
                    string firstName = Console.ReadLine();
                    Console.Write("Nazwisko: ");
                    string lastName = Console.ReadLine();
                    UserManager.AddUser(users, firstName, lastName);
                    break;
                case "3":
                    FleetManager.DisplayVehicles(vehicles);
                    break;
                case "4":
                    UserManager.DisplayUsers(users);
                    break;
                case "5":
                    Console.Write("ID pojazdu: ");
                    int vId = int.Parse(Console.ReadLine());
                    Console.Write("ID użytkownika: ");
                    int uId = int.Parse(Console.ReadLine());
                    RentalService.RentVehicle(vehicles, users, vId, uId);
                    break;
                case "6":
                    Console.Write("ID pojazdu do zwrotu: ");
                    int returnId = int.Parse(Console.ReadLine());
                    RentalService.ReturnVehicle(vehicles, returnId);
                    break;
                case "7":
                    RentalService.DisplayRentalHistory();
                    break;
                case "8":
                    FleetManager.DisplayVehicleStatus(vehicles);
                    break;
                case "9":
                    Console.Write("ID pojazdu do aktualizacji: ");
                    int updateId = int.Parse(Console.ReadLine());
                    Console.Write("Nowy przebieg (km): ");
                    double newMileage = double.Parse(Console.ReadLine());
                    Console.Write("Stan techniczny (Dobry / Wymaga serwisu / Uszkodzony): ");
                    string newCondition = Console.ReadLine();
                    FleetManager.UpdateVehicleStatus(vehicles, updateId, newMileage, newCondition);
                    break;
                case "10":
                    FleetManager.SaveVehicles(vehicles);
                    UserManager.SaveUsers(users);
                    return;
            }
        }
    }
}