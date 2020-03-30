using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MyCalendarApp.Models;
using MyCalendarApp.Services;

namespace MyCalendarApp.Controllers
{
    // Controller for Find Events page
    public class FindEventsController : Controller
    {
        private readonly EventService EventService;
        private readonly TicketmasterService TmService;
        private static List<TmEvent> eventsNearMe;
        private static string searchKeywords = "";

        public FindEventsController(EventService eventService)
        {
            this.EventService = eventService;
            TmService = new TicketmasterService(385); // TODO: SET RELEVANT CITY CODE HERE (ie. SEATTLE = 385)
        }

        [Route("EventsNearMe")]
        public async Task<IActionResult> FindEvents()
        {
            eventsNearMe = await TmService.GetEventsByLocation(); //retrieve Seattle events

            ViewData["FindEvents"] = eventsNearMe;
            ViewData["Title"] = "MyEvents";

            return View(eventsNearMe);
        }

        [Route("EventsNearMe/AddToMyEvents/{id}/{isFromSearch}")]
        public IActionResult AddToMyEvents(string id, bool isFromSearch)
        {
            TmEvent tmEvent = null;
            for (int i = 0; i < eventsNearMe.Count; i++)
            {
                if (eventsNearMe[i].Id == id)
                {
                    tmEvent = eventsNearMe[i];
                    break;
                }
            }
            EventService.ConvertTmEvent(tmEvent);
            TempData["Message"] = tmEvent.Name + " event added to My Events successfully.";

            if (isFromSearch)
            {
                ViewData["Keywords"] = searchKeywords;
                return Redirect("/EventsNearMe/SearchResults/" + searchKeywords);
            }
            else
            {
                searchKeywords = "";
                return Redirect("/EventsNearMe");
            }
        }

        [Route("/EventsNearMe/SearchResults")]
        [Route("/EventsNearMe/SearchResults/{keywords}")]
        public async Task<IActionResult> SearchResults(string keywords)
        {
            if (keywords == "") // if no search was made, show no results found
            {
                ViewData["ResultCount"] = 0;
                eventsNearMe = null;
                return View(eventsNearMe);
            }
            
            searchKeywords = keywords;
            eventsNearMe = await TmService.SearchEvents(searchKeywords);

            ViewData["SearchResults"] = eventsNearMe;
            ViewData["Title"] = "SearchResults";
            ViewData["Keywords"] = searchKeywords;

            if (eventsNearMe != null) // if no results have been found
            {
                ViewData["ResultCount"] = eventsNearMe.Count;
            }
            else
            {
                ViewData["ResultCount"] = 0;
            }

            return View(eventsNearMe);
        }
    }
}