using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace OnlineVacationRequestPlatform.Web.Models
{
    public class ExtendedUserViewModel
    {
        public Guid Id { get; set; }

        [DisplayName("First Name")]
        public string FirstName { get; set; }

        [DisplayName("Last Name")]
        public string LastName { get; set; }

        public string Email { get; set; }

        public Guid RoleId { get; set; }

        public Guid? SupervisorId { get; set; }

        public string Password { get; set; }

        [JsonIgnore]
        [Compare("Password")]
        [DisplayName("Confirm Password")]
        public string ConfirmPassword { get; set; }

        [JsonIgnore]
        public SelectList AvailableRoles { get; set; }

        [JsonIgnore]
        public SelectList AvailableSupervisors { get; set; }
    }
}
