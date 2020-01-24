using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ChessBackend.Models;
using ChessBackend.Data;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Authorization;

namespace ChessBackend.Controllers
{
    public class FeedbackController : Controller
    {
        private readonly IFeedbackRepository feedbackRepo;

        public FeedbackController(IFeedbackRepository feedbackRepo)
        {
            this.feedbackRepo = feedbackRepo;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var model = await feedbackRepo.GetFeedbacks();
            return View(model);
        }

        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> Add(Feedback feedback)
        {
            string jsonResponse;
            try
            {
                await feedbackRepo.Add(feedback);
                jsonResponse = JsonConvert.SerializeObject(feedback);
            }
            catch(Exception ex)
            {
                Dictionary<string, string> dict = new Dictionary<string, string>
                {
                    { "Error", ex.Message }
                };
                jsonResponse = JsonConvert.SerializeObject(dict);
            }

            return Content(jsonResponse, "application/json");
        }
    }
}