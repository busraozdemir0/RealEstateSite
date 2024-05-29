namespace RealEstate.UI.DTOs.TestimonialDtos
{
    public class ResultTestimonialDto
    {
        // Buradaki alanlar SQL'deki tablo alanlariyla eslesebilmesi icin birebir ayni olmak zorundadir.
        public int TestimonialID { get; set; }
        public string NameSurname { get; set; }
        public string Title { get; set; }
        public string Comment { get; set; }
        public bool Status { get; set; }
    }
}
