using System;

namespace OnlineVacationRequestPlatform.BusinessLayer.Models
{
    public class ExtendedBaseModel : BaseModel
    {
        public Guid? CreatorUserId { get; set; }

        public Guid? ModifierUserId { get; set; }
    }
}
