using System;
using System.ComponentModel.DataAnnotations;

namespace OnlineVacationRequestPlatform.DataLayer.Entities
{
    public class BaseEntity
    {
        [Key]
        public Guid Id { get; set; }

        private DateTime _dateAdded;
        public DateTime DateAdded { get { return _dateAdded.ToLocalTime(); } set { _dateAdded = value.ToUniversalTime(); } }

        private DateTime _dateModified;
        public DateTime DateModified { get { return _dateModified.ToLocalTime(); } set { _dateModified = value.ToUniversalTime(); } }
    }
}
