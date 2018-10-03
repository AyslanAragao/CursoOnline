using Bogus;
using CursoOnline.Dominio.Cursos;
using CursoOnline.Dominio.Test._Builders;
using CursoOnline.Dominio.Test._Util;
using System;
using Xunit;
using Xunit.Abstractions;

namespace CursoOnline.Dominio.Test.Cursos
{
    public class CursoTest : IDisposable
    {
        private ITestOutputHelper _output;
        private string _Nome;
        private double _CargaHoraria;
        private PublicoAlvo _PublicoAlvo;
        private double _Valor;
        private string _Descricao;
        
        public CursoTest(ITestOutputHelper output)
        {
            var faker = new Faker();

            _output = output;
            _Nome = faker.Random.Word();
            _CargaHoraria = faker.Random.Double(50,1000);
            _PublicoAlvo = PublicoAlvo.Estudante;
            _Valor = faker.Random.Double(100,1000);
            _Descricao = faker.Lorem.Paragraph();

        }

        public void Dispose()
        {
            _output.WriteLine("Dispose esta sendo usado");
        }

        [Fact(DisplayName = "DeveCriarCurso")]
        public void DeveCriarCurso()
        {

            var cursoEsperado = new
            {
                Nome = _Nome,
                CargaHoraria = _CargaHoraria,
                PublicoAlvo = _PublicoAlvo,
                Valor = _Valor,
                Descricao = _Descricao
            };

            var curso = new Curso(cursoEsperado.Nome, cursoEsperado.Descricao, cursoEsperado.CargaHoraria, cursoEsperado.PublicoAlvo, cursoEsperado.Valor);

            Assert.Equal(cursoEsperado.Nome, curso.Nome);
            Assert.Equal(cursoEsperado.CargaHoraria, curso.CargaHoraria);
            Assert.Equal(cursoEsperado.PublicoAlvo, curso.PublicoAlvo);
            Assert.Equal(cursoEsperado.Valor, curso.Valor);

        }

        [Theory]
        [InlineData("")]
        [InlineData(null)]
        public void Curso_New_Nome_Obrigatorios(string nomeInvalido)
        {

            Assert.Throws<ArgumentException>(() =>
                CursoBuilder.Novo().ComNome(nomeInvalido).Build()
                ).ComMensagem("Nome Invalido");
        }

        [Theory]
        [InlineData(0)]
        [InlineData(-5)]
        [InlineData(-100)]
        public void Curso_New_CargaHorario_MenorQ1(double CargaHorarioInvalido)
        {
            Assert.Throws<ArgumentException>(() =>
                CursoBuilder.Novo().ComCargaHoraria(CargaHorarioInvalido).Build()
                ).ComMensagem("Carga Horaria Invalida");
        }

        [Theory]
        [InlineData(0)]
        [InlineData(-5)]
        [InlineData(-100)]
        public void Curso_New_Valor_MenorQ1(double valorInvalido)
        {
            Assert.Throws<ArgumentException>(() =>
                CursoBuilder.Novo().ComValor(valorInvalido).Build()
                ).ComMensagem("Valor Invalido");
        }
    }
}
