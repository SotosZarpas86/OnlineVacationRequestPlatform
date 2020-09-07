using System;
using System.ComponentModel.DataAnnotations;

namespace OnlineVacationRequestPlatform.Web.Models
{
    public class VacationRequestViewModel
    {
        [Required]
        public DateTime DateFrom { get; set; }

        [Required]

        public DateTime DateTo { get; set; }

        [Required]
        public string Reason { get; set; }
    }
}
