using NHISDosageParser.Contracts;
using NHISDosageParser.Models;
using NHISDosageParser.Services;
using System;
using System.Threading.Tasks;

namespace DemoApp
{
    class Program
    {
        static async Task Main(string[] args)
        {
            INhisNomenclatureService nomenclatureService = new NhisNomenclatureService();
            INhisDosageParser parser = new NhisDosageParserService(nomenclatureService);

            var dosageInstruction = new NhisDosageInstruction()
            {
                Sequence = 1,
                DoseQuantityValue = 1,
                DoseQuantityCode = "78",
                Frequency = 2,
                Period = 1,
                PeriodUnit = "d",
                BoundsDuration = 28,
                BoundsDurationUnit = "d",
                When = new string[] { "1", "12", "8", "13" },
                Offset = 15,
                Text = null,
                Route = "61"
            };

            Console.WriteLine(parser.DosageParser(dosageInstruction));

            await parser.UpdateNomenclatures();
        }
    }
}
