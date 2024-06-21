namespace RealEstate.UI.DTOs.ProductImageDtos
{
    public class CreateProductImageDto
    {
        public string ImageUrl { get; set; }
        public IFormFile Image { get; set; } // View tarafinda dosyadan gorsel secebilmek icin
        public int ProductId { get; set; }
    }
}
