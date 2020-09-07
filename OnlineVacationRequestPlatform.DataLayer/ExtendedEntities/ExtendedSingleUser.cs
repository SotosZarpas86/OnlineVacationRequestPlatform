using System;

namespace OnlineVacationRequestPlatform.DataLayer.ExtendedEntities
{
    public class ExtendedSingleUser : ExtendedUser
    {
        public string Password { get; set; }

        public Guid SupervisorId { get; set; }
    }
}
