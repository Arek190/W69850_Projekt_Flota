namespace W69850_Projekt_flota.Models;

public class RentalRecord
{
    public int VehicleId { get; set; } // ID pojazdu
    public string VehicleInfo { get; set; } // Informacje o pojeździe
    public int UserId { get; set; } // ID użytkownika
    public string UserInfo { get; set; } // Informacje o użytkowniku
    public DateTime RentDate { get; set; } // Data wypożyczenia
    public DateTime? ReturnDate { get; set; } // Data zwrotu

    public override string ToString()
    {
        string returnInfo = ReturnDate.HasValue ? $"Zwrócono: {ReturnDate}" : "Aktualnie wypożyczony";
        return $"{RentDate}: {UserInfo} wypożyczył {VehicleInfo}. {returnInfo}";
    }
}