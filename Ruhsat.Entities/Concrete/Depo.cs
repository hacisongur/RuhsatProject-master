

using RuhsatProject.Entities.Concrete;

namespace RuhsaProject.Entities.Concrete
{
    public class Depo
    {
        public int Id { get; set; }
        public string Adi { get; set; }  // Depo adı

        public int RuhsatSinifiId { get; set; }  // FK
        public RuhsatSinifi RuhsatSinifi { get; set; }  // Navigation property
    }
}
