using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ChessBackend.Models;

namespace ChessBackend.Data
{
    public interface IPgnRepository
    {
    	Task Add(Pgn pgn);
		Task Update(Pgn pgn);
		Task Delete(Pgn pgn);
		Task<Pgn> GetGame(int? id);
		Task<IEnumerable<Pgn>> GetFeaturedGames();
        Task<IEnumerable<Pgn>> GetGames();
        Task<IEnumerable<Pgn>> GetGamesFiltered(string eventName);
    }
}
