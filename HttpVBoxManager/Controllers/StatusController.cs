using HttpVBoxManager.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HttpVBoxManager.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class StatusController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet("[action]")]
        public IActionResult GetVmsStatus()
        {
            Dictionary<string, string> allvms = Services.CmdLinker.AllDevsStatus();

            return Json(allvms);
        }

        [HttpGet("GetVmStatus/{name}")]
        public IActionResult GetVmStatus(string name)
        {
            string vm = Services.CmdLinker.getVmStatus(name);
            Dictionary<string, string> keyValuePairs = new Dictionary<string, string>();
            keyValuePairs.Add(name, vm);

            return Json(keyValuePairs);
        }



    }
}
