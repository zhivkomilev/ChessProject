using System;

namespace Chess.Users.Models.EntityModels
{
    public class BaseModel : IBaseModel
    {
        public Guid Id { get; set; }
    }
}
