using NHISDosageParser.Contracts;
using NHISDosageParser.Models;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace NHISDosageParser.Services
{
    /// <summary>
    /// NHIS Prescription dosage instruction parser
    /// </summary>
    public class NhisDosageParserService : INhisDosageParser
    {
        /// <summary>
        /// Loads and updates 
        /// NHIS Dosage Parser nomenclatures
        /// </summary>
        private readonly INhisNomenclatureService nomenclatureService;

        /// <summary>
        /// CL013 - Medicine intake routes
        /// </summary>
        public NhisNomenclature Cl013 { get; set; }

        /// <summary>
        /// CL020 - Time units
        /// </summary>
        public NhisNomenclature Cl020 { get; set; }

        /// <summary>
        /// CL034 - Time of intake of medical product
        /// </summary>
        public NhisNomenclature Cl034 { get; set; }

        /// <summary>
        /// CL035 - Medicine dosage form
        /// </summary>
        public NhisNomenclature Cl035 { get; set; }

        /// <summary>
        /// Subset of Medicine dosage form
        /// for use with Dosage Parser
        /// </summary>
        public Dictionary<string, NhisForm> Forms { get; set; }

        /// <summary>
        /// Dependancy injection and 
        /// Initialization of nomenclatures
        /// </summary>
        /// <param name="_nomenclatureService">Nomenclature service injected</param>
        public NhisDosageParserService(INhisNomenclatureService _nomenclatureService)
        {
            nomenclatureService = _nomenclatureService;
            (Cl013, Cl020, Cl034, Cl035) = nomenclatureService.LoadNomenclatures();
            Forms = _nomenclatureService.LoadMedicineDosageForm();
        }

        /// <summary>
        /// Parses the dosage instructio to
        /// human readable text
        /// </summary>
        /// <param name="dosage">Prescription dosage instruction</param>
        /// <returns>Text representation of dosage instruction</returns>
        public string DosageParser(NhisDosageInstruction dosage)
        {
            // Full formula:
            //  A + B + (if K then K else skip) + C + "на" + (if D>1 then "всеки" else skip) + D + Е
            //  + (if H then ( if I then I + "минути" else skip) + H else "")
            //  + (if F then "за" + F + G else skip)
            //  + (if J then "\n" + J else skip)
            StringBuilder dosageInstructions = new StringBuilder();

            dosageInstructions.AppendFormat("{0} ", dosage.DoseQuantityValue);

            if (dosage.DoseQuantityCode != null && Cl035.Nom.ContainsKey(dosage.DoseQuantityCode))
            {
                dosageInstructions.AppendFormat("{0} ", 
                    ConvertDosageFormToText(dosage.DoseQuantityCode, dosage.DoseQuantityValue <= 1));
            }
            else if (dosage.DoseQuantityCode != null)
            {
                dosageInstructions.AppendFormat("{0} ", dosage.DoseQuantityCode);
            }

            //Processing K (optional):
            // route (K) -> represent as CL013 description
            if (string.IsNullOrEmpty(dosage.Route) == false && 
                Cl013.Nom.ContainsKey(dosage.Route))
            {
                dosageInstructions.AppendFormat("{0} ", Cl013.Nom[dosage.Route]);
            }

            // Processing C:
            // frequency(C)->represent as: if C = 1 then "веднъж" else as a number + "пъти"
            if (dosage.Frequency == 1)
            {
                dosageInstructions.Append("веднъж ");
            }
            else
            {
                dosageInstructions.AppendFormat("{0} пъти ", dosage.Frequency);
            }

            dosageInstructions.Append("на ");

            if (dosage.Period > 1)
            {
                dosageInstructions.AppendFormat("всеки {0} ", dosage.Period);
            }

            dosageInstructions.AppendFormat("{0} ", ConvertTimeToText(dosage.PeriodUnit, dosage.Period == 1));

            if (dosage.When != null && dosage.When.Length > 0)
            {
                dosageInstructions.AppendFormat("{0} {1}", 
                    ConvertOffsetToText(dosage.Offset),
                    ConcatWhens(dosage.When));
            }

            // Processing F and G(optional):
            // boundsDuration(F)->represent as a number
            //  boundsDurationUnit(G) -> represent as textual description:
            //    ->s-> if F = 1 then "секунда" else if F > 1 then "секунди"
            //    ->min-> if F = 1 then "минута" else if F > 1 then "минути"
            //    ->h-> if F = 1 then "час" else if F > 1 then "часа"
            //    ->d-> if F = 1 then "ден" else if F > 1 then "дни"
            //    ->wk-> if F = 1 then "седмица" else if F > 1 then "седмици"
            //    ->mo-> if F = 1 then "месец" else if F > 1 then "месеца"
            //    ->a-> if F = 1 then "година" else if F > 1 then "години"
            if (dosage.BoundsDuration > 0)
            {
                dosageInstructions.AppendFormat("за {0} {1} ", 
                    dosage.BoundsDuration,
                    ConvertTimeToText(dosage.BoundsDurationUnit, dosage.BoundsDuration == 1));
            }

            string result = dosageInstructions.ToString();

            if (result.Length > 1)
            {
                result = result[0].ToString().ToUpper() + result.Substring(1).ToLower();
            }

            // Processing J(optional):
            // text(J)->represent directly below the rest of the dosage instructions
            if (string.IsNullOrEmpty(dosage.Text) == false)
            {
                result = string.Format("{0}\n\r{1}", result, dosage.Text);
            }

            return result;
        }

        /// <summary>
        /// Downloads nomenclatures from NHIS 
        /// and saves them to files
        /// </summary>
        /// <returns></returns>
        public async Task UpdateNomenclatures()
        {
            await nomenclatureService.UpdateNomenclatures();
        }

        /// <summary>
        /// Processing A and B:
        ///  doseQuantityValue(A) -> represent as a number
        ///  doseQuantityCode(B)
        ///   -> represent as UCUM unit without brackets
        ///   -> represent as CL035 description
        /// </summary>
        /// <param name="form">form value</param>
        /// <param name="isSigle">single or plural form</param>
        /// <returns></returns>
        private string ConvertDosageFormToText(string form, bool isSigle)
        {
            string result = string.Empty;

            if (string.IsNullOrEmpty(form) == false)
            {
                if (isSigle)
                {
                    result = Forms.ContainsKey(form) ? Forms[form].Single : Forms["default"].Single;
                }
                else
                {
                    result = Forms.ContainsKey(form) ? Forms[form].Plural : Forms["default"].Plural;
                }
            }

            return result;
        }

        /// <summary>
        /// Processing D and E:
        ///  period(D) -> represent as: if D=1 then skip else if D>1 then as a number
        ///  periodUnit(E) -> represent as textual description:
        ///   -> s -> if D=1 then "секунда" else if D>1 then "секунди"
        ///   -> min -> if D=1 then "минута" else if D>1 then "минути"
        ///   -> h -> if D=1 then "час" else if D>1 then "часа"
        ///   -> d -> if D=1 then "ден" else if D>1 then "дни"
        ///   -> wk -> if D=1 then "седмица" else if D>1 then "седмици"
        ///   -> mo -> if D=1 then "месец" else if D>1 then "месеца"
        ///   -> a -> if D=1 then "година" else if D>1 then "години"
        /// </summary>
        /// <param name="time">Time value</param>
        /// <param name="isSigle">single or plural form</param>
        /// <returns></returns>
        private string ConvertTimeToText(string time, bool isSigle)
        {
            string result = string.Empty;

            if (string.IsNullOrEmpty(time) == false)
            {
                if (isSigle)
                {
                    result = Cl020.Nom.ContainsKey(time) ? Cl020.Nom[time] : string.Empty;
                }
                else
                {
                    result = Cl020.NomPlural.ContainsKey(time) ? Cl020.NomPlural[time] : string.Empty;
                }
            }

            return result;
        }

        /// <summary>
        /// Processing H and I (optional):
        ///  when(H) -> represent as CL034 description
        ///  offset(I) -> represent as a number
        /// </summary>
        /// <param name="offset">Offset value</param>
        /// <returns></returns>
        private string ConvertOffsetToText(int? offset)
        {
            string result = string.Empty;

            if (offset == 1)
            {
                result = $"{offset} минута";
            }
            else if (offset > 1)
            {
                result = $"{offset} минути";
            }

            return result;
        }

        /// <summary>
        /// Processing when values
        /// </summary>
        /// <param name="whens">Array of whens</param>
        /// <returns></returns>
        private string ConcatWhens(string[] whens)
        {
            StringBuilder result = new StringBuilder();

            foreach (string when in whens)
            {
                if (Cl034.Nom.ContainsKey(when))
                {
                    result.AppendFormat("{0} ", Cl034.Nom[when]);
                }
            }

            return result.ToString();
        }
    }
}
