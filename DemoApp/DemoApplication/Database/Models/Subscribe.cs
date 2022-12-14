using DemoApplication.Database.Models.Common;

namespace DemoApplication.Database.Models
{
    public class Subscribe : BaseEntity, IAuditable
    {
      

        public string EmailAdress { get; set; }
        public DateTime CreatedAt { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public DateTime UpdatedAt { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
    }
}
