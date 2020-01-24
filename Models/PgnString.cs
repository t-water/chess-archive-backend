using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ChessBackend.Models
{
    public class PgnString
    {
        [DataType(DataType.MultilineText)]
        public string Pgn { get; set; }

        [Display(Name = "White Player")]
        public int WhitePlayerId { get; set; }

        [Display(Name = "Black Player")]
        public int BlackPlayerId { get; set; }
    }
}