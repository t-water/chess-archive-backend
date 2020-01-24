using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ChessBackend.Models
{
    public class PlayerGames
    {
        public Player Player {get; set;}
        public IEnumerable<Pgn> Games {get; set;}
    }
}