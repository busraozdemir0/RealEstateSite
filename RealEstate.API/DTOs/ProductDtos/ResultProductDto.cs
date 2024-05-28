namespace RealEstate.API.DTOs.ProductDtos
{
    // Urunu getirmek icin kullanilan dto
    public class ResultProductDto
    {
        public int ProductID { get; set; }
        public string Title { get; set; }
        public decimal Price { get; set; }
        public string City { get; set; }
        public string District { get; set; }
        public int ProductCategory { get; set; }
    }
}
