using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PunchIT.Models
{
    public class TimeClock
    {
        public int Id { get; set; }

        [Required]
        public DateTime TimeIn { get; set; }

        public DateTime TimeOut { get; set; }

        [Required]
        public string ApplicationUserId { get; set; }
        public ApplicationUser ApplicationUser { get; set; }

    }
}