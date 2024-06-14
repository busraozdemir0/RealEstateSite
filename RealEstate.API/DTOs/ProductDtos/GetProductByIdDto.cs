namespace RealEstate.API.DTOs.ProductDtos
{
    public class GetProductByIdDto
    {
        public int ProductID { get; set; }
        public string Title { get; set; }
        public decimal Price { get; set; }
        public string City { get; set; }
        public string District { get; set; }
        public string CoverImage { get; set; }
        public string Address { get; set; }
        public string Description { get; set; }
        public string Type { get; set; }
        public bool DealOfTheDay { get; set; } = false;
        public DateTime AdvertisementDate { get; set; } = DateTime.Now;
        public bool ProductStatus { get; set; } = false;
        public int ProductCategory { get; set; }
        public int AppUserId { get; set; }
    }
}
