namespace RealEstate.API.DTOs.PopularLocationDtos
{
    public class ResultPopularLocationDto
    {
        // Json veriyi yakalayip normal formata donusturebilmesi icin UI tarafinda da dto'larla isliyoruz.
        public int LocationID { get; set; }
        public string CityName { get; set; }
        public string ImageUrl { get; set; }
        public int PropertyCount { get; set; }
    }
}
