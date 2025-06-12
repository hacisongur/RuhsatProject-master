
namespace RuhsaProject.DTOs.DepoDtos
{
    public class DepoBilgiDto
    {
        public int DepoId { get; set; }  // Seçilen depo

        public string Bilgi { get; set; }  // Kullanıcının girdiği bilgi (stok / adet / açıklama)
    }
}
