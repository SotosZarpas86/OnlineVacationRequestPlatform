using System;

namespace OnlineVacationRequestPlatform.BusinessLayer.Models
{
    public class ExtendedSingleUserModel : ExtendedUserModel
    {
        public string Password { get; set; }

        public Guid SupervisorId { get; set; }
    }
}
