namespace W69850_Projekt_flota.Models;

    public class User
    {
        public int Id { get; set; } // ID użytkownika
        public string FirstName { get; set; } // Imię
        public string LastName { get; set; } // Nazwisko

        public override string ToString()
        {
            return $"{Id}: {FirstName} {LastName}";
        }
    }
