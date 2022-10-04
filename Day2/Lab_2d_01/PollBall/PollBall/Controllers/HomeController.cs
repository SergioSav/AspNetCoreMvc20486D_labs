using Microsoft.AspNetCore.Mvc;
using PollBall.Services;
using System;
using System.Text;

namespace PollBall.Controllers
{
    public class HomeController : Controller
    {
        private IPollResultsService _pollResults;

        public HomeController(IPollResultsService pollResults)
        {
            _pollResults = pollResults;
        }

        public IActionResult Index()
        {
            var results = new StringBuilder();
            if (Request.Query.ContainsKey("submitted"))
            {
                var voteList = _pollResults.GetVoteResult();
                foreach (var gameVotes in voteList)
                {
                    results.Append($"Game name: {gameVotes.Key}. Votes: {gameVotes.Value}{Environment.NewLine}");
                }
            }
            else
            {
                return Redirect("poll-questions.html");
            }
            return Content(results.ToString());
        }
    }
}
