using OnlineVacationRequestPlatform.Web.Utilities;
using System;

namespace OnlineVacationRequestPlatform.Web.Models
{
    public class VacationApplicationStatusViewModel
    {
        public Guid VacationApplicationId { get; set; }

        public Guid ApplicantId { get; set; }

        public DateTime DateSubmitted { get; set; }

        public RequestStatus RequestStatus { get; set; }
    }
}
