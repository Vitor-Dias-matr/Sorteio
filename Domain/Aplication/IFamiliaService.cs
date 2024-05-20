using Domain.DTO;
using Domain.Model;
using System.Collections.Generic;

namespace Domain.Aplication
{
    public interface IFamiliaService
    {
        int CalcularPontosPorRenda(Familia familia);
        int CalcularPontosPorDependente(Familia familia);
        PontosTotaisDto CalcularPontosTotais(Familia familia);
        List<FamiliaDto> SortearFamilia();
    }
}
