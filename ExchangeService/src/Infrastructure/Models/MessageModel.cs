namespace Infrastructure.Models
{
    public class MessageModel
    {
        public MessageModel(int quote, long messageNumber)
        {
            Quote = quote;
            MessageNumber = messageNumber;
        }
        
        public int Quote { get; set; }
        public long MessageNumber { get; set; }
    }
}