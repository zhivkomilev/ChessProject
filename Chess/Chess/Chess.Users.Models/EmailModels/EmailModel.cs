namespace Chess.Users.Models.EmailModels
{
    public class EmailModel
    {
        public UserEmailModel Reciever { get; set; }

        public UserEmailModel Sender { get; set; }

        public string Subject { get; set; }

        public string Body { get; set; }
    }
}