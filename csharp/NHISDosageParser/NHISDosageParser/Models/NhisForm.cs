using Newtonsoft.Json;

namespace NHISDosageParser.Models
{
    /// <summary>
    /// Text values
    /// </summary>
    public class NhisForm
    {
        /// <summary>
        /// Single form
        /// </summary>
        [JsonProperty("single")]
        public string Single { get; set; }

        /// <summary>
        /// Plural form
        /// </summary>
        [JsonProperty("plural")]
        public string Plural { get; set; }
    }
}