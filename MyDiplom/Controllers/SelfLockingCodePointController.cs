using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace MyDiplom.Controllers
{
    public class SelfLockingCodePointController : Controller
    {
        public IActionResult Status_Relay_G()
        {
            return View();
        }

        public IActionResult Relay_G_Under_Umperage()
        {
            return View();
        }

        public IActionResult Relay_G_Without_Umperage()
        {
            return View();
        }

        public IActionResult ImpulseRelayWithoutAmperage()
        {
            return View();
        }

        public IActionResult Path_RelayWitoutVoltage()
        {
            return View();
        }

        public IActionResult Path_RelayVoltageIsFine()
        {
            return View();
        }

        public IActionResult FilterIs_NotOK()
        {
            return View();
        }

        public IActionResult FilterIs_OK()
        {
            return View();
        }
        public IActionResult ImpulseRelayCorruptCode()
        {
            return View();
        }
        
        public IActionResult VoltagePathRelayOverNormal()
        {
            return View();
        }

        public IActionResult VoltagePathRelayIsNormal()
        {
            return View();
        }
        
        public IActionResult ImpulseRelayCodeIsNormal()
        {
            return View();
        }
        
        public IActionResult VoltageCellLessNormal()
        {
            return View();
        }

        public IActionResult VoltageCellIsNormal()
        {
            return View();
        }
        
        public IActionResult SupplyVoltageLessNormal()
        {
            return View();
        }

        public IActionResult SupplyVoltageIsNormal()
        {
            return View();
        }
        
        public IActionResult VoltageOnCellExitLessNormal()
        {
            return View();
        }
        
        public IActionResult VoltageOnCellExitIsNormal()
        {
            return View();
        }
        
        public IActionResult ImpulseRelayWithAmperage()
        {
            return View();
        }
    }
}