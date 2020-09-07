using System;

namespace OnlineVacationRequestPlatform.BusinessLayer.Models
{
    public class UserModel : BaseModel
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Email { get; set; }

        public string Password { get; set; }

        public Guid RoleId { get; set; }

        public Guid? SupervisorId { get; set; }
    }
}
