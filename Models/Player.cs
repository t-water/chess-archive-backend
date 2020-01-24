using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ChessBackend.Models
{
    public class Player
    {
        public Player()
        {
            Featured = false;
        }

        public int PlayerId { get; set; }

        [Required]
        [Display(Name = "First Name")]
        public string FirstName { get; set; }

        [Required]
        [Display(Name = "Last Name")]
        public string LastName { get; set; }

        [Display(Name = "Birth Date")]
        [DataType(DataType.Date)]
        public DateTime BirthDate { get; set; }

        [Display(Name = "Death Date")]
        [DataType(DataType.Date)]
        public DateTime? DeathDate { get; set; }

        public string Country { get; set; }
        public string ImageSrc { get; set; }
        public bool Featured { get; set; }
        public string FullName
        {
            get
            {
                return FirstName + " " + LastName;
            }
        }
    }
}
