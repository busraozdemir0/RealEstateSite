namespace RealEstate.UI.DTOs.ProductDtos
{
    public class CreateProductDto
    {
        public string Title { get; set; }
        public decimal Price { get; set; }
        public string City { get; set; }
        public string District { get; set; }
        public string CoverImage { get; set; }
        public IFormFile Image { get; set; } // Dosyadan gorsel secebilmek icin
        public string Address { get; set; }
        public string Description { get; set; }
        public string Type { get; set; }
        public bool DealOfTheDay { get; set; }
        public DateTime AdvertisementDate { get; set; }
        public bool ProductStatus { get; set; }
        public int ProductCategory { get; set; }
        public int AppUserId { get; set; }
    }
}
