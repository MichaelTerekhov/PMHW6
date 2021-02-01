using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace Task_3.Models
{
    class Result
    {
        [JsonPropertyName("successful")]
        public int Successful { get; set; }
        [JsonPropertyName("failed")]
        public int Failed { get; set; }
    }
}
