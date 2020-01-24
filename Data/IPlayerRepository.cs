using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ChessBackend.Models;

namespace ChessBackend.Data
{
    public interface IPlayerRepository
    {
        Task Add(Player player);
        Task<Player> Edit(Player player);
        Task Delete(Player player);
        Task<IEnumerable<Player>> GetPlayers();
        Task<IEnumerable<Player>> GetPlayersFiltered(string name);
        Task<Player> GetPlayer(int? id);
        Task<PlayerGames> ViewGames(int id);
        Task<IEnumerable<Player>> GetFeaturedPlayers();
    }
}
