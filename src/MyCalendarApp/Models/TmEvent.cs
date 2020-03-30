using Microsoft.CodeAnalysis.Classification;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

namespace MyCalendarApp.Models
{
    public class TmEvent
    {
        [JsonProperty("id", NullValueHandling = NullValueHandling.Ignore)]
        public string Id { get; set; }

        [JsonProperty("name", NullValueHandling = NullValueHandling.Ignore)]
        public string Name { get; set; }

        [JsonProperty("url", NullValueHandling = NullValueHandling.Ignore)]
        public string Url { get; set; }

        [JsonProperty("dates", NullValueHandling = NullValueHandling.Ignore)]
        public TmDate Date { get; set; }

        [JsonProperty("classifications", NullValueHandling = NullValueHandling.Ignore)]
        public List<TmClassification> Classifications { get; set; }
    }

    public partial class TmDate
    {
        [JsonProperty("start", NullValueHandling = NullValueHandling.Ignore)]
        public TmStart Start { get; set; }

        [JsonProperty("end", NullValueHandling = NullValueHandling.Ignore)]
        public TmEnd End { get; set; }
    }

    public partial class TmStart
    {
        [JsonProperty("dateTime", NullValueHandling = NullValueHandling.Ignore)]
        public DateTime DateTime { get; set; }

        [JsonProperty("localDate", NullValueHandling = NullValueHandling.Ignore)]
        public DateTime LocalDate { get; set; }

        [JsonProperty("localTime", NullValueHandling = NullValueHandling.Ignore)]
        public DateTime LocalTime { get; set; }
    }

    public partial class TmEnd
    {
        [JsonProperty("datetime", NullValueHandling = NullValueHandling.Ignore)]
        public DateTime DateTime { get; set; }

        [JsonProperty("localDate", NullValueHandling = NullValueHandling.Ignore)]
        public DateTime LocalDate { get; set; }

        [JsonProperty("localTime", NullValueHandling = NullValueHandling.Ignore)]
        public DateTime LocalTime { get; set; }
    }

    public partial class TmClassification
    {
        [JsonProperty("primary", NullValueHandling = NullValueHandling.Ignore)]
        public bool Primary { get; set; }

        [JsonProperty("segment", NullValueHandling = NullValueHandling.Ignore)]
        public TmSegment Segment { get; set; }
    }

    public partial class TmSegment
    {
        [JsonProperty("id", NullValueHandling = NullValueHandling.Ignore)]
        public string Id { get; set; }

        [JsonProperty("name", NullValueHandling = NullValueHandling.Ignore)]
        public string Name { get; set; }
    }
}
