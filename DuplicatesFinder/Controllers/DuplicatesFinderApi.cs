using DuplicatesFinder.Core;
using DuplicatesFinder.HddResources;
using Microsoft.AspNetCore.Mvc;

namespace DuplicatesFinder.Controllers
{
    public class DuplicatesFinder : Controller
    {
        private readonly DuplicateFinder _duplicatesFinder;

        public DuplicatesFinder(DuplicateFinder duplicatesFinder)
        {
            _duplicatesFinder = duplicatesFinder;
        }

        [HttpPost]
        public IActionResult Job([FromBody] string path)
        {
            _duplicatesFinder.StartSearchForDuplicates(new FileSizeResourcesContainer(path));
            return Ok();
        }
    }
}