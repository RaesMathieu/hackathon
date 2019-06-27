using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace AWSLambda1.Models
{
    public class SelectionResult
    {
        [JsonProperty(PropertyName = "selection_id")]
        public int SelectionId { get; set; }
        [JsonProperty(PropertyName = "result")]
        public bool Result { get; set; }
    }
}
