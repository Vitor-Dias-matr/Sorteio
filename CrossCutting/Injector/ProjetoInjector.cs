using Domain.Aplication;
using Domain.Infra;
using Microsoft.Extensions.DependencyInjection;
using Repository.Repository;
using Service.Service;

namespace CrossCutting.Injector
{
    public class ProjetoInjector
    {
        public static void RegistrarServicos(IServiceCollection services)
        {
            services.AddScoped<IFamiliaRepository, FamiliaRepository>();

            services.AddScoped<IFamiliaService, FamiliaService>();
        } 
    }
}
