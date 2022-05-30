namespace NHISDosageParser.Models
{
    /// <summary>
    /// Prescription Dosage Instruction
    /// </summary>
    public class NhisDosageInstruction
    {
        /// <summary>
        /// Sequence of execution
        /// </summary>
        public int? Sequence { get; set; }

        /// <summary>
        /// Dose Quantity Value
        /// </summary>
        public double DoseQuantityValue { get; set; }

        /// <summary>
        /// Dose Quantity Unit
        /// </summary>
        public string DoseQuantityCode { get; set; }

        /// <summary>
        /// Frequency of execution
        /// </summary>
        public int Frequency { get; set; }

        /// <summary>
        /// Period of execution
        /// </summary>
        public double Period { get; set; }

        /// <summary>
        /// Period Unit
        /// </summary>
        public string PeriodUnit { get; set; }

        /// <summary>
        /// Therapy Duration
        /// </summary>
        public decimal? BoundsDuration { get; set; }

        /// <summary>
        /// Therapy Duration Unit
        /// </summary>
        public string BoundsDurationUnit { get; set; }

        /// <summary>
        /// When to Take
        /// </summary>
        public string[] When { get; set; } = new string[0];

        /// <summary>
        /// Offset to When to Take
        /// </summary>
        public int? Offset { get; set; }

        /// <summary>
        /// Text instructions
        /// </summary>
        public string Text { get; set; }

        /// <summary>
        /// Route
        /// </summary>
        public string Route { get; set; }
    }
}