using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using System.Text.Json;
using Microsoft.AspNetCore.Hosting;

namespace MyCalendarApp.Models
{
    public class EventService
    {
        //public static EventService Singleton;

        //static EventService()
        //{
        //    Singleton = new EventService();
        //}

        private static string relativePath = "~/data/events.json";

        private Dictionary<long, Event> savedEvents = new Dictionary<long, Event>();

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
            if (savedEvents.ContainsKey(e.Id))
            {
                savedEvents[e.Id] = e;
            }
            else
            {
                savedEvents.Add(e.Id, e);
            }

            // Write updates to JSON file
            WriteJson();
        }

        private void WriteJson()
        {
            using (FileStream outputStream = File.OpenWrite(relativePath))
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
