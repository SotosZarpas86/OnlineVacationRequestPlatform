using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace OnlineVacationRequestPlatform.DataLayer.Entities
{
    [Table("Roles")]
    public class Role : BaseEntity
    {
        public string Name { get; set; }

        public ICollection<User> Users { get; set; }
    }
}
