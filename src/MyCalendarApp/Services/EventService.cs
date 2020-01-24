using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using System.Text.Json;
using Microsoft.AspNetCore.Hosting;
using System.Diagnostics.Tracing;

namespace MyCalendarApp.Models
{
    public class EventService
    {

        private Dictionary<string, Event> savedEvents = new Dictionary<string, Event>();

        private string JsonFileName
        {
            get { return Path.Combine(WebHostEnvironment.WebRootPath, "data", "events.json"); }
        }

        public IWebHostEnvironment WebHostEnvironment { get; }

        public EventService(IWebHostEnvironment webHostEnvironment)
        {
            WebHostEnvironment = webHostEnvironment;

            List<Event> eventList;
            using (StreamReader r = new StreamReader(JsonFileName))
            {
                string json = r.ReadToEnd();
                eventList = JsonConvert.DeserializeObject<List<Event>>(json);
            }

            // convert list of events to dictionary
            savedEvents = eventList.ToDictionary(x => x.Id, x => x);
        }

        // Returns a list of all saved events
        public List<Event> GetEvents()
        {
            return savedEvents.Values.ToList();
        }

        // Save/update event to JSON file
        public void SaveEvent(Event e)
        {
            // If a saved event already exists, just update the existing event, else add the event to savedEvents dictionary
            if (e.Id != null) // check if a new event has just been created
            {
                if (savedEvents.ContainsKey(e.Id)) // if the event already exists, update it
                {
                    savedEvents[e.Id] = e;
                }
            }
            else // if the event is new, create a unique ID and add to savedEvents
            {
                e.Id = Guid.NewGuid().ToString(); // generate unique GUID ID for new events
                savedEvents.Add(e.Id, e);
            }

            // Write updates to JSON file
            WriteJson();
        }

        // Delete event from JSON file
        public bool DeleteEvent(string eventID)
        {
            if (eventID == null || eventID == "") // return false if the event doesn't exist
            { 
                return false; 
            }
            else
            {
                savedEvents.Remove(eventID); // remove event from event dictionary
                WriteJson(); // update JSON file
                return true;
            }
        } 

        // Update JSON file
        private void WriteJson()
        {
            using (FileStream outputStream = File.OpenWrite(JsonFileName))
            {
                System.Text.Json.JsonSerializer.Serialize<IEnumerable<Event>>(
                    new Utf8JsonWriter(outputStream, new JsonWriterOptions
                    {
                        SkipValidation = true,
                        Indented = true
                    }),
                    GetEvents()
                );
            }
        }
    }
}
