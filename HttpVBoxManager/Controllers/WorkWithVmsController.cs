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
    public class WorkWithVmsController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet("StartVM/{name}")]
        public IActionResult StartVM(string name)
        {
            string vm = Services.CmdLinker.startVM(name);
            Dictionary<string, string> keyValuePairs = new Dictionary<string, string>();
            keyValuePairs.Add(name, vm);
            return Json(keyValuePairs);
        }


        [HttpGet("PowerOffVM/{name}")]
        public IActionResult PowerOffVM(string name)
        {
            string vm = Services.CmdLinker.PowerOffVM(name);
            Dictionary<string, string> keyValuePairs = new Dictionary<string, string>();
            keyValuePairs.Add(name, "Powering Off");
            return Json(keyValuePairs);
        }

        [HttpGet("modifyvm/{name}/{cpu}/{memory}")]
        public IActionResult modifyVM(string name, int cpu, int memory)
        {
            string vm = Services.CmdLinker.ModifyVM(name, cpu, memory);
            string a = "{command: setting, vmName: " + name + ",cpu: " + cpu + ",memory: " + memory + " ,status:  OK" + " }";

            return Json(a);
        }

        [HttpGet("cloneVM/{sname}/{dname}")]
        public IActionResult cloneVM(string sname, string dname)
        {
            string vm = Services.CmdLinker.CloneVM(sname, dname);
            string a = "{command: clone, vm1: " + sname + ", vm2: " + dname + " ,status:  OK" + " }";
            return Json(a);
        }

        [HttpGet("DeleteVM/{sname}")]
        public IActionResult DeleteVM(string sname)
        {
            string vm = Services.CmdLinker.DeleteVM(sname);
            string a = "{command: delete, vmName: " + sname + " ,status:  OK" + " }";
            return Json(a);
        }


        [HttpGet("ExecuteCommandVM/{sname}/{userName}/{password}/{command}")]
        public IActionResult ExecuteCommandVM(string sname, string userName, string password, string command)
        {
            string vm = Services.CmdLinker.ExecuteCommandVM(sname, userName, password, command);
            string a = "{command: execute, vmName: " + sname + " ,status:  OK" + ",response " + vm + " }";
            return Json(a);
        }

        [HttpGet("sendfileFromHostToVM/{sname}/{hostPath}/{userName}/{password}/{vmPath}")]
        public IActionResult sendfileFromHostToVM(string sname, string hostPath, string vmPath, string userName, string password)
        {
            string vm = Services.CmdLinker.sendfileFromHostToVM(sname, hostPath, vmPath, userName, password);
            string a = "{command: transfer, OriginVM: " + sname + " ,originPath: " + hostPath + " ,status:  OK" + ",response " + vm + " }";
            return Json(a);
        }

        [HttpGet("sendfileFromVmToVM/{sname}/{hostPath}/{userName}/{password}/{dname}")]
        public IActionResult sendfileFromVmToVM(string sname, string hostPath, string vmPath, string userName, string password, string dname)
        {
            string vm = Services.CmdLinker.sendfileFromVmToVM(sname, hostPath, vmPath, userName, password, dname);
            string a = "{command: transfer, OriginVM: " + sname + " ,originPath: " + hostPath + " ,destVM: " + dname + ",destPath: " + vmPath + " ,status:  OK" + ",response " + vm + " }";
            return Json(a);
        }

    }
}
