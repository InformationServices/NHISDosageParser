using NHISDosageParser.Contracts;
using NHISDosageParser.Services;

namespace Microsoft.Extensions.DependencyInjection
{
    /// <summary>
    /// Service Collection Extension to use with
    /// Microsoft Dependancy Injection
    /// </summary>
    public static class NhisDosageParserServiceCollectionExtension
    {
        /// <summary>
        /// Adding NHIS Dosage Parser to IoC Container
        /// </summary>
        /// <param name="services">Service Collection</param>
        /// <returns>Service Collection</returns>
        public static IServiceCollection AddNhisDosageParser(this IServiceCollection services)
        {
            services.AddHttpClient();
            services.AddScoped<INhisNomenclatureService, NhisNomenclatureService>();
            services.AddScoped<INhisDosageParser, NhisDosageParserService>();

            return services;
        }
    }
}
