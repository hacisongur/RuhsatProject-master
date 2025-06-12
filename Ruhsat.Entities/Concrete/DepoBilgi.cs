using RuhsatProject.Entities.Concrete;
namespace RuhsaProject.Entities.Concrete
{
    public class DepoBilgi
    {
        public int Id { get; set; }  // Primary Key

        public int RuhsatId { get; set; }  // FK
        public Ruhsat Ruhsat { get; set; }  // Navigation

        public int DepoId { get; set; }  // FK
        public Depo Depo { get; set; }  // Navigation

        public string DepoAdi { get; set; }  // Gerekiyorsa

        public string Bilgi { get; set; }  // Kullanıcının girdiği bilgi
    }

}
