using System;

namespace OnlineVacationRequestPlatform.DataLayer.Entities
{
    public class BaseExtendedEntity : BaseEntity
    {
        public Guid? CreatorUserId { get; set; }

        public Guid? ModifierUserId { get; set; }
    }
}
