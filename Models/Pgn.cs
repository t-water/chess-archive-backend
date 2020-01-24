using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ChessBackend.Models
{
    public class Pgn
    {
        public Pgn()
        {
            Featured = false;
        }

        public int Id { get; set; }
        public string Event { get; set; }
        public string Site { get; set; }
        public string Date { get; set; }
        public string Round { get; set; }
        public string Result { get; set; }
        public string Moves { get; set; }
        public bool Featured { get; set; }

        public int WhitePlayerId { get; set; }

        [Display(Name = "White Player")]
        public Player WhitePlayer { get; set; }

        public int BlackPlayerId { get; set; }

        [Display(Name = "Black Player")]
        public Player BlackPlayer { get; set; }
    }
}