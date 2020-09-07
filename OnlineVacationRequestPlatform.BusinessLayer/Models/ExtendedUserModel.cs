using System;

namespace OnlineVacationRequestPlatform.BusinessLayer.Models
{
    public class ExtendedUserModel
    {
        public Guid Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Email { get; set; }

        public string RoleName { get; set; }
    }
}
