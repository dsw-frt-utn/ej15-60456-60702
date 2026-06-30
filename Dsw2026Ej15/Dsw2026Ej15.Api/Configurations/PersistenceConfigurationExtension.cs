using Dsw2026Ej15.Data;
using Dsw2026Ej15.Domain.Interfaces;

namespace Dsw2026Ej15.Api.Configurations
{
    public static class PersistenceConfigurationExtension
    {

        public static IServiceCollection AddApliacationPersistence(this IServiceCollection service)
        {
            service.AddScoped<IPersistence, PersitenceEf>();
            return service;
        }
    }
}
