using CursoOnline.Dominio.Cursos;
using System;
using System.Collections.Generic;
using System.Text;

namespace CursoOnline.Dominio.Test._Builders
{
    public class CursoBuilder
    {
        private string _Nome = "Informatica Basica";
        private double _CargaHoraria = 80;
        private PublicoAlvo _PublicoAlvo = PublicoAlvo.Estudante;
        private double _Valor = 950;
        private string _Descricao = "Uma Desrição";

        public static CursoBuilder Novo()
        {
            return new CursoBuilder();
        }

        public CursoBuilder ComNome(string nome)
        {
            _Nome = nome;
            return this;
        }

        public CursoBuilder ComDescricao(string descricao)
        {
            _Descricao = descricao;
            return this;
        }
        public CursoBuilder ComCargaHoraria(double cargaHoraria)
        {
            _CargaHoraria = cargaHoraria;
            return this;
        }
        public CursoBuilder ComValor(double valor)
        {
            _Valor = valor;
            return this;
        }

        public CursoBuilder ComPublicoAlvo(PublicoAlvo publicoAlvo)
        {
            _PublicoAlvo = publicoAlvo;
            return this;
        }
        public Curso Build()
        {
            return new Curso(_Nome, _Descricao, _CargaHoraria, _PublicoAlvo, _Valor);
        }
    }
}
