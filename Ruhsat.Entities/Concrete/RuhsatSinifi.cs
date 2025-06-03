namespace RuhsatProject.Entities.Concrete
{
    public class RuhsatSinifi
    {
        public int Id { get; set; }
        public string Name { get; set; }  // Sınıf adı, örneğin: 1. Sınıf, 2. Sınıf
        public int RuhsatTuruId { get; set; }  // Hangi RuhsatTuru'na ait olduğu
        public RuhsatTuru RuhsatTuru { get; set; }  // RuhsatTuru ile ilişki
    }
}
