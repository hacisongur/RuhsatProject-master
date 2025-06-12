using RuhsaProject.Entities.Concrete;

namespace RuhsatProject.Entities.Concrete
{
    public class Ruhsat
    {
        public int Id { get; set; }

        public string? RuhsatNo { get; set; }
        public DateTime VerilisTarihi { get; set; } = DateTime.Now;
        public string? TcKimlikNo { get; set; }
        public string? Adi { get; set; }
        public string? Soyadi { get; set; }
        public string? IsyeriUnvani { get; set; }

        // FaaliyetKonusu ile ilişki
        public int FaaliyetKonusuId { get; set; }
        public FaaliyetKonusu FaaliyetKonusu { get; set; } // Faaliyet Konusu (Navigation Property)

        // RuhsatTuru ile ilişki
        public int RuhsatTuruId { get; set; }
        public RuhsatTuru RuhsatTuru { get; set; } // Ruhsat Türü (Navigation Property)
        // RuhsatImza ile ilişki

        // RuhsatSinifi ile ilişki
        public int? RuhsatSinifiId { get; set; }
        public RuhsatSinifi? RuhsatSinifi { get; set; } // Ruhsat Sınıfı (Navigation Property)

        public string? Adres { get; set; }
        public string? Not { get; set; }
        public string? PhotoPath { get; set; }
        public string? ScannedFilePath { get; set; }

        public string? Ada { get; set; }
        public string? Parsel { get; set; }
        public string? Pafta { get; set; }
        // Aktiflik durumu
        public bool? IsActive { get; set; }
        public ICollection<DepoBilgi> DepoBilgileri { get; set; } = new List<DepoBilgi>();
    }
}
