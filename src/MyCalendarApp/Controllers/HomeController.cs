using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MyCalendarApp.Models;

namespace MyCalendarApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly EventService EventService;

        public HomeController(EventService eventService)
        {
            this.EventService = eventService;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        [Route("Home/GetEvents")]
        // Return all user-saved events
        public JsonResult GetEvents()
        {
            List<Event> events = EventService.GetEvents();
            return Json(events);
        }

        [HttpPost]
        [Route("Home/SaveEvent")]
        // Save the created/updated event to "event database" (aka JSON file)
        public JsonResult SaveEvent(Event e) {
            bool status = false;

            // Notify the event service to save the event if a valid one has been created by the user
            if (e != null)
            {
                e.IsCustomEvent = true;
                EventService.SaveEvent(e); // EventService will update or create the new event accordingly before saving
                status = true;
            }

            return Json(status);
        }

        [HttpPost]
        [Route("Home/DeleteEvent")]
        // Delete an event from the "event database" (JSON file)
        public JsonResult DeleteEvent(string eventID)
        {
            bool status = false;
            if (EventService.DeleteEvent(eventID)) // return true status if deleting the event is successful
            {
                status = true;
            }
            return Json(status);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
