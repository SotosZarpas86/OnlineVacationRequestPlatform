using System;
using System.ComponentModel;
using System.Text.Json.Serialization;

namespace OnlineVacationRequestPlatform.Web.Models
{
    public class UserListViewModel
    {
        public Guid Id { get; set; }

        [DisplayName("First Name")]
        public string FirstName { get; set; }

        [DisplayName("Last Name")]
        public string LastName { get; set; }

        public string Email { get; set; }

        [DisplayName("User Type")]
        public string RoleName { get; set; }

        [JsonIgnore]
        public string FullName => FirstName + " " + LastName;
    }
}
