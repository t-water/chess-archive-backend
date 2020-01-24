using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ChessBackend.Models;
using System.Text.RegularExpressions;
using Newtonsoft.Json;
using ChessBackend.Data;
using Microsoft.AspNetCore.Authorization;

namespace ChessBackend.Controllers
{
    public class PgnController : Controller
    {
        private readonly IPgnRepository pgnRepo;

        public PgnController(IPgnRepository pgnRepo)
        {
            this.pgnRepo = pgnRepo;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var model = await pgnRepo.GetGames();
            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int? id)
        {
            if(id == null)
            {
                return NotFound();
            }

            var model = await pgnRepo.GetGame(id);

            if(model == null)
            {
                return NotFound();
            }

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Pgn model)
        {
            if (ModelState.IsValid)
            {
                await pgnRepo.Update(model);
                return RedirectToAction("index");
            }

            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int? id)
        {
            if(id == null)
            {
                return NotFound();
            }

            var model = await pgnRepo.GetGame(id);

            if(model == null)
            {
                return NotFound();
            }

            return View(model);
        }

        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var model = await pgnRepo.GetGame(id);

            if (model == null)
            {
                return NotFound();
            }

            await pgnRepo.Delete(model);

            return RedirectToAction("index");
        }

        [AllowAnonymous]
        [HttpGet]
        public async Task<IActionResult> GetGame(int id){
            var model = await pgnRepo.GetGame(id);
            string jsonResponse = JsonConvert.SerializeObject(model);
            return Content(jsonResponse, "application/json");
        }

        [AllowAnonymous]
        [HttpGet]
        public async Task<IActionResult> GetGames()
        {
            var model = await pgnRepo.GetGames();
            string jsonResponse = JsonConvert.SerializeObject(model);
            return Content(jsonResponse, "application/json");
        }

        [AllowAnonymous]
        [HttpGet]
        public async Task<IActionResult> GetFeaturedGames()
        {
            var model = await pgnRepo.GetFeaturedGames();
            string jsonResponse = JsonConvert.SerializeObject(model);
            return Content(jsonResponse, "application/json");
        }

        [HttpGet]
        public IActionResult Add()
        {
            var model = new PgnString();
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Add(PgnString text)
        {
            Pgn model;

            try
            {
                model = ParsePGNString(text.Pgn);
                model.WhitePlayerId = text.WhitePlayerId;
                model.BlackPlayerId = text.BlackPlayerId;
                await pgnRepo.Add(model);
            }
            catch(Exception ex)
            {
                return View(text);
            }

            return RedirectToAction("index");
        }

        [AllowAnonymous]
        [HttpPost]
        public IActionResult SubmitText(PgnString text)
        {
            Pgn model;
            string jsonResponse;

            try
            {
                model = ParsePGNString(text.Pgn);
                jsonResponse = JsonConvert.SerializeObject(model);
            }
            catch(Exception ex)
            {
                jsonResponse = ReturnErrorMessage(ex.Message);
            } 
            
            return Content(jsonResponse, "application/json");
        }

        private Pgn ParsePGNString(string pgn)
        {
            pgn = pgn.Trim();
            string[] pgnArray = Regex.Split(pgn, @"(?<=\])\s*(?=1\.\s{0,1}[a-zA-Z])");
            string headers;
            string moves;
            Pgn model;

            if(pgnArray.Length > 2)
            {
                throw new Exception("Invalid Format");
            }
            else
            {
                if(pgnArray.Length == 1)
                {
                    moves = pgnArray[0].Trim();
                    if (!moves.StartsWith("1"))
                    {
                        throw new Exception("Invalid Format");
                    }

                    if (!moves.EndsWith("0-1") && !moves.EndsWith("1-0") && !moves.EndsWith("1/2-1/2"))
                    {
                        throw new Exception("Invalid Format");
                    }

                    model = new Pgn
                    {
                        Moves = moves
                    };
                }
                else if(pgnArray.Length == 2)
                {
                    headers = pgnArray[0];
                    moves = pgnArray[1].Trim();
                    if (!moves.StartsWith("1"))
                    {
                        throw new Exception("Invalid Format");
                    }

                    if (!moves.EndsWith("0-1") && !moves.EndsWith("1-0") && !moves.EndsWith("1/2-1/2"))
                    {
                        throw new Exception("Invalid Format");
                    }

                    if (!headers.Contains("[") && !headers.Contains("]"))
                    {
                        throw new Exception("Invalid Format");
                    }

                    model = new Pgn
                    {
                        Moves = moves
                    };

                    string[] headerArray = Regex.Split(headers, @"\s(?=\[)");
                    foreach (string h in headerArray)
                    {
                        string headerTitle = Regex.Match(h, @"(?<=\[)[a-zA-Z]+").Value;
                        var propertyName = model.GetType().GetProperty(headerTitle);
                        if (propertyName != null)
                        {
                            var headerData = h.Replace("\"", "")
                               .Replace(headerTitle, "")
                               .Replace("[", "")
                               .Replace("]", "")
                               .Trim();
                            propertyName.SetValue(model, headerData);
                        }
                    };
                }
                else
                {
                    throw new Exception("Invalid Format");
                }
            }
            
            return model;
        }
        
        private string ReturnErrorMessage(string message){
            Dictionary<string, string> dict = new Dictionary<string, string>();
            dict.Add("Error", message);
            return JsonConvert.SerializeObject(dict);
        }
    }
}