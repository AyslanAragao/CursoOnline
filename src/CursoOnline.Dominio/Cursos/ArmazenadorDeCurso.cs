using CursoOnline.Dominio.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace CursoOnline.Dominio.Cursos
{
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

            Enum.TryParse<PublicoAlvo>(cursoDTO.PublicoAlvo, out var publicoAlvo);

            if (publicoAlvo == null)
                throw new ArgumentException("Publico Alvo Invalido!");

            var curso = new Curso(cursoDTO.Nome, cursoDTO.Descricao, cursoDTO.CargaHoraria, publicoAlvo, cursoDTO.Valor);
            _cursoRepositorioMock.Adicionar(curso);
        }
    }
}
