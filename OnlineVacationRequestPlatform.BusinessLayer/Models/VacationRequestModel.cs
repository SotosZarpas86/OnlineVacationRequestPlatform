using OnlineVacationRequestPlatform.BusinessLayer.Utilities;
using System;
using System.Collections.Generic;
using System.Text;

namespace OnlineVacationRequestPlatform.BusinessLayer.Models
{
    public class VacationRequestModel : ExtendedBaseModel
    {
        public DateTime DateSubmitted { get; set; }

        public DateTime VacationStartDate { get; set; }

        public DateTime VacationEndDate { get; set; }

        public int DaysRequested { get; set; }

        public string Reason { get; set; }

        public RequestStatus RequestStatus { get; set; }

        public Guid UserId { get; set; }
    }
}
