using NHISDosageParser.Models;
using System.Threading.Tasks;

namespace NHISDosageParser.Contracts
{
    /// <summary>
    /// NHIS Prescription dosage instruction parser
    /// </summary>
    public interface INhisDosageParser
    {
        /// <summary>
        /// Parses the dosage instructio to
        /// human readable text
        /// </summary>
        /// <param name="dosage">Prescription dosage instruction</param>
        /// <returns>Text representation of dosage instruction</returns>
        string DosageParser(NhisDosageInstruction dosage);

        /// <summary>
        /// Downloads nomenclatures from NHIS 
        /// and saves them to files
        /// </summary>
        /// <returns></returns>
        Task UpdateNomenclatures();
    }
}
