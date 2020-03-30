using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using MyCalendarApp.Controllers;
using MyCalendarApp.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyCalendarApp.Services
{
    /// <summary>
    /// A service responsible for handling TicketMaster API calls (See https://developer.ticketmaster.com/products-and-docs/apis/getting-started/ for more info)
    /// </summary>
    public class TicketmasterService
    {
        // a property responsible for handling API calls
        private HttpHelper httpHelper; 

        // a property containing the base URL for Ticketmaster API calls
        private const string rootURL = "https://app.ticketmaster.com/discovery/v2/";

        // Key required for Ticketmaster API calls
        private const string apiKey = "RdntbKoAQviA8C7YsmLAGKUzNFlnu1HZ";

        public int CityCode { get; set; }

        public TicketmasterService(int cityCode)
        {
            // Initialize HTTP Helper with base API URL
            httpHelper = new HttpHelper(rootURL);
            CityCode = cityCode;
        }

        /// <summary>
        /// Get all upcoming events in the U.S.
        /// </summary>
        public async Task<List<Event>> GetUsaEvents()
        {
            string urlExt = "events.json?countryCode=US&apikey=" + apiKey;
            string eventsJson = await httpHelper.GetAsStringAsync(urlExt);

            return JsonConvert.DeserializeObject<List<Event>>(eventsJson);
        }

        /// <summary>
        /// Get all events in specified city area
        /// </summary>
        /// <param name="cityCode"></param>
        /// <returns></returns>
        public async Task<List<TmEvent>> GetEventsByLocation()
        {
            string urlExt = "events.json?dmaId=" + CityCode + "&apikey=" + apiKey;
            string eventsJson = await httpHelper.GetAsStringAsync(urlExt);

            List<TmEvent> events = ParseEvents(eventsJson);

            return events;
        }

        /// <summary>
        /// Search for specific events based on your location
        /// </summary>
        /// <param name="keyword"></param>
        /// <returns></returns>
        public async Task<List<TmEvent>> SearchEvents(string keyword)
        {
            string urlExt = "events.json?dmaId=" + CityCode + "&keyword=" + keyword + "&apikey=" + apiKey;
            string eventsJson = await httpHelper.GetAsStringAsync(urlExt);

            List<TmEvent> events = ParseEvents(eventsJson);
            return events;
        }

        private List<TmEvent> ParseEvents(string json)
        {
            try
            {
                JToken embToken = JObject.Parse(json).SelectToken("_embedded"); // returns "_embedded" JSON field
                string eventsStr = embToken.SelectToken("events").ToString(); // returns "events" JSON field
                return JsonConvert.DeserializeObject<List<TmEvent>>(eventsStr);
            }
            catch (NullReferenceException)
            {
                return null;
            }
        }
    }
}
