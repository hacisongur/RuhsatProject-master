
using RuhsaProject.DTOs.DepoDtos;
using RuhsaProject.DTOs.FaaliyetKonusuDtos;
using RuhsaProject.DTOs.RuhsatSinifiDtos;
using RuhsaProject.DTOs.RuhsatTuruDtos;
using System.ComponentModel;

namespace RuhsatProject.DTOs.Ruhsat
{
    public class RuhsatDto
    {
        public int Id { get; set; }

        [DisplayName("Ruhsat No")]
        public string? RuhsatNo { get; set; }

        [DisplayName("Veriliş Tarihi")]
        public DateTime VerilisTarihi { get; set; } = DateTime.Now;

        [DisplayName("T.C. / Vergi No")]
        public string? TcKimlikNo { get; set; }

        [DisplayName("Adı")]
        public string? Adi { get; set; }

        [DisplayName("Soyadı")]
        public string? Soyadi { get; set; }

        [DisplayName("İşyeri Ünvanı")]
        public string? IsyeriUnvani { get; set; }

        [DisplayName("Faaliyet Konusu")]
        public int FaaliyetKonusuId { get; set; }
        public FaaliyetKonusuDto? FaaliyetKonusu { get; set; } // Faaliyet Konusu DTO

        [DisplayName("Ruhsat Türü")]
        public int RuhsatTuruId { get; set; }
        public RuhsatTuruDto? RuhsatTuru { get; set; } // Ruhsat Türü DTO

        [DisplayName("Ruhsat Sınıfı")]
        public int? RuhsatSinifiId { get; set; }
        public RuhsatSinifiDto? RuhsatSinifi { get; set; } // Ruhsat Sinifi DTO

        [DisplayName("Adres")]
        public string? Adres { get; set; }

        [DisplayName("Açıklama / Not")]
        public string? Not { get; set; }

        [DisplayName("Fotoğraf")]
        public string? PhotoPath { get; set; }
        [DisplayName("Taranmış Dosya")]
        public string? ScannedFilePath { get; set; } // ← doğru hali bu

        [DisplayName("Ada")]
        public string? Ada { get; set; }

        [DisplayName("Parsel")]
        public string? Parsel { get; set; }

        [DisplayName("Pafta")]
        public string? Pafta { get; set; }

        [DisplayName("Aktif Mi?")]
        public bool IsActive { get; set; }
        public List<DepoBilgiDto> DepoBilgileri { get; set; } = new List<DepoBilgiDto>();
        [DisplayName("Oluşturma Tarihi")]
        public DateTime CreatedDate { get; set; }
        [DisplayName("Değiştirilme Tarihi")]
        public DateTime? ModifiedDate { get; set; }

    }
}
