using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace NHISDosageParser.Models
{
    /// <summary>
    /// Nomenclature model
    /// </summary>
    public class NhisNomenclature
    {
        /// <summary>
        /// Date and time of the last nomenclature update
        /// </summary>
        [JsonProperty("dateUpdated")]
        public DateTime DateUpdated { get; set; }

        /// <summary>
        /// NHIS nomenclature
        /// </summary>
        [JsonProperty("nom")]
        public Dictionary<string, string> Nom { get; set; } = new Dictionary<string, string>();

        /// <summary>
        /// NHIS Nomenclature pluralized form
        /// </summary>
        [JsonProperty("nomPlural")]
        public Dictionary<string, string> NomPlural { get; set; } = new Dictionary<string, string>();
    }
}
