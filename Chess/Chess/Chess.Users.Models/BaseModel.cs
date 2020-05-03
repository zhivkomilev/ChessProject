using System;

namespace Chess.Users.Models
{
    public class BaseModel : IBaseModel
    {
        public Guid Id { get; set; }
    }
}
