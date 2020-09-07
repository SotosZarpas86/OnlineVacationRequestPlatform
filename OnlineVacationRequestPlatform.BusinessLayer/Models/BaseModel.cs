using System;

namespace OnlineVacationRequestPlatform.BusinessLayer.Models
{
    public class BaseModel
    {
        public Guid Id { get; set; }

        public DateTime? DateAdded { get; set; }

        public DateTime? DateModified { get; set; }
    }
}
