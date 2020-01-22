using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MyCalendarApp.Models;

namespace MyCalendarApp.Controllers
{
    public class SearchController : Controller
    {
        private readonly EventService eventService;

        public SearchController(EventService eventService)
        {
            this.eventService = eventService;
        }

        public IActionResult SearchEvents()
        {
            return View();
        }
    }
}