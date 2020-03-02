using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MyCalendarApp.Models;

namespace MyCalendarApp.Controllers
{
    // Controller for Find Events page
    public class FindEventsController : Controller
    {
        private readonly EventService eventService;

        [Route("EventsNearMe")]
        public IActionResult FindEvents()
        {
            return View();
        }
    }
}