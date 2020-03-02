using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MyCalendarApp.Models;

namespace MyCalendarApp.Controllers
{
    // Controller for My Events page
    public class MyEventsController : Controller
    {
        private readonly EventService EventService;

        public MyEventsController(EventService eventService)
        {
            this.EventService = eventService;
        }

        [Route("MyEvents")]
        public IActionResult MyEvents()
        {
            List<Event> myEvents = EventService.GetEvents();

            ViewData["MyEvents"] = myEvents;
            ViewData["Title"] = "MyEvents";

            return View(myEvents);
        }

        [HttpPost]
        [Route("MyEvents/DeleteEvent")]
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
    }
}