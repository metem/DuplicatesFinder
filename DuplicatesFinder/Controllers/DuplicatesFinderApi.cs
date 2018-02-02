using System.IO;
using DuplicatesFinder.Core;
using DuplicatesFinder.HddResources;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;

namespace DuplicatesFinder.Controllers
{
    public class DuplicatesFinder : Controller
    {
        private readonly DuplicateFinder _duplicatesFinder;
        private readonly IHubContext<DuplicatesFinderHub> _hubContext;

        public DuplicatesFinder(DuplicateFinder duplicatesFinder, IHubContext<DuplicatesFinderHub> hubContext)
        {
            _duplicatesFinder = duplicatesFinder;
            _hubContext = hubContext;
        }

        [HttpPost]
        public IActionResult Job([FromBody] string path)
        {
            if (string.IsNullOrEmpty(path) || !Directory.Exists(path))
            {
                return BadRequest("Directory does not exist");
            }

            var jobId = _duplicatesFinder.StartSearchForDuplicates(new FileSizeResourcesContainer(path));
            _hubContext.Clients.All.InvokeAsync("JobStarted", new {path, jobId});

            return Accepted(jobId);
        }
    }
}