using Domain.Aplication;
using Domain.Infra;
using Domain.Model;
using Moq;
using Service.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace XUnitTestSorteioCasaPopular
{
    public class UnitTestFamiliaService
    {
        private readonly IFamiliaService _familiaService;
        private readonly Mock<IFamiliaRepository> _familiaRepositoryMock = new Mock<IFamiliaRepository>();

        public UnitTestFamiliaService()
        {
            _familiaService = new FamiliaService(_familiaRepositoryMock.Object);
        }

        #region Mock da lista de famílias

        public List<Familia> FuncaoRetornaFamilias()
        {
            var lista = new List<Familia>();


            lista.Add(new Familia
            {
                Id = "10",
                Rendas = new List<Renda> { new Renda { Valor = 300 }, new Renda { Valor = 200 }, },
                Pessoas = new List<Pessoa> {
                    new Pessoa { DataDeNascimento = new DateTime(1976,1,1), Tipo = "Pretendente"},
                    new Pessoa { DataDeNascimento = new DateTime(1980,1,1), Tipo = "Dependente"}
                },
                Status = 1
            });

            lista.Add(new Familia
            {
                Id = "11",
                Rendas = new List<Renda> { new Renda { Valor = 700 }, new Renda { Valor = 190 }, },
                Pessoas = new List<Pessoa> {
                    new Pessoa { DataDeNascimento = new DateTime(1995,1,1), Tipo = "Pretendente"},
                    new Pessoa { DataDeNascimento = new DateTime(2015,1,1), Tipo = "Dependente"},
                    new Pessoa { DataDeNascimento = new DateTime(2002,1,1), Tipo = "Dependente"},
                    new Pessoa { DataDeNascimento = new DateTime(2013,1,1), Tipo = "Dependente"}
                },
                Status = 3
            });

            lista.Add(new Familia
            {
                Id = "12",
                Rendas = new List<Renda> { new Renda { Valor = 1200 }, new Renda { Valor = 1500 }, },
                Pessoas = new List<Pessoa> {
                    new Pessoa { DataDeNascimento = new DateTime(1995, 1, 1), Tipo = "Pretendente" },
                    new Pessoa { DataDeNascimento = new DateTime(2002, 1, 1), Tipo = "Dependente" },
                    new Pessoa { DataDeNascimento = new DateTime(2001, 1, 1), Tipo = "Dependente" },
                    new Pessoa { DataDeNascimento = new DateTime(2010, 1, 1), Tipo = "Dependente" },
                    new Pessoa { DataDeNascimento = new DateTime(2011, 1, 1), Tipo = "Dependente" }
                },
                Status = 0
            });

            lista.Add(new Familia
            {
                Id = "13",
                Rendas = new List<Renda> { new Renda { Valor = 900 }, new Renda { Valor = 300 }, },
                Pessoas = new List<Pessoa> {
                          new Pessoa { DataDeNascimento = new DateTime(1974, 12, 31), Tipo = "Pretendente" },
                          new Pessoa { DataDeNascimento = new DateTime(2002, 1, 1), Tipo = "Dependente" }
                },
                Status = 0
            });

            lista.Add(new Familia
            {
                Id = "14",
                Rendas = new List<Renda> { new Renda { Valor = 900 }, new Renda { Valor = 500 } },
                Pessoas = new List<Pessoa> {
                    new Pessoa { DataDeNascimento = new DateTime(1968,1,1), Tipo = "Pretendente"},
                    new Pessoa { DataDeNascimento = new DateTime(2015,1,1), Tipo = "Dependente"},
                    new Pessoa { DataDeNascimento = new DateTime(2014,1,1), Tipo = "Dependente"},
                    new Pessoa { DataDeNascimento = new DateTime(1988,1,1), Tipo = "Dependente"}
                },
                Status = 0

            });

            lista.Add(new Familia
            {
                Id = "15",
                Rendas = new List<Renda> { new Renda { Valor = 200 }, new Renda { Valor = 200 } },
                Pessoas = new List<Pessoa> {
                    new Pessoa { DataDeNascimento = new DateTime(1968,1,1), Tipo = "Pretendente"},
                    new Pessoa { DataDeNascimento = new DateTime(2015,1,1), Tipo = "Dependente"},
                    new Pessoa { DataDeNascimento = new DateTime(2014,1,1), Tipo = "Dependente"},
                    new Pessoa { DataDeNascimento = new DateTime(2002,1,1), Tipo = "Dependente"},
                    new Pessoa { DataDeNascimento = new DateTime(2003,1,1), Tipo = "Dependente"},
                    new Pessoa { DataDeNascimento = new DateTime(2004,1,1), Tipo = "Dependente"},
                    new Pessoa { DataDeNascimento = new DateTime(2005,1,1), Tipo = "Dependente"},
                    new Pessoa { DataDeNascimento = new DateTime(2006,1,1), Tipo = "Dependente"},
                },
                Status = 3
            });


            return lista;
        }
        #endregion

        [Theory]
        [InlineData("11", 5)]
        [InlineData("12", 0)]
        [InlineData("13", 3)]

        public void TestCalcularPontosPorRenda(string idFamilia, int pontuacao)
        {
            //Arrange
            var familia = FuncaoRetornaFamilias().FirstOrDefault(p => p.Id == idFamilia);

            //Act
            var pontos = _familiaService.CalcularPontosPorRenda(familia);

            //Assert
            Assert.Equal(pontuacao, pontos);
        }


        [Theory]
        [InlineData("12", 2)]
        [InlineData("13", 0)]
        [InlineData("14", 2)]
        [InlineData("15", 2)]
        public void TestCalcularPontosPorDependente(string idFamilia, int pontuacao)
        {
            //Arrange
            var familia = FuncaoRetornaFamilias().FirstOrDefault(p => p.Id == idFamilia);

            //Act
            var pontos = _familiaService.CalcularPontosPorDependente(familia);

            //Assert
            Assert.Equal(pontuacao, pontos);
        }


        [Theory]
        [InlineData("11", 8)]
        [InlineData("14", 8)]
        [InlineData("15", 10)]
        public void TestCalcularPontosTotais(string idFamilia, int pontuacao)
        {
            //Arrange
            var familia = FuncaoRetornaFamilias().FirstOrDefault(p => p.Id == idFamilia);

            //Act
            var pontos = _familiaService.CalcularPontosTotais(familia);

            //Assert
            Assert.Equal(pontuacao, pontos.TotalDePontos);
        }

        [Fact]
        public void TestListaSorteadaDeFamilia()
        {
            //Arrange
            _familiaRepositoryMock.Setup(p => p.Query()).Returns(FuncaoRetornaFamilias());

            //Act
            var lista = _familiaService.SortearFamilia();

            //Assert
            var familiaSorteada = lista.First();
            Assert.Equal("14", familiaSorteada.FamiliaId);
            Assert.Equal(3, familiaSorteada.PontosECriterios.QuantidadeDeCriteriosAtendidos);
            Assert.Equal(8, familiaSorteada.PontosECriterios.TotalDePontos);
            Assert.NotNull(familiaSorteada.DataSelecao);
        }


    }
}
