using Domain.Aplication;
using Domain.DTO;
using Domain.Infra;
using Domain.Model;
using Service.Tools;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Service.Service
{
    public class FamiliaService : IFamiliaService
    {
        private readonly IFamiliaRepository _familiaRepository;

        public FamiliaService(IFamiliaRepository repository)
        {
            _familiaRepository = repository;
        }
        public int CalcularPontosPorRenda(Familia familia)
        {
            var renda = familia.Rendas.Sum(p => p.Valor);
            if (renda <= 900)
            {
                return 5;
            }

            else if (renda <= 1500)
            {
                return 3;
            }

            else if (renda <= 2000)
            {
                return 1;
            }

            return 0;
        }

        public int CalcularPontosPorDependente(Familia familia)
        {
            var quantidadeDeDependentes = familia.Pessoas
                .Where(p => p.Tipo == "Dependente"
                && Auxiliar.CalculateAge(p.DataDeNascimento) < 18)
                .Count();

            if (quantidadeDeDependentes >= 3)
            {
                return 3;
            }
            else if (quantidadeDeDependentes > 0)
            {
                return 2;
            }
            return 0;
        }

        public PontosTotaisDto CalcularPontosTotais(Familia familia)
        {
            var ptRenda = CalcularPontosPorRenda(familia);
            //var ptIdade = CalcularPontosPorIdade(familia);
            var ptDependente = CalcularPontosPorDependente(familia);

            var qtdCriteriosAtendido = 0;
            if (ptRenda > 0)
                qtdCriteriosAtendido++;
            //if (ptIdade > 0)
            //    qtdCriteriosAtendido++;
            if (ptDependente > 0)
                qtdCriteriosAtendido++;


            return new PontosTotaisDto
            {
                //TotalDePontos = ptRenda + ptIdade + ptDependente,
                TotalDePontos = ptRenda +  ptDependente,
                QuantidadeDeCriteriosAtendidos = qtdCriteriosAtendido
            };
        }

        private int CalcularCriteriosAtendidos(Familia familia)
        {
            var criteriosAtendidos = 0;
            //if (CalcularPontosPorIdade(familia) > 0)
            //{
            //    criteriosAtendidos++;
            //}
            if (CalcularPontosPorRenda(familia) > 0)
            {
                criteriosAtendidos++;
            }
            if (CalcularPontosPorDependente(familia) > 0)
            {
                criteriosAtendidos++;
            }
            return criteriosAtendidos;
        }

        public List<FamiliaDto> SortearFamilia()
        {
            return _familiaRepository.Query().Where(p => p.Status == 0).Select(p => new FamiliaDto
            {
                FamiliaId = p.Id,
                DataSelecao = DateTime.Now,
                PontosECriterios = CalcularPontosTotais(p),
            }).OrderByDescending(p => p.PontosECriterios.TotalDePontos).ToList();
        }
    }
}
