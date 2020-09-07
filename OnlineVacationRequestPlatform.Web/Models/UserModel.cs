using System;

namespace OnlineVacationRequestPlatform.Web.Models
{
    public class UserModel
    {
        public Guid Id { get; set; }

        public DateTime? DateAdded { get; set; } = DateTime.Now;

        public DateTime? DateModified { get; set; } = DateTime.Now;

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Email { get; set; }

        public string Password { get; set; }

        public Guid RoleId { get; set; }

        public Guid? SupervisorId { get; set; }
    }
}
