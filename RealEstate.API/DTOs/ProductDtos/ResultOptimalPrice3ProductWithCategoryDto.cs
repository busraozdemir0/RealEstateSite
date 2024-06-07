namespace RealEstate.API.DTOs.ProductDtos
{
    public class ResultOptimalPrice3ProductWithCategoryDto
    {
        // Ana sayfada gunun 3 firsati kismi icin en uygun/ucuz fiyatli 3 ilan listelenecek
        public int ProductID { get; set; }
        public string Title { get; set; }
        public decimal Price { get; set; }
        public string City { get; set; }
        public string District { get; set; }
        public string Description { get; set; }
        public int ProductCategory { get; set; }
        public string CategoryName { get; set; }
        public string CoverImage { get; set; }
        public DateTime AdvertisementDate { get; set; }
    }
}
