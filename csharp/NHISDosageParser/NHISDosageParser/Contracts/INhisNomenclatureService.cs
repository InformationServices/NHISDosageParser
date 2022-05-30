using NHISDosageParser.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace NHISDosageParser.Contracts
{
    /// <summary>
    /// Loads and updates 
    /// NHIS Dosage Parser nomenclatures
    /// </summary>
    public interface INhisNomenclatureService
    {
        /// <summary>
        /// Load nomenclatures from files
        /// </summary>
        /// <returns>Tuple with loaded nomenclatures</returns>
        (NhisNomenclature cl013, NhisNomenclature cl020, NhisNomenclature cl034, NhisNomenclature cl035) LoadNomenclatures();
        
        /// <summary>
        /// Downloads nomenclatures from NHIS 
        /// and saves them to files
        /// </summary>
        /// <returns></returns>
        Task UpdateNomenclatures();

        /// <summary>
        /// Load Medicine dosage form from file
        /// </summary>
        /// <returns></returns>
        Dictionary<string, NhisForm> LoadMedicineDosageForm();
    }
}
