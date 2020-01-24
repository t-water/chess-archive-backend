using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ChessBackend.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace ChessBackend.Data
{
    public class AppDbContext : IdentityDbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }

        public DbSet<Pgn> Pgns { get; set; }
        public DbSet<Player> Players { get; set; }
        public DbSet<Feedback> Feedbacks { get; set; }
    }
}
