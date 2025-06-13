
namespace RuhsaProject.DTOs.DepoDtos
{
    public class DepoBilgiDto
    {
        public int DepoId { get; set; }  // Seçilen depo

        public string DepoAdi { get; set; }  // Depo Adı → Bu inputu da gönderiyorsun!

        public string Bilgi { get; set; }  // Kullanıcının girdiği bilgi (stok / adet / açıklama)

    }
}
