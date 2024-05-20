using System;

namespace Domain.DTO
{
    public class FamiliaDto
    {
        public string FamiliaId { get; set; }
        public PontosTotaisDto PontosECriterios { get; set; }
        public DateTime? DataSelecao { get; set; }
    }
}
