using OnlineVacationRequestPlatform.BusinessLayer.Utilities;
using System;

namespace OnlineVacationRequestPlatform.BusinessLayer.Models
{
    public class VacationRequestStatusModel
    {
        public Guid VacationApplicationId { get; set; }

        public DateTime DateSubmitted { get; set; }

        public RequestStatus RequestStatus { get; set; }
    }
}
