namespace RealEstate.API.DTOs.MessageDtos
{
    public class CreateMessageDto
    {
        public int Sender { get; set; }
        public int Receiver { get; set; }
        public string ReceiverEmail { get; set; }
        public string Subject { get; set; }
        public string Detail { get; set; }
        public DateTime SendDate { get; set; }
        public bool IsRead { get; set; }
    }
}
