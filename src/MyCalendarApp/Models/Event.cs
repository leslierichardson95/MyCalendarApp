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
        // Unique ID for each event
        [JsonProperty("id", NullValueHandling = NullValueHandling.Ignore)]
        public long Id { get; set; }

        // Name of the event
        [JsonProperty("title", NullValueHandling = NullValueHandling.Ignore)]
        public string Title { get; set; }

        // Description of the event
        [JsonProperty("description", NullValueHandling = NullValueHandling.Ignore)]
        public string Description { get; set; }

        // The color the event will be displayed as in the calendar
        [JsonProperty("color", NullValueHandling = NullValueHandling.Ignore)]
        public string Color { get; set; }

        // Is the event a day/multiple-day event?
        [JsonProperty("isFullDay", NullValueHandling = NullValueHandling.Ignore)]
        public bool IsFullDay { get; set; }

        // The event's start date and start time
        [JsonProperty("startTime", NullValueHandling = NullValueHandling.Ignore)]
        public DateTime StartTime { get; set; }

        // The event's end date and end time
        [JsonProperty("endTime", NullValueHandling = NullValueHandling.Ignore)]
        public DateTime EndTime { get; set; }

        // An image representing the event
        [JsonProperty("image", NullValueHandling = NullValueHandling.Ignore)]
        public string Image { get; set; }

        // Custom tags associated with the event
        [JsonProperty("tags", NullValueHandling = NullValueHandling.Ignore)]
        public List<string> Tags { get; set; }

        // Was the event made by the user (true) or is it from TicketMaster (false)?
        [JsonProperty("isCustomEvent", NullValueHandling = NullValueHandling.Ignore)]
        public bool IsCustomEvent { get; set; }
    }
}