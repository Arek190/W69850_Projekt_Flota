namespace W69850_Projekt_flota.Services;
using W69850_Projekt_flota.Models;
using Newtonsoft.Json;

public class RentalService
    {
        private static string historyFilePath = Path.Combine(Directory.GetParent(AppDomain.CurrentDomain.BaseDirectory).Parent.Parent.FullName, "Data", "rental_history.json");

        public static List<RentalRecord> LoadHistory()
        {
            if (!File.Exists(historyFilePath))
                return new List<RentalRecord>();

            string json = File.ReadAllText(historyFilePath);
            return JsonConvert.DeserializeObject<List<RentalRecord>>(json) ?? new List<RentalRecord>();
        }

        public static void SaveHistory(List<RentalRecord> history)
        {
            string directoryPath = Path.GetDirectoryName(historyFilePath);
            if (!Directory.Exists(directoryPath))
            {
                Directory.CreateDirectory(directoryPath);
            }

            string json = JsonConvert.SerializeObject(history, Formatting.Indented);
            File.WriteAllText(historyFilePath, json);
        }

        public static void RentVehicle(List<Vehicle> vehicles, List<User> users, int vehicleId, int userId)
        {
            var vehicle = vehicles.Find(v => v.Id == vehicleId && v.IsAvailable);
            var user = users.Find(u => u.Id == userId);

            if (vehicle == null || user == null)
            {
                Console.WriteLine("Niepoprawne ID pojazdu lub użytkownika.");
                return;
            }

            vehicle.IsAvailable = false;
            vehicle.RentedByUserId = user.Id;

            var history = LoadHistory();
            history.Add(new RentalRecord
            {
                VehicleId = vehicle.Id,
                VehicleInfo = $"{vehicle.Brand} {vehicle.Model}",
                UserId = user.Id,
                UserInfo = $"{user.FirstName} {user.LastName}",
                RentDate = DateTime.Now,
                ReturnDate = null
            });

            SaveHistory(history);
            Console.WriteLine($"Pojazd {vehicle.Brand} {vehicle.Model} wypożyczony przez {user.FirstName} {user.LastName}.");
        }

        public static void ReturnVehicle(List<Vehicle> vehicles, int vehicleId)
        {
            var vehicle = vehicles.Find(v => v.Id == vehicleId && !v.IsAvailable);
            if (vehicle == null)
            {
                Console.WriteLine("Pojazd nie jest wypożyczony.");
                return;
            }

            vehicle.IsAvailable = true;
            vehicle.RentedByUserId = null;

            var history = LoadHistory();
            var record = history.FindLast(r => r.VehicleId == vehicleId && r.ReturnDate == null);
            if (record != null)
            {
                record.ReturnDate = DateTime.Now;
            }

            SaveHistory(history);
            Console.WriteLine($"Pojazd {vehicle.Brand} {vehicle.Model} został zwrócony.");
        }

        public static void DisplayRentalHistory()
        {
            var history = LoadHistory();
            if (history.Count == 0)
            {
                Console.WriteLine("Brak historii wypożyczeń.");
                return;
            }

            foreach (var record in history)
            {
                Console.WriteLine(record);
            }
        }
    }