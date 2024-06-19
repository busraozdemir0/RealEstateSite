namespace RealEstate.API.DTOs.MessageDtos
{
    public class ResultSendBoxMessageDto
    {
        public int MessageID { get; set; }
        public string Name { get; set; } // AppUser ve Message tablosunu inner join ile birlestirerek gelen mesajin kimden geldigini gosterecegiz
        public string Subject { get; set; }
        public string Detail { get; set; }
        public DateTime SendDate { get; set; }
        public bool IsRead { get; set; }
        public string UserImageUrl { get; set; }
    }
}
