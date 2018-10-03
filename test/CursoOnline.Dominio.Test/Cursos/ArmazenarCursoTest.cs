using CursoOnline.Dominio.Cursos;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace CursoOnline.Dominio.Test.Cursos
{
    public class ArmazenarCursoTest
    {
        [Fact]
        public void DeveAdicionarCurso()
        {

            var cursoDTO = new CursoDTO
            {
                Nome = "Informatica",
                Descricao = "Uma descrição",
                CargaHoraria = 80,
                PublicoAlvoId = 1,
                Valor = 850.00
            };
            var cursoRepositorioMock = new Mock<ICursoRepositorio>();
            var armazenadorDeCurso = new ArmazenadorDeCurso(cursoRepositorioMock.Object);

            armazenadorDeCurso.Armazenar(cursoDTO);

            cursoRepositorioMock.Verify(r => r.Adicionar(It.IsAny<Curso>()));
        }
    }

    public interface ICursoRepositorio
    {
        void Adicionar(Curso curso);
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
            var curso = new Curso(cursoDTO.Nome, cursoDTO.Descricao, cursoDTO.CargaHoraria, PublicoAlvo.Estudante, cursoDTO.Valor);

            _cursoRepositorioMock.Adicionar(curso);
        }
    }

    public class CursoDTO
    {
        public string Nome { get; set; }
        public string Descricao { get; set; }
        public int CargaHoraria { get; set; }
        public int PublicoAlvoId { get; set; }
        public double Valor { get; set; }

    }
}
