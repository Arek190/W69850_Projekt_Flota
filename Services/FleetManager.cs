namespace W69850_Projekt_flota.Services;
using W69850_Projekt_flota.Models;
using Newtonsoft.Json;

public class FleetManager
    {
        private static string filePath = Path.Combine(
            Path.GetFullPath(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"..\..\..\Data")),
            "vehicles.json"
        );
        public static List<Vehicle> LoadVehicles()
        {
            if (!File.Exists(filePath))
                return new List<Vehicle>();

            string json = File.ReadAllText(filePath);
            return JsonConvert.DeserializeObject<List<Vehicle>>(json) ?? new List<Vehicle>();
        }

        public static void SaveVehicles(List<Vehicle> vehicles)
        {
            string directoryPath = Path.GetDirectoryName(filePath);
            if (!Directory.Exists(directoryPath))
            {
                Directory.CreateDirectory(directoryPath);
            }

            string json = JsonConvert.SerializeObject(vehicles, Formatting.Indented);
            File.WriteAllText(filePath, json);
        }

        public static void AddVehicle(List<Vehicle> vehicles, string brand, string model, int year)
        {
            int newId = vehicles.Count + 1;
            vehicles.Add(new Vehicle
            {
                Id = newId,
                Brand = brand,
                Model = model,
                Year = year,
                IsAvailable = true,
                Mileage = 0,  // Nowe auta zaczynają od 0 km
                Condition = "Dobry"  // Domyślny stan techniczny
            });

            SaveVehicles(vehicles);
            Console.WriteLine($"Dodano pojazd: {brand} {model} ({year})");
        }
        
        
        public static void DisplayVehicles(List<Vehicle> vehicles)
        {
            foreach (var vehicle in vehicles)
            {
                Console.WriteLine(vehicle);
            }
        }

        public static void DisplayVehicleStatus(List<Vehicle> vehicles)
        {
            if (vehicles.Count == 0)
            {
                Console.WriteLine("Brak pojazdów w systemie.");
                return;
            }

            Console.WriteLine("\n📋 Stan pojazdów:");
            foreach (var vehicle in vehicles)
            {
                Console.WriteLine(vehicle);
            }
        }

        public static void UpdateVehicleStatus(List<Vehicle> vehicles, int vehicleId, double mileage, string condition)
        { 
            var vehicle = vehicles.Find(v => v.Id == vehicleId);
            if (vehicle == null)
            {
                Console.WriteLine("Pojazd nie istnieje.");
                return;
            }

            vehicle.Mileage = mileage;
            vehicle.Condition = condition;
            SaveVehicles(vehicles);
            Console.WriteLine($"Zaktualizowano stan pojazdu: {vehicle.Brand} {vehicle.Model}.");
        }
    }
