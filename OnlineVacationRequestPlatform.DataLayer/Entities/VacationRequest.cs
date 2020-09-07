using OnlineVacationRequestPlatform.DataLayer.Utilities;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace OnlineVacationRequestPlatform.DataLayer.Entities
{
    [Table("VacationRequests")]
    public class VacationRequest : BaseExtendedEntity
    {
        private DateTime _dateSubmitted;
        public DateTime DateSubmitted { get { return _dateSubmitted.ToLocalTime(); } set { _dateSubmitted = value.ToUniversalTime(); } }

        private DateTime _vacationStartDate;
        public DateTime VacationStartDate { get { return _vacationStartDate.ToLocalTime(); } set { _vacationStartDate = value.ToUniversalTime(); } }

        private DateTime _vacationEndTime;
        public DateTime VacationEndDate { get { return _vacationEndTime.ToLocalTime(); } set { _vacationEndTime = value.ToUniversalTime(); } }

        public int DaysRequested { get; set; }

        public string Reason { get; set; }

        public RequestStatus RequestStatus { get; set; }

        public Guid UserId { get; set; }
    }
}
