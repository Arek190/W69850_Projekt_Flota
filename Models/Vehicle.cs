namespace W69850_Projekt_flota.Models;

public class Vehicle
{
    public int Id { get; set; } // ID pojazdu
    public string Brand { get; set; } // Marka pojazdu
    public string Model { get; set; } // Model pojazdu
    public int Year { get; set; } // Rok produkcji
    public bool IsAvailable { get; set; } = true; // Dostępność pojazdu
    public int? RentedByUserId { get; set; } = null; // ID użytkownika, który wypożyczył pojazd (null jeśli dostępny)
    public double Mileage { get; set; } // Przebieg w km
    public string Condition { get; set; } // Status techniczny

    public override string ToString()
    {
        string status = IsAvailable ? "Dostępny" : $"Wypożyczony (ID użytkownika: {RentedByUserId})";
        return $"{Id}. {Brand} {Model} ({Year}) - {status}, Przebieg: {Mileage} km, Stan: {Condition}";
    }
}