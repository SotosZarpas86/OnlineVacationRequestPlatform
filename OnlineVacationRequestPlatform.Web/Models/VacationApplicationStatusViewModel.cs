using OnlineVacationRequestPlatform.Web.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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
