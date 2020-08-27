using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace MyDiplom.Controllers
{
    public class BranchedRailChainController : Controller
    {
        public IActionResult measuringPanelVoltageRelay()
        {
            return View();
        }


        ////////////Start algorithm/////////////////


        public IActionResult VoltageRelayLessVoltageRelayWork()
        {
            return View();
        }

        public IActionResult VoltageRelayMoreVoltageRelayWork()
        {
            return View();
        }

        public IActionResult VoltageRelayOnTransformerMoreThanPost()
        {
            return View();
        }

        public IActionResult VoltageRelayOnTransformerSameAtPost()
        {
            return View();
        }

        public IActionResult VoltageDoesntChangeAfterShutdown()
        {
            return View();
        }

        public IActionResult VoltageGrowSharplyAfterShutdown()
        {
            return View();
        }

        public IActionResult VoltageFeedTransformerPrimaryEqualNull()
        {
            return View();
        }

        public IActionResult VoltageFeedTransformerPrimaryIsNormal()
        {
            return View();
        }

        public IActionResult VoltageFeedTransformerSecondaryEqualNull()
        {
            return View();
        }

        public IActionResult VoltageFeedTransformerSecondaryIsNormal()
        {
            return View();
        }

        public IActionResult VoltageOnReostatFeedEndMoreNormal()
        {
            return View();
        }

        public IActionResult VoltageOnReostatFeedEndLessNormal()
        {
            return View();
        }

        public IActionResult TrackRelayUnderAmperage()
        {
            return View();
        }

        public IActionResult TrackRelayWithoutAmperage()
        {
            return View();
        }

        public IActionResult RelayAnchorAttractsAfterMovingRelay()
        {
            return View();
        }

        public IActionResult RelayAnchorDoesntAttractsAfterMovingRelay()
        {
            return View();
        }

        public IActionResult VoltageOnRelayWindingMoreVoltageRelayWork()
        {
            return View();
        }

        public IActionResult VoltageOnRelayWindingLessVoltageRelayWork()
        {
            return View();
        }

    }
}