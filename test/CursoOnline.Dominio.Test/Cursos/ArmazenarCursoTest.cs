using Bogus;
using CursoOnline.Dominio.Cursos;
using CursoOnline.Dominio.Test._Builders;
using CursoOnline.Dominio.Test._Util;
using Moq;
using System;
using Xunit;

namespace CursoOnline.Dominio.Test.Cursos
{
    public class ArmazenadorDeCursoTest
    {
        private CursoDTO _cursoDTO;
        private Mock<ICursoRepositorio> _cursoRepositorioMock;
        private ArmazenadorDeCurso _armazenadorDeCurso;

        public ArmazenadorDeCursoTest()
        {
            var fake = new Faker();
            _cursoDTO = new CursoDTO
            {
                Nome = fake.Random.Word(),
                Descricao = fake.Lorem.Paragraph(),
                CargaHoraria = fake.Random.Double(50, 1000),
                PublicoAlvo = "Estudante",
                Valor = fake.Random.Double(800, 1000)
            };

            _cursoRepositorioMock = new Mock<ICursoRepositorio>();
            _armazenadorDeCurso = new ArmazenadorDeCurso(_cursoRepositorioMock.Object);

        }
        [Fact]
        public void DeveAdicionarCurso()
        {

            _armazenadorDeCurso.Armazenar(_cursoDTO);

            _cursoRepositorioMock.Verify(r => r.Adicionar(
                It.Is<Curso>(
                    c => c.Nome == _cursoDTO.Nome &&
                    c.Descricao == _cursoDTO.Descricao
                )
            ));
        }

        [Fact]
        public void NaoDeveAdcionarCursoComMesmoNomeDeOutroCursoJaSalvo()
        {
            var cursoJaSalvo = CursoBuilder.Novo().ComNome(_cursoDTO.Nome).Build();

            _cursoRepositorioMock.Setup(r => r.ObterPeloNome(cursoJaSalvo.Nome)).Returns(cursoJaSalvo);

            Assert.Throws<ArgumentException>(() => _armazenadorDeCurso.Armazenar(_cursoDTO))
                .ComMensagem("Nome do Curso ja consta no Banco de Dados");
        }

        [Fact]
        public void NaoDeveInformarPublicoAlvoInvalido()
        {
            var publicoAlvoInvalido = "Medico";
            _cursoDTO.PublicoAlvo = publicoAlvoInvalido;

            Assert.Throws<ArgumentException>(() => _armazenadorDeCurso.Armazenar(_cursoDTO))
                .ComMensagem("Publico Alvo Invalido!");
        }


    }

    public interface ICursoRepositorio
    {
        void Adicionar(Curso curso);
        Curso ObterPeloNome(string nome);
    }
    public class ArmazenadorDeCurso
    {
        private ICursoRepositorio _cursoRepositorioMock;

        public ArmazenadorDeCurso(ICursoRepositorio cursoRepositorioMock)
        {
            _cursoRepositorioMock = cursoRepositorioMock;
        }

        public void Armazenar(CursoDTO cursoDTO)
        {
            var cursoJaSalvo = _cursoRepositorioMock.ObterPeloNome(cursoDTO.Nome);

            if (cursoJaSalvo != null)
                throw new ArgumentException("Nome do Curso ja consta no Banco de Dados");

            Enum.TryParse(typeof(PublicoAlvo), cursoDTO.PublicoAlvo, out var publicoAlvo);

            if (publicoAlvo == null)
                throw new ArgumentException("Publico Alvo Invalido!");

            var curso = new Curso(cursoDTO.Nome, cursoDTO.Descricao, cursoDTO.CargaHoraria, (PublicoAlvo)publicoAlvo, cursoDTO.Valor);
            _cursoRepositorioMock.Adicionar(curso);
        }
    }

    public class CursoDTO
    {
        public string Nome { get; set; }
        public string Descricao { get; set; }
        public double CargaHoraria { get; set; }
        public string PublicoAlvo { get; set; }
        public double Valor { get; set; }

    }
}
