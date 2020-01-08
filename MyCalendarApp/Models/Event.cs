using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyCalendarApp.Models
{
    // Class representing custom events and events added from Ticketmaster API
    public class Event
    {
        // Name of the event
        [JsonProperty("title", NullValueHandling = NullValueHandling.Ignore)]
        private string Title { get; set; }

        // Description of the event
        [JsonProperty("description", NullValueHandling = NullValueHandling.Ignore)]
        private string Description { get; set; }

        // The color the event will be displayed as in the calendar
        [JsonProperty("color", NullValueHandling = NullValueHandling.Ignore)]
        private string Color { get; set; }

        // Is the event a day/multiple-day event?
        [JsonProperty("isFullDay", NullValueHandling = NullValueHandling.Ignore)]
        private bool IsFullDay { get; set; }

        // The event's start date and start time
        [JsonProperty("startTime", NullValueHandling = NullValueHandling.Ignore)]
        private DateTime StartTime { get; set; }

        // The event's end date and end time
        [JsonProperty("endTime", NullValueHandling = NullValueHandling.Ignore)]
        private DateTime EndTime { get; set; }

        // An image representing the event
        [JsonProperty("image", NullValueHandling = NullValueHandling.Ignore)]
        private string Image { get; set; }

        // Custom tags associated with the event
        [JsonProperty("tags", NullValueHandling = NullValueHandling.Ignore)]
        private List<string> Tags { get; set; }

        // Was the event made by the user (true) or is it from TicketMaster (false)?
        [JsonProperty("isCustomEvent", NullValueHandling = NullValueHandling.Ignore)]
        private bool IsCustomEvent { get; set; }
    }
}