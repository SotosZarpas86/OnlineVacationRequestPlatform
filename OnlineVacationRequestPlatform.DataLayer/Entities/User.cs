using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace OnlineVacationRequestPlatform.DataLayer.Entities
{
    [Table("Users")]
    public class User : BaseEntity
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Email { get; set; }

        public string Password { get; set; }

        public Guid? SupervisorId { get; set; }

        public Guid RoleId { get; set; }

        public ICollection<VacationRequest> VacationRequests { get; set; }
    }
}
