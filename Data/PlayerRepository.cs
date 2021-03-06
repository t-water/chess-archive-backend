﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ChessBackend.Models;
using Microsoft.EntityFrameworkCore;

namespace ChessBackend.Data
{
    public class PlayerRepository : IPlayerRepository
    {
        private readonly AppDbContext _context;

        public PlayerRepository(AppDbContext context)
        {
            _context = context;
        }
        public async Task Add(Player player)
        {
            _context.Players.Add(player);
            await _context.SaveChangesAsync();
        }

        public async Task<Player> Edit(Player player)
        {
            _context.Players.Update(player);
            await _context.SaveChangesAsync();
            return player;
        }

        public async Task Delete(Player player)
        {
            _context.Players.Remove(player);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Player>> GetPlayers()
        {
            return await _context.Players.ToListAsync();
        }

        public async Task<IEnumerable<Player>> GetPlayersFiltered(string name)
        {
            if (!string.IsNullOrEmpty(name))
            {
                return await _context.Players.Where(p => p.FirstName.StartsWith(name) || p.LastName.StartsWith(name) || (p.FirstName + " " + p.LastName).StartsWith(name)).ToListAsync();
            }

            return await _context.Players.ToListAsync();
        }

        public async Task<Player> GetPlayer(int? id)
        {
            return await _context.Players.FirstOrDefaultAsync(p => p.PlayerId == id);
        }

        public async Task<PlayerGames> ViewGames(int id)
        {
            var model = new PlayerGames();
            model.Player = await _context.Players.FirstOrDefaultAsync(p => p.PlayerId == id);
            model.Games = _context.Pgns.Where(g => g.WhitePlayerId == id || g.BlackPlayerId == id).Include(g => g.WhitePlayer).Include(g => g.BlackPlayer);
            return model;
        }

        public async Task<IEnumerable<Player>> GetFeaturedPlayers(){
            return await _context.Players.Where(p => p.Featured).ToListAsync();
        }
    }
}
