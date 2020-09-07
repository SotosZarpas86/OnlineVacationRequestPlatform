using OnlineVacationRequestPlatform.Web.Utilities;
using System;
using System.ComponentModel;

namespace OnlineVacationRequestPlatform.Web.Models
{
    public class VacationApplicationViewModel
    {
        public Guid Id { get; set; }

        [DisplayName("Date Submitted")]
        public DateTime DateSubmitted { get; set; }

        [DisplayName("Vacation Start Date")]
        public DateTime VacationStartDate { get; set; }

        [DisplayName("Vacation End Date")]
        public DateTime VacationEndDate { get; set; }

        [DisplayName("Days Requested")]
        public int DaysRequested { get; set; }

        [DisplayName("Request Status")]
        public RequestStatus RequestStatus { get; set; }

        public string Reason { get; set; }

        public Guid UserId { get; set; }
    }
}
