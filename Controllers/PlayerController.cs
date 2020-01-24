using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ChessBackend.Models;
using ChessBackend.Data;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;

namespace ChessBackend.Controllers
{
    public class PlayerController : Controller
    {
        private readonly IPlayerRepository playerRepo;

        public PlayerController(IPlayerRepository playerRepo)
        {
            this.playerRepo = playerRepo;
        }

        public async Task<IActionResult> Index()
        {
            var model = await playerRepo.GetPlayers();
            return View(model);
        }

        [HttpGet]
        public IActionResult Add()
        {
            var model = new Player();
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Add(Player player)
        {
            if (ModelState.IsValid)
            {
                await playerRepo.Add(player);
                return RedirectToAction("index");
            }

            return View(player);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int? id)
        {
            if(id == null)
            {
                return NotFound();
            }

            var model = await playerRepo.GetPlayer(id);

            if(model == null)
            {
                return NotFound();
            }

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Player player)
        {
            if (ModelState.IsValid)
            {
                await playerRepo.Edit(player);
                return RedirectToAction("index");
            }

            return View(player);
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int? id)
        {
            if(id == null)
            {
                return NotFound();
            }

            var model = await playerRepo.GetPlayer(id);

            if(model == null)
            {
                return NotFound();
            }

            return View(model);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int? id)
        {
            if(id == null)
            {
                return NotFound();
            }

            var model = await playerRepo.GetPlayer(id);

            if(model == null)
            {
                return NotFound();
            }

            await playerRepo.Delete(model);

            return RedirectToAction("index");
        }

        [AllowAnonymous]
        [HttpGet]
        public async Task<IActionResult> GetPlayers(string name)
        {
            var model = await playerRepo.GetPlayersFiltered(name);
            string jsonResponse = JsonConvert.SerializeObject(model);
            return Content(jsonResponse, "application/json");
        }

        [AllowAnonymous]
        [HttpGet]
        public async Task<IActionResult> GetPlayer(int id)
        {
            var model = await playerRepo.GetPlayer(id);
            string jsonResponse = JsonConvert.SerializeObject(model);
            return Content(jsonResponse, "application/json");
        }

        [AllowAnonymous]
        [HttpGet]
        public async Task<IActionResult> ViewGames(int id)
        {
            var model = await playerRepo.ViewGames(id);
            string jsonResponse = JsonConvert.SerializeObject(model);
            return Content(jsonResponse, "application/json");
        }

        [AllowAnonymous]
        [HttpGet]
        public async Task<IActionResult> GetFeaturedPlayers(){
            var model = await playerRepo.GetFeaturedPlayers();
            string jsonResponse = JsonConvert.SerializeObject(model);
            return Content(jsonResponse, "application/json");
        }
    }
}