using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ChessBackend.Models
{
    public class Feedback
    {
        public Feedback()
        {
            DateSent = DateTime.Now;
        }
        public int Id { get; set; }

        [Display(Name = "Date Sent")]
        public DateTime DateSent { get; set; }
        public string Email { get; set; }
        public string Comment { get; set; }
    }
}
